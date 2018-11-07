using System;
using EllaMakerTool.Message;

namespace EllaMakerTool.API
{
    internal class BookAPIs:APIBase
    {

        #region 路由常量
        public const string BOOK = "Book";
        public const string BOOK_AllBook = "AllBooksByPage";

        internal BookAPIs(String ServiceIP,int ServicePort):base(ServiceIP,ServicePort)
        {
        }
        #endregion  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public ResponseModelBase<BookListByPage> AllBookListByPage( BookListByPageParam param)
        {

                WebApiUtil.Url = $"{_ServiceIP}:{_ServicePort}/{BOOK}";
                var obj = WebApiUtil.PostAPI<ResponseModelBase<BookListByPage>>(BOOK_AllBook, param);
                return obj;

        }




    }
}
