using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using EllaMakerTool.Message;
using EllaMakerTool.Models;

namespace EllaMakerTool.Converter
{
    public class SyncButtonVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //var para = (DocumentsModel)value;
            //if (para.Type ==EnumDocFileType.Folder) return Visibility.Collapsed;
            //if (para.StatusType != EnumDocStatusType.Share) return Visibility.Collapsed;
            //if (para.CreatorId == Global.authToken.ID) return Visibility.Visible;
            //if (para.SynergyRange == null || para.SynergyRange.departs == null) return Visibility.Collapsed;
            //bool hasSyncRight = (para.SynergyRange.departs.Select(p => p.DepartmentId).ToList().Intersect(Global.DepartId).Any() || para.SynergyRange.users.Select(p => p.ProfileId).ToList().Intersect(new List<string>() { Global.authToken.ID }).Any());
            //if (para.StatusType==EnumDocStatusType.Share&& hasSyncRight)
            //    return Visibility.Visible;
            //return Visibility.Collapsed;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
