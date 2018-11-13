using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using EllaMakerTool.Message;
using EllaMakerTool.Core;
using EllaMakerTool.Models;

namespace EllaMakerTool.Models
{
     public class Global
    {
        public static string ImageServerAdress = DESEncryptHelper.Decrypt(ConfigurationManager.AppSettings.Get("ImageServerAdress"));
        public static bool ArrowDeleteFolder = false;
        public static bool ArrowDeleteFile = false;
        public static bool ArrowEditFolder = false;
        public static bool ArrowEditFile = false;
        public static AuthToken authToken = new AuthToken();
        public static List<string> DepartId;
        public static List<OpenFolderOptDataModel> RecordList = new List<OpenFolderOptDataModel>();

        #region ERROR Code

        public const string ERROR_TOKEN = "0x0000001";

        #endregion
        #region 事件消息
        /// <summary>
        /// 显示图书列表
        /// </summary>
        public const string ShowBookListMSG = "MainBookListEventRouter";
        /// <summary>
        /// 加图书列表数据
        /// </summary>
        public const string RefreshBookListData = "RefreshBookListData";
        /// <summary>
        /// 显示图书列表
        /// </summary>
        public const string ShowEBookListMSG = "MainEBookListEventRouter";
        /// <summary>
        /// 加图书列表数据
        /// </summary>
        public const string RefreshEBookListData = "RefreshEBookListData";

        public const string EBookBrowserMSG = "EBookBrowserMSG";

        public const string BookBrowserMSG = "BookBrowserMSG";

        public const string LoadFTPExplorerMSG = "LoadFTPExplorer";
        /// <summary>
        ///重新登录 
        /// </summary>
        public const string ReLoginMSG = "ReLoginMSG";
        /// <summary>
        /// 切换部门或公司
        /// </summary>
        public const string CompanySwitchEventRouter = "CompanySwitchEventRouter";

       

        #endregion
    }
}
