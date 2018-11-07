using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 用户账户基本信息
    /// </summary>
    public class AuthToken
    {
        /// <summary>
        /// 凭具
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 账户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 用户 信息
        /// </summary>
        public ProfileApiModel Profile { get; set; }


    }
}
