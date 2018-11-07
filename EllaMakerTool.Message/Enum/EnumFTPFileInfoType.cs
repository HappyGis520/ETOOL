/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： EnumFTPFileInfoType.cs
 * * 功   能：  文件夹/文件类别
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 13:40:21
 * * 修改记录： 
 * * 日期时间： 2018-09-25 13:40:21  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaMaker.FTP.Model.Message
{

    /// <summary>
    /// 文件夹/文件类别
    /// </summary>
    internal enum  EnumFileInfoType
        {

            [Description("文件或文件夹")]
            ALL = 0,
                [Description("文件")]
            FILE = 1,
                [Description("文件夹")]
            FOLDER =2

        }
    
}
