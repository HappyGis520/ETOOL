using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data;
using System.Collections;
using Spire.Xls;
using System.Web;

namespace EllaMakerTool
{
    public static class Utility
    {

        public static bool isNumberic(string message, out int result)
        {
            Regex rex =
                new Regex(@"^\d+$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = int.Parse(message);
                return true;
            }
            else
                return false;
        }
        public static string ToCamelStyle(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            else
            {
                return $"{Char.ToLower(str[0])}{str.Substring(1, str.Length - 1)}";
            }

        }

        /// <summary>
        /// 将API请求中带有#$%等特殊字符的参数转换为全角防止参数中符号被误分析为特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToApiSafeStr(this string str)
        {
            return str.Replace("+", Utility.ToSBC("+"))
                .Replace("%", Utility.ToSBC("%")).Replace("&", Utility.ToSBC("&")).Replace("#", Utility.ToSBC("#"));
        }

        public static String ToSBC(String input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public static string GetSixCode()
        {
            var guid = Guid.NewGuid().ToByteArray();
            var arr = guid.Take(6).Select(c => c % 10);
            return string.Join("", arr);
        }
        #region 用户名打码


        /// <summary>
        /// 用户名打码
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string Mosaic(string username)
        {
            return PrivateMosaic(username ?? "");
        }
        public static string Mosaic(string nickName, string username)
        {
            return PrivateMosaic((nickName ?? username) ?? "");
        }
        public static readonly Regex RegexMobile = new Regex("1[\\d]{10}", RegexOptions.Compiled);
        public static readonly Regex RegexEmail = new Regex(@"[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})", RegexOptions.Compiled);

        private static string PrivateMosaic(string str)
        {
            var match = RegexMobile.Match(str);
            if (match.Success)
            {
                return MarkPhone(str);
            }
            var email = RegexEmail.Match(str);
            if (email.Success)
            {
                return MarkEmail(str);
            }
            return str;
        }

        private static string MarkEmail(string str)
        {
            var arr = str.Split('@');
            if (arr.Length != 2) { return str; }
            return $"{(arr[0].Length <= 3 ? "***" : Strings.Left(str, 3).PadRight(arr[0].Length, '*'))}@{arr[1]}";
        }

        private static string MarkPhone(string str)
        {
            return $"{Strings.Left(str, 3)}*****{Strings.Right(str, 3)}";
        }

        #endregion

        public const int CardSeed = 1000000;

        #region 62进制ID编码

        public static string EncryptId(int? id)
        {
            if (!id.HasValue || id.Value == -1)
                return string.Empty;
            //使用62进制加种子
            Number62 int62 = id + CardSeed;
            return int62.ToString();
        }

        public static string EncryptId2(int id)
        {
            Number62 int62 = id + CardSeed;
            return int62.ToString();
        }

        public static List<string> EncryptIdList(List<int> list)
        {
            List<string> list2 = new List<string>();
            if (list.Count > 0)
            {
                foreach (int? id in list)
                {
                    if (!id.HasValue || id.Value == -1)
                        continue;
                    //使用62进制加种子
                    Number62 int62 = id + CardSeed;
                    list2.Add(int62.ToString());
                }
            }
            return list2;
        }

        public static List<int> DecryptIdList(this List<string> strlist)
        {

            List<int> result = new List<int>();
            strlist.ForEach(fo =>
            {
                result.Add(string.IsNullOrEmpty(fo) ? -1 : Utility.DecryptId(fo));
            });
            return result;
        }


        public static int DecryptId(string idCode)
        {
            return _DecryptId(idCode, -1);

        }

        public static int DecryptId0(string idCode)
        {
            return _DecryptId(idCode, 0);

        }

        private static int _DecryptId(string idCode, int defaultVal)
        {
            try
            {
                if (string.IsNullOrEmpty(idCode))
                    return defaultVal;
                Number62 int62 = idCode;
                return int62.ToInt32() - CardSeed;
            }
            catch
            {
                return 0;
            }

        }

        #endregion

        #region 小赢号 Id 处理

        public static int FromXiaoYingCode(int xiaoYingCode)
        {
            return xiaoYingCode - 10000;
        }

        public static int ToXiaoYingCode(int id)
        {
            return id + 10000;
        }
        #endregion

        #region 公司号 Id 处理

        public static int FromCompanyCode(int CompanyCode)
        {
            if (CompanyCode == -1)
                return -1;
            return CompanyCode - 10000;
        }

        public static int ToCompanyCode(int id)
        {
            if (id == -1)
                return -1;
            return id + 10000;
        }
        #endregion

        #region 友好时间表示

        public static string FriendlyTime(DateTime visitTime)
        {
            return Ago(visitTime);
        }

        private static string Ago(DateTime target)
        {
            var result = new StringBuilder();
            TimeSpan diff = (DateTime.Now - target);
            if (diff.Days > 365)
            {

                result.AppendFormat("{0} 年", diff.Days / 365);
            }
            else if (diff.Days > 30)
            {

                result.AppendFormat("{0} 个月", diff.Days / 30);
            }
            else if (diff.Days > 0)
            {
                result.AppendFormat("{0} 天", diff.Days);
            }
            else if (diff.Hours > 0)
            {
                result.AppendFormat("{0} 小时", diff.Hours);
            }
            else if (diff.Minutes > 0)
            {
                result.AppendFormat("{0} 分钟", diff.Minutes);
            }
            result.Append(result.Length == 0 ? "刚刚" : "前");

            return result.ToString();
        }

        public static string FromBirthdayDate(this DateTime Birthday)
        {
            if (Birthday == System.Data.SqlTypes.SqlDateTime.MinValue.Value)
                return string.Empty;
            return Birthday.ToString("yyyy-MM-dd");

        }
        public static DateTime ToBirthdayDate(this string Birthday)
        {
            DateTime _Birthday = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            if (string.IsNullOrEmpty(Birthday))
                return _Birthday;
            DateTime.TryParse(Birthday, out _Birthday);
            return _Birthday;

        }

        public static string DateFormat(DateTime? time)
        {
            var str = time == null ? "" : time.Value.ToString("yyyy-MM-dd HH:mm:ss");
            return str;
        }

        public static string DateFormat1(DateTime? time)
        {
            var str = time == null ? "" : time.Value.ToString("yyyy-MM-dd");
            return str;
        }

        public static string DateFormat2(DateTime? time)
        {
            var str = time == null ? "" : time.Value.ToString("t");
            return str;
        }
        #endregion

        #region 时间戳
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToLong(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToLong1(System.DateTime time)
        {
            DateTime today = DateTime.Now; //.AddDays(-1);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(today.Year, today.Month, today.Day, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        ///// <summary>        
        ///// 时间戳转为C#格式时间        
        ///// </summary>        
        ///// <param name=”timeStamp”></param>        
        ///// <returns></returns>        
        //public static DateTime ConvertStringToDateTime(string timeStamp)
        //{
        //    DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //    long lTime = long.Parse(timeStamp + "0000");
        //    TimeSpan toNow = new TimeSpan(lTime);
        //    return dtStart.Add(toNow);
        //}
        #endregion

        #region 申请编号
        /// <summary>
        /// 申请编号
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ApplySerialNumber()
        {
            return string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmssffff"));
        }
        /// <summary>
        /// 申请编号年月日
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ApplySerialNumber1()
        {
            return string.Format("{0}", DateTime.Now.ToString("yyyyMMdd"));
        }

        #endregion
        #region wjj
        /// <summary>
        /// 枚举转字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EnumToString(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                   (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        /// 此类必须标注可序列化属性[Serializable]
        /// </summary>
        public static class DeepCopy
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public static object DeepCopyObject(object obj)
            {
                //浅复制
                //this.MemberwiseClone();

                var ms = new MemoryStream();
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, 0);
                var value = bf.Deserialize(ms);
                ms.Close();
                return value;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="obj"></param>
            /// <returns></returns>
            public static T DeepCopyObject<T>(T obj)
            {
                using (var ms = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, obj);
                    ms.Position = 0;
                    return (T)formatter.Deserialize(ms);
                }
            }

        }

        public static int TimespaneMinutes(DateTime StartTime, DateTime EndTime)
        {
            return (int)Math.Round(((EndTime - StartTime).TotalMinutes), MidpointRounding.AwayFromZero);
        }
        #endregion


        public static string JoinToStr(this List<int> list)
        {
            return string.Join(",", list.ToArray());
        }

        #region 自定义

        /// <summary>
        /// 获取会议时长（秒数转：30分钟、1.5小时）
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string VideoMGetDur(long duration)
        {
            var res = "";
            var dur = (double)duration / 3600;
            if (duration >= 3600) //大于等于一小时
            {
                if (dur.ToString().Contains('.'))
                {
                    res = dur.ToString().Substring(0, (dur.ToString().IndexOf('.') + 2)) + "小时";
                }
                else
                {
                    res = dur + "小时";
                }
            }
            else                   //小雨一小时
            {
                if ((dur * 60).ToString().Contains('.'))
                {
                    res = (dur * 60).ToString().Substring(0, ((dur * 60).ToString().IndexOf('.') + 2)) + "分钟";
                }
                else
                {
                    res = (dur * 60) + "分钟";
                }
            }
            return res;
        }

        #endregion

        #region json

        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Any())
            {

                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="strColumns"></param>
        /// <param name="tb"></param>
        /// <param name="sheetName"></param>
        public static void Export(DataTable tb, string sheetName, string strColumns)
        {
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            sheet.InsertDataTable(tb, true, 1, 1);
            var dataRow = sheet.Rows[0];
            string[] strArry = strColumns.Split(',');
            for (int i = 0; i < strArry.Length; i++)
            {
                dataRow.Cells[i].Value = strArry[i];
            }
            var resp = HttpContext.Current.Response;
            workbook.SaveToHttpResponse(sheetName + DateTime.Now.Ticks + ".xls", resp);//设置文件名
            resp.Flush();
            resp.End();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="sheetName"></param>
        public static void Export1(DataTable tb, string sheetName)
        {
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            sheet.InsertDataTable(tb, true, 1, 1);
            var resp = HttpContext.Current.Response;
            workbook.SaveToHttpResponse(sheetName + DateTime.Now.Ticks + ".xls", resp);//设置文件名
            resp.Flush();
            resp.End();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">数据源</param>
        /// <param name="title">列名</param>
        /// <param name="fileName">文件名</param>
        public static void Except<T>(IEnumerable<T> collection, string title, string fileName)
        {
            var tb = Utility.ToDataTable(collection);

            Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
            Spire.Xls.Worksheet sheet = workbook.Worksheets[0];
            sheet.InsertDataTable(tb, true, 1, 1);

            var dataRow = sheet.Rows[0];
            var strColumns = title;
            string[] strArry = strColumns.Split(',');
            for (int i = 0; i < strArry.Length; i++)
            {
                dataRow.Cells[i].Value = strArry[i];
            }

            var resp = System.Web.HttpContext.Current.Response;
            workbook.SaveToHttpResponse(fileName + ".xls", resp);

            resp.Flush();
            resp.End();
        }

        /// <summary>
        /// DateTime 转 时间戳
        /// </summary>
        /// <param name="theDate"></param>
        /// <returns></returns>
        public static string GetMilliseconds(DateTime theDate)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            var intResult = (theDate - startTime).TotalMilliseconds;
            return Math.Round(intResult, 0).ToString();
        }

        /// <summary>  
        /// 计算文件大小函数(保留两位小数),Size为字节大小  
        /// </summary>  
        /// <param name="Size">初始文件大小</param>  
        /// <returns></returns>  
        public static string CountSize(long Size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " Byte";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " M";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
            return m_strSize;
        }

        public static string ReplaceInviadCharInFileName(string newname)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                newname.Replace(invalidChar.ToString(), string.Empty);
            }
            return newname;
        }
    }
}