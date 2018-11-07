using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    public class CompanyStoreStatusApiModel
    {
        public int? CompanyId { get; set; } // CompanyId
        public int? DocumentsSize { get; set; } // DocumentsSize
        public int? UsedDocumentsSize { get; set; } // UsedDocumentsSize
        public int? OtherSize { get; set; } // OtherSize
        public int? UsedOtherSize { get; set; } // UsedOtherSize
    }
}
