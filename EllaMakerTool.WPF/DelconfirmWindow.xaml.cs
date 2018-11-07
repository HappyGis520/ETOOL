using MVVMSidekick.Views;
using System.Windows.Input;

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for DelconfirmWindow.xaml
    /// </summary>
    public partial class DelconfirmWindow : MVVMWindow
    {
        public DelconfirmWindow()	           
        {
            InitializeComponent();
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                base.OnMouseLeftButtonDown(e);
                this.DragMove();
            }
            catch
            {

            }
        }
    }
}

