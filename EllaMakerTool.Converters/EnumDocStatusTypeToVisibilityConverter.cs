using EllaMakerTool.Api.Enum;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EllaMakerTool.WPF.Converter
{
    public class EnumDocStatusTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EnumDocStatusType v = (EnumDocStatusType) value;
            switch (v)
            {
                case EnumDocStatusType.Share:
                    return Visibility.Visible;
                default:
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
