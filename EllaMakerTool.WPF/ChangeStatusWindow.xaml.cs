using MVVMSidekick.Views;
using System.Windows.Input;

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for ChangeStatusWindow.xaml
    /// </summary>
    public partial class ChangeStatusWindow : MVVMWindow
    {
        public ChangeStatusWindow()
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

