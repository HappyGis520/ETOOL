/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： BookListByPage.cs
 * * 功   能：  分页获取的图书列表
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 13:48:01
 * * 修改记录： 
 * * 日期时间： 2018-09-25 13:48:01  修改人：王建军  创建
 * *******************************************************************/
using System.Collections.Generic;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 分页获取的图书列表
    /// </summary>
    public class EBookListByPage 
	{

	    public IList<BookItem> Items { get; set; }
	}

}