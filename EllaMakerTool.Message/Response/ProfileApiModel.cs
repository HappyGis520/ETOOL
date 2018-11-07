using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Message
{
    /// <summary>
    /// 用户账户基本信息
    /// </summary>
    public class ProfileApiModel
    {
        /// <summary>
        /// ID，其它需要用到此用户信息时使用,注意此编码区分大小写,不是XiaoYingHao
        ///  </summary>
        public string ProfileId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Fullname { get; set; }
        /// <summary>
        /// 头像URL
        /// </summary>
        public string FaceUrl { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        public string RegionName { get; set; } // RegionName
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } // Address
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; } // Gender
        /// <summary>
        /// 小赢号
        /// </summary>
        public string XiaoYingCode { get; set; } // 小赢号

    }
}
