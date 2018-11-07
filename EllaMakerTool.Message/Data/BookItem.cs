/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： BookItem.cs
 * * 功   能：  原版书信息描述对象，关联多张表数据的对象
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 13:47:35
 * * 修改记录： 
 * * 日期时间： 2018-09-25 13:47:35  修改人：王建军  创建
 * *******************************************************************/
namespace EllaMakerTool.Message
{
    /// <summary>
    /// 原版书信息描述对象，关联多张表数据的对象
    /// </summary>
	internal class BookItem
	{
	    public string id { set; get ; }
	    protected internal string isbn;
		protected internal string Name_Renamed;
		protected internal decimal Price_Renamed;
		private string PublisherID_Renamed;
		private string PublisherName_Renamed;
		private string AuthorID_Renamed;
		private string AuthorName_Renamed;
		private string BookSetID_Renamed;
		private string BookSetName_Renamed;
		private int EBookCount_Renamed;
		protected internal bool? enabled;
		protected internal bool? visibled;

		public virtual string Isbn
		{
			get
			{
				return isbn;
			}
			set
			{
				this.isbn = string.ReferenceEquals(value, null) ? null : value.Trim();
			}
		}
		public virtual string Name
		{
			get
			{
				return Name_Renamed;
			}
			set
			{
				Name_Renamed = value;
			}
		}
		public virtual string PublisherID
		{
			get
			{
				return PublisherID_Renamed;
			}
			set
			{
				PublisherID_Renamed = value;
			}
		}
		public virtual string PublisherName
		{
			get
			{
				return PublisherName_Renamed;
			}
			set
			{
				PublisherName_Renamed = value;
			}
		}
		public virtual string AuthorName
		{
			get
			{
				return AuthorName_Renamed;
			}
			set
			{
				AuthorName_Renamed = value;
			}
		}
		public virtual string BookSetName
		{
			get
			{
				return BookSetName_Renamed;
			}
			set
			{
				BookSetName_Renamed = value;
			}
		}
		public virtual int EBookCount
		{
			get
			{
				return EBookCount_Renamed;
			}
			set
			{
				this.EBookCount_Renamed = value;
			}
		}
		public virtual string AuthorID
		{
			get
			{
				return AuthorID_Renamed;
			}
			set
			{
				AuthorID_Renamed = value;
			}
		}

		public virtual string BookSetID
		{
			get
			{
				return BookSetID_Renamed;
			}
			set
			{
				BookSetID_Renamed = value;
			}
		}

		public virtual bool? Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				this.enabled = value;
			}
		}
		public virtual bool? getvisibled()
		{
			return visibled;
		}
		public virtual decimal Price
		{
			get
			{
				return Price_Renamed;
			}
			set
			{
				Price_Renamed = value;
			}
		}
	}

}