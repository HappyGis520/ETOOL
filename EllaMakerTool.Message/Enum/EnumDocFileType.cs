using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 文件类型
    /// </summary>
    public enum EnumDocFileType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Picture = 1,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        Video = 2,
        /// <summary>
        /// 文件
        /// </summary>
        [Description("文件")]
        File = 3,
        /// <summary>
        /// 文件
        /// </summary>
        [Description("文件夹")]
        Folder = 0
    }
}
