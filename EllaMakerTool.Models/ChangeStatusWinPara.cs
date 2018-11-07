using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EllaMakerTool.Message;
using EllaMakerTool.Models;

namespace EllaMakerTool.Models
{
    public class ChangeStatusWinPara
    {
        public bool IsFromTopBar { get; set; }
        public EnumDocStatusType newStatus { get; set; }
        public StatusList shareList { get; set; } = new StatusList();
        public StatusList syncList { get; set; } = new StatusList();
    }
}
