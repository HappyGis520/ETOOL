using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EllaMakerTool.Converter
{
    public class GobackEnableVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int index = (int)value;
            if (index > 0)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
