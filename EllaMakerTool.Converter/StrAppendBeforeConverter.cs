using System;
using System.Globalization;
using System.Windows.Data;

namespace EllaMakerTool.Converter
{
    public class StrAppendBeforeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var para = value.ToString();
            var param = parameter.ToString();
            return param+para;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
