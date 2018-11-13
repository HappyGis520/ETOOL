using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action SelectDeptWindowConfig =
            CreateAndAddToAllConfig(ConfigSelectDeptWindow);

        public static void ConfigSelectDeptWindow()
        {
            ViewModelLocator<SelectDeptWindow_Model>
                .Instance
                .Register(context =>
                    new SelectDeptWindow_Model())
                .GetViewMapper()
                .MapToDefault<SelectDeptWindow>();

        }
    }
}
