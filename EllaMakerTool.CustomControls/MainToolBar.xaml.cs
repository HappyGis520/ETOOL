using System.Windows;
using System.Windows.Controls;

namespace EllaMakerTool.CustomControls
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class MainToolBar : UserControl
    {
        public MainToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
            InitializeComponent();
        }
    }
}
