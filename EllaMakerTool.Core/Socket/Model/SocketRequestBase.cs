using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool.SocketModel
{
    public class SocketRequestBase
    {
        /// <summary>
        /// 操作码四位
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 请求键值对
        /// </summary>
        public string content
        {
            get;
            set;
        }

        public void SetContent(object obj)
        {
            content = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public T GetData<T>() where T : class
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
        }
    }
}
