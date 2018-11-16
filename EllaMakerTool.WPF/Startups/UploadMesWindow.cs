using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action UploadMesWindowConfig =
            CreateAndAddToAllConfig(ConfigUploadMesWindow);

        public static void ConfigUploadMesWindow()
        {
            ViewModelLocator<UploadMesWindow_Model>.Instance.Register(context => new UploadMesWindow_Model()).GetViewMapper().MapToDefault<UploadMesWindow>();
        }
    }
}
