using System.Windows;
using EllaMakerTool.Core;
namespace DESTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnChar2Code_OnClick(object sender, RoutedEventArgs e)
        {
            var _Chare = txtChare.Text.Trim();
            txtCode.Text = DESEncryptHelper.Encrypt(_Chare);
        }

        private void BtnCode2Char_OnClick(object sender, RoutedEventArgs e)
        {
            txtChare.Text = DESEncryptHelper.Decrypt(txtCode.Text.Trim());
        }

        private void BtnExite_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }
    }
}
