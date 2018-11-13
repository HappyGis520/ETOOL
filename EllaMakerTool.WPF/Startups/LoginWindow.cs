using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
		static Action LoginWindowConfig =
			CreateAndAddToAllConfig(ConfigLoginWindow);

		public static void ConfigLoginWindow()
        {
            ViewModelLocator<LoginWindow_Model>
                .Instance
                .Register(context=>
                    new LoginWindow_Model())
                .GetViewMapper()
                .MapToDefault<LoginWindow>();

        }
    }
}
