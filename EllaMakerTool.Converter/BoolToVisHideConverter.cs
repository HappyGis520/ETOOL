using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EllaMakerTool.Converter
{
    public class BoolToVisHideConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool v = (bool)value;
            switch (v)
            {
                case true:
                    return Visibility.Visible;
                case false:
                    return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
