using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 雇主信息
    /// </summary>
    public class EmployerApiModel
    {
        /// <summary>
        /// 父公司号
        /// </summary>
        public string ParentCompanyCode { get; set; }
        /// <summary>
        /// 公司号
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司职级
        /// </summary>
        public byte CompanyRanks { get; set; }
        /// <summary>
        /// 工作岗位
        /// </summary>
        public List<EmployeeJobApiModel> Jobs { get; set; }
        /// <summary>
        /// 角色:创建者　
        /// </summary>
        public EnumEmployeeRoleType Role { get; set; }
        /// <summary>
        /// 是否为最后登录公司
        /// </summary>
        public bool LastEnter { get; set; }
        /// <summary>
        /// 公司LOG地址
        /// </summary>
        public string LOGFormatUrl { get; set; }
        /// <summary>
        /// 最高领导人账户编号
        /// </summary>
        public string AdminProfileId { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string EmpNo { get; set; }
    }
}
