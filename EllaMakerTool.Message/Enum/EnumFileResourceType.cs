/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： EnumFileResourceType.cs
 * * 功   能：  FTP文件资源类别
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 13:40:03
 * * 修改记录： 
 * * 日期时间： 2018-09-25 13:40:03  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// FTP文件资源类别
    /// </summary>
    internal enum EnumFileResourceType
        {
            [Description("高清资源")] HDRESOURCE = 0,
            [Description("低清资源")] LOWRESOURCE = 1,
            [Description("剧本")] SCENARIO = 2,
            [Description("音频资源")] AUDIO = 3,
            [Description("PSD素材")] PSD = 4,
            [Description("中间文件")] MATERIAL = 5,
            [Description("工程文件")] PROJECT = 6,
            [Description("发布文件")] RELEASE = 7
        }
}
