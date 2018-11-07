using EllaMakerTool.Models;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace EllaMakerTool.WPF.ViewModels
{

    public class DelconfirmWindow_Model : ViewModelBase<DelconfirmWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public DelconfirmWindow_Model()
        {
            if (IsInDesignMode )
            {
             
            }
        
        }

        public string TipStr
        {
            get { return _TipStrLocator(this).Value; }
            set { _TipStrLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string TipStr Setup        
        protected Property<string> _TipStr = new Property<string> { LocatorFunc = _TipStrLocator };
        static Func<BindableBase, ValueContainer<string>> _TipStrLocator = RegisterContainerLocator<string>("TipStr", model => model.Initialize("TipStr", ref model._TipStr, ref _TipStrLocator, _TipStrDefaultValueFactory));
        static Func<string> _TipStrDefaultValueFactory = () => "";
        #endregion


        public List<string> folderIds
        {
            get { return _folderIdsLocator(this).Value; }
            set { _folderIdsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property List<string> folderIds Setup        
        protected Property<List<string>> _folderIds = new Property<List<string>> { LocatorFunc = _folderIdsLocator };
        static Func<BindableBase, ValueContainer<List<string>>> _folderIdsLocator = RegisterContainerLocator<List<string>>("folderIds", model => model.Initialize("folderIds", ref model._folderIds, ref _folderIdsLocator, _folderIdsDefaultValueFactory));
        static Func<List<string>> _folderIdsDefaultValueFactory = () => new List<string>();
        #endregion



        public List<string> fileIds
        {
            get { return _fileIdsLocator(this).Value; }
            set { _fileIdsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property List<string> fileIds Setup        
        protected Property<List<string>> _fileIds = new Property<List<string>> { LocatorFunc = _fileIdsLocator };
        static Func<BindableBase, ValueContainer<List<string>>> _fileIdsLocator = RegisterContainerLocator<List<string>>("fileIds", model => model.Initialize("fileIds", ref model._fileIds, ref _fileIdsLocator, _fileIdsDefaultValueFactory));
        static Func<List<string>> _fileIdsDefaultValueFactory = () => new List<string>();
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
                            if (!para)
                            {
                                vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm, "MesWindowOptResEventRouter", new MesWindowResModel()
                                {
                                    IsOk = para,
                                    WinType = MesWinType.DelConfirmWin
                                });
                                vm.CloseViewAndDispose();
                            }
                            DelConfirmWinParaModel data = new DelConfirmWinParaModel()
                            {
                                fileIds=vm.fileIds,
                                folderIds=vm.folderIds
                            };
                            vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm, "MesWindowOptResEventRouter", new MesWindowResModel()
                            {
                                IsOk = para,
                                WinType = MesWinType.DelConfirmWin,
                                ResData= data
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

