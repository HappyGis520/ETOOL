using MVVMSidekick.Views;
using System.Windows.Input;

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for DownSavePathMesWindow.xaml
    /// </summary>
    public partial class DownSavePathMesWindow : MVVMWindow
    {
        public DownSavePathMesWindow()	           
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

