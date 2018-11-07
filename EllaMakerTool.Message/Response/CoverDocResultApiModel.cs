namespace EllaMakerTool.Message
{
    public class CoverDocResultApiModel
    {
        /// <summary>
        /// 文档基本信息
        /// </summary>
        public DocBaseInfoApiModel baseInfo { get; set; } = new DocBaseInfoApiModel();
        /// <summary>
        /// 是否没有共享权限的重名
        /// </summary>
        public bool IsNosyncCover { get; set; } = false;
    }
}
