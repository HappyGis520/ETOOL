using EllaMakerTool.Models;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using EllaMakerTool.Message;

namespace EllaMakerTool.WPF.ViewModels
{

    public class UploadMesWindow_Model : ViewModelBase<UploadMesWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public UploadMesWindow_Model()
        {
            if (IsInDesignMode)
            {

            }
            GlobalPara.UploadItems = new List<DocBaseInfoApiModel>();
        }


        //propvm tab tab string tab Title





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
                            vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm, "MesWindowOptResEventRouter", new MesWindowResModel()
                            {
                                IsOk = true,
                                WinType = MesWinType.UploadWin,
                                ResData = ""
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

        #endregion

    }

}

