using System.Windows;
using System.Windows.Controls;

namespace EllaMakerTool.Controls
{
    public class StoreProgressBar:ProgressBar
    {
        static StoreProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StoreProgressBar), new FrameworkPropertyMetadata(typeof(StoreProgressBar)));//去掉默认属性
        }
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(StoreProgressBar));

        public string TipContent
        {
            get => (string)GetValue(TipContentProperty);
            set => SetValue(TipContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for BarTipContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipContentProperty =
            DependencyProperty.Register("TipContent", typeof(string), typeof(StoreProgressBar));


    }
}
    