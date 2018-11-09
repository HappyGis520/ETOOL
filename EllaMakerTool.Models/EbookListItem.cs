using System;
using System.Collections.Generic;
using MVVMSidekick.ViewModels;

namespace EllaMakerTool.Models
{
    public class EBookListItem:BindableBase<EBookListItem>
    {
        /// <summary>
        /// 电子书唯一编码
        /// </summary>
        public String id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public String ebookname { get; set; }
        /// <summary>
        /// 图书编号
        /// </summary>
        public String bookid { get; set; }
        /// <summary>
        /// 图书名称
        /// </summary>
        public String bookname { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public String createtime { get; set; }
        /// <summary>
        /// 出版商名称
        /// </summary>
        public String publishername { get; set; }
        /// <summary>
        /// 最近发布时间
        /// </summary>
        public String publishtime { get; set; }
        /// <summary>
        /// 书籍当前状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public String uploadtime { get; set; }
        /// <summary>
        /// 创建者名称
        /// </summary>
        public String creatorname { get; set; }
        /// <summary>
        /// 系列名/套书名/书集名
        /// </summary>
        public String seriesname { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public float price { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public String pinyin { get; set; }
    }
}