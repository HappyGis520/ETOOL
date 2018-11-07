using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 文档列表
    /// </summary>
    public class DocumentV1QueryModel
    {
        /// <summary>
        /// 当前文件夹ID
        /// </summary>
        public string CatelogId { get; set; }
        /// <summary>
        /// 创建者 根目录时为空
        /// </summary>
        public ProfileApiModel Creator { get; set; }
        /// <summary>
        /// 符合条件的记录总数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 共享范围(仅在是共享时有效)
        /// </summary>
        public DocRangeItem ShareRange { get; set; } = new DocRangeItem();

        /// <summary>
        /// 协作范围(仅在是共享时有效)
        /// </summary>
        public DocRangeItem SynergyRange { get; set; } = new DocRangeItem();
        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页记录数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 路径信息
        /// </summary>
        public List<CatalogSimpleModel> pathInfo { get; set; } = new List<CatalogSimpleModel>();
        /// <summary>
        /// 当前页的所有记录
        /// </summary>
        public List<DocumentV1ApiModel> Records { get; set; }

    }

    /// <summary>
    /// 文档列表
    /// </summary>
    public class DocumentV1ApiModel
    {
        /// <summary>
        /// 文档id
        /// </summary>
        public string Id { get; set; } // ID (Primary key)
        /// <summary>
        /// 文档名
        /// </summary>
        public string Name { get; set; } // Name      
        /// <summary>
        /// 文档类型  0表示文件夹， 1表示图片，2表示视频，3表示文件
        /// </summary>
        public EnumDocFileType Type { get; set; } // Type
        /// <summary>
        /// 分享状态
        /// </summary>
        public EnumDocStatusType StatusType { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public string ComapnyId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 所属文件夹Id
        /// </summary>
        public string CatelogId { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadTimes { get; set; }
        /// <summary>
        /// 创建者Id
        /// </summary>
        public string CreatorId { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// 是否有迭代记录
        /// </summary>
        public bool HasIr { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// 共享范围
        /// </summary>
        public DocRangeItem ShareRange { get; set; }
        /// <summary>
        /// 协作范围
        /// </summary>
        public DocRangeItem SynergyRange { get; set; }
        /// <summary>
        /// 迭代列表
        /// </summary>
        public List<IterationItem> IrList { get; set; } = new List<IterationItem>();
    }

    public class CatalogSimpleModel
    {
        /// <summary>
        /// 文件夹名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件夹ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 文件夹相对层级越外层越小（最外层为1）
        /// </summary>
        public int Level { get; set; }
    }

    public class DocRangeItem
    {
        /// <summary>
        /// 部门列表
        /// </summary>
        public List<DepartmentApiModel> departs { get; set; } = new List<DepartmentApiModel>();

        /// <summary>
        /// 人员列表
        /// </summary>
        public List<ProfileApiModel> users { get; set; } = new List<ProfileApiModel>();
    }

    /// <summary>
    /// 迭代记录项
    /// </summary>
    public class IterationItem
    {
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadTimes { get; set; }
        /// <summary>
        /// 创建者Id
        /// </summary>
        public string CreatorId { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatorName { get; set; }

    }
}
