using System;
using System.Globalization;
using System.Windows.Data;

namespace EllaMakerTool.Converter
{
    public class StrAppendConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value==null)
                return parameter.ToString();
            var para = value.ToString();
            var param = parameter.ToString();
            return para + param;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
