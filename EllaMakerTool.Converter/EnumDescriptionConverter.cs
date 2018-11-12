using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using EllaMakerTool.Models;
using EllaMakerTool.Message;

namespace EllaMakerTool.Converter
{
    public class EnumDescriptionConverter : IValueConverter
    {
        private string GetEnumDescription(EnumDocStatusType enumObj)
        {
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                DescriptionAttribute attrib = attribArray[0] as DescriptionAttribute;
                return attrib.Description;
            }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DocumentsModel myModel = (DocumentsModel)value;
            if (myModel.StatusType == EnumDocStatusType.Department) return "公司";
            else if (myModel.StatusType == EnumDocStatusType.Personal) return "个人";
            if (myModel.ShareRange == null ) return "仅自己";
            if (myModel.ShareRange.departs == null)
            {
                myModel.ShareRange.departs = new List<DepartmentApiModel>();
            }
            if (myModel.ShareRange.users == null)
            {
                myModel.ShareRange.users = new List<ProfileApiModel>();
            }
            if (myModel.ShareRange.departs.Count + myModel.ShareRange.users.Count < 1) return "仅自己";
            string description = GetEnumDescription(myModel.StatusType);
            return description;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
