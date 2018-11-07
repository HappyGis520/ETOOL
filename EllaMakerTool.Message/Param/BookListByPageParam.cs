/*******************************************************************
* * 版权所有： 郑州点读科技杭州研发中心
* * 文件名称： BookListByPageParam.cs
* * 功   能：  分页获取图书列表的请求参数
* * 作   者： 王建军
* * 编程语言： C# 
* * 电子邮箱： 595303122@qq.com
* * 创建日期： 2018-09-25 13:45:53
* * 修改记录： 
* * 日期时间： 2018-09-25 13:45:53  修改人：王建军  创建
* *******************************************************************/
namespace EllaMakerTool.Message
{
    //分页获取图书列表的请求参数
    internal class BookListByPageParam : ListByPageParam
	{
        /// <summary>
        /// 作者搜索关键字
        /// </summary>
        private string SearchAuthorName_Renamed;
        /// <summary>
        /// 出版社搜索关键字
        /// </summary>
        private string SearchPublisherName_Renamed;
        /// <summary>
        /// 书集搜索关键字
        /// </summary>
        private string SearchBookSetName_Renamed;
		public virtual string SearchAuthorName
		{
			get
			{
				return SearchAuthorName_Renamed;
			}
			set
			{
				SearchAuthorName_Renamed = value;
			}
		}
		public virtual string SearchPublisherName
		{
			get
			{
				return SearchPublisherName_Renamed;
			}
			set
			{
				SearchPublisherName_Renamed = value;
			}
		}

		public virtual string SearchBookSetName
		{
			get
			{
				return SearchBookSetName_Renamed;
			}
			set
			{
				SearchBookSetName_Renamed = value;
			}
		}

	}

}