using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    public class DocBaseInfoApiModel
    {
        public string sourceId { get; set; }
        public string sourceName { get; set; }
        public EnumDocType type { get; set; }
    }
}
