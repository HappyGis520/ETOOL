/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： ListByPageParam.cs
 * * 功   能：  分页获取对象集合信息
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 13:46:38
 * * 修改记录： 
 * * 日期时间： 2018-09-25 13:46:38  修改人：王建军  创建
 * *******************************************************************/
namespace EllaMakerTool.Message
{
    /// <summary>
    /// 分页获取对象集合信息
    /// </summary>
    public class ListByPageParam:RequestModelBase
	{
        /// <summary>
        /// 每页记录数量
        /// </summary>
        protected internal int pageSize;
        /// <summary>
        /// 加码页码
        /// </summary>
        protected internal int pageIndex;
        /// <summary>
        /// 起始记录数，数值=pageIndex*pageSize
        /// </summary>
        protected internal int startIndex;
		public  int PageSize
		{
			get
			{
				return pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}
		public  int PageIndex
		{
			get
			{
				return pageIndex;
			}
			set
			{
				this.pageIndex = value;
			}
		}
		public  int StartIndex
		{
			get
			{
				startIndex = pageIndex * pageSize;
				return startIndex;
			}
		}
	}

}