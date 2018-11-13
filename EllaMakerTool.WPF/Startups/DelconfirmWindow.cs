using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
		static Action DelconfirmWindowConfig =
			CreateAndAddToAllConfig(ConfigDelconfirmWindow);

		public static void ConfigDelconfirmWindow()
        {
            ViewModelLocator<DelconfirmWindow_Model>
                .Instance
                .Register(context=>
                    new DelconfirmWindow_Model())
                .GetViewMapper()
                .MapToDefault<DelconfirmWindow>();

        }
    }
}
