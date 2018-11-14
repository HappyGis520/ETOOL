/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： FTPFileInfoListParam.cs
 * * 功   能：  获取文件或文件夹参数
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 13:41:01
 * * 修改记录： 
 * * 日期时间： 2018-09-25 13:41:01  修改人：王建军  创建
 * *******************************************************************/

using System;

namespace  EllaMakerTool.Message
{

    /// <summary>
    /// 获取FTP非根目录下文件或文件夹参数
    /// </summary>
    public class ListFTPParam : RequestModelBase
    {
        /// <summary>
        /// 搜索的文件或文件夹类别
        /// </summary>
        public string fileInfoType;
        /// <summary>
        ///  名称关键字
        /// </summary>
        public string fileName;
        /// <summary>
        /// 资源目录ID
        /// </summary>
        public string resourceDirID;

        public ListFTPParam(string Token, EnumFileInfoType FileType,string Searchar,string DirID ):base(Token)
        {
            fileInfoType = FileType.ToString();
            fileName = Searchar;
            resourceDirID = DirID;

        }
    }
}
