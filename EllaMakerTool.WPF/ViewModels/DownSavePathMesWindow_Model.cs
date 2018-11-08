using EllaMakerTool.WPF.Helper;
using EllaMakerTool.Models;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Reactive.Linq;
using System.Windows.Forms;
using EllaMakerTool.Core;

namespace EllaMakerTool.WPF.ViewModels
{

    public class DownSavePathMesWindow_Model : ViewModelBase<DownSavePathMesWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public DownSavePathMesWindow_Model()
        {
            if (IsInDesignMode )
            {
             
            }
        
        }


        //propvm tab tab string tab Title


        public string FullFileName
        {
            get { return _FullFileNameLocator(this).Value; }
            set { _FullFileNameLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string FullFileName Setup        
        protected Property<string> _FullFileName = new Property<string> { LocatorFunc = _FullFileNameLocator };
        static Func<BindableBase, ValueContainer<string>> _FullFileNameLocator = RegisterContainerLocator<string>("FullFileName", model => model.Initialize("FullFileName", ref model._FullFileName, ref _FullFileNameLocator, _FullFileNameDefaultValueFactory));
        static Func<string> _FullFileNameDefaultValueFactory = () => "";
        #endregion


        public string URL
        {
            get { return _URLLocator(this).Value; }
            set { _URLLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string URL Setup        
        protected Property<string> _URL = new Property<string> { LocatorFunc = _URLLocator };
        static Func<BindableBase, ValueContainer<string>> _URLLocator = RegisterContainerLocator<string>("URL", model => model.Initialize("URL", ref model._URL, ref _URLLocator, _URLDefaultValueFactory));
        static Func<string> _URLDefaultValueFactory = () => "";
        #endregion


        public string SavePath
        {
            get { return _SavePathLocator(this).Value; }
            set { _SavePathLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string SavePath Setup        
        protected Property<string> _SavePath = new Property<string> { LocatorFunc = _SavePathLocator };
        static Func<BindableBase, ValueContainer<string>> _SavePathLocator = RegisterContainerLocator<string>("SavePath", model => model.Initialize("SavePath", ref model._SavePath, ref _SavePathLocator, _SavePathDefaultValueFactory));
        static Func<string> _SavePathDefaultValueFactory = () => "";
        #endregion


        public string FileName
        {
            get { return _FileNameLocator(this).Value; }
            set { _FileNameLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string FileName Setup        
        protected Property<string> _FileName = new Property<string> { LocatorFunc = _FileNameLocator };
        static Func<BindableBase, ValueContainer<string>> _FileNameLocator = RegisterContainerLocator<string>("FileName", model => model.Initialize("FileName", ref model._FileName, ref _FileNameLocator, _FileNameDefaultValueFactory));
        static Func<string> _FileNameDefaultValueFactory = () => GlobalPara.DefaultSavePath??"";
        #endregion


        public long Size
        {
            get { return _SizeLocator(this).Value; }
            set { _SizeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property long Size Setup        
        protected Property<long> _Size = new Property<long> { LocatorFunc = _SizeLocator };
        static Func<BindableBase, ValueContainer<long>> _SizeLocator = RegisterContainerLocator<long>("Size", model => model.Initialize("Size", ref model._Size, ref _SizeLocator, _SizeDefaultValueFactory));
        static Func<long> _SizeDefaultValueFactory = () => default(long);
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
                            if (!para)
                            {
                                vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm, "MesWindowOptResEventRouter", new MesWindowResModel()
                                {
                                    IsOk = para,
                                    WinType = MesWinType.DownSaveWin
                                });
                                vm.CloseViewAndDispose();
                            }
                            var data = new DownSaveWinParaModel()
                            {
                                Filepath = System.IO.Path.Combine(vm.SavePath, vm.FullFileName),
                                Url = vm.URL
                            };
                            vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm, "MesWindowOptResEventRouter", new MesWindowResModel()
                            {
                                IsOk = para,
                                WinType = MesWinType.DownSaveWin,
                                ResData = data
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

        

        public CommandModel<ReactiveCommand, String> CommandBrosPath
        {
            get { return _CommandBrosPathLocator(this).Value; }
            set { _CommandBrosPathLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandBrosPath Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandBrosPath = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandBrosPathLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandBrosPathLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandBrosPath", model => model.Initialize("CommandBrosPath", ref model._CommandBrosPath, ref _CommandBrosPathLocator, _CommandBrosPathDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandBrosPathDefaultValueFactory =
            model =>
            {
                var state = "CommandBrosPath";           // Command state  
                var commandId = "CommandBrosPath";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            FolderBrowserDialog dialog = new FolderBrowserDialog();
                            dialog.Description = "请选择保存路径";
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string foldPath = dialog.SelectedPath;
                                vm.SavePath = foldPath;
                                GlobalPara.DefaultSavePath = foldPath;
                                INIOperationHelper.INIWriteValue(GlobalPara.IniPath, "System", "DefaultSavePath",
                                    foldPath);
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

