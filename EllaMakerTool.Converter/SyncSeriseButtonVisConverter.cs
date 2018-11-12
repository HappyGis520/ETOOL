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
    public class SyncSeriseButtonVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            var para = (DocumentsModel)value;
            if (para.StatusType ==EnumDocStatusType.Department)
            {
                if (para.Type == EnumDocFileType.Folder)
                {
                    if (Global.CompanyDocEditRight) return Visibility.Visible;
                    return Visibility.Collapsed;
                }
                else
                {
                    if (Global.CompanyFileEditRight) return Visibility.Visible;
                    return Visibility.Collapsed;
                }
            }
            if(para.StatusType != EnumDocStatusType.Share) return Visibility.Visible;
            {
                if (para.CreatorId == Global.authToken.Profile.ProfileId) return Visibility.Visible;
            }
            if(para.SynergyRange==null || para.SynergyRange.departs == null) return Visibility.Collapsed;
            bool hasSyncRight = (para.SynergyRange.departs.Select(p => p.DepartmentId).ToList().Intersect(Global.DepartId).Any()|| para.SynergyRange.users.Select(p=>p.ProfileId).ToList().Intersect(new List<string>(){Global.authToken.Profile.ProfileId}).Any());
            if (hasSyncRight)
                return Visibility.Visible;
            return Visibility.Collapsed;



        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
