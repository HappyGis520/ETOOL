using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Models
{
    public class DelConfirmWinParaModel
    {
        /// <summary>
        /// 文件夹ids，
        /// </summary>
        public List<string> folderIds { get; set; } = new List<string>();

        /// <summary>
        /// 文件ids
        /// </summary>
        public List<string> fileIds { get; set; } = new List<string>();
    }
}
