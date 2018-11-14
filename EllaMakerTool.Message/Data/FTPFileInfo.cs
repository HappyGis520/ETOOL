/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： FTPFileInfo.cs
 * * 功   能：  文件或文件夹描述信息
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 13:49:34
 * * 修改记录： 
 * * 日期时间： 2018-09-25 13:49:34  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 文件或文件夹描述信息
    /// </summary>
    public class FTPFileInfo
        {
            /// <summary>
            /// 文件夹/文件编号
            /// </summary>
            
            private string fileID;
            /// <summary>
            /// 上级文件夹编号
            /// </summary>
            private string parentID;
            /// <summary>
            /// 文件夹/文件名称
            /// </summary>
            private string fileName;
            /// <summary>
            /// 是否为文件
            /// </summary>
            private bool isFile;
            public  bool IsFile
            {
                get
                {
                    return isFile;
                }
            set { isFile = value; }
            }


            public  string ParentID
            {
                get
                {
                    return parentID;
                }
                set { parentID = value; }
            }

            public  string FileID
            {
                get
                {

                    return fileID;
                }
            set { fileID = value; }
        }

            public  string FileName
            {
                get
                {

                    return fileName;
                }
            set { fileName = value; }
        }

            public FTPFileInfo()
            {
                
            }

        public FTPFileInfo(bool isFile, string parentID, string fileID, string fileName)
            {

                this.parentID = parentID;
                this.fileID = fileID;
                this.fileName = fileName;
                this.isFile = isFile;
            }

        }

}
