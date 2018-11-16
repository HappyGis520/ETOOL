/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： FTPClient.cs
 * * 功   能：  
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-11-15 16:22:18
 * * 修改记录： 
 * * 日期时间： 2018-11-15 16:22:18  修改人：王建军  创建
 * *******************************************************************/
using EllaMakerTool.Core.FTP.Entity;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;

namespace EllaMakerTool.Core.FTP
{
    /// <summary>
    /// FTP客户端
    /// </summary>
    public partial class FTPClientWrapper
    {
        #region 事件
        /// <summary>
        /// 完成事件
        /// </summary>
        /// <param name="sessionID">ID</param>
        public delegate void CompleteHandler(string sessionID,string Url);
        /// <summary>
        /// 完成事件
        /// </summary>
        /// <param name="sessionID">ID</param>
        public event CompleteHandler OnCompleted;

        /// <summary>
        /// 进度通知
        /// </summary>
        /// <param name="sessionID">ID</param>
        /// <param name="progress">进度</param>
        public delegate void ProgressHandler(string sessionID, double progress);
        /// <summary>
        /// 进度通知
        /// </summary>
        /// <param name="sessionID">ID</param>
        /// <param name="progress">进度</param>
        public event ProgressHandler OnProgressChanged;

        /// <summary>
        /// 失败事件
        /// </summary>
        /// <param name="sessionID">ID</param>
        /// <param name="errorID">失败信息</param>
        public delegate void FailedHandler(string sessionID, int errorID);
        /// <summary>
        /// 失败事件
        /// </summary>
        /// <param name="sessionID">ID</param>
        /// <param name="errorID">失败信息</param>
        public event FailedHandler OnFailed;
        #endregion

        #region 字段属性
        /// <summary>
        /// 并发线程数目
        /// </summary>
        public static int ThreadsCount = 2;

        /// <summary>
        /// Ftp服务器IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// Ftp服务器端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// ftp用户名
        /// </summary>
        public string FTPUser { get; set; }
        /// <summary>
        /// FTP密码
        /// </summary>
        public string FTPPassword { get; set; }

        private FTPModel _ftpModel = FTPModel.Binary;

        /// <summary>
        /// FTP模式
        /// </summary>
        public FTPModel FTPModel
        {
            get
            {
                return _ftpModel;
            }
            set
            {
                _ftpModel = value;
            }
        }

        /// <summary>
        /// FTP编码
        /// </summary>
        public Encoding FTPEncoding
        {
            get;
            set;
        }

        /// <summary>
        /// 正在上传文件列表
        /// </summary>
        ConcurrentDictionary<string, FTPTransListItem> UploadFileList = new ConcurrentDictionary<string, FTPTransListItem>();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Host">FTP服务器IP地址</param>
        /// <param name="User">FTP用户账户登录名称</param>
        /// <param name="Password">FTP用户登录密码</param>
        /// <param name="Model">FTP传输模式</param>
        /// <param name="Encoding">FTP编码模式</param>
        /// <param name="Port">FTP端口</param>
        public FTPClientWrapper(string Host, string User, string Password, FTPModel Model, Encoding Encoding, int Port=21)
        {
            IP =Host;
            this.Port = Port;
            FTPUser = User;
            FTPPassword = Password;
            FTPEncoding = Encoding;
            FTPModel = Model;
        }

        /// <summary>
        /// 连接FTP测试
        /// </summary>
        /// <returns>0：成功  非0：失败</returns>
        public int TryConnect()
        {
            int result = -1;
            FTPClient tranfer = new FTPClient(IP, "/", FTPUser, FTPPassword, Port);
            tranfer.ASCII = FTPEncoding;
            result = tranfer.Connect();//开始链接
            tranfer.SetTransferType((EnumTransferType)(int)FTPModel);
            tranfer.DisConnect();//关闭连接
            return result;
        }

        /// <summary>
        ///上传文件，异步实现
        /// 注意如果传输中断，下次再上传时，应该是续传
        /// </summary>
        /// <param name="Filename">文件名（包含完整路径）C：\aa.txt</param>
        /// <param name="SerAddress">FTP地址+端口：192.168.1.114：21</param>
        /// <param name="UpLoadPath">上传路径，根目录，用“/”</param>
        /// <param name="ServerFilePath">服务端文件路径</param>
        /// <returns></returns>
        public FTPResult Upload(string Filename, string SerAddress, string UpLoadPath,string ServerFilePath="")
        {
            FTPResult result = new FTPResult();
            if (File.Exists(Filename))
            {
                result.Result = 0;
                result.SessionID = Guid.NewGuid().ToString();
                if (string.IsNullOrWhiteSpace(ServerFilePath))
                {
                    result.NewFilename = string.Concat(SerAddress, UpLoadPath, result.SessionID, Path.GetExtension(Filename));
                }
                else
                {
                    result.NewFilename = ServerFilePath;
                }
                UploadFileList.TryAdd(result.SessionID, new FTPTransListItem()
                {
                    SessionID = result.SessionID,
                    ServerFilePath = result.NewFilename,
                    LocFilePath = Filename,
                    IsContinue = false
                });
                Exec();//执行上传 下载
            }
            else
            {
                result.Result = -100;//文件不存在 
            }
            return result;
        }

        /// <summary>
        /// 下载文件，异步实现 非续传
        /// </summary>
        /// <param name="saveFilename"></param>
        /// <param name="saveFilename"></param>
        /// <returns>SessionID，用来多个任务时区分</returns>
        public FTPResult Download(string saveFilename, string remotefilename)
        {
            FTPResult result = new FTPResult();

            result.SessionID = Guid.NewGuid().ToString();
            result.NewFilename = remotefilename;
            UploadFileList.TryAdd(result.SessionID, new FTPTransListItem()
            {
                SessionID = result.SessionID,
                ServerFilePath = result.NewFilename,
                LocFilePath = saveFilename,
                IsContinue = false,
                IsUpload = false
            });
            result.Result = 0;
            Exec();//执行上传 下载

            return result;
        }



    }

    /// <summary>
    /// FTP结果
    /// </summary>
    public class FTPResult
    {
        /// <summary>
        /// 返回结果 0：成功 -100：文件不存在  其他:失败
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 返回新文件名
        /// </summary>
        public string NewFilename { get; set; }
        /// <summary>
        /// 当前SessionID
        /// </summary>
        public string SessionID { get; set; }
    }

    /// <summary>
    /// FTP模式
    /// </summary>
    public enum FTPModel
    {
        /// <summary>
        /// 二进制传输
        /// </summary>
        Binary = 1,
        /// <summary>
        /// ASCII传输
        /// </summary>
        ASCII = 2
    }
}
