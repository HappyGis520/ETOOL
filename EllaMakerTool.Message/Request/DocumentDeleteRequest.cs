using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    public class DocumentDeleteRequest
    {
        /// <summary>
        /// 文件夹ids，格式  [F9VU, F9VU]
        /// </summary>
        public List<string> folderIds { get; set; } = new List<string>();

        /// <summary>
        /// 文件ids
        /// </summary>
        public List<string> fileIds { get; set; } = new List<string>();
    }
    public class ShareDocumentDeleteRequest
    {
        /// <summary>
        /// 文件夹ids，格式  [F9VU, F9VU]
        /// </summary>
        public string folderId { get; set; }

        /// <summary>
        /// 文件ids
        /// </summary>
        public string fileId { get; set; }



    }
}
