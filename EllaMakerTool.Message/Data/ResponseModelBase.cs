/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： ResponseModelBase.cs
 * * 功   能：  服务端应答消息
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 09:27:12
 * * 修改记录： 
 * * 日期时间： 2018-09-25 09:27:12  修改人：王建军  创建
 * *******************************************************************/
using System;

namespace EllaMakerTool.Message
{

	[Serializable]
    public class ResponseModelBase<T>
	{

	//    private  String version;
	//    public String getVersion() {
	//        return version;
	//    }
	//    public void setVersion(String version) {
	//        this.version = version;
	//    }

		protected internal string status = "0"; //状态（0或1）

		protected internal string code = "0x00000000"; // 错误码,成功则忽略该错误码

		protected internal string message = ""; //描述信息,一般是对错误的描述，与Code对应

		protected internal string remark = ""; //接口名

		private T data; //返回的对象
		public virtual string Code
		{
			get
			{
				return code;
			}
			set
			{
				this.code = value;
			}
		}


		public virtual string Status
		{
			get
			{
				return status;
			}
			set
			{
				this.status = value;
			}
		}


		public virtual string Message
		{
			get
			{
				return message;
			}
			set
			{
				this.message = value;
			}
		}


		public virtual string Remark
		{
			get
			{
				return remark;
			}
			set
			{
				this.remark = value;
			}
		}


		public virtual T Data
		{
			get
			{
				return data;
			}
			set
			{
				this.data = value;
			}
		}

        internal ResponseModelBase()
		{
		}

        internal ResponseModelBase(string code, string status, string message, string remark)
		{
			this.code = code;
			this.status = status;
			this.message = message;
			this.remark = remark;
		}
        internal ResponseModelBase(string code, string status, string message, T data, string remark)
		{
			this.code = code;
			this.status = status;
			this.message = message;
			this.remark = remark;
			this.data = data;
		}


        public bool Successful
        {
           get { return status.Equals("1"); }
        }






	}

}