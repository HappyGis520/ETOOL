using EllaMakerTool.WPF.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace EllaMakerTool.WPF.Converter
{
    public class DeleteButtonVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var para = (DocumentsModel)value;
            if (para.StatusType == Api.Enum.EnumDocStatusType.Company)
            {
                if (para.Type == Api.Enum.EnumDocFileType.Folder)
                {
                    if (GlobalPara.CompanyDocDeletRight) return Visibility.Visible;
                    return Visibility.Collapsed;
                }
                else
                {
                    if (GlobalPara.CompanyFileDeletRight) return Visibility.Visible;
                    return Visibility.Collapsed;
                }
            }
            if (para.StatusType != Api.Enum.EnumDocStatusType.Share) return Visibility.Visible;
            if (para.CreatorId == GlobalPara.authToken.Profile.ProfileId) return Visibility.Visible;
            if (para.SynergyRange == null || para.SynergyRange.departs == null) return Visibility.Collapsed;
            bool hasSyncRight = (para.SynergyRange.departs.Select(p => p.DepartmentId).ToList().Intersect(GlobalPara.DepartId).Any() || para.SynergyRange.users.Select(p => p.ProfileId).ToList().Intersect(new List<string>() { GlobalPara.authToken.Profile.ProfileId }).Any());
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
