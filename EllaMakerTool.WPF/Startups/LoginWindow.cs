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
