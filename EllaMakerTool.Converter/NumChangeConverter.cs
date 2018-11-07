using System;
using System.Windows.Data;

namespace EllaMakerTool.Converter
{
    public class NumChangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double SourceValue = (double)value;
            double changeVale = System.Convert.ToInt64(parameter.ToString());
            return SourceValue + changeVale;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
