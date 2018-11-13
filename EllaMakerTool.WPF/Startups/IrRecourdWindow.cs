using EllaMakerTool.WPF;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action IrRecourdWindowConfig =
            CreateAndAddToAllConfig(ConfigIrRecourdWindow);

        public static void ConfigIrRecourdWindow()
        {
            ViewModelLocator<IrRecourdWindow_Model>
                .Instance
                .Register(context =>
                    new IrRecourdWindow_Model())
                .GetViewMapper()
                .MapToDefault<IrRecourdWindow>();

        }
    }
}
