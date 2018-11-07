using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 员工角色类别
    /// </summary>
    public enum EnumEmployeeRoleType
    {
        /// <summary>
        /// 创建者
        /// </summary>
        Creator = 128,
        /// <summary>
        /// 超级管理员：具备“权限管理权限”
        /// </summary>
        Admin = 96,
        /// <summary>
        /// 管理员
        /// </summary>
        Manager = 64,
        /// <summary>
        /// 普通成员
        /// </summary>
        Member = 2,

    }
}
