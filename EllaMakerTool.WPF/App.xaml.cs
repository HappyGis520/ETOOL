////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//         佛祖保佑       永无BUG     永不修改                       //
//////////////////////////////////////////////////////////////////// 
using EllaMakerTool.WPF.Startups;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void InitNavigationConfigurationInThisAssembly()
        {
            MVVMSidekick.Startups.StartupFunctions.RunAllConfig();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //初始化AutoMapper映射
            AutoMapperConfiguration.AutoMapperInit();
            InitNavigationConfigurationInThisAssembly();
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            ApplySkin();
            base.OnStartup(e);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString(), e.ExceptionObject?.GetType().Name);
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //e.Exception   发生的异常
            //e.Handled     是否已处理异常事件
            
if(!e.Handled)

                System.Windows.MessageBox.Show(e.Exception + "");
        }


        public void ApplySkin(string Themxaml= "/EllaMakerTool.Theme;component/Style.xaml")
        {
          
            Uri _uri = new Uri(Themxaml, UriKind.Relative);
            // Load the ResourceDictionary into memory.
            ResourceDictionary skinDict = Application.LoadComponent(_uri) as ResourceDictionary;

            Collection<ResourceDictionary> mergedDicts = base.Resources.MergedDictionaries;

            // Remove the existing skin dictionary, if one exists.
            // NOTE: In a real application, this logic might need
            // to be more complex, because there might be dictionaries
            // which should not be removed.
            if (mergedDicts.Count > 0)
                mergedDicts.Clear();

            // Apply the selected skin so that all elements in the
            // application will honor the new look and feel.
            mergedDicts.Add(skinDict);
        }
    }
}
