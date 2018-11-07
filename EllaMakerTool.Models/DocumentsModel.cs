using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EllaMakerTool.Message;
using  EllaMakerTool.Models;
using MVVMSidekick.ViewModels;

namespace EllaMakerTool.Models
{
    public class DocumentsModel:BindableBase<DocumentsModel>
    {

        public bool isChecked
        {
            get { return _isCheckedLocator(this).Value; }
            set { _isCheckedLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool isChecked Setup        
        protected Property<bool> _isChecked = new Property<bool> { LocatorFunc = _isCheckedLocator };
        static Func<BindableBase, ValueContainer<bool>> _isCheckedLocator = RegisterContainerLocator<bool>("isChecked", model => model.Initialize("isChecked", ref model._isChecked, ref _isCheckedLocator, _isCheckedDefaultValueFactory));
        static Func<bool> _isCheckedDefaultValueFactory = () =>false;
        #endregion

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
}
