using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using EllaMakerTool.Message;

namespace EllaMakerTool.Converter
{
    public class DocTypeVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool v = (bool)value;
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
