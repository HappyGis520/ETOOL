using EllaMakerTool.Core.FTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using EllaMakerTool.Message;
using EllaMakerTool.Models;

namespace EllaMakerTool.WPF.Controls.UserControls
{
    /// <summary>
    /// UploadUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class UploadUserControl : UserControl
    {
        public UploadUserControl()
        {
            InitializeComponent();
            //Init();
        }

        public string FTPUserName { get; set; } = GlobalPara.SourceUserName;

        public string FTPPassword { get; set; } = GlobalPara.SourcePwd;

        public string FTPAddress { get; set; } = GlobalPara.SourceServerAdress;

        public void startUpload()
        {
            Upload();
        }
        /// <summary>
        /// 选择框窗体
        /// </summary>
        public Dictionary<string, FtpUploadItemControl> DicFileItems = new Dictionary<string, FtpUploadItemControl>();

        /// <summary>
        /// 是否开始上传了
        /// </summary>
        private bool IsStartUpload = false;

        /// <summary>
        /// 检查是否上传成功
        /// </summary>
        private Thread th_CheckUpload = null;

        /// <summary>
        /// 是否检查线程
        /// </summary>
        private bool IsContinueCheck = false;

        FTPClient client;

        /// <summary>
        /// 是否已经执行
        /// </summary>
        private bool IsExcute = false;

        private void Btn_AddFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == true && dialog.FileNames.Length > 0)
            {
                AddFiles(dialog.FileNames.ToList());
                scr.Visibility = Visibility.Visible;
                Btn_AddFile.Visibility = Visibility.Collapsed;
                g.Visibility = Visibility.Hidden;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            th_CheckUpload = new Thread(CheckUploadFile);
            th_CheckUpload.IsBackground = true; // 设置为后台线程，当主线程退出时该线程也会退出
            th_CheckUpload.Start();
            Init();
            scr.Visibility = Visibility.Collapsed;
            Btn_AddFile.Visibility = Visibility.Visible;
            g.Visibility = Visibility.Visible;
        }

        private void Init()
        {
            FTPAddress = GlobalPara.SourceServerAdress;
            FTPUserName = GlobalPara.SourceUserName;
            FTPPassword = GlobalPara.SourcePwd;
            client = new FTPClient(FTPAddress, FTPUserName, FTPPassword, FTPModel.ASCII, Encoding.Default);
        }

