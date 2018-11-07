/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： FTPFileInfoListParam.cs
 * * 功   能：  获取文件或文件夹参数
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-09-25 13:41:01
 * * 修改记录： 
 * * 日期时间： 2018-09-25 13:41:01  修改人：王建军  创建
 * *******************************************************************/

using System;

namespace  EllaMakerTool.Message
{

        /// <summary>
        /// 获取文件或文件夹参数
        /// </summary>
        internal class ListFTPRootParam : RequestModelBase
        {
            /// <summary>
            /// 是否为动画书
            /// </summary>
             public bool book { get;  }
            /// <summary>
            /// 动画书或图书编号
            /// </summary>
            public string bookID { get;  }

            internal ListFTPRootParam(bool isBook,String BookID)
            {
                this.book = isBook;
                this.bookID = bookID;
            }
        }

}
