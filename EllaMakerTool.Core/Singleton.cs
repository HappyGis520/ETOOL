/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： Singleton.cs
 * * 功   能：  单一实例模板
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016-06-15 13:55:47
 * * 修改记录： 
 * * 日期时间： 2016-06-15 13:55:47  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Core
{

        /// <summary>
        /// 单一实例模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Singleton<T> where T : new()
        {
            protected Singleton()
            {
                if (Instance != null)
                {
                    throw (new Exception("单例模式，请用class.Instance方式\""));
                }
            }
            public static T Instance
            {
                get
                {
                    return SingletonCreator.instance;
                }
            }
            class SingletonCreator
            {
                static SingletonCreator()
                {
                }
                internal static readonly T instance = new T();
            }
        }
}
