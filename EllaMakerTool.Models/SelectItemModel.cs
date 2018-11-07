using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMSidekick.ViewModels;

namespace EllaMakerTool.Models
{
    public class SelectItemModel:BindableBase<SelectItemModel>
    {


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


        public PsAndDeptItemtype Itemtype
        {
            get { return _ItemtypeLocator(this).Value; }
            set { _ItemtypeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property PsAndDeptItemtype Itemtype Setup        
        protected Property<PsAndDeptItemtype> _Itemtype = new Property<PsAndDeptItemtype> { LocatorFunc = _ItemtypeLocator };
        static Func<BindableBase, ValueContainer<PsAndDeptItemtype>> _ItemtypeLocator = RegisterContainerLocator<PsAndDeptItemtype>("Itemtype", model => model.Initialize("Itemtype", ref model._Itemtype, ref _ItemtypeLocator, _ItemtypeDefaultValueFactory));
        static Func<PsAndDeptItemtype> _ItemtypeDefaultValueFactory = () => PsAndDeptItemtype.Person;
        #endregion


    }
}
