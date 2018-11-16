/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： LoginWindow_Model.cs
 * * 功   能：  
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-11-13 14:50:14
 * * 修改记录： 
 * * 日期时间： 2018-11-13 14:50:14  修改人：王建军  创建
 * *******************************************************************/
using EllaMakerTool.Core;
using EllaMakerTool.Message;
using EllaMakerTool.Models;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace EllaMakerTool.WPF.ViewModels
{

    public class LoginWindow_Model : ViewModelBase<LoginWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令
        public static bool IsReload = false;

        public LoginWindow_Model()
        {
            if (IsInDesignMode )
            {
             
            }

           
        }

        //propvm tab tab string tab Title
        public CommandModel<ReactiveCommand, String> CommandNagetiveToUrl
        {
            get { return _CommandNagetiveToUrlLocator(this).Value; }
            set { _CommandNagetiveToUrlLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandNagetiveToUrl Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandNagetiveToUrl = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandNagetiveToUrlLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandNagetiveToUrlLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandNagetiveToUrl", model => model.Initialize("CommandNagetiveToUrl", ref model._CommandNagetiveToUrl, ref _CommandNagetiveToUrlLocator, _CommandNagetiveToUrlDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandNagetiveToUrlDefaultValueFactory =
            model =>
            {
                var state = "CommandNagetiveToUrl";           // Command state  
                var commandId = "CommandNagetiveToUrl";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            //调用系统默认的浏览器   
                            System.Diagnostics.Process.Start(e.EventArgs.Parameter.ToString());
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


        public UserLoginItem UserLoginInfo
        {
            get { return _UserLoginInfoLocator(this).Value; }
            set { _UserLoginInfoLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Model.UserLoginItem UserLoginInfo Setup        
        protected Property<UserLoginItem> _UserLoginInfo = new Property<UserLoginItem> { LocatorFunc = _UserLoginInfoLocator };
        static Func<BindableBase, ValueContainer<UserLoginItem>> _UserLoginInfoLocator = RegisterContainerLocator<UserLoginItem>("UserLoginInfo", model => model.Initialize("UserLoginInfo", ref model._UserLoginInfo, ref _UserLoginInfoLocator, _UserLoginInfoDefaultValueFactory));
        static Func<UserLoginItem> _UserLoginInfoDefaultValueFactory = () => new UserLoginItem();
        #endregion


        public CommandModel<ReactiveCommand, String> CommandLogin
        {
            get { return _CommandLoginLocator(this).Value; }
            set { _CommandLoginLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLogin Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandLogin = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLoginLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoginLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLogin", model => model.Initialize("CommandLogin", ref model._CommandLogin, ref _CommandLoginLocator, _CommandLoginDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoginDefaultValueFactory =
            model =>
            {
                var state = "CommandLogin";           // Command state  
                var commandId = "CommandLogin";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            Global.authToken = new AuthToken();
                            var para = e.EventArgs.Parameter as UserLoginItem;
                            var res = GlobalPara.webApis.UserLogin(para.UserName, para.Password);
                            INIOperationHelper.INIWriteValue(GlobalPara.IniPath, "User", "Name", vm.UserLoginInfo.UserName);
                            if (res.Successful)
                            {
                                if (vm.Remember)
                                {
                                    INIOperationHelper.INIWriteValue(GlobalPara.IniPath, "User", "Pwd",
                                        DESEncryptHelper.Encrypt(vm.UserLoginInfo.Password));
                                }
                                else
                                {
                                    INIOperationHelper.INIWriteValue(GlobalPara.IniPath, "User", "Pwd", "");
                                }
                                Global.authToken = new AuthToken()
                                {
                                    Token = res.Data.token,
                                    Username = res.Data.name,
                                    ID = res.Data.id,
                                    FaceUri = res.Data.face
                                };
                                vm.GlobalEventRouter.GetEventChannel(typeof(MesWindowResModel)).RaiseEvent(vm,
                                    "MesWindowOptResEventRouter", new MesWindowResModel()
                                    {
                                        IsOk = true,
                                        WinType = MesWinType.LoginWin,
                                        ResData = IsReload.ToString()
                                    });
                                vm.CloseViewAndDispose();
                            }
                            else
                            {
                                System.Windows.MessageBox.Show(res.Message);
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


        public bool Remember
        {
            get { return _RememberLocator(this).Value; }
            set
            {
                _RememberLocator(this).SetValueAndTryNotify(value);
                INIOperationHelper.INIWriteValue(GlobalPara.IniPath,"User","Remember",value?"1":"0");
            }
        }
        #region Property bool Remember Setup        
        protected Property<bool> _Remember = new Property<bool> { LocatorFunc = _RememberLocator };
        static Func<BindableBase, ValueContainer<bool>> _RememberLocator = RegisterContainerLocator<bool>("Remember", model => model.Initialize("Remember", ref model._Remember, ref _RememberLocator, _RememberDefaultValueFactory));
        static Func<bool> _RememberDefaultValueFactory = () => GlobalPara.Remember;
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

        /// <summary>
        /// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        /// </summary>
        /// <param name="view">View that firing Unload event</param>
        /// <returns>Task awaiter</returns>
        protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        {
            this.CloseViewAndDispose();
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

