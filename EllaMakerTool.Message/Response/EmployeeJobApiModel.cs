using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 雇员岗位信息
    /// </summary>
    public class EmployeeJobApiModel
    {
        /// <summary>
        /// 所在部门编号
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 所在部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 职位编号
        /// </summary>
        public string CompanyJobId { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 是否为部门领导人
        /// </summary>
        public bool IsJobLeader { get; set; }
        /// <summary>
        /// 是否为主要工作，兼职工作则为false
        /// </summary>
        public bool IsMastJob { get; set; }
        /// <summary>
        /// 领导人编号
        /// </summary>
        public string LeaderId { get; set; }
        /// <summary>
        /// 领导人部门编号
        /// </summary>
        public string LeaderDepartmentId { get; set; }
        /// <summary>
        /// 领导人名称
        /// </summary>
        public string LeaderName { get; set; }
        /// <summary>
        /// 领导人头像
        /// </summary>
        public string LeaderFaceUrl { get; set; } // FaceUrl
    }
}
