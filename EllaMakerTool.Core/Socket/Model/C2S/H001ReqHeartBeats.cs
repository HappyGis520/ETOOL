using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool.SocketModel.C2S
{
    public class H001ReqHeartBeats
    {
        public DateTime BeaTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 在线设备数
        /// </summary>
        public int OnlineMacNum { get; set; }
    }
}
