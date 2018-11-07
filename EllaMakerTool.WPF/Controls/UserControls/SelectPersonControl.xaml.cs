using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EllaMakerTool.Models;

namespace EllaMakerTool.WPF.Controls.UserControls
{
    /// <summary>
    /// SelectPersonControl.xaml 的交互逻辑
    /// </summary>
    public partial class SelectPersonControl : UserControl
    {
        public SelectPersonControl()
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
            DependencyProperty.Register("TreeSource", typeof(ObservableCollection<PsAndDeptTreeNodeItem>), typeof(SelectPersonControl));



    }
}
