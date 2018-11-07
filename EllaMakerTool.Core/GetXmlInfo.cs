using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GTD.Core
{
    public static class GetXmlInfo
    {
        /// <summary>
        /// 获取xml配置信息
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        public static List<string> GetNodesList(string modelName)
        {
            XElement xmldoc = XElement.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"\Condition.xml");
            //获取审批的节点
            var node = from a in xmldoc.Descendants("model")
                       where a.Attribute("name").Value == modelName
                       select a;
            //获取nodeName下的node节点集合
            var list = from a in node.Descendants("nodes")
                       select a.Attribute("name").Value;
            if (list.Count() > 0)
            {
                return list.ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取xml配置信息
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        public static List<string> GetNodeList(string nodeName)
        {
            XElement xmldoc = XElement.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"\Condition.xml");
            //获取审批的节点
            var node = from a in xmldoc.Descendants("nodes")
                       where a.Attribute("name").Value == nodeName
                       select a;
            //获取nodeName下的node节点集合
            var list = from a in node.Descendants("node")
                       select a.Attribute("name").Value;
            if (list.Count() > 0)
            {
                return list.ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取首页默认模块
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        public static Object GetXmlDic(string modelName, string nodeName)
        {
            XElement xmldoc = XElement.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"\Condition.xml");
            var nodes = from a in xmldoc.Descendants("model")
                        where a.Attribute("name").Value == modelName
                        select a;
            //获取nodeName的节点
            var node = from a in nodes.Descendants("nodes")
                       where a.Attribute("name").Value == nodeName
                       select a;
            //获取nodeName下的node节点集合
            var list = from a in node.Descendants("node")
                       select new { Id = Convert.ToInt32(a.Attribute("id").Value), Name = a.Attribute("name").Value };
            if (list.Count() > 0)
            {
                return list.ToList();
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public class KeyValue
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }
        }
    }
}
