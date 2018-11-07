using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using EllaMakerTool.Models;

namespace EllaMakerTool.Converter
{
    public class GofrontEnableVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int index = (int)value;
            if (index+1 < Global.RecordList.Count)
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
