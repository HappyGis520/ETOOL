using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using EllaMakerTool.Models;

namespace EllaMakerTool.Converter
{
    public class DocmentItemsToIrVisConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var para = (DocumentsModel) value;
            if (para.IrList.Count > 0) return Visibility.Visible;
            return Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
