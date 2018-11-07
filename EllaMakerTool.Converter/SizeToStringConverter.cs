using System;
using System.Globalization;
using System.Windows.Data;

namespace EllaMakerTool.Converter
{
    public class SizeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string para = parameter?.ToString();
            string m_strSize = "";
            long FactSize = (long)value;
            if (FactSize <= 0) return "";
            if (FactSize < 1024.00)
                m_strSize = (FactSize).ToString("F2") + "KB";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + "MB";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + "GB";
            if (string.IsNullOrEmpty(para))
                return m_strSize;
            else
            {
                return para + m_strSize;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
