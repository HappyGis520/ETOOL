using System.Windows;
using System.Windows.Input;

namespace EllaMakerTool.WPF.Controls.UserControls
{
    /// <summary>
    /// CoverMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class CoverMessageBoxControl : Window
    {
        public CoverMessageBoxControl()
        {
            InitializeComponent();
        }

        public MessageBoxResult messageboxResult;

        public string TipStr
        {
            get { return (string)GetValue(TipStrProperty); }
            set { SetValue(TipStrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TipStr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipStrProperty =
            DependencyProperty.Register("TipStr", typeof(string), typeof(CoverMessageBoxControl));



        public string MainStr
        {
            get { return (string)GetValue(MainStrProperty); }
            set { SetValue(MainStrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainStr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainStrProperty =
            DependencyProperty.Register("MainStr", typeof(string), typeof(CoverMessageBoxControl));



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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            messageboxResult = MessageBoxResult.OK;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            messageboxResult = MessageBoxResult.Cancel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainStrLb.Content = MainStr;
            TipStrLb.Content = TipStr;
        }
    }
}
