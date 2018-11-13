using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action RenameWindowConfig =
            CreateAndAddToAllConfig(ConfigRenameWindow);

        public static void ConfigRenameWindow()
        {
            ViewModelLocator<RenameWindow_Model>
                .Instance
                .Register(context =>
                    new RenameWindow_Model())
                .GetViewMapper()
                .MapToDefault<RenameWindow>();

        }
    }
}
