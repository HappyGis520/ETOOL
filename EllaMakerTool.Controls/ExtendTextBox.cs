using System.Windows;
using System.Windows.Controls;

namespace EllaMakerTool.Controls
{
    public class ExtendTextBox:TextBox
    {
        static ExtendTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendTextBox), new FrameworkPropertyMetadata(typeof(ExtendTextBox)));//去掉默认属性
        }

        public string WaterMark
        {
            get { return (string)GetValue(WaterMarkProperty); }
            set { SetValue(WaterMarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterMark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("WaterMark", typeof(string), typeof(ExtendTextBox), new PropertyMetadata(""));

        public object ButtonArea
        {
            get { return (object)GetValue(ButtonAreaProperty); }
            set { SetValue(ButtonAreaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonArea.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonAreaProperty =
            DependencyProperty.Register("ButtonArea", typeof(object), typeof(ExtendTextBox));

        public object HeaderObj
        {
            get { return (object)GetValue(HeaderObjProperty); }
            set { SetValue(HeaderObjProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderObj.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderObjProperty =
            DependencyProperty.Register("HeaderObj", typeof(object), typeof(ExtendTextBox));



        public CornerRadius CornerRadiusValue
        {
            get { return (CornerRadius)GetValue(CornerRadiusValueProperty); }
            set { SetValue(CornerRadiusValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadiusValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusValueProperty =
            DependencyProperty.Register("CornerRadiusValue", typeof(CornerRadius), typeof(ExtendTextBox));
    }
}
