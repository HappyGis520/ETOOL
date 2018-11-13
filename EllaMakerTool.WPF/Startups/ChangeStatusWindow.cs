/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： ChangeStatusWindow.cs
 * * 功   能：  
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-11-13 14:19:07
 * * 修改记录： 
 * * 日期时间： 2018-11-13 14:19:07  修改人：王建军  创建
 * *******************************************************************/
using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action ChangeStatusWindowConfig =
            CreateAndAddToAllConfig(ConfigChangeStatusWindow);

        public static void ConfigChangeStatusWindow()
        {
            ViewModelLocator<ChangeStatusWindow_Model>
                .Instance
                .Register(context =>
                    new ChangeStatusWindow_Model())
                .GetViewMapper()
                .MapToDefault<ChangeStatusWindow>();

        }
    }
}
