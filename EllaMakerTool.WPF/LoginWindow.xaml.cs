using MVVMSidekick.Views;
using System.Windows;
using System.Windows.Input;

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MVVMWindow
    {
        public LoginWindow()	           
        {
            InitializeComponent();
        }

        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void FButton_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void ExtendTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textB.SelectionStart = textB.Text.Length;
        }

    }
}

