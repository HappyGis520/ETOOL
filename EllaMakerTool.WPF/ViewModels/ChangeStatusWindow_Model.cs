﻿using EllaMakerTool.Api;
using EllaMakerTool.Core;
using EllaMakerTool.WPF.Startups;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EllaMakerTool.Message;
using EllaMakerTool.Models;

namespace EllaMakerTool.WPF.ViewModels
{

    public class ChangeStatusWindow_Model : ViewModelBase<ChangeStatusWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public ChangeStatusWindow_Model()
        {
            if (IsInDesignMode)
            {

            }
            
        }

        private ToastInstance _vm;

        public bool IsFolder
        {
            get { return _IsFolderLocator(this).Value; }
            set
            {
                _IsFolderLocator(this).SetValueAndTryNotify(value);
                if (Global.CompanyDocEditRight && value)
                    ComBoxItemSource = new Dictionary<EnumDocStatusType, string>()
                    {
                        {EnumDocStatusType.Company, "公司"},
                        {EnumDocStatusType.Share, "共享"},
                        {EnumDocStatusType.Personal, "个人"},

                    };
                if (Global.CompanyFileEditRight && !value)
                    ComBoxItemSource = new Dictionary<EnumDocStatusType, string>()
                    {
                        {EnumDocStatusType.Company, "公司"},
                        {EnumDocStatusType.Share, "共享"},
                        {EnumDocStatusType.Personal, "个人"},

                    };
            }
        }
        #region Property bool IsFolder Setup        
        protected Property<bool> _IsFolder = new Property<bool> { LocatorFunc = _IsFolderLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsFolderLocator = RegisterContainerLocator<bool>("IsFolder", model => model.Initialize("IsFolder", ref model._IsFolder, ref _IsFolderLocator, _IsFolderDefaultValueFactory));
        static Func<bool> _IsFolderDefaultValueFactory = () => false;
        #endregion


        public bool CanStatusChange
        {
            get { return _CanStatusChangeLocator(this).Value; }
            set { _CanStatusChangeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool CanStatusChange Setup        
        protected Property<bool> _CanStatusChange = new Property<bool> { LocatorFunc = _CanStatusChangeLocator };
        static Func<BindableBase, ValueContainer<bool>> _CanStatusChangeLocator = RegisterContainerLocator<bool>("CanStatusChange", model => model.Initialize("CanStatusChange", ref model._CanStatusChange, ref _CanStatusChangeLocator, _CanStatusChangeDefaultValueFactory));
        static Func<bool> _CanStatusChangeDefaultValueFactory = () => true;
        #endregion

        /// <summary>
        /// 订阅事件
        /// </summary>
        private void SubscribeCommand()
        {
            //注册全局事件路由
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<MesWindowResModel>()
                .Where(x => x.EventName == "SelectPersonResFromSyncEventRouter").Subscribe(
                    async e =>
                    {
                        var para = e.EventData;
                        if (para.IsOk)
                        {
                            System.Diagnostics.Debug.WriteLine("开始"+this.BindableInstanceId);
                            ObservableCollection<SelectItemModel> param =
                                (ObservableCollection<SelectItemModel>) para.ResData;
                            foreach (var item in param)
                            {
                                if (!this.SyncRange
                                    .Any(p => p.Itemtype == item.Itemtype && p.ItemId == item.ItemId))
                                {
                                    System.Diagnostics.Debug.WriteLine("中间");
                                    if (!this.ShareRange.Any(p=>p.ItemId==item.ItemId&&p.Itemtype==item.Itemtype))
                                    {
                                        _vm.ShowError($"{item.Name} 不具有共享范围，添加失败！");
                                    }
                                    else
                                    {
                                        this.SyncRange.Add(item);
                                    }
                                    
                                }
                            }
                            System.Diagnostics.Debug.WriteLine("结束");
                        }
                        await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    })
                .DisposeWith(this);

            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<MesWindowResModel>()
                .Where(x => x.EventName == "SelectPersonResFromShareEventRouter").Subscribe(
                    async e =>
                    {
                        var para = e.EventData;
                        if (para.IsOk)
                        {
                            ObservableCollection<SelectItemModel> param =
                                (ObservableCollection<SelectItemModel>)para.ResData;
                            
                            foreach (var item in param)
                            {
                                if (!this.ShareRange
                                    .Any(p => p.Itemtype == item.Itemtype && p.ItemId == item.ItemId))
                                {
                                    this.ShareRange.Add(item);
                                }
                            }
                        }
                        await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    })
                .DisposeWith(this);

            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<MesWindowResModel>()
                .Where(x => x.EventName == "SelectDeptResFromSyncEventRouter").Subscribe(
                    async e =>
                    {
                        var para = e.EventData;
                        if (para.IsOk)
                        {
                            ObservableCollection<SelectItemModel> param =
                                (ObservableCollection<SelectItemModel>)para.ResData;
                            foreach (var item in param)
                            {
                                if (!this.SyncRange
                                    .Any(p => p.Itemtype == item.Itemtype && p.ItemId == item.ItemId))
                                {
                                    if (!this.ShareRange.Any(p => p.ItemId == item.ItemId && p.Itemtype == item.Itemtype))
                                    {
                                        _vm.ShowError($"{item.Name} 不具有共享范围，添加失败！");
                                    }
                                    else
                                    {
                                        this.SyncRange.Add(item);
                                    }
                                }
                            }
                        }
                        await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    })
                .DisposeWith(this);

            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<MesWindowResModel>()
                .Where(x => x.EventName == "SelectDeptResFromShareEventRouter").Subscribe(
                    async e =>
                    {
                        var para = e.EventData;
                        if (para.IsOk)
                        {
                            ObservableCollection<SelectItemModel> param =
                                (ObservableCollection<SelectItemModel>)para.ResData;
                            foreach (var item in param)
                            {
                                if (!this.ShareRange
                                    .Any(p => p.Itemtype == item.Itemtype && p.ItemId == item.ItemId))
                                {
                                    this.ShareRange.Add(item);
                                }
                            }
                        }
                        await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    })
                .DisposeWith(this);
        }
        //propvm tab tab string tab Title

        public bool IsFromRoot
        {
            get { return _IsFromRootLocator(this).Value; }
            set { _IsFromRootLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool IsFromRoot Setup        
        protected Property<bool> _IsFromRoot = new Property<bool> { LocatorFunc = _IsFromRootLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsFromRootLocator = RegisterContainerLocator<bool>("IsFromRoot", model => model.Initialize("IsFromRoot", ref model._IsFromRoot, ref _IsFromRootLocator, _IsFromRootDefaultValueFactory));
        static Func<bool> _IsFromRootDefaultValueFactory = () => true;
        #endregion


        public bool OpenSync
        {
            get { return _OpenSyncLocator(this).Value; }
            set { _OpenSyncLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool OpenSync Setup        
        protected Property<bool> _OpenSync = new Property<bool> { LocatorFunc = _OpenSyncLocator };
        static Func<BindableBase, ValueContainer<bool>> _OpenSyncLocator = RegisterContainerLocator<bool>("OpenSync", model => model.Initialize("OpenSync", ref model._OpenSync, ref _OpenSyncLocator, _OpenSyncDefaultValueFactory));
        static Func<bool> _OpenSyncDefaultValueFactory = () => false;
        #endregion


        public EnumDocStatusType StatusType
        {
            get { return _StatusTypeLocator(this).Value; }
            set
            {
                _StatusTypeLocator(this).SetValueAndTryNotify(value);
                if (value != EnumDocStatusType.Share)
                {
                    OpenSync = false;
                }
            }
        }
        #region Property EnumDocStatusType StatusType Setup        
        protected Property<EnumDocStatusType> _StatusType = new Property<EnumDocStatusType> { LocatorFunc = _StatusTypeLocator };
        static Func<BindableBase, ValueContainer<EnumDocStatusType>> _StatusTypeLocator = RegisterContainerLocator<EnumDocStatusType>("StatusType", model => model.Initialize("StatusType", ref model._StatusType, ref _StatusTypeLocator, _StatusTypeDefaultValueFactory));
        static Func<EnumDocStatusType> _StatusTypeDefaultValueFactory = () => default(EnumDocStatusType);
        #endregion


        public Dictionary<EnumDocStatusType, string> ComBoxItemSource { get; set; } = new Dictionary<EnumDocStatusType, string>()
        {
            {EnumDocStatusType.Share, "共享"},
            {EnumDocStatusType.Personal, "个人"},

        };


        public bool IsFromTopBar
        {
            get { return _IsFromTopBarLocator(this).Value; }
            set { _IsFromTopBarLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool IsFromTopBar Setup        
        protected Property<bool> _IsFromTopBar = new Property<bool> { LocatorFunc = _IsFromTopBarLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsFromTopBarLocator = RegisterContainerLocator<bool>("IsFromTopBar", model => model.Initialize("IsFromTopBar", ref model._IsFromTopBar, ref _IsFromTopBarLocator, _IsFromTopBarDefaultValueFactory));
        static Func<bool> _IsFromTopBarDefaultValueFactory = () => false;
        #endregion


        public ObservableCollection<SelectItemModel> ShareRange
        {
            get { return _ShareRangeLocator(this).Value; }
            set { _ShareRangeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<SelectItemModel> ShareRange Setup        
        protected Property<ObservableCollection<SelectItemModel>> _ShareRange = new Property<ObservableCollection<SelectItemModel>> { LocatorFunc = _ShareRangeLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<SelectItemModel>>> _ShareRangeLocator = RegisterContainerLocator<ObservableCollection<SelectItemModel>>("ShareRange", model => model.Initialize("ShareRange", ref model._ShareRange, ref _ShareRangeLocator, _ShareRangeDefaultValueFactory));
        static Func<ObservableCollection<SelectItemModel>> _ShareRangeDefaultValueFactory = () => new ObservableCollection<SelectItemModel>();
        #endregion

        public ObservableCollection<SelectItemModel> SyncRange
        {
            get { return _SyncRangeLocator(this).Value; }
            set { _SyncRangeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<SelectItemModel> SyncRange Setup        
        protected Property<ObservableCollection<SelectItemModel>> _SyncRange = new Property<ObservableCollection<SelectItemModel>> { LocatorFunc = _SyncRangeLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<SelectItemModel>>> _SyncRangeLocator = RegisterContainerLocator<ObservableCollection<SelectItemModel>>("SyncRange", model => model.Initialize("SyncRange", ref model._SyncRange, ref _SyncRangeLocator, _SyncRangeDefaultValueFactory));
        static Func<ObservableCollection<SelectItemModel>> _SyncRangeDefaultValueFactory = () => new ObservableCollection<SelectItemModel>();
        #endregion

        public CommandModel<ReactiveCommand, String> CommandDeletSyncItem
        {
            get { return _CommandDeletSyncItemLocator(this).Value; }
            set { _CommandDeletSyncItemLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandDeletSyncItem Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandDeletSyncItem = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandDeletSyncItemLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandDeletSyncItemLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandDeletSyncItem", model => model.Initialize("CommandDeletSyncItem", ref model._CommandDeletSyncItem, ref _CommandDeletSyncItemLocator, _CommandDeletSyncItemDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandDeletSyncItemDefaultValueFactory =
            model =>
            {
                var state = "CommandDeletSyncItem";           // Command state  
                var commandId = "CommandDeletSyncItem";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            SelectItemModel para = (SelectItemModel) e.EventArgs.Parameter;
                            var item = vm.SyncRange.FirstOrDefault(p => p.Itemtype == para.Itemtype && p.ItemId == para.ItemId);
                            if (item == null) return;
                            var index = vm.SyncRange.IndexOf(item);
                            vm.SyncRange.RemoveAt(index);
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion

        public CommandModel<ReactiveCommand, String> CommandDeletShareItem
        {
            get { return _CommandDeletShareItemLocator(this).Value; }
            set { _CommandDeletShareItemLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandDeletShareItem Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandDeletShareItem = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandDeletShareItemLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandDeletShareItemLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandDeletShareItem", model => model.Initialize("CommandDeletShareItem", ref model._CommandDeletShareItem, ref _CommandDeletShareItemLocator, _CommandDeletShareItemDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandDeletShareItemDefaultValueFactory =
            model =>
            {
                var state = "CommandDeletShareItem";           // Command state  
                var commandId = "CommandDeletShareItem";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            SelectItemModel para = (SelectItemModel)e.EventArgs.Parameter;
                            var item = vm.ShareRange.FirstOrDefault(p => p.Itemtype == para.Itemtype && p.ItemId == para.ItemId);
                            if (item == null) return;
                            var index = vm.ShareRange.IndexOf(item);
                            vm.ShareRange.RemoveAt(index);
                            var item1 = vm.SyncRange.FirstOrDefault(p => p.Itemtype == para.Itemtype && p.ItemId == para.ItemId);
                            if (item1 == null) return;
                            var index1 = vm.SyncRange.IndexOf(item1);
                            vm.SyncRange.RemoveAt(index1);
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion


        public CommandModel<ReactiveCommand, String> CommandShowSyncDeptWin
        {
            get { return _CommandShowSyncDeptWinLocator(this).Value; }
            set { _CommandShowSyncDeptWinLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowSyncDeptWin Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowSyncDeptWin = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowSyncDeptWinLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowSyncDeptWinLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowSyncDeptWin", model => model.Initialize("CommandShowSyncDeptWin", ref model._CommandShowSyncDeptWin, ref _CommandShowSyncDeptWinLocator, _CommandShowSyncDeptWinDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowSyncDeptWinDefaultValueFactory =
            model =>
            {
                var state = "CommandShowSyncDeptWin";           // Command state  
                var commandId = "CommandShowSyncDeptWin";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            SelectDeptWindow_Model req = new SelectDeptWindow_Model();
                            if (vm.IsFromRoot)
                            {
                                req.TreeSource =
                                    new ObservableCollection<PsAndDeptTreeNodeItem>()
                                    {
                                        MapperUtil.Mapper.Map<PsAndDeptTreeNodeItem>(GlobalPara.DeptTreesSource
                                            .FirstOrDefault())
                                    };
                            }
                            else
                            {
                                var source = new ObservableCollection<PsAndDeptTreeNodeItem>();
                                foreach (var item in GlobalPara.CatalogNow.SynergyRange.departs)
                                {
                                    source.Add(new PsAndDeptTreeNodeItem()
                                    {
                                        IsChecked = false,
                                        ItemType = PsAndDeptItemtype.Dept,
                                        ItemId = item.DepartmentId,
                                        Name = item.Title
                                    });
                                }
                                req.TreeSource = source;
                            }
                            req.IsFromShare = false;
                            await vm.StageManager.DefaultStage.Show(req);
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion


        public CommandModel<ReactiveCommand, String> CommandShowSyncPersonWin
        {
            get { return _CommandShowSyncPersonWinLocator(this).Value; }
            set { _CommandShowSyncPersonWinLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowSyncPersonWin Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowSyncPersonWin = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowSyncPersonWinLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowSyncPersonWinLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowSyncPersonWin", model => model.Initialize("CommandShowSyncPersonWin", ref model._CommandShowSyncPersonWin, ref _CommandShowSyncPersonWinLocator, _CommandShowSyncPersonWinDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowSyncPersonWinDefaultValueFactory =
            model =>
            {
                var state = "CommandShowSyncPersonWin";           // Command state  
                var commandId = "CommandShowSyncPersonWin";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            SelectPersonWindow_Model req = new SelectPersonWindow_Model();
                            if (vm.IsFromRoot)
                            {
                                req.TreeSource = new ObservableCollection<PsAndDeptTreeNodeItem>()
                                {
                                    MapperUtil.Mapper.Map<PsAndDeptTreeNodeItem>(GlobalPara.PersonTreesSource.FirstOrDefault())

                                };
                            }
                            else
                            {
                                var source = new ObservableCollection<PsAndDeptTreeNodeItem>();
                                foreach (var item in GlobalPara.CatalogNow.SynergyRange.users)
                                {
                                    source.Add(new PsAndDeptTreeNodeItem()
                                    {
                                        IsChecked = false,
                                        ItemType = PsAndDeptItemtype.Person,
                                        HeadUrl = item.FaceUrl,
                                        ItemId = item.ProfileId,
                                        Name = item.Fullname
                                    });
                                }
                                if (source.All(p => p.ItemId != GlobalPara.CatalogNow.Creator.ProfileId))
                                {
                                    source.Add(new PsAndDeptTreeNodeItem()
                                    {
                                        IsChecked = false,
                                        ItemType = PsAndDeptItemtype.Person,
                                        HeadUrl = GlobalPara.CatalogNow.Creator.FaceUrl,
                                        ItemId = GlobalPara.CatalogNow.Creator.ProfileId,
                                        Name = GlobalPara.CatalogNow.Creator.Fullname
                                    });
                                }
                                
                                req.TreeSource = source;
                            }
                            req.IsFromShare = false;
                            await vm.StageManager.DefaultStage.Show(req);
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion



        public CommandModel<ReactiveCommand, String> CommandShowShareDeptWin
        {
            get { return _CommandShowShareDeptWinLocator(this).Value; }
            set { _CommandShowShareDeptWinLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowShareDeptWin Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowShareDeptWin = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowShareDeptWinLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowShareDeptWinLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowShareDeptWin", model => model.Initialize("CommandShowShareDeptWin", ref model._CommandShowShareDeptWin, ref _CommandShowShareDeptWinLocator, _CommandShowShareDeptWinDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowShareDeptWinDefaultValueFactory =
            model =>
            {
                var state = "CommandShowShareDeptWin";           // Command state  
                var commandId = "CommandShowShareDeptWin";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            SelectDeptWindow_Model req = new SelectDeptWindow_Model();
                            if (vm.IsFromRoot)
                            {
                                req.TreeSource = new ObservableCollection<PsAndDeptTreeNodeItem>()
                                {
                                    MapperUtil.Mapper.Map<PsAndDeptTreeNodeItem>(GlobalPara.DeptTreesSource
                                        .FirstOrDefault())

                                };
                            }
                            else
                            {
                                var source = new ObservableCollection<PsAndDeptTreeNodeItem>();
                                foreach (var item in GlobalPara.CatalogNow.ShareRange.departs)
                                {
                                    source.Add(new PsAndDeptTreeNodeItem()
                                    {
                                        IsChecked = false,
                                        ItemType = PsAndDeptItemtype.Dept,
                                        ItemId = item.DepartmentId,
                                        Name = item.Title
                                    });
                                }
                                req.TreeSource = source;
                            }
                            req.IsFromShare = true;
                            await vm.StageManager.DefaultStage.Show(req);
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion


        public CommandModel<ReactiveCommand, String> CommandShowSharePersonWin
        {
            get { return _CommandShowSharePersonWinLocator(this).Value; }
            set { _CommandShowSharePersonWinLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowSharePersonWin Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowSharePersonWin = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowSharePersonWinLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowSharePersonWinLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowSharePersonWin", model => model.Initialize("CommandShowSharePersonWin", ref model._CommandShowSharePersonWin, ref _CommandShowSharePersonWinLocator, _CommandShowSharePersonWinDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowSharePersonWinDefaultValueFactory =
            model =>
            {
                var state = "CommandShowSharePersonWin";           // Command state  
                var commandId = "CommandShowSharePersonWin";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            SelectPersonWindow_Model req = new SelectPersonWindow_Model();
                            if (vm.IsFromRoot)
                            {
                                req.TreeSource = new ObservableCollection<PsAndDeptTreeNodeItem>()
                                {
                                    MapperUtil.Mapper.Map<PsAndDeptTreeNodeItem>(GlobalPara.PersonTreesSource
                                        .FirstOrDefault())
                                };
                            }
                            else
                            {
                                var source = new ObservableCollection<PsAndDeptTreeNodeItem>();
                                foreach (var item in GlobalPara.CatalogNow.ShareRange.users)
                                {
                                    source.Add(new PsAndDeptTreeNodeItem()
                                    {
                                        IsChecked = false,
                                        ItemType = PsAndDeptItemtype.Person,
                                        HeadUrl = item.FaceUrl,
                                        ItemId = item.ProfileId,
                                        Name = item.Fullname
                                    });
                                }
                                if (source.All(p => p.ItemId != GlobalPara.CatalogNow.Creator.ProfileId))
                                {
                                    source.Add(new PsAndDeptTreeNodeItem()
                                    {
                                        IsChecked = false,
                                        ItemType = PsAndDeptItemtype.Person,
                                        HeadUrl = GlobalPara.CatalogNow.Creator.FaceUrl,
                                        ItemId = GlobalPara.CatalogNow.Creator.ProfileId,
                                        Name = GlobalPara.CatalogNow.Creator.Fullname
                                    });
                                }

                                req.TreeSource = source;
                            }
                            req.IsFromShare = true;
                            await vm.StageManager.DefaultStage.Show(req);
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion


        public CommandModel<ReactiveCommand, String> CommandCloseWindow
        {
            get { return _CommandCloseWindowLocator(this).Value; }
            set { _CommandCloseWindowLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandCloseWindow Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandCloseWindow = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandCloseWindowLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandCloseWindowLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandCloseWindow", model => model.Initialize("CommandCloseWindow", ref model._CommandCloseWindow, ref _CommandCloseWindowLocator, _CommandCloseWindowDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandCloseWindowDefaultValueFactory =
            model =>
            {
                var state = "CommandCloseWindow";           // Command state  
                var commandId = "CommandCloseWindow";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            var para = e.EventArgs.Parameter.ToString()=="1";
                            ChangeStatusWinPara res = new ChangeStatusWinPara()
                            {
                                newStatus = vm.StatusType,
                                IsFromTopBar=vm.IsFromTopBar
                            };
                            
                            if (vm.StatusType == EnumDocStatusType.Share)
                            {
                                foreach (var item in vm.SyncRange)
                                {
                                    if (item.Itemtype == PsAndDeptItemtype.Dept)
                                    {
                                        res.syncList.DeptList.Add(item.ItemId);
                                    }
                                    if (item.Itemtype == PsAndDeptItemtype.Person)
                                    {
                                        res.syncList.UserList.Add(item.ItemId);
                                    }
                                }
                                foreach (var item in vm.ShareRange)
                                {
                                    if (item.Itemtype == PsAndDeptItemtype.Dept)
                                    {
                                        res.shareList.DeptList.Add(item.ItemId);
                                    }
                                    if (item.Itemtype == PsAndDeptItemtype.Person)
                                    {
                                        res.shareList.UserList.Add(item.ItemId);
                                    }
                                }
                                
                            }
                            
                            vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm,
                                "MesWindowOptResEventRouter", new MesWindowResModel()
                                {
                                    IsOk = para,
                                    WinType = MesWinType.StatusChangeWin,
                                    ResData=res
                                });
                            vm.CloseViewAndDispose();
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        })
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(state);

                cmdmdl.ListenToIsUIBusy(
                    model: vm,
                    canExecuteWhenBusy: false);
                return cmdmdl;
            };

        #endregion

        #region Life Time Event Handling

        /// <summary>
        /// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        /// </summary>
        /// <param name="view">Set target</param>
        /// <param name="oldValue">Value before set.</param>
        /// <returns>Task awaiter</returns>
        protected override Task OnBindedToView(MVVMSidekick.Views.IView view, IViewModel oldValue)
        {
            
            return base.OnBindedToView(view, oldValue);
        }

        ///// <summary>
        ///// This will be invoked by view when this instance of viewmodel in ViewModel property is overwritten.
        ///// </summary>
        ///// <param name="view">Overwrite target view.</param>
        ///// <param name="newValue">The value replacing </param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnUnbindedFromView(MVVMSidekick.Views.IView view, IViewModel newValue)
        //{
        //    return base.OnUnbindedFromView(view, newValue);
        //}

        /// <summary>
        /// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        /// </summary>
        /// <param name="view">View that firing Load event</param>
        /// <returns>Task awaiter</returns>
        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            SubscribeCommand();
            _vm = Singleton<ToastInstance>.Instance;
            return base.OnBindedViewLoad(view);
        }

        /// <summary>
        /// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        /// </summary>
        /// <param name="view">View that firing Unload event</param>
        /// <returns>Task awaiter</returns>
        protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        {
            return base.OnBindedViewUnload(view);
        }

        ///// <summary>
        ///// <para>If dispose actions got exceptions, will handled here. </para>
        ///// </summary>
        ///// <param name="exceptions">
        ///// <para>The exception and dispose infomation</para>
        ///// </param>
        //protected override async void OnDisposeExceptions(IList<DisposeInfo> exceptions)
        //{
        //    base.OnDisposeExceptions(exceptions);
        //    await TaskExHelper.Yield();
        //}

        #endregion

    }

}

