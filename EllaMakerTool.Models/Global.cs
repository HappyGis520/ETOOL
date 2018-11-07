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
        public static bool CompanyDocDeletRight = false;
        public static bool CompanyFileDeletRight = false;
        public static bool CompanyDocEditRight = false;
        public static bool CompanyFileEditRight = false;
        public static AuthToken authToken = new AuthToken();
        public static List<string> DepartId;
        public static List<OpenFolderOptDataModel> RecordList = new List<OpenFolderOptDataModel>();
    }
}
