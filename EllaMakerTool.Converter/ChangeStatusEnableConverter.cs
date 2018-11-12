//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Windows.Data;
//using EllaMakerTool.Message;
//using EllaMakerTool.Models;

//namespace EllaMakerTool.Converter
//{
//    public class ChangeStatusEnableConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            var para = (FTPli)value;
//            return getStatus(para);
//        }


//        public bool getStatus(DocumentsModel para)
//        {
            
//            if (para.StatusType == EnumDocStatusType.Department)
//            {
//                if (para.Type ==EnumDocFileType.Folder)
//                {
//                    if (Global.CompanyDocEditRight) return true;
//                    return false;
//                }
//                else
//                {
//                    if (Global.CompanyFileEditRight) return true;
//                    return false;
//                }
//            }
//            if (para.StatusType != EnumDocStatusType.Share) return true;
//            if (para.CreatorId == Global.authToken.Profile.ProfileId) return true;
//            if (para.SynergyRange == null || para.SynergyRange.departs == null) return false;
//            bool hasSyncRight =
//            (para.SynergyRange.departs.Select(p => p.DepartmentId).ToList().Intersect(Global.DepartId).Any() ||
//             para.SynergyRange.users.Select(p => p.ProfileId).ToList()
//                 .Intersect(new List<string>() { Global.authToken.Profile.ProfileId }).Any());
//            if (hasSyncRight)
//                return true;
//            return false;
//        }
//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
