using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EllaMakerTool.Models
{
    public class MesWindowResModel
    {
        public MesWinType WinType { get; set; }

        public bool IsOk { get; set; }

        public object ResData { get; set; }
    }

    public enum MesWinType
    {
        /// <summary>
        /// 登录窗口
        /// </summary>
        [Description("登录")]
        LoginWin = 0,
        /// <summary>
        /// 上传窗口
        /// </summary>
        [Description("上传")]
        UploadWin = 1,
        /// <summary>
        ///更新记录
        /// </summary>
        [Description("更新记录")]
        UpdateLogWin = 2,
        /// <summary>
        /// 修改状态
        /// </summary>
        [Description("修改状态")]
        StatusChangeWin = 3,
        /// <summary>
        /// 新建文件夹
        /// </summary>
        [Description("新建文件夹")]
        CataLogAddWin = 4,
        /// <summary>
        /// 设置下载路径
        /// </summary>
        [Description("设置下载路径")]
        DownSaveWin=5,
        /// <summary>
        /// 重命名
        /// </summary>
        [Description("重命名")]
        RenameWin = 6,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        DelConfirmWin = 7,
        /// <summary>
        /// 部门选择
        /// </summary>
        [Description("部门选择")]
        DeptSelectWin = 8,
        /// <summary>
        /// 人员选择
        /// </summary>
        [Description("人员选择")]
        PersonSelectWin = 9,
        /// <summary>
        /// 移动
        /// </summary>
        [Description("移动")]
        MoveWin = 10
    }
}
