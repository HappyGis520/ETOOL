using EllaMakerTool.WPF.Helper;
using EllaMakerTool.Models;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace EllaMakerTool.WPF.ViewModels
{

    public class SelectDeptWindow_Model : ViewModelBase<SelectDeptWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public SelectDeptWindow_Model()
        {
            if (IsInDesignMode)
            {

            }

        }

        public bool IsFromShare
        {
            get { return _IsFromShareLocator(this).Value; }
            set { _IsFromShareLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool IsFromShare Setup        
        protected Property<bool> _IsFromShare = new Property<bool> { LocatorFunc = _IsFromShareLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsFromShareLocator = RegisterContainerLocator<bool>("IsFromShare", model => model.Initialize("IsFromShare", ref model._IsFromShare, ref _IsFromShareLocator, _IsFromShareDefaultValueFactory));
        static Func<bool> _IsFromShareDefaultValueFactory = () => false;
        #endregion

        //propvm tab tab string tab Title
        public ObservableCollection<PsAndDeptTreeNodeItem> TreeSource
        {
            get { return _TreeSourceLocator(this).Value; }
            set { _TreeSourceLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<PsAndDeptTreeNodeItem> TreeSource Setup        
        protected Property<ObservableCollection<PsAndDeptTreeNodeItem>> _TreeSource = new Property<ObservableCollection<PsAndDeptTreeNodeItem>> { LocatorFunc = _TreeSourceLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<PsAndDeptTreeNodeItem>>> _TreeSourceLocator = RegisterContainerLocator<ObservableCollection<PsAndDeptTreeNodeItem>>("TreeSource", model => model.Initialize("TreeSource", ref model._TreeSource, ref _TreeSourceLocator, _TreeSourceDefaultValueFactory));
        static Func<ObservableCollection<PsAndDeptTreeNodeItem>> _TreeSourceDefaultValueFactory = () => new ObservableCollection<PsAndDeptTreeNodeItem>();
        #endregion


        //propvm tab tab string tab Title


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

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            var para = e.EventArgs.Parameter.ToString() == "1";
                            if (!para) vm.CloseViewAndDispose();
                            string EventRouterName = "SelectDeptResFromSyncEventRouter";
                            if (vm.IsFromShare)
                                EventRouterName= "SelectDeptResFromShareEventRouter";
                            vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm, EventRouterName, new MesWindowResModel()
                            {
                                IsOk = para,
                                WinType = MesWinType.DeptSelectWin,
                                ResData = CollectHelper.GetSelecItems(vm.TreeSource.ToList()),
                                
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

        ///// <summary>
        ///// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        ///// </summary>
        ///// <param name="view">Set target</param>
        ///// <param name="oldValue">Value before set.</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedToView(MVVMSidekick.Views.IView view, IViewModel oldValue)
        //{
        //    return base.OnBindedToView(view, oldValue);
        //}

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

        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewLoad(view);
        //}

        ///// <summary>
        ///// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Unload event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewUnload(view);
        //}

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

