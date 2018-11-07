using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EllaMakerTool.Models;

namespace EllaMakerTool.WPF.Helper
{
    public static class CollectHelper
    {
        public static ObservableCollection<SelectItemModel> GetSelecItems(List<PsAndDeptTreeNodeItem> list)
        {
            var res = new ObservableCollection<SelectItemModel>();
            foreach (var item in list)
            {
                if (item.IsChecked)
                    res.Add(new SelectItemModel()
                    {
                        ItemId=item.ItemId,
                        Itemtype=item.ItemType,
                        Name=item.Name
                    });
                GetSelecItems(item.Childrens).ToList().ForEach(p => { res.Add(p); });
            }

            return res;
        }
    }
}
