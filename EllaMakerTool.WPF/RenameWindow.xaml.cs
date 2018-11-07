using MVVMSidekick.Views;
using System.Windows.Input;

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow : MVVMWindow
    {
        public RenameWindow()
        {
            InitializeComponent();
        }

        private void FilenameBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FilenameBox.SelectionStart = FilenameBox.Text.Length;
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

