/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： RequestModelBase.cs
 * * 功   能：  请求消息基类
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 13:50:30
 * * 修改记录： 
 * * 日期时间： 2018-09-25 13:50:30  修改人：王建军  创建
 * *******************************************************************/
namespace  EllaMakerTool.Message
{
    internal class RequestModelBase
	{

		private string token;
		public virtual string Token
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}


	}

}