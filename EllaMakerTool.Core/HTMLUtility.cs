/*******************************************************************
 * * 版权所有： 杭州齐集科技有限公司
 * * 文件名称： HTMLUtility.cs
 * * 功   能：  HTML操作相关类
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016-08-30 15:38:12
 * * 修改记录： 
 * * 日期时间： 2016-08-30 15:38:12  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EllaMakerTool
{
    public  class HTMLUtility
    {
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="strHtml">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string StripHTML(string strHtml)
        {
            string[] aryReg ={
          @"<script[^>]*?>.*?</script>",

          @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
          @"([\r\n])[\s]+",
          @"&(quot|#34);",
          @"&(amp|#38);",
          @"&(lt|#60);",
          @"&(gt|#62);",
          @"&(nbsp|#160);",
          @"&(iexcl|#161);",
          @"&(cent|#162);",
          @"&(pound|#163);",
          @"&(copy|#169);",
          @"&#(\d+);",
          @"-->",
          @"<!--.*\n"

         };

            string[] aryRep = {
           "",
           "",
           "",
           "\"",
           "&",
           "<",
           ">",
           " ",
           "\xa1",//chr(161),
           "\xa2",//chr(162),
           "\xa3",//chr(163),
           "\xa9",//chr(169),
           "",
           "\r\n",
           ""
          };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }


        #region   取出文本中的图片地址
        ///   <summary>
        ///   取出文本中的图片地址
        ///   </summary>
        ///   <param   name="HTMLStr">HTMLStr</param>
        public static string GetImgUrl(string HTMLStr)
        {
            string str = string.Empty;
            Regex r = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>",
             RegexOptions.Compiled);
            Match m = r.Match(HTMLStr.ToLower());
            if (m.Success)
                str = m.Result("${url}");
            return str;
        }
        #endregion
    }
}
