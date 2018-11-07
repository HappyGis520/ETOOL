using System.Collections.Generic;

namespace EllaMakerTool.Message
{
    public class EmployeeAndDeptNodelApiModel
    {
        /// <summary>
        /// 节点名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 节点类型（0部门 1人）
        /// </summary>
        public EnumPsAndDeptItemtype ItemType { get; set; }
        /// <summary>
        /// 节点ID（部门为部门ID 人为PID）
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 节点头图（个人为头像，部门为部门图标【暂无部门图标 填空""】）
        /// </summary>
        public string HeadUrl { get; set; }
        /// <summary>
        /// 子节点内容
        /// </summary>
        public List<EmployeeAndDeptNodelApiModel> Childrens { get; set; } = new List<EmployeeAndDeptNodelApiModel>();
    }
}
