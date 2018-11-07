using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Core.FTP.Entity
{
    // <summary>
    /// 正在上传列表
    /// </summary>
    public class FTPItem
    {
        /// <summary>
        /// true：上传 false：下载
        /// </summary>
        public bool IsUpload = true;
        /// <summary>
        /// SessionID
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// 服务器文件路径
        /// </summary>
        public string ServerFilePath { get; set; }

        /// <summary>
        /// 客户端文件路径
        /// </summary>
        public string LocFilePath { get; set; }

        /// <summary>
        /// 是否续传
        /// </summary>
        public bool IsContinue { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        public FTPRunState RunState = FTPRunState.None;

        /// <summary>
        /// 是否已取消
        /// </summary>
        public bool CloseState { get; set; }
    }
}
