using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllaMakerTool.SocketModel.S2C
{
    public class A003ReqAddNewEmp
    {
        /// <summary>
        /// A003员工信息同步
        /// </summary>
        public List<AddNewItem> list { get; set; }
    }

    public class AddNewItem
    {
        public int EmpNo { get; set; }
        public string EmpName { get; set; }
    }
}
