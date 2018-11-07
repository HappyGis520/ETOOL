using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 文档或文件夹
    /// </summary>
    public enum EnumDocType
    {
        /// <summary>
        /// 文件
        /// </summary>
        [Description("文件")]
        File = 1,
        /// <summary>
        /// 文件夹
        /// </summary>
        [Description("文件夹")]
        Catalog = 2
    }
}
