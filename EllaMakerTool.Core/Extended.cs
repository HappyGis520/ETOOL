using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool
{
    public static class Extended
    {
        /// <summary>
        /// "F9VU,F9VU" 转 [11,21]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<int> ToIntList(this string str)
        {
            return str.Split(',').Select(o => Convert.ToInt32(Utility.DecryptId(o))).ToList();

            return str.Split(',').Select(o => Convert.ToInt32(o)).ToList();
        }

        /// <summary>
        /// [ F9VU,F9VU ] 转 [11,21]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<int> DecToIntList(this List<string> str)
        {
            if (str==null)
                return new List<int> { };
            return str.Select(o => Convert.ToInt32(Utility.DecryptId(o))).ToList();
        }

        /// <summary>
        /// [11,21] 转 "F9VU,F9VU"
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> EncToList(this List<int> list)
        {
            return list.Select(o => Utility.EncryptId(o)).ToList();
        }


        /// <summary>
        /// 将公司ID号转为公司号
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public static string ToCompanyCode(this int CompanyId)
        {
            return Utility.EncryptId(Utility.ToCompanyCode(CompanyId));
        }

        /// <summary>
        /// 将公司号转为公司ID
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <returns></returns>
        public static int ToCompanyId(this string CompanyCode)
        {
            return Utility.FromCompanyCode(Utility.DecryptId(CompanyCode));
        }

        public static string ToXiaoYingCode(this int companyId)
        {
            return Utility.EncryptId(Utility.ToXiaoYingCode(companyId));
        }

        public static int FromXiaoYingCode(this string CompanyCode)
        {
            return Utility.FromXiaoYingCode(Utility.DecryptId(CompanyCode));
        }

        public static string EncryptId(this int? id)
        {
            return Utility.EncryptId(id);
        }

        public static int DecryptId(this string idCode)
        {
            return Utility.DecryptId(idCode);
        }


        public static int ToMinusOne(this int i)
        {
            if (i == 0)
                return -1;
            return i;
        }


        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy(object destination, object source)

        {

            if (destination == null || source == null)

            {

                return 0;

            }

            return Copy(destination, source, source.GetType());

        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy(object destination, object source, Type type)
        {

            return Copy(destination, source, type, null);

        }


        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source"></param>
        /// <param name="type">复制的属性字段模板</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy(object destination, object source, Type Sourcetype, List<string> excludeName)
        {

            if (destination == null || source == null)
            {
                return 0;
            }
            if (excludeName == null)
                excludeName = new List<string>();
            int i = 0;
            Type desType = destination.GetType();

            foreach (FieldInfo mi in Sourcetype.GetFields())
            {
                #region 设置字段
                if (excludeName.Contains(mi.Name))

                {
                    continue;
                }


                try

                {
                    FieldInfo des = desType.GetField(mi.Name);

                    if (des != null && des.FieldType == mi.FieldType)

                    {

                        des.SetValue(destination, mi.GetValue(source));

                        i++;

                    }
                }
                catch
                {

                } 
                #endregion
            }

            foreach (PropertyInfo pi in Sourcetype.GetProperties())
            {

                #region 属性设置
                if (excludeName.Contains(pi.Name))

                {

                    continue;

                }

                try

                {

                    PropertyInfo des = desType.GetProperty(pi.Name);

                    if (des != null && des.PropertyType == pi.PropertyType && des.CanWrite && pi.CanRead)

                    {
                        des.SetValue(destination, pi.GetValue(source, null), null);
                        i++;
                    }

                }
                catch
                {
                    //throw ex;

                } 
                #endregion

            }

            return i;

        }
    }
}

    
