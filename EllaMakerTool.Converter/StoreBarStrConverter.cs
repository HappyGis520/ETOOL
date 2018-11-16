using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using EllaMakerTool.Message;

namespace EllaMakerTool.Converter
{
    public class StoreBarStrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserStoreStatusApiModel para =(UserStoreStatusApiModel)value;
            StringBuilder sb = new StringBuilder();
            
            string m_strSize = "";
            long FactSize = (long)para.UsedDocumentsSize;
            if (FactSize < 1024.00)
                m_strSize = (FactSize).ToString("F2") + "K";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + "M";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + "G";

            sb.Append(m_strSize + "/" + (int)(para.DocumentsSize / 1000000) + "G");
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
