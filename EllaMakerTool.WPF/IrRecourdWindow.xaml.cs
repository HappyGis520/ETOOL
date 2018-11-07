using MVVMSidekick.Views;
using System.Windows.Input;

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for IrRecourdWindow.xaml
    /// </summary>
    public partial class IrRecourdWindow : MVVMWindow
    {
        public IrRecourdWindow()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

