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
