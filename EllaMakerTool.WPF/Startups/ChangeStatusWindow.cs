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
        static Action ChangeStatusWindowConfig =
            CreateAndAddToAllConfig(ConfigChangeStatusWindow);

        public static void ConfigChangeStatusWindow()
        {
            ViewModelLocator<ChangeStatusWindow_Model>
                .Instance
                .Register(context =>
                    new ChangeStatusWindow_Model())
                .GetViewMapper()
                .MapToDefault<ChangeStatusWindow>();

        }
    }
}
