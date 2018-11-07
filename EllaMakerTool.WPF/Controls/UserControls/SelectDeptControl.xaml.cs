using System.Collections.ObjectModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using EllaMakerTool.Models;

namespace EllaMakerTool.WPF.Controls.UserControls
{
    /// <summary>
    /// SelectDeptControl.xaml 的交互逻辑
    /// </summary>
    public partial class SelectDeptControl : UserControl
    {
        public SelectDeptControl()
        {
            InitializeComponent();
        }

        public ObservableCollection<PsAndDeptTreeNodeItem> TreeSource
        {
            get { return (ObservableCollection<PsAndDeptTreeNodeItem>)GetValue(TreeSourceProperty); }
            set { SetValue(TreeSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TreeSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TreeSourceProperty =
            DependencyProperty.Register("TreeSource", typeof(ObservableCollection<PsAndDeptTreeNodeItem>), typeof(SelectDeptControl));


    }
}
