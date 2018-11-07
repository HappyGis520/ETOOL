using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EllaMakerTool.WPF.Converter
{
    public class ProgressImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int progress = (int)value;
            if (progress == 100) return new BitmapImage(new Uri("../Resources/Icon_UploadSuccess.png", UriKind.Relative));
            return new BitmapImage();

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }
    }
}
