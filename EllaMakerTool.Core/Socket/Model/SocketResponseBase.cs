using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool.SocketModel
{
    public class SocketResponseBase
    {
        /// <summary>
        /// 操作码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public string retData { get; set; }

        public void setRetData(object obj)
        {
            retData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public T GetData<T>() where T : class
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(retData);
        }
    }
}
