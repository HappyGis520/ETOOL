using System;
using System.Globalization;
using System.Windows.Data;

namespace EllaMakerTool.Converter
{
    public class DataTimeTostringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime para = (DateTime)value;
            return para.ToString(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
