using EllaMakerTool.Core.FTP.Common;
using EllaMakerTool.Core.FTP.Entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Core.FTP
{
    /// <summary>
    /// FTP客户端
    /// </summary>
    public partial class FTPClient
    {
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
        ConcurrentDictionary<string, FTPItem> UploadFileList = new ConcurrentDictionary<string, FTPItem>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="model"></param>
        /// <param name="encoding"></param>
        public FTPClient(string serAddress, string user, string password, FTPModel model, Encoding encoding)
        {
            string[] address = serAddress.Split(':');
            IP = address[1].Substring(2);
            Port =int.Parse(address[2].Substring(0,address[2].Length-1));
            FTPUser = user;
            FTPPassword = password;
            FTPEncoding = encoding;
            FTPModel = model;
            //if (ExecTimer == null)
            //{
            //    ExecTimer = new Timer(Exec, null, 5000, 5000);//一分钟检查一次
            //}
        }

        /// <summary>
        /// 连接FTP测试
        /// </summary>
        /// <returns>0：成功  非0：失败</returns>
        public int Connect()
        {
            int result = -1;
            FileTransfer tranfer = new FileTransfer(IP, "/", FTPUser, FTPPassword, Port);
            tranfer.ASCII = FTPEncoding;
            tranfer.SetTransferType((FileTransfer.TransferType)(int)FTPModel);
            result = tranfer.Connect();//开始链接
            tranfer.DisConnect();//关闭连接
            return result;
            //return Connect(IP, Port, FTPUser, FTPPassword, false);
        }

        /// <summary>
        /// 上传文件，异步实现
        /// 注意如果传输中断，下次再上传时，应该是续传
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>SessionID，用来多个任务时区分</returns>
        public FTPResult Upload(string filename, string serAddress, string upPath)
        {
            //FTPResult result = new FTPResult();
            //if (File.Exists(filename))
            //{
            //    result.Result = 0;
            //    result.SessionID = Guid.NewGuid().ToString();
            //    result.NewFilename = string.Concat("ftp://", IP, ":", Port, "/", DateTime.Now.ToString("yyyy/MM/dd"), "/", Guid.NewGuid().ToString(), Path.GetExtension(filename));
            //    UploadFileList.TryAdd(result.SessionID, new FTPItem()
            //    {
            //        SessionID = result.SessionID,
            //        ServerFilePath = result.NewFilename,
            //        LocFilePath = filename,
            //        IsContinue = false
            //    });
            //}
            //else
            //{
            //    result.Result = -100;//文件不存在 
            //}
            //return result;

            return Upload(filename,serAddress,upPath, string.Empty);
        }

        /// <summary>
        /// 上传文件，异步实现
        /// 注意如果传输中断，下次再上传时，应该是续传
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="serverFileName">服务文件名</param>
        /// <returns>SessionID，用来多个任务时区分</returns>
        public FTPResult Upload(string filename, string serAddress,string upPath,string serverFilePath)
        {
            FTPResult result = new FTPResult();
            if (File.Exists(filename))
            {
                result.Result = 0;
                result.SessionID = Guid.NewGuid().ToString();
                if (string.IsNullOrWhiteSpace(serverFilePath))
                {
                    result.NewFilename = string.Concat(serAddress, upPath, result.SessionID, Path.GetExtension(filename));
                }
                else
                {
                    result.NewFilename = serverFilePath;
                }
                UploadFileList.TryAdd(result.SessionID, new FTPItem()
                {
                    SessionID = result.SessionID,
                    ServerFilePath = result.NewFilename,
                    LocFilePath = filename,
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
        /// 续传
        /// </summary>
        /// <param name="fileName">本地文件名</param>
        /// <param name="serverFileName">服务文件名</param>
        /// <param name="sessionID">可自定义SessionID,为空自动生成SessionID</param>
        /// <returns></returns>
        public FTPResult ContinueUpload(string filename, string serverFileName, string sessionID = "")
        {
            return ContinueUploadOp(filename, serverFileName, sessionID);
        }
        /// <summary>
        /// 续传
        /// </summary>
        /// <param name="fileName">本地文件名</param>
        /// <param name="serverFileName">服务文件名</param>
        /// <returns></returns>
        FTPResult ContinueUploadOp(string filename, string serverFileName, string sessionID)
        {
            FTPResult result = new FTPResult();
            if (File.Exists(filename))
            {
                result.Result = 0;
                if (string.IsNullOrWhiteSpace(sessionID))
                {
                    result.SessionID = Guid.NewGuid().ToString();
                }
                else
                {
                    result.SessionID = sessionID;
                }
                result.NewFilename = serverFileName;
                UploadFileList.TryAdd(result.SessionID, new FTPItem()
                {
                    SessionID = result.SessionID,
                    ServerFilePath = result.NewFilename,
                    LocFilePath = filename,
                    IsContinue = true
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
        /// 终止某个上传或下载任务
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public int Abort(string sessionID)
        {
            int result = -1;
            FTPItem item = null;
            if (UploadFileList.TryRemove(sessionID, out item))//直接删除
            {
                //item.RunState = FTPRunState.Abort;
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 暂停某个任务
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public int Pause(string sessionID)
        {
            int result = -1;
            FTPItem item = null;
            if (UploadFileList.TryGetValue(sessionID, out item))
            {
                item.RunState = FTPRunState.Pause;
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 重新开始某个任务
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public int Resume(string sessionID)
        {
            int result = -1;
            FTPItem item = null;
            if (UploadFileList.TryGetValue(sessionID, out item))
            {
                item.IsContinue = true;
                item.RunState = FTPRunState.None;
                result = 0;
                Exec();//执行上传 下载
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
            UploadFileList.TryAdd(result.SessionID, new FTPItem()
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

        /// <summary>
        /// 下载文件，异步实现 续传
        /// </summary>
        /// <param name="saveFilename"></param>
        /// <param name="saveFilename"></param>
        /// <returns>SessionID，用来多个任务时区分</returns>
        public FTPResult ContinueDownload(string saveFilename, string remotefilename)
        {
            FTPResult result = new FTPResult();

            result.SessionID = Guid.NewGuid().ToString();
            result.NewFilename = remotefilename;
            UploadFileList.TryAdd(result.SessionID, new FTPItem()
            {
                SessionID = result.SessionID,
                ServerFilePath = result.NewFilename,
                LocFilePath = saveFilename,
                IsContinue = true,
                IsUpload = false
            });
            result.Result = 0;
            Exec();//执行上传 下载

            return result;
        }

        /// <summary>
        /// 同步实现直接返回结果 0:成功 非0:失败
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public int Delete(string url)
        {
            //int result = -1;
            //FileTransfer tranfer = new FileTransfer(IP, "", FTPUser, FTPPassword, Port);
            //tranfer.ASCII = FTPEncoding;
            //tranfer.SetTransferType((FileTransfer.TransferType)(int)FTPModel);
            //if (tranfer.Connect() == 0)
            //{//开始链接
            //    result = tranfer.Delete(url);
            //    tranfer.DisConnect();//关闭连接
            //}
            //return result;
            return DeleteFtpFile(url, FTPUser, FTPPassword);
        }


        public void Close()
        {
            foreach (var item in UploadFileList.Values)
            {
                item.CloseState = true;
            }
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
