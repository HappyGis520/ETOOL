using System;
using System.Globalization;
using System.Windows.Data;

namespace EllaMakerTool.WPF.Converter
{
    public class SizeToProgressStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string m_strSize = "";
            long FactSize = (long)value;
            if (FactSize <= 0) return "";
            if (FactSize < 1024.00)
                m_strSize = (FactSize).ToString("F2") + "KB";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + "MB";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + "GB";
                return m_strSize+"/"+m_strSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
