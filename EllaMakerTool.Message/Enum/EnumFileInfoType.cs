using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 文件夹/文件类别   ALL(0,"文件或文件夹"), FILE(1,"文件"),FOLDER(2,"文件夹");
    /// </summary>
    public enum EnumFileInfoType
    {
        /// <summary>
        /// 文件或文件夹
        /// </summary>
        [Description("文件或文件夹")]
        ALL,
        /// <summary>
        /// 文件
        /// </summary>
        [Description("文件")]
        FILE,
        /// <summary>
        /// 文件夹
        /// </summary>
        [Description("文件夹")]
        FOLDER


    }
}
