using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
		static Action DownSavePathMesWindowConfig =
			CreateAndAddToAllConfig(ConfigDownSavePathMesWindow);

		public static void ConfigDownSavePathMesWindow()
        {
            ViewModelLocator<DownSavePathMesWindow_Model>
                .Instance
                .Register(context=>
                    new DownSavePathMesWindow_Model())
                .GetViewMapper()
                .MapToDefault<DownSavePathMesWindow>();

        }
    }
}
