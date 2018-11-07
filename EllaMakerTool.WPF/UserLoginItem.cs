using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MVVMSidekick.ViewModels;

namespace EllaMakerTool.WPF
{
    public class UserLoginItem: BindableBase<UserLoginItem>
    {

        public string UserName
        {
            get { return _UserNameLocator(this).Value; }
            set { _UserNameLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string UserName Setup        
        protected Property<string> _UserName = new Property<string> { LocatorFunc = _UserNameLocator };
        static Func<BindableBase, ValueContainer<string>> _UserNameLocator = RegisterContainerLocator<string>("UserName", model => model.Initialize("UserName", ref model._UserName, ref _UserNameLocator, _UserNameDefaultValueFactory));
        static Func<string> _UserNameDefaultValueFactory = () => GlobalPara.UserName;
        #endregion


        public string Password
        {
            get { return _PasswordLocator(this).Value; }
            set { _PasswordLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Password Setup        
        protected Property<string> _Password = new Property<string> { LocatorFunc = _PasswordLocator };
        static Func<BindableBase, ValueContainer<string>> _PasswordLocator = RegisterContainerLocator<string>("Password", model => model.Initialize("Password", ref model._Password, ref _PasswordLocator, _PasswordDefaultValueFactory));
        static Func<string> _PasswordDefaultValueFactory = () => GlobalPara.UserPwd;
        #endregion

    }
}
