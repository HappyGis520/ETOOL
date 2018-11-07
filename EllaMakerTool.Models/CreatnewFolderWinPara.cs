using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EllaMakerTool.Message;

namespace EllaMakerTool.Models
{
    public class CreatnewFolderWinPara
    {
        public EnumDocStatusType newStatus { get; set; }
        public StatusList shareList { get; set; } = new StatusList();
        public StatusList syncList { get; set; } = new StatusList();

        public string newName { get; set; }
    }
}
