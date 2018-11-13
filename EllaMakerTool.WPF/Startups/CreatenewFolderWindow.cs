
using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
		static Action CreatenewFolderWindowConfig =
			CreateAndAddToAllConfig(ConfigCreatenewFolderWindow);

		public static void ConfigCreatenewFolderWindow()
        {
            ViewModelLocator<CreatenewFolderWindow_Model>
                .Instance
                .Register(context=>
                    new CreatenewFolderWindow_Model())
                .GetViewMapper()
                .MapToDefault<CreatenewFolderWindow>();

        }
    }
}
