using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 文件状态类型
    /// </summary>
    public enum EnumDocStatusType
    {
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        Department = 1,
        /// <summary>
        /// 共享
        /// </summary>
        [Description("共享")]
        Share = 2,
        /// <summary>
        /// 个人
        /// </summary>
        [Description("个人")]
        Personal = 3
    }

}
