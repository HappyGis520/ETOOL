using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MVVMSidekick.ViewModels;

namespace EllaMakerTool.Models
{
    public class PsAndDeptTreeNodeItem: BindableBase<PsAndDeptTreeNodeItem>
    {
        public bool IsChecked
        {
            get { return _IsCheckedLocator(this).Value; }
            set
            {
                _IsCheckedLocator(this).SetValueAndTryNotify(value);
                if (!value) return;
                foreach (var item in Childrens)
                {
                    item.IsChecked = value;
                }
                
            }
        }
        #region Property bool IsChecked Setup        
        protected Property<bool> _IsChecked = new Property<bool> { LocatorFunc = _IsCheckedLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsCheckedLocator = RegisterContainerLocator<bool>("IsChecked", model => model.Initialize("IsChecked", ref model._IsChecked, ref _IsCheckedLocator, _IsCheckedDefaultValueFactory));
        static Func<bool> _IsCheckedDefaultValueFactory = () => false;
        #endregion

        public PsAndDeptItemtype ItemType
        {
            get { return _ItemTypeLocator(this).Value; }
            set { _ItemTypeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property PsAndDeptItemtype ItemType Setup        
        protected Property<PsAndDeptItemtype> _ItemType = new Property<PsAndDeptItemtype> { LocatorFunc = _ItemTypeLocator };
        static Func<BindableBase, ValueContainer<PsAndDeptItemtype>> _ItemTypeLocator = RegisterContainerLocator<PsAndDeptItemtype>("ItemType", model => model.Initialize("ItemType", ref model._ItemType, ref _ItemTypeLocator, _ItemTypeDefaultValueFactory));
        static Func<PsAndDeptItemtype> _ItemTypeDefaultValueFactory = () => PsAndDeptItemtype.Person;
        #endregion


        public string ItemId
        {
            get { return _ItemIdLocator(this).Value; }
            set { _ItemIdLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string ItemId Setup        
        protected Property<string> _ItemId = new Property<string> { LocatorFunc = _ItemIdLocator };
        static Func<BindableBase, ValueContainer<string>> _ItemIdLocator = RegisterContainerLocator<string>("ItemId", model => model.Initialize("ItemId", ref model._ItemId, ref _ItemIdLocator, _ItemIdDefaultValueFactory));
        static Func<string> _ItemIdDefaultValueFactory = () => "";
        #endregion


        public string HeadUrl
        {
            get { return _HeadUrlLocator(this).Value; }
            set { _HeadUrlLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string HeadUrl Setup        
        protected Property<string> _HeadUrl = new Property<string> { LocatorFunc = _HeadUrlLocator };
        static Func<BindableBase, ValueContainer<string>> _HeadUrlLocator = RegisterContainerLocator<string>("HeadUrl", model => model.Initialize("HeadUrl", ref model._HeadUrl, ref _HeadUrlLocator, _HeadUrlDefaultValueFactory));
        static Func<string> _HeadUrlDefaultValueFactory = () => "";
        #endregion


        public List<PsAndDeptTreeNodeItem> Childrens
        {
            get { return _ChildrensLocator(this).Value; }
            set { _ChildrensLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property List<PsAndDeptTreeNodeItem> Childrens Setup        
        protected Property<List<PsAndDeptTreeNodeItem>> _Childrens = new Property<List<PsAndDeptTreeNodeItem>> { LocatorFunc = _ChildrensLocator };
        static Func<BindableBase, ValueContainer<List<PsAndDeptTreeNodeItem>>> _ChildrensLocator = RegisterContainerLocator<List<PsAndDeptTreeNodeItem>>("Childrens", model => model.Initialize("Childrens", ref model._Childrens, ref _ChildrensLocator, _ChildrensDefaultValueFactory));
        static Func<List<PsAndDeptTreeNodeItem>> _ChildrensDefaultValueFactory = () => new List<PsAndDeptTreeNodeItem>();
        #endregion


        public string Name
        {
            get { return _NameLocator(this).Value; }
            set { _NameLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Name Setup        
        protected Property<string> _Name = new Property<string> { LocatorFunc = _NameLocator };
        static Func<BindableBase, ValueContainer<string>> _NameLocator = RegisterContainerLocator<string>("Name", model => model.Initialize("Name", ref model._Name, ref _NameLocator, _NameDefaultValueFactory));
        static Func<string> _NameDefaultValueFactory = () => "";
        #endregion

    }

    public enum PsAndDeptItemtype
    {
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        Dept = 0,
        /// <summary>
        /// 个人
        /// </summary>
        [Description("个人")]
        Person = 1
    }
}
