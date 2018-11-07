using EllaMakerTool.Models;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using EllaMakerTool.Message;

namespace EllaMakerTool.WPF.ViewModels
{

    public class IrRecourdWindow_Model : ViewModelBase<IrRecourdWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public IrRecourdWindow_Model()
        {
            if (IsInDesignMode)
            {

            }
        }

        public string FileNameStr
        {
            get { return _FileNameStrLocator(this).Value; }
            set { _FileNameStrLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string FileNameStr Setup        
        protected Property<string> _FileNameStr = new Property<string> { LocatorFunc = _FileNameStrLocator };
        static Func<BindableBase, ValueContainer<string>> _FileNameStrLocator = RegisterContainerLocator<string>("FileNameStr", model => model.Initialize("FileNameStr", ref model._FileNameStr, ref _FileNameStrLocator, _FileNameStrDefaultValueFactory));
        static Func<string> _FileNameStrDefaultValueFactory = () => default(string);
        #endregion


        public CommandModel<ReactiveCommand, String> CommandDownLoadSingle
        {
            get { return _CommandDownLoadSingleLocator(this).Value; }
            set { _CommandDownLoadSingleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandDownLoadSingle Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandDownLoadSingle = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandDownLoadSingleLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandDownLoadSingleLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandDownLoadSingle", model => model.Initialize("CommandDownLoadSingle", ref model._CommandDownLoadSingle, ref _CommandDownLoadSingleLocator, _CommandDownLoadSingleDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandDownLoadSingleDefaultValueFactory =
            model =>
            {
                var state = "CommandDownLoadSingle";           // Command state  
                var commandId = "CommandDownLoadSingle";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            try
                            {
                                var para = (IterationItem)e.EventArgs.Parameter;
                                var req = new DownSavePathMesWindow_Model()
                                {
                                    FileName = vm.FileNameStr,
                                    SavePath = GlobalPara.DefaultSavePath,
                                    Size = para.Size,
                                    URL = para.Url,
                                    FullFileName= vm.FileNameStr
                                };
                                if (req.FileName.Length > 10)
                                {
                                    req.FileName = req.FileName.Substring(0, 10) + "...";
                                }
                                await vm.StageManager.DefaultStage.Show(req);
                            }
                            catch (Exception ex)
                            {
                                System.Windows.MessageBox.Show(ex+"","警告");
                                
                            }

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


        public ObservableCollection<IterationItem> IrData
        {
            get { return _IrDataLocator(this).Value; }
            set { _IrDataLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<IterationItem> IrData Setup        
        protected Property<ObservableCollection<IterationItem>> _IrData = new Property<ObservableCollection<IterationItem>> { LocatorFunc = _IrDataLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<IterationItem>>> _IrDataLocator = RegisterContainerLocator<ObservableCollection<IterationItem>>("IrData", model => model.Initialize("IrData", ref model._IrData, ref _IrDataLocator, _IrDataDefaultValueFactory));
        static Func<ObservableCollection<IterationItem>> _IrDataDefaultValueFactory = () => new ObservableCollection<IterationItem>();
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

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            var para = e.EventArgs.Parameter.ToString() == "1";
                            vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm, "MesWindowOptResEventRouter", new MesWindowResModel()
                            {
                                IsOk = para,
                                WinType = MesWinType.UpdateLogWin,
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
        //    this.CloseViewAndDispose();
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

