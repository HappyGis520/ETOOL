using EllaMakerTool.WPF.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace EllaMakerTool.WPF.Converter
{
    public class ChangeStatusEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var para = (DocumentsModel)value;
            return getStatus(para);
        }


        public bool getStatus(DocumentsModel para)
        {
            
            if (para.StatusType == Api.Enum.EnumDocStatusType.Company)
            {
                if (para.Type == Api.Enum.EnumDocFileType.Folder)
                {
                    if (GlobalPara.CompanyDocEditRight) return true;
                    return false;
                }
                else
                {
                    if (GlobalPara.CompanyFileEditRight) return true;
                    return false;
                }
            }
            if (para.StatusType != Api.Enum.EnumDocStatusType.Share) return true;
            if (para.CreatorId == GlobalPara.authToken.Profile.ProfileId) return true;
            if (para.SynergyRange == null || para.SynergyRange.departs == null) return false;
            bool hasSyncRight =
            (para.SynergyRange.departs.Select(p => p.DepartmentId).ToList().Intersect(GlobalPara.DepartId).Any() ||
             para.SynergyRange.users.Select(p => p.ProfileId).ToList()
                 .Intersect(new List<string>() { GlobalPara.authToken.Profile.ProfileId }).Any());
            if (hasSyncRight)
                return true;
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
