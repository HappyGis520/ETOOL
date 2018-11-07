using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Api
{
    /// <summary>
    /// API返回值基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResult
    {

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
        public static ApiResult<T> Error(string message)
        {
            return new ApiResult<T>() { Message = message, Code = 1 };
        }
        public static ApiResult<T> Error(int code, string message)
        {
            return new ApiResult<T>() { Message = message, Code = code };
        }
    }
    public class ErrorApiResult : ApiResult
    {
        public ErrorApiResult(string message)
        {
            Message = message;
            Code = 1;
        }
    }
    public class PaginateApiResult<T> : ApiResult<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

    }

    public class SucessApiResult : ApiResult
    {
        public SucessApiResult(string message = "")
        {
            Code = 0;
        }
    }

    public abstract class ApiResult
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
        /// 发生错误
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ApiResult Error(string message)
        {
            return new ErrorApiResult(message);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ApiResult<T> Success<T>(T data)
        {
            return new ApiResult<T>()
            {
                Code = 0,
                Message = null,
                Data = data
            };
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">内容</param>
        /// <param name="message">错误信息</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ApiResult<T> Error<T>(T data, string message)
        {
            return new ApiResult<T>()
            {
                Code = 1,
                Message = message,
                Data = data
            };
        }

        public static ApiResult<string> Error<T>(string v)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 是否为成功的执行结果，成功为True,即code =0
        /// </summary>
        public bool successful
        {
            get { return Code == 0; }
        }
    }
}
