using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    public class EBookListByPageParam:ListByPageParam
    {

        /// <summary>
        /// 系列名
        /// </summary>
        public string seriesId { get; set; } = "";
        /// <summary>
        /// 出版社ID
        /// </summary>
        public string publisherId { get; set; } = "";
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string creatorId { get; set; } = "";
        /// <summary>
        /// 动画书名称
        /// </summary>
        public string ebookName { get; set; } = "";
        /// <summary>
        /// 起始创建时间
        /// </summary>
        public string startCreateTime { get; set; } = "";
        /// <summary>
        /// 结束创建时间
        /// </summary>
        public string endCreateTime { get; set; } = "";
        /// <summary>
        /// 开始发布时间
        /// </summary>
        public string startPublishTime { get; set; } = "";
        /// <summary>
        /// 结束发布时间
        /// </summary>
        public string endPublishTime { get; set; } = "";
        /// <summary>
        /// 开始发布时间
        /// </summary>
        public string startUploadTime { get; set; } = "";
        /// <summary>
        /// 结束发布时间
        /// </summary>
        public string endUploadTime { get; set; } = "";
    }
}