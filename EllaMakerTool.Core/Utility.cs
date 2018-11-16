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
        public static readonly Regex RegexMobile = new Regex("1[\\d]{10}", RegexOptions.Compiled);
        public static readonly Regex RegexEmail = new Regex(@"[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})", RegexOptions.Compiled);
        public const int CardSeed = 1000000;
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