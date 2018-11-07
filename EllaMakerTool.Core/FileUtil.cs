using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EllaMakerTool.Core;

namespace EllaMakerTool
{
    public class FileUtil
    {
        public static string NewPath(int companyId)
        {
            var path = $"/{companyId}/{DateTime.Now.ToString("yyMM/dd")}/{Guid.NewGuid().ToString("n")}/";
            return path;
        }

        public static string GetFileDir()
        {
       

            string fileDir = System.Configuration.ConfigurationManager.AppSettings["FileDir"];
         
            return fileDir;
        }

        public static string GetFileName(string path)
        {
            var idx = path.LastIndexOf('/');
            return path.Substring(idx + 1);
        }
    }
}
