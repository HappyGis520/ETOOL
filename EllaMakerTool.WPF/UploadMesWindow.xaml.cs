using MVVMSidekick.Views;
using System.Windows;
using System.Windows.Input;

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for UploadMesWindow.xaml
    /// </summary>
    public partial class UploadMesWindow : MVVMWindow
    {
        public UploadMesWindow()
        {
            InitializeComponent();
            startBtn.Visibility = Visibility.Visible;
            closeBtn.Visibility = Visibility.Hidden;
            calBtn.Visibility = Visibility.Visible;
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if (upControl.DicFileItems.Count > 0)
            {
                upControl.startUpload();
                startBtn.Visibility = Visibility.Hidden;
                closeBtn.Visibility = Visibility.Visible;
                calBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                return;
            }
            
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

