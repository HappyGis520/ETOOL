using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;  
using System;
using System.Net;
using System.Windows;


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
