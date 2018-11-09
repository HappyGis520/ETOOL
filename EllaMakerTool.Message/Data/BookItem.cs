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
	public class BookItem
	{
	    public string id { set; get ; }
	    protected  string isbn;
		protected  string name;
		protected  decimal price;
		private string publisherID;
		private string publisherName;
		private string authorID;
		private string authorName;
		private string bookSetID;
		private string bookSetName;
		private int eBookCount;
		protected  bool? enabled;
		protected  bool? visibled;

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
				return name;
			}
			set
			{
				name = value;
			}
		}
		public virtual string PublisherID
		{
			get
			{
				return publisherID;
			}
			set
			{
				publisherID = value;
			}
		}
		public virtual string PublisherName
		{
			get
			{
				return publisherName;
			}
			set
			{
				publisherName = value;
			}
		}
		public virtual string AuthorName
		{
			get
			{
				return authorName;
			}
			set
			{
				authorName = value;
			}
		}
		public virtual string BookSetName
		{
			get
			{
				return bookSetName;
			}
			set
			{
				bookSetName = value;
			}
		}
		public virtual int EBookCount
		{
			get
			{
				return eBookCount;
			}
			set
			{
				this.eBookCount = value;
			}
		}
		public virtual string AuthorID
		{
			get
			{
				return authorID;
			}
			set
			{
				authorID = value;
			}
		}

		public virtual string BookSetID
		{
			get
			{
				return bookSetID;
			}
			set
			{
				bookSetID = value;
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
				return price;
			}
			set
			{
				price = value;
			}
		}
	}

}