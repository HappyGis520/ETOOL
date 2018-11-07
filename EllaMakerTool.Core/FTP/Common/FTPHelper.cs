using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace EllaMakerTool.Core.FTP.Common
{
    /// <summary>
    /// FTP帮助类
    /// </summary>
    public class FtpHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item">FTP配置</param>
        public FtpHelper()
        {

        }

        /// <summary>
        /// 上传文件到FTP 
        /// </summary>
        /// <param name="locfilename">本地文件</param>
        /// <param name="remoteUrl">远程文件</param>
        /// <param name="ftpUserName">ftp用户名</param>
        /// <param name="ftpPassword">ftp密码</param>
        /// <returns>上传成功或失败</returns>
        public bool Upload(string locfilename, string remoteUrl, string ftpUserName, string ftpPassword)
        {
            bool success = true;
            FileInfo fileInf = new FileInfo(locfilename);
            long allbye = (long)fileInf.Length;

            long startfilesize = 0;// GetFileSize(newFileName, ftpServerIP, ftpUserID, ftpPassword, path);
            if (startfilesize >= allbye)
            {
                return false;
            }
            //startfilesize=0;
            long startbye = startfilesize;

            string uri = remoteUrl;
            FtpWebRequest reqFTP;
            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            // ftp用户名和密码 
            reqFTP.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
            // 默认为true，连接不会被关闭 
            // 在一个命令之后被执行 
            reqFTP.KeepAlive = false;
            // 指定执行什么命令 
            reqFTP.Method = WebRequestMethods.Ftp.AppendFile;
            // 指定数据传输类型 
            reqFTP.UseBinary = true;
            // 上传文件时通知服务器文件的大小 
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;// 缓冲大小设置为2kb 
            byte[] buff = new byte[buffLength];
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件 
            FileStream fs = fileInf.OpenRead();
            try
            {
                // 把上传的文件写入流 
                Stream strm = reqFTP.GetRequestStream();
                // 每次读文件流的2kb   
                fs.Seek(startfilesize, 0);
                int contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束 
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    startbye += contentLen;
                    //if (handler != null)
                    //{
                    //    handler(Math.Round(((double)startbye / (double)reqFTP.ContentLength) * 100, 2).ToString() + "%");
                    //}
                }
                // 关闭两个流 
                strm.Close();
                fs.Close();
                //handler("100%");
            }
            catch
            {
                //handler("0%");
                success = false;
            }
            return success;
        }

    }
}
