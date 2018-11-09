using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using EllaMakerTool.Message;
using EllaMakerTool.Models;

namespace EllaMakerTool.Models
{
    public class AutoMapperConfiguration
    {
        public static void AutoMapperInit()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.CreateMap<DocumentV1ApiModel, DocumentsModel>();
                cfg.CreateMap<EmployeeAndDeptNodelApiModel, PsAndDeptTreeNodeItem>();
                cfg.CreateMap<BookItem, BookListItem>();
            });
            MapperUtil.Config(config.CreateMapper());
        }
    }
}
