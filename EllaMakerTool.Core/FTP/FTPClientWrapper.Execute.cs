using EllaMakerTool.Core.FTP.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool.Core.FTP
{
    public partial class FTPClientWrapper
    {
        /// <summary>
        /// 检查状态
        /// </summary>
        bool CheckState = true;
        /// <summary>
        /// 检查对象
        /// </summary>
        object CheckObj = new object();
        ///// <summary>
        ///// 执行计时器
        ///// </summary>
        //Timer ExecTimer { get; set; }

        /// <summary>
        /// 执行计时操作
        /// </summary>
        /// <param name="target"></param>
        private void Exec()//object target)
        {
            if (CheckState)
            {
                lock (CheckObj)
                {
                    if (CheckState)
                    {
                        CheckState = false;
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                if (UploadFileList.Count > 0)
                                {
                                    int count = ThreadsCount - UploadFileList.Where(c => c.Value.RunState == Entity.FTPRunState.Run && c.Value.CloseState == false).Select(c => c.Key).Count();
                                    //检查并发数目是否超出限制

                                    FTPTransListItem item = null;
                                    if (count > 0)
                                    {
                                        //等待并发ID
                                        string[] keys = UploadFileList.Where(c => c.Value.RunState == Entity.FTPRunState.None && c.Value.CloseState == false).Select(c => c.Key).Take(count).ToArray();
                                        if (keys != null && keys.Length > 0)
                                        {
                                            foreach (var key in keys)
                                            {
                                                if (UploadFileList.TryGetValue(key, out item))
                                                {
                                                    //item = UploadFileList[key];
                                                    //if (item.CloseState == false && item.RunState == Entity.FTPRunState.None)
                                                    {
                                                        item.RunState = FTPRunState.Run;
                                                        Task.Factory.StartNew(ExecFTPTransfor, key);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            CheckState = true;
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 执行FTP操作
        /// </summary>
        /// <param name="part"></param>
        private void ExecFTPTransfor(object target)
        {
            try
            {
                string key = Convert.ToString(target);

                FTPTransListItem part = null;
                if (UploadFileList.TryGetValue(key, out part))
                {
                    if (part.IsUpload)//上传
                    {
                        if (part.IsContinue == false)//新上传的文件需检查目录
                        {
                            string ftp = string.Concat("ftp://", IP, ":", Port);
                            string[] path = part.ServerFilePath.ToLower().Split('/');

                            if (path.Length > 4)//一级级创建FTP目录
                            {
                                int len = path.Length - 1;
                                for (int i = 3; i < len; i++)
                                {
                                    ftp = string.Concat(ftp, "/", path[i]);//检查目录
                                    if (CheckDir(ftp, FTPUser, FTPPassword) == false)
                                    {
                                        //创建目录
                                        CreateDir(ftp, FTPUser, FTPPassword);
                                    }
                                }
                            }

                        }
                        UploadFTPFile(key, part);
                    }
                    else//下载
                    {
                        DownloadFTPFile(key, part, GetFtpFileSize(part.ServerFilePath, FTPUser, FTPPassword));
                    }
                }
                Exec();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// FTP创建目录 
        /// </summary>
        /// <param name="dirName">目录名</param>
        /// <param name="ftpServerIP">服务器地址</param>
        /// <param name="ftpUserID">ftp用户名</param>
        /// <param name="ftpPassword">ftp密码</param>
        /// <returns></returns>
        private bool CreateDir(string dirName, string ftpUserID, string ftpPassword)
        {
            bool sRet = false;
            try
            {
                string uri = dirName;
                FtpWebRequest reqFTP;

                // 根据uri创建FtpWebRequest对象   
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                // ftp用户名和密码  
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                // 默认为true，连接不会被关闭  
                // 在一个命令之后被执行  
                reqFTP.KeepAlive = false;

                // 指定执行什么命令  
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

                // 指定数据传输类型  
                reqFTP.UseBinary = true;

                FtpWebResponse respFTP = (FtpWebResponse)reqFTP.GetResponse();
                respFTP.Close();
                sRet = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return sRet;
        }

        /// <summary>
        /// FTP检查目录 
        /// </summary>
        /// <param name="dirName">目录名</param>
        /// <param name="ftpServerIP">服务器地址</param>
        /// <param name="ftpUserID">ftp用户名</param>
        /// <param name="ftpPassword">ftp密码</param>
        /// <returns></returns>
        private bool CheckDir(string dirName, string ftpUserID, string ftpPassword)
        {
            bool sRet = false;
            try
            {
                string uri = dirName;
                FtpWebRequest reqFTP;

                // 根据uri创建FtpWebRequest对象   
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                // ftp用户名和密码  
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                // 默认为true，连接不会被关闭  
                // 在一个命令之后被执行  
                reqFTP.KeepAlive = false;

                // 指定执行什么命令  
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                // 指定数据传输类型  
                reqFTP.UseBinary = true;

                FtpWebResponse respFTP = (FtpWebResponse)reqFTP.GetResponse();
                respFTP.Close();
                sRet = true;
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex);
            }
            return sRet;
        }


        /// <summary>
        /// 获取FTP文件大小
        /// </summary>
        /// <param name="dirName">目录名</param>
        /// <param name="ftpServerIP">服务器地址</param>
        /// <param name="ftpUserID">ftp用户名</param>
        /// <param name="ftpPassword">ftp密码</param>
        /// <returns></returns>
        private long GetFtpFileSize(string filepath, string ftpUserID, string ftpPassword)
        {
            long sRet = 0;
            try
            {
                string uri = filepath;
                FtpWebRequest reqFTP;

                // 根据uri创建FtpWebRequest对象   
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                // ftp用户名和密码  
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                // 默认为true，连接不会被关闭  
                // 在一个命令之后被执行  
                reqFTP.KeepAlive = false;

                // 指定执行什么命令  
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;

                // 指定数据传输类型  
                reqFTP.UseBinary = true;

                FtpWebResponse respFTP = (FtpWebResponse)reqFTP.GetResponse();
                sRet = respFTP.ContentLength;
                respFTP.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                sRet = 0;
            }
            return sRet;
        }

        /// <summary>
        /// 上传FTP文件
        /// </summary>
        /// <param name="part"></param>
        private void UploadFTPFile(string key, FTPTransListItem part)
        {
            //bool success = true;
            FileInfo fileInf = new FileInfo(part.LocFilePath);
            long allbye = (long)fileInf.Length;

            long startfilesize = 0;
            if (part.IsContinue)
            {
                startfilesize = GetFtpFileSize(part.ServerFilePath, FTPUser, FTPPassword);// GetFileSize(newFileName, ftpServerIP, ftpUserID, ftpPassword, path);
            }
            if (startfilesize >= allbye)
            {
                return;
            }
            long startbye = startfilesize;

            string uri = part.ServerFilePath;
            FtpWebRequest reqFTP;
            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            // ftp用户名和密码 
            reqFTP.Credentials = new NetworkCredential(FTPUser, FTPPassword);
            // 默认为true，连接不会被关闭 
            // 在一个命令之后被执行 
            reqFTP.KeepAlive = false;
            // 指定执行什么命令 
            if (part.IsContinue)
            {
                reqFTP.Method = WebRequestMethods.Ftp.AppendFile;
            }
            else
            {
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            }
            // 指定数据传输类型 
            reqFTP.UseBinary = true;
            // 上传文件时通知服务器文件的大小 
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 20480;// 缓冲大小设置为20kb 
            byte[] buff = new byte[buffLength];
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件 
            FileStream fs = fileInf.OpenRead();
            try
            {
                // 把上传的文件写入流 
                Stream strm = reqFTP.GetRequestStream();
                // 每次读文件流的20kb   
                fs.Seek(startfilesize, SeekOrigin.Begin);
                int contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束 
                while (contentLen != 0 && part.CloseState == false && part.RunState == FTPRunState.Run)
                {
                    // 把内容从file stream 写入 upload stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    startbye += contentLen;
                    if (OnProgressChanged != null)
                    {
                        //System.Threading.Tasks.Task.Factory.StartNew(() =>
                        //{
                        OnProgressChanged(key, Math.Round(((double)startbye / (double)reqFTP.ContentLength) * 100, 2));
                        //});
                    }
                }
                // 关闭两个流 
                strm.Close();
                fs.Close();

                if (part.RunState == FTPRunState.Run)
                {
                    UploadFileList.TryRemove(key, out part);
                    if (OnProgressChanged != null)
                    {
                        OnProgressChanged(key, 100);
                    }
                    if (OnCompleted != null)
                    {
                        OnCompleted(key, part.ServerFilePath);
                    }
                }
            }
            catch(Exception ex)
            {
                part.RunState = FTPRunState.Error;
                UploadFileList.TryRemove(key, out part);//出错则删除
                OnFailed(key, 150);
            }
        }

        /// <summary>
        /// 下载FTP文件
        /// </summary>
        /// <param name="part"></param>
        private void DownloadFTPFile(string key, FTPTransListItem part, double filesize)
        {
            try
            {
                //下载文件的URI
                Uri u = new Uri(part.ServerFilePath);
                //设定下载文件的保存路径
                string downFile = part.LocFilePath;

                //FtpWebRequest的作成
                System.Net.FtpWebRequest ftpReq = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(u);

                //设定用户名和密码

                ftpReq.Credentials = new System.Net.NetworkCredential(FTPUser, FTPPassword);

                //MethodにWebRequestMethods.Ftp.DownloadFile("RETR")设定

                ftpReq.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;

                //要求终了后关闭连接
                ftpReq.KeepAlive = false;

                //使用ASCII方式传送
                ftpReq.UseBinary = false;

                //设定PASSIVE方式无效
                ftpReq.UsePassive = false;



                //判断是否继续下载
                //继续写入下载文件的FileStream
                System.IO.FileStream fs;
                if (part.IsContinue)
                {
                    if (System.IO.File.Exists(downFile))
                    {
                        //继续下载
                        ftpReq.ContentOffset = (new System.IO.FileInfo(downFile)).Length;
                        fs = new System.IO.FileStream(downFile, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                    }

                    else
                    {
                        //一般下载
                        fs = new System.IO.FileStream(downFile, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    }
                }
                else
                {
                    if (System.IO.File.Exists(downFile))
                    {
                        File.Delete(downFile);
                    }
                    //一般下载
                    fs = new System.IO.FileStream(downFile, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                }



                //取得FtpWebResponse
                System.Net.FtpWebResponse ftpRes = (System.Net.FtpWebResponse)ftpReq.GetResponse();

                //为了下载文件取得Stream
                System.IO.Stream resStrm = ftpRes.GetResponseStream();

                //写入下载的数据
                byte[] buffer = new byte[20480];

                double posLen = 0;
                while (part.CloseState == false && part.RunState == FTPRunState.Run)
                {
                    int readSize = resStrm.Read(buffer, 0, buffer.Length);
                    posLen += readSize;
                    if (readSize == 0)
                        break;

                    fs.Write(buffer, 0, readSize);

                    if (OnProgressChanged != null)
                    {
                        //System.Threading.Tasks.Task.Factory.StartNew(() =>
                        //{
                        OnProgressChanged(key, Math.Round((posLen / filesize) * 100, 2));
                        //});
                    }
                }
                fs.Close();
                resStrm.Close();

                if (part.RunState == FTPRunState.Run)
                {
                    UploadFileList.TryRemove(key, out part);
                    if (OnProgressChanged != null)
                    {
                        OnProgressChanged(key, 100.00);
                    }
                    
                    if (OnCompleted != null)
                    {
                        OnCompleted(key, part.ServerFilePath);
                    }
                }


                //表示从FTP服务器被送信的状态
                //Console.WriteLine("{0}: {1}", ftpRes.StatusCode, ftpRes.StatusDescription);

                //关闭连接
                ftpRes.Close();

            }
            catch(Exception ex)
            {
                part.RunState = FTPRunState.Error;
                UploadFileList.TryRemove(key, out part);//出错则删除
                OnFailed(key, 150);
            }
        }

    }
}
