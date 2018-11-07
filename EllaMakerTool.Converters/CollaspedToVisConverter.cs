using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EllaMakerTool.WPF.Converter
{
    public class CollaspedToVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility v = (Visibility)value;
            switch (v)
            {
                case Visibility.Collapsed:
                    return Visibility.Visible;
                case Visibility.Visible:
                    return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
