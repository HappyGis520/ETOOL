using System.ComponentModel.DataAnnotations;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 文件夹请求类
    /// </summary>
    public class DocumentCatalogV1Request
    {
        /// <summary>
        /// 文件夹名 
        /// </summary>
        [Required]
        public string Name { get; set; } // Name

        /// <summary>
        /// 父文件夹id
        /// </summary>
        [Required]
        public string ParentId { get; set; } // ParentId

    }

    /// <summary>
    /// 文件请求类
    /// </summary>
    public class DocumentV1Request
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        public string Name { get; set; } // Name

        /// <summary>
        /// 文件类型 1表示图片，2表示视频，3表示文件
        /// </summary>
        [Required]
        public EnumDocFileType Type { get; set; } // Type
        /// <summary>
        /// 文件上传返回的url
        /// </summary>
        [Required]
        public string Url { get; set; } // Url

        /// <summary>
        /// 文件原路径（eg1:文件夹1\文件夹2；eg2:文件夹1；）(在根目录时传空字符串)
        /// </summary>
        [Required]
        public string FilePath { get; set; } // Url

        /// <summary>
        /// 目标文件夹id
        /// </summary>
        [Required]
        public string CatalogId { get; set; } // CatalogId

        /// <summary>
        /// 文件大小，单位kb
        /// </summary>
        [Required]
        public int Size { get; set; } // Size

        /// <summary>
        /// 图片上传返回的缩略图url
        /// </summary>
        public string ThumbnailUrl { get; set; } // ThumbnailUrl
    }
}
