using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// FileList.xaml 的交互逻辑
    /// </summary>
    public partial class ucBookList : UserControl
    {
        public ucBookList()
        {
            InitializeComponent();
            SubscribeCommand();
        }
        private void ckall_Checked(object sender, RoutedEventArgs e)
        {
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<string>(null, "", "AllCheckedEventRouter");
        }

        private void ckall_Unchecked(object sender, RoutedEventArgs e)
        {
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<string>(null, "", "AllUnCheckedEventRouter");
        }
        private void SubscribeCommand()
        {
            //MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
            //    .Where(p => p.EventName == "MainGetFoucusEventRouter").Subscribe(
            //        p =>
            //        {
            //            this.Focus();
            //        });

            //MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
            //    .Where(p => p.EventName == "MainWinCkAllEventRouter").Subscribe(
            //        p =>
            //        {
            //            ckall.IsChecked = false;
            //        });
        }
    }
}
