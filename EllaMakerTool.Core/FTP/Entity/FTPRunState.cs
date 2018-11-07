using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Core.FTP.Entity
{
    /// <summary>
    /// FTP运行状态
    /// </summary>
    public enum FTPRunState
    {
        /// <summary>
        /// 无状态等待执行
        /// </summary>
        None,
        /// <summary>
        /// 正在跑
        /// </summary>
        Run,
        /// <summary>
        /// 中断
        /// </summary>
        Abort,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 出错状态
        /// </summary>
        Error
        /// <summary>
        /// 等待关闭
        /// </summary>
        //WaitingCloseing//,
        /// <summary>
        /// 已关闭 直接移除
        /// </summary>
        //Closed
    }
}
