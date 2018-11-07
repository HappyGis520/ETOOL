using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 操作执行结果
    /// </summary>
    /// <typeparam name="T">成功后返回的对象类型</typeparam>
    public class TheResult<T> : TheResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public TheResult() { }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SucessTheResult : TheResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SucessTheResult(string message = "")
        {
            Code = 0;
        }
    }
    /// <summary>
    /// 结果
    /// </summary>
    public abstract class TheResult
    {
        /// <summary>
        /// 0成功1失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 0成功-非0失败
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static TheResult<T> Success<T>(T data)
        {
            return new TheResult<T>()
            {
                Code = 0,
                Message = "",
                Successful = true,
                Data = data
            };
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data">内容</param>
        /// <param name="message">错误信息</param>
        /// <param name="code"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static TheResult<T> Error<T>(T data, string message, int code = 1)
        {
            return new TheResult<T>()
            {
                Code = code,
                Message = message,
                Successful = false,
                Data = data
            };
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginateTheResult<T> : TheResult<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

    }
}
