using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    public class FunctionPermissionApiModel
    {
        /// <summary>
        /// 自增长编号
        /// </summary>
        public string Id { get; set; } // Id (Primary key)
        /// <summary>
        /// 父级模块编号
        /// </summary>
        public string ParentID { get; set; } // ModuleId
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; } // Name
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; } // DefaultEnable
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartmentId { get; set; } // DepartmentId
        /// <summary>
        /// 模块编号
        /// </summary>
        public int _ModuleId { get; set; } // ModuleId
    }
}
