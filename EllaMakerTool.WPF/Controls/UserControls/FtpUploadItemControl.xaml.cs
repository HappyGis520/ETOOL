using EllaMakerTool.Core.FTP;
using EllaMakerTool.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using EllaMakerTool.Message;
using EllaMakerTool.Models;

namespace EllaMakerTool.WPF.Controls.UserControls
{
    /// <summary>
    /// FtpUploadItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class FtpUploadItemControl : UserControl
    {

        #region

        /// <summary>
        /// 父控件
        /// </summary>
        private WrapPanel ParentControl = null;

        /// <summary>
        /// 父窗体选择集合
        /// </summary>
        private Dictionary<string, FtpUploadItemControl> DicFileItems = null;

        /// <summary>
        /// 本地路径
        /// </summary>
        public string LocalPath { get; set; }

        public DocumentV1Request docV1Source = new DocumentV1Request();
        /// <summary>
        /// FTP用户名
        /// </summary>
        public string FTPUserName { get; set; }

        /// <summary>
        /// FTP密码
        /// </summary>
        public string FTPPassword { get; set; }

        /// <summary>
        /// FTP服务器地址
        /// </summary>
        public string FTPAddress { get; set; }

        /// <summary>
        /// 上传对象
        /// </summary>
        private int fileType = 2;

        /// <summary>
        /// 是否正在上传
        /// </summary>
        public bool IsUpload = false;

        /// <summary>
        /// 开始上传
        /// </summary>
        private FTPClient ftpClient = null;

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsComplete = false;

        /// <summary>
        /// 是否已出错
        /// </summary>
        public bool IsError = false;

        /// <summary>
        /// 上传至服务器路径
        /// </summary>
        public string ServerUploadPath { get; set; }

        /// <summary>
        /// 返回ftp结果
        /// </summary>
        private FTPResult FtpResult { get; set; }

        /// <summary>
        /// 源图片FTP结果
        /// </summary>
        private FTPResult SourceImageResult { get; set; }
        /// <summary>
        /// 上传百分比
        /// </summary>
        public string Precent = "0%";
        /// <summary>
        /// 新源视频
        /// </summary>
        private string newSourcePath = "";

        #endregion

        public FtpUploadItemControl(WrapPanel stackPanel, Dictionary<string, FtpUploadItemControl> dicFileItems)
        {
            InitializeComponent();
            
            ParentControl = stackPanel;
            DicFileItems = dicFileItems;
            bg.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory+"/Resources/Icon_UploadSuccess.png",UriKind.RelativeOrAbsolute)); 
        }


        public void InitFileInfo()
        {
            System.IO.FileInfo fileInfo = new FileInfo(LocalPath);
            FileNameLb.Content = fileInfo.Name.Substring(0,fileInfo.Name.LastIndexOf('.'));
            FileSizelb.Content = Utility.CountSize(fileInfo.Length);
        }
        /// <summary>
        /// 初始化图片视图 FileType 2-图片 1-其他
        /// </summary>
        public void InitImageView(int FileType = 2,string type="other")
        {
            if (!string.IsNullOrEmpty(LocalPath))
            {
                try
                {
                    
                    fileType = FileType;
                    newSourcePath = LocalPath;
                    if (fileType == 2) // 图片
                    {
                        BitmapImage bi = new BitmapImage(new Uri(LocalPath, UriKind.Absolute));
                        TypeIcon.Source = bi;
                    }
                    else // 其他类型
                    {
                        TypeIcon.Source = XConverter.SvgToXamlConverter.SvgToPic(type);
                    }
                }
                catch (Exception ex)
                {
                    //Log.Error(ex);
                }
            }
        }

        
        /// <summary>
        /// 开始上传
        /// </summary>
        public void StartUpload()
        {
            IsUpload = true;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                ProgressBar.Value = 0;
            }));
            Thread uploadThread = new Thread(Upload);
            uploadThread.IsBackground = true;
            uploadThread.Start();
            //new Thread(Upload).Start();
        }

        /// <summary>
        /// 上传
        /// </summary>
        private void Upload()
        {
            ftpClient = new FTPClient(FTPAddress,FTPUserName, FTPPassword, FTPModel.Binary, Encoding.Default);

            int result = ftpClient.Connect();
            if (result == 0)
            {
                ftpClient.OnProgressChanged += ftpClient_OnProgressChanged;
                ftpClient.OnCompleted += ftpClient_OnCompleted;
                ftpClient.OnFailed += ftpClient_OnFailed;
                FtpResult = ftpClient.Upload(LocalPath,FTPAddress,ServerUploadPath); // 开始上传
            }
            else
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ProgressBar.Value = -1;
                }));
        }

        /// <summary>
        /// 进度显示事件
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="progress"></param>
        private void ftpClient_OnProgressChanged(string sessionID, double progress)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ProgressBar.Value = progress;
                    Precent = progress+"%";
                }));
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
            }
        }

        /// <summary>
        /// 上传完成事件
        /// </summary>
        /// <param name="sessionID"></param>
        private void ftpClient_OnCompleted(string sessionID,string url)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                StatusImg.Visibility = Visibility.Visible;
                string[] arr = url.Split(new string[] {sessionID},StringSplitOptions.RemoveEmptyEntries);
                docV1Source.Url = GlobalPara.UploadPathNow + sessionID;
                if (arr.Length > 1)
                    docV1Source.Url = docV1Source.Url + arr[1];
                docV1Source.ThumbnailUrl = docV1Source.Url;
                docV1Source.Name = docV1Source.Name.ToApiSafeStr();
                var req = GlobalPara.webApis.HasSameName(docV1Source.CatalogId, docV1Source.Name,
                    docV1Source.Type == EnumDocFileType.Folder
                        ? EnumDocType.Catalog
                        : EnumDocType.File,docV1Source.FilePath);
                
                var reqpara = new List<DocumentV1Request>()
                {
                    docV1Source
                };
                if (!req.Successful)
                {
                    if (GlobalPara.rootTypeNow == 2)
                    {
                        //同命且没有权限
                        if (req.Code == 2)
                        {
                            if (CoverMessageBox.Show(null,
                                    $"正在上传 {docV1Source.Name} 到……",
                                    $"目标包含同名文件(您没有权限修改)", "替换目标中的文件", "保存成新的文件", false, true) !=
                                MessageBoxResult.None)
                            {
                                var res = GlobalPara.webApis.Upload(GlobalPara.rootTypeNow, reqpara);
                                if (!res.Successful)
                                {
                                    MessageBox.Show(res.Message, "错误");
                                }
                                else
                                {
                                    GlobalPara.UploadItems.AddRange(res.Data);
                                }
                            }
                        }
                        if (req.Code == 3)
                        {
                            if (CoverMessageBox.Show(null,
                                    $"正在上传 {docV1Source.Name} 到……",
                                    $"目标包含同名文件", "替换目标中的文件", "保存成新的文件") ==
                                MessageBoxResult.OK)
                            {
                                var res2 = GlobalPara.webApis.AddSyncFile(req.Data, docV1Source.Url,
                                    docV1Source.ThumbnailUrl,
                                    docV1Source.Size);
                            }
                            else
                            {
                                var res = GlobalPara.webApis.Upload(GlobalPara.rootTypeNow, reqpara);
                                if (!res.Successful)
                                {
                                    MessageBox.Show(res.Message, "错误");
                                }
                                else
                                {
                                    GlobalPara.UploadItems.AddRange(res.Data);
                                }
                            }
                        }
                    }
                    else
                    {
                        GlobalPara.webApis.AddSyncFile(req.Data, docV1Source.Url,
                            docV1Source.ThumbnailUrl,
                            docV1Source.Size);
                    }
                }
                else
                {
                    

                    var res = GlobalPara.webApis.Upload(GlobalPara.rootTypeNow, reqpara);
                    if (!res.Successful)
                    {
                        MessageBox.Show(res.Message,"错误");
                    }
                    else
                    {
                        GlobalPara.UploadItems.AddRange(res.Data);
                    }
                }
            }));
            
        }

        /// <summary>
        /// 上传失败事件
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="errorID"></param>
        private void ftpClient_OnFailed(string sessionId, int errorId)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ProgressBar.Value = -1;
                    IsError = true;
                }));
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
            }
        }
    }
}
