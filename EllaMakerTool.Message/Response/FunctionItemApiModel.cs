using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    ///　功能描述信息
    /// </summary>
    public class FunctionItemApiModel
    {
        /// <summary>
        /// 公司领导人编号
        /// </summary>
        public string CompanyLeaderID { get; set; }
        /// <summary>
        /// 主功能模块编号，空白表示该模块为主功能模块
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 功能模块编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string FormatUrl { get; set; }
        /// <summary>
        /// 字体图标
        /// </summary>
        public string FontIcon { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        public string RoteUrl { get; set; }
        /// <summary>
        /// 是否启用（有权限访问）
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<FunctionPermissionApiModel> FuncList { get; set; }

    }
}
