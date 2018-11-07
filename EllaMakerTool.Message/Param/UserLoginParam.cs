/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： UserLoginParam.cs
 * * 功   能：  用户登录请求参数
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-11-01 15:27:10
 * * 修改记录： 
 * * 日期时间： 2018-11-01 15:27:10  修改人：王建军  创建
 * *******************************************************************/
namespace EllaMakerTool.Message.Param
{
    /// <summary>
    /// 用户登录请求参数
    /// </summary>
    public class UserLoginParam
    {

        public string Password { get; }
        public string UserName { get; }

        public UserLoginParam(string UserName, string Password)
        {
            this.Password = Password;
            this.UserName = UserName;
        }
    }

}
