using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EllaMakerTool.WPF.Converter
{
    public class BarEditButtonVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int v = (int)value;
            if (v == 1)
                if (GlobalPara.CompanyDocDeletRight || GlobalPara.CompanyFileDeletRight) return Visibility.Visible;
            switch (v)
            {
                case 0:
                    return Visibility.Collapsed;
                default:
                    return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
