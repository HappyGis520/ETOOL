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
    public class DeleteButtonVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //var para = (FTPList)value;
            //if (para.StatusType == EnumDocStatusType.Department)
            //{
            //    if (para.Type == EnumDocFileType.Folder)
            //    {
            //        if (Global.ArrowDeleteFolder) return Visibility.Visible;
            //        return Visibility.Collapsed;
            //    }
            //    else
            //    {
            //        if (Global.ArrowDeleteFile) return Visibility.Visible;
            //        return Visibility.Collapsed;
            //    }
            //}
            //if (para.StatusType != EnumDocStatusType.Share) return Visibility.Visible;
            //if (para.CreatorId == Global.authToken.ID) return Visibility.Visible;
            //if (para.SynergyRange == null || para.SynergyRange.departs == null) return Visibility.Collapsed;
            //bool hasSyncRight = (para.SynergyRange.departs.Select(p => p.DepartmentId).ToList().Intersect(Global.DepartId).Any() || para.SynergyRange.users.Select(p => p.ProfileId).ToList().Intersect(new List<string>() { Global.authToken.ID }).Any());
            //if (hasSyncRight)
            //    return Visibility.Visible;
            //return Visibility.Collapsed;
            var para = (FTPListItem)value;
            return true;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
