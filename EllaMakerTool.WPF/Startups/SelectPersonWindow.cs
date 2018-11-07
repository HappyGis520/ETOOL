using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action SelectPersonWindowConfig =
            CreateAndAddToAllConfig(ConfigSelectPersonWindow);

        public static void ConfigSelectPersonWindow()
        {
            ViewModelLocator<SelectPersonWindow_Model>
                .Instance
                .Register(context =>
                    new SelectPersonWindow_Model())
                .GetViewMapper()
                .MapToDefault<SelectPersonWindow>();

        }
    }
}
