using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EllaMakerTool.API;
using EllaMakerTool.Message;
using EllaMakerTool.Message.Data;

namespace EllaMakerTool.Api
{
    internal class LoginApIs:APIBase
    {


        #region 路由常量
        public const string Login_UserLogin = "UserLogin";
        #endregion


        public LoginApIs(string ip, int Port) : base(ip, Port)
        {
        }

        public ResponseModelBase<LoginUser> UserLogin(string UserName, String Password)
        {

            WebApiUtil.Url = $"{_ServiceIP}:{_ServicePort}";
            var obj = WebApiUtil.PostAPI<ResponseModelBase<LoginUser>>(Login_UserLogin, "");
            return obj;

        }
    }
}