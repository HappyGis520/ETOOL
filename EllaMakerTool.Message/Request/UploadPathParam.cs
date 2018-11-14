using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message.Request
{
    /// <summary>
    /// 获取上传路径参数
    /// </summary>
    public class UploadPathParam:RequestModelBase
    {
        public string id { get;  }

        public UploadPathParam(string Token, string DirID) : base(Token)
        {
            id = DirID;
            this.token = Token;

        }
    }
}
