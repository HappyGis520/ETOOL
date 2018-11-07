using System.Windows;
using System.Windows.Controls;

namespace EllaMakerTool.Controls
{
    public class MenuTabControl:TabControl
    {
        static MenuTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuTabControl), new FrameworkPropertyMetadata(typeof(MenuTabControl)));//去掉默认属性
        }



        public object MenuMoreObj
        {
            get { return (object)GetValue(MenuMoreObjProperty); }
            set { SetValue(MenuMoreObjProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuMoreObj.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuMoreObjProperty =
            DependencyProperty.Register("MenuMoreObj", typeof(object), typeof(MenuTabControl));




        public object ContentMoreObj
        {
            get { return (object)GetValue(ContentMoreObjProperty); }
            set { SetValue(ContentMoreObjProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentMoreObj.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentMoreObjProperty =
            DependencyProperty.Register("ContentMoreObj", typeof(object), typeof(MenuTabControl));

    }

    public class MenuTabItem : TabItem
    {
        static MenuTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuTabItem), new FrameworkPropertyMetadata(typeof(MenuTabItem)));//去掉默认属性
        }



        public object ButtonIcon
        {
            get { return (object)GetValue(ButtonIconProperty); }
            set { SetValue(ButtonIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonIconProperty =
            DependencyProperty.Register("ButtonIcon", typeof(object), typeof(MenuTabItem));


    }
}
