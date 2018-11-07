using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    public class DepartmentApiModel
    {
        /// <summary>
        /// 父级单元Id，父级部门为公司,编号为-1
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartmentId { get; set; } // Id (Primary key)
        /// <summary>
        /// 单元名称
        /// </summary>
        public string Title { get; set; } // Title
        /// <summary>
        /// 单元负责人Id,如果没有指定则为-1
        /// </summary>
        public string ManagerProfileId { get; set; } // ManagerProfileId
        /// <summary>
        /// 排序序号,级父级的单元，按该值升序排序
        /// </summary>
        public byte OrderID { get; set; }
        /// <summary>
        /// 职级等级，级别超高，数值越大
        /// </summary>
        public int Ranks { get; set; }

        public int NodeLevel { get; set; }
    }
}
