using EllaMakerTool.Api;
using EllaMakerTool.Models;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Reactive.Linq;
using EllaMakerTool.Message;

namespace EllaMakerTool.WPF.ViewModels
{

    public class RenameWindow_Model : ViewModelBase<RenameWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public RenameWindow_Model()
        {
            if (IsInDesignMode)
            {


            }

        }


        public string CataLogId
        {
            get { return _CataLogIdLocator(this).Value; }
            set { _CataLogIdLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string CataLogId Setup        
        protected Property<string> _CataLogId = new Property<string> { LocatorFunc = _CataLogIdLocator };
        static Func<BindableBase, ValueContainer<string>> _CataLogIdLocator = RegisterContainerLocator<string>("CataLogId", model => model.Initialize("CataLogId", ref model._CataLogId, ref _CataLogIdLocator, _CataLogIdDefaultValueFactory));
        static Func<string> _CataLogIdDefaultValueFactory = () => default(string);
        #endregion


        public string OriginName
        {
            get { return _OriginNameLocator(this).Value; }
            set { _OriginNameLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string OriginName Setup        
        protected Property<string> _OriginName = new Property<string> { LocatorFunc = _OriginNameLocator };
        static Func<BindableBase, ValueContainer<string>> _OriginNameLocator = RegisterContainerLocator<string>("OriginName", model => model.Initialize("OriginName", ref model._OriginName, ref _OriginNameLocator, _OriginNameDefaultValueFactory));
        static Func<string> _OriginNameDefaultValueFactory = () => "";
        #endregion


        public bool IsFolder
        {
            get { return _IsFolderLocator(this).Value; }
            set { _IsFolderLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool IsFolder Setup        
        protected Property<bool> _IsFolder = new Property<bool> { LocatorFunc = _IsFolderLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsFolderLocator = RegisterContainerLocator<bool>("IsFolder", model => model.Initialize("IsFolder", ref model._IsFolder, ref _IsFolderLocator, _IsFolderDefaultValueFactory));
        static Func<bool> _IsFolderDefaultValueFactory = () => false;
        #endregion


        public string DocId
        {
            get { return _DocIdLocator(this).Value; }
            set { _DocIdLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string DocId Setup        
        protected Property<string> _DocId = new Property<string> { LocatorFunc = _DocIdLocator };
        static Func<BindableBase, ValueContainer<string>> _DocIdLocator = RegisterContainerLocator<string>("DocId", model => model.Initialize("DocId", ref model._DocId, ref _DocIdLocator, _DocIdDefaultValueFactory));
        static Func<string> _DocIdDefaultValueFactory = () => "";
        #endregion


        public string Extension
        {
            get { return _ExtensionLocator(this).Value; }
            set { _ExtensionLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Extension Setup        
        protected Property<string> _Extension = new Property<string> { LocatorFunc = _ExtensionLocator };
        static Func<BindableBase, ValueContainer<string>> _ExtensionLocator = RegisterContainerLocator<string>("Extension", model => model.Initialize("Extension", ref model._Extension, ref _ExtensionLocator, _ExtensionDefaultValueFactory));
        static Func<string> _ExtensionDefaultValueFactory = () => "";
        #endregion


        public string DefalutName
        {
            get { return _DefalutNameLocator(this).Value; }
            set { _DefalutNameLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string DefalutName Setup        
        protected Property<string> _DefalutName = new Property<string> { LocatorFunc = _DefalutNameLocator };
        static Func<BindableBase, ValueContainer<string>> _DefalutNameLocator = RegisterContainerLocator<string>("DefalutName", model => model.Initialize("DefalutName", ref model._DefalutName, ref _DefalutNameLocator, _DefalutNameDefaultValueFactory));
        static Func<string> _DefalutNameDefaultValueFactory = () => "";
        #endregion


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
                                    IsOk = false,
                                    WinType = MesWinType.RenameWin
                                });
                                vm.CloseViewAndDispose();
                            }
                            vm.DefalutName = vm.DefalutName.Trim().ToApiSafeStr();
                            if (string.IsNullOrEmpty(vm.DefalutName))
                            {
                                vm.TipStr = "名称不可为空！";
                                return;
                            }
                            
                            var fullname = "";
                            vm.DefalutName =Utility.ReplaceInviadCharInFileName(vm.DefalutName);
                            if (vm.DefalutName.Length > 30)
                            {
                                vm.DefalutName = vm.DefalutName.Substring(0, 30);
                            }
                            if (para)
                            {
                                if (vm.IsFolder) fullname = vm.DefalutName;
                                else fullname = vm.DefalutName + "." + vm.Extension;
                                if (vm.OriginName != vm.DefalutName)
                                {
                                    var res = GlobalPara.webApis.HasSameName(vm.CataLogId, vm.DefalutName,
                                        vm.IsFolder ? EnumDocType.Catalog : EnumDocType.File);
                                    if (!res.Successful)
                                    {
                                        vm.TipStr = res.Message;
                                        return;
                                    }
                                }
                            }

                            RenameWinParaModel req = new RenameWinParaModel()
                            {
                                FullName = fullname,
                                IsFolder = vm.IsFolder,
                                NewName = fullname,
                                CatalogId = vm.DocId
                            };
                            vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm, "MesWindowOptResEventRouter", new MesWindowResModel()
                            {
                                IsOk = para,
                                WinType = MesWinType.RenameWin,
                                ResData = req
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

