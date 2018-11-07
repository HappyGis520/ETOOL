using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EllaMakerTool.WPF.Converter
{
    public class GetNetImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string) value)) return new BitmapImage();
            Uri uri = new Uri(string.Format(GlobalPara.ImageServerAdress+(string)value,20,20,"c"));
            return new BitmapImage(uri);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
