using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Models
{
    public class RenameWinParaModel
    {
        public bool IsFolder { get; set; }
        public string FullName { get; set; }
        public string NewName { get; set; }
        public string CatalogId { get; set; }
    }
}