        public void CheckUploadFile()
        {
            while (IsContinueCheck)
            {
                try
                {
                    // 包含待上传文件 并且开始上传后 进行检查
                    if (DicFileItems.Values.Count > 0 && IsStartUpload)
                    {
                        Dictionary<string, string> removeKeys = new Dictionary<string, string>();
                        foreach (var keyValue in DicFileItems)
                        {
                            if (keyValue.Value.IsComplete)
                            {
                                IsExcute = false;
                                if (!removeKeys.ContainsKey(keyValue.Key))
                                    removeKeys.Add(keyValue.Key, "");
                            }
                            else
                            {
                                if (keyValue.Value.Precent.Contains("100"))
                                {
                                    IsExcute = false;
                                    if (!removeKeys.ContainsKey(keyValue.Key))
                                        removeKeys.Add(keyValue.Key, "");
                                }
                            }
                        }
                        if (removeKeys.Count > 0 && !IsExcute)
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                IsExcute = true;
                                try
                                {
                                    foreach (var item in removeKeys)
                                    {
                                        if (DicFileItems.ContainsKey(item.Key))
                                        {
                                            if (UploadstackPanel.Children.Contains(DicFileItems[item.Key]))
                                                UploadstackPanel.Children.Remove(DicFileItems[item.Key]);
                                            DicFileItems.Remove(item.Key);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    IsContinueCheck = false;
                                }
                            }));
                    }
                }
                catch (Exception ex)
                {

                }
                Thread.Sleep(2 * 1000);
            }
        }

        private void AddFiles(List<string> fileNames,string rootStr)
        {
            List<string> imageExtList = new List<string> {".jpg", ".bmp", ".jpeg", ".png", ".gif",".icon"};
            List<string> videoExtList =
                new List<string> {"rmvb", "wmv", "asf", "avi", "3gp", "mpg", "mkv", "mp4", "mpeg2", "mpeg4"};
            foreach (var fileName in fileNames)
            {
                if (!DicFileItems.ContainsKey(fileName))
                {
                    string fileExt = System.IO.Path.GetExtension(fileName).Substring(1);
                    bool isPic = imageExtList.Contains(fileExt.ToLower());
                    bool isVideo = videoExtList.Contains(fileExt.ToLower());
                    FtpUploadItemControl item = new FtpUploadItemControl(UploadstackPanel, DicFileItems);
                    item.LocalPath = fileName;
                    item.FTPUserName = FTPUserName;
                    item.FTPPassword = FTPPassword;
                    item.FTPAddress = FTPAddress;
                    item.ServerUploadPath = GlobalPara.UploadPathNow;
                    System.IO.FileInfo fileInfo = new FileInfo(fileName);
                    item.docV1Source = new DocumentV1Request()
                    {
                       CatalogId= GlobalPara.CatalogNow.CatelogId,
                       Name=System.IO.Path.GetFileName(fileName),
                       Size= Convert.ToInt32(fileInfo.Length/1024.00),
                       FilePath=fileName.Substring(0,fileName.LastIndexOf('\\')).Replace(rootStr, ""),
                       Type= isPic?(EnumDocFileType)1:(isVideo? (EnumDocFileType)2 : (EnumDocFileType)3)
                    };
                    if (item.docV1Source.Size == 0) item.docV1Source.Size = 1;
                    
                    
                    item.InitFileInfo();
                    if (isPic)
                    {
                        item.InitImageView(2, fileExt.ToLower());
                    }
                    else
                    {
                        item.InitImageView(1, fileExt.ToLower());
                    }

                    UploadstackPanel.Children.Add(item);
                    DicFileItems.Add(fileName, item);
                }
            }

            grid.Background = null;
        }

        private void AddFiles(List<string> fileNames)
        {
            List<string> imageExtList = new List<string> { ".jpg", ".bmp", ".jpeg", ".png", ".gif", ".icon" };
            List<string> videoExtList =
                new List<string> { "rmvb", "wmv", "asf", "avi", "3gp", "mpg", "mkv", "mp4", "mpeg2", "mpeg4" };
            foreach (var fileName in fileNames)
            {
                if (!DicFileItems.ContainsKey(fileName))
                {
                    string fileExt = System.IO.Path.GetExtension(fileName).Substring(1);
                    bool isPic = imageExtList.Contains(fileExt.ToLower());
                    bool isVideo = videoExtList.Contains(fileExt.ToLower());
                    FtpUploadItemControl item = new FtpUploadItemControl(UploadstackPanel, DicFileItems);
                    item.LocalPath = fileName;
                    item.FTPUserName = FTPUserName;
                    item.FTPPassword = FTPPassword;
                    item.FTPAddress = FTPAddress;
                    item.ServerUploadPath = GlobalPara.UploadPathNow;
                    System.IO.FileInfo fileInfo = new FileInfo(fileName);
                    item.docV1Source = new DocumentV1Request()
                    {
                        CatalogId = GlobalPara.CatalogNow.CatelogId,
                        Name = System.IO.Path.GetFileName(fileName),
                        Size = Convert.ToInt32(fileInfo.Length / 1024.00),
                        FilePath = "",
                        Type = isPic ? (EnumDocFileType)1 : (isVideo ? (EnumDocFileType)2 : (EnumDocFileType)3)
                    };
                    if (item.docV1Source.Size == 0) item.docV1Source.Size = 1;


                    item.InitFileInfo();
                    if (isPic)
                    {
                        item.InitImageView(2, fileExt.ToLower());
                    }
                    else
                    {
                        item.InitImageView(1, fileExt.ToLower());
                    }

                    UploadstackPanel.Children.Add(item);
                    DicFileItems.Add(fileName, item);
                }
            }

            grid.Background = null;
        }

        private void Upload()
        {
            try
            {
                if (DicFileItems.Count > 0)
                {
                    IsStartUpload = true;

                    foreach (var item in DicFileItems.Values.Where(m => !m.IsUpload))
                        item.StartUpload();
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
            }
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            if
                (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects =
                    DragDropEffects.Link;
            else
                e.Effects =
                    DragDropEffects.None;
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            string[] fileNames = ((string[]) e.Data.GetData(DataFormats.FileDrop));
            List<string> fileList = new List<string>();
            foreach(string I in fileNames)
            {
                List<string> resFiles = new List<string>();
                var root = I.Substring(0, I.LastIndexOf("\\") + 1);
                if (IsDirectory(I))
                {
                    if (GlobalPara.rootTypeNow == 1&&!Global.ArrowEditFolder)
                    {
                        continue;
                    }

                    AddFiles(GetAllFiles(I), root);

                }
                else
                {
                    if (GlobalPara.rootTypeNow == 1 && !Global.ArrowEditFile)
                    {
                        continue;
                    }
                    fileList.Add(I);
                }
            }
            AddFiles(fileList);
            scr.Visibility = Visibility.Visible;
            Btn_AddFile.Visibility = Visibility.Collapsed;
            g.Visibility = Visibility.Hidden;

        }

        /// <summary>  
        /// 判断path是否为目录  
        /// </summary>  
        public bool IsDirectory(String path)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(path);
            return (info.Attributes & System.IO.FileAttributes.Directory) != 0;
        }

        /// <summary>
        /// 获得指定目录下的所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<FileInfo> GetFilesByDir(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            //找到该目录下的文件
            FileInfo[] fi = di.GetFiles();

            //把FileInfo[]数组转换为List
            List<FileInfo> list = fi.ToList<FileInfo>();
            return list;
        }

        /// <summary>
        /// 获得指定目录及其子目录的所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<FileInfo> GetAllFilesByDir(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            //找到该目录下的文件
            FileInfo[] fi = dir.GetFiles();

            //把FileInfo[]数组转换为List
            List<FileInfo> list = fi.ToList<FileInfo>();

            //找到该目录下的所有目录里的文件
            DirectoryInfo[] subDir = dir.GetDirectories();
            foreach (DirectoryInfo d in subDir)
            {
                List<FileInfo> subList = GetFilesByDir(d.FullName);
                foreach (FileInfo subFile in subList)
                {
                    list.Add(subFile);
                }
            }
            return list;
        }

        /// <summary>
        /// 列出指定目录下及所其有子目录及子目录里更深层目录里的文件（需要递归）
        /// </summary>
        /// <param name="path"></param>
        public List<string> GetAllFiles(string path)
        {
            List<string> DocumentTress = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);

            //找到该目录下的文件
            FileInfo[] fi = dir.GetFiles();
            foreach (var item in fi)
            {
                DocumentTress.Add(item.FullName);
            }
            //找到该目录下的所有目录再递归
            DirectoryInfo[] subDir = dir.GetDirectories();
            foreach (DirectoryInfo d in subDir)
            {
                DocumentTress=DocumentTress.Concat(GetAllFiles(d.FullName)).ToList();
            }
            return DocumentTress;
        }
    }
}
