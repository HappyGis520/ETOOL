using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace EllaMakerTool
{
    public static class WebApiUtil
    {
        /// <summary>
        /// WEBAPI请求地址
        /// </summary>
        public static string Url { get; set; }

        private static string GetFullUrl(string apiRoute)
        {
            if (string.IsNullOrEmpty(Url))
            {
                throw new Exception("没有定义Api根地址");
            }
            return $"{Url}/{apiRoute}";
        }

        /// <summary>
        /// 调用GET API
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetAPI<T>(string apiRoute) where T : class
        {
            System.Net.HttpWebRequest request =
                System.Net.WebRequest.Create(GetFullUrl(apiRoute)) as System.Net.HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            System.Net.HttpWebResponse result = request.GetResponse() as System.Net.HttpWebResponse;
            System.IO.StreamReader sr = new System.IO.StreamReader(result.GetResponseStream(), System.Text.Encoding.UTF8);
            string strResult = sr.ReadToEnd();
            var res = JsonConvert.DeserializeObject<T>(strResult);
            sr.Close();
            //Console.WriteLine(strResult);
            return res;
        }

        /// <summary>
        /// 调用GET API
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetAPI(string apiRoute)
        {
            System.Net.HttpWebRequest request =
                System.Net.WebRequest.Create(GetFullUrl(apiRoute)) as System.Net.HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            System.Net.HttpWebResponse result = request.GetResponse() as System.Net.HttpWebResponse;
            System.IO.StreamReader sr = new System.IO.StreamReader(result.GetResponseStream(), System.Text.Encoding.UTF8);
            string strResult = sr.ReadToEnd();
            sr.Close();
            //Console.WriteLine(strResult);
            return strResult;
        }

        private static readonly string DefaultUserAgent =
            "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        /// <summary>
        /// 调用POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">JSON数据</param>
        /// <returns></returns>
        public static T PostAPI<T>(string apiRoute, object param) where T : class
        {

                string strURL = GetFullUrl(apiRoute);
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                request.UserAgent = DefaultUserAgent;
                var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                string paraUrlCoded = JsonConvert.SerializeObject(param);
            
                byte[] payload;
                payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
                request.ContentLength = payload.Length;
                System.IO.Stream writer = request.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();
                System.Net.HttpWebResponse response;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.Stream s;
                s = response.GetResponseStream();
                string StrDate = "";
                string strValue = "";
                System.IO.StreamReader Reader = new System.IO.StreamReader(s, System.Text.Encoding.UTF8);
                while ((StrDate = Reader.ReadLine()) != null)
                {
                    strValue += StrDate;
                }
                var res = JsonConvert.DeserializeObject<T>(strValue,jsonSetting);
                Reader.Close();
                return res;

        }

        /// <summary>
        /// 调用POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">JSON数据</param>
        /// <returns></returns>
        public static string PostAPI(string apiRoute, object param)
        {
            string strURL = GetFullUrl(apiRoute);
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            request.UserAgent = DefaultUserAgent;
            string paraUrlCoded = JsonConvert.SerializeObject(param);
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            System.IO.Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            System.IO.StreamReader Reader = new System.IO.StreamReader(s, System.Text.Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate;
            }
            Reader.Close();
            return strValue;
        }
    }
}
