using EllaMakerTool.Core;
using EllaMakerTool.Core.FTP;
using EllaMakerTool.WPF.Controls.UserControls;
using EllaMakerTool.Converter;
using EllaMakerTool.WPF.Startups;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EllaMakerTool.Message;
using EllaMakerTool.Models;

namespace EllaMakerTool.WPF.ViewModels
{

    public class MainWindow_Model : ViewModelBase<MainWindow_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public MainWindow_Model()
        {
            if (IsInDesignMode)
            {

            }
            SubscribeEventHandle();
        }

        public long lastSize = 0;
        private ToastInstance _vm;
        public bool IsFTPRoot = true;
        public FTPListItem FTPItem = null;


        /// <summary>
        /// 订阅事件
        /// </summary>
        private void SubscribeEventHandle()
        {
            //注册全局事件路由

            //加载图书列表数据
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<BookListByPageParam>()
                .Where(x => x.EventName == Global.RefreshBookListData).Subscribe(
                    async e =>
                    {
                        var Param = e.EventData;
                        var res = GlobalPara.webApis.AllBookListByPage(Param);
                        if (res.Successful)
                        {
                            BroswerPathStr = "图书资源--图书列表";
                            //GlobalPara.CatalogNow = res.Data;
                             ClearData();
                            foreach (var item in res.Data.Items)
                            {
                                BookListData.Add(MapperUtil.Mapper.Map<BookListItem>(item));
                            }
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        }
                        else
                        {
                            if (res.Code.Equals(Global.ERROR_TOKEN))
                            {
                                MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<string>(null, "",
                                    Global.ReLoginMSG);
                            }
                            else
                            {
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    _vm.ShowError(res.Message);
                                }));
                            }
                        }
                    }).DisposeWith(this);

            //加载动画书列表数据
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<EBookListByPageParam>()
                .Where(x => x.EventName == Global.RefreshEBookListData).Subscribe(
                    async e =>
                    {
                        var Param = e.EventData;
                        var res = GlobalPara.webApis.AllEBookListByPage(Param);
                        if (res.Successful)
                        {
                            BroswerPathStr = "动画资源--动画书列表";
                            //GlobalPara.CatalogNow = res.Data;
                            ClearData();
                            foreach (var item in res.Data)
                            {
                                EBookListData.Add(MapperUtil.Mapper.Map<EBookListItem>(item));
                            }
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        }
                        else
                        {
                            if (res.Code.Equals(Global.ERROR_TOKEN))
                            {
                                MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<bool>(null,true,
                                    Global.ReLoginMSG);
                            }
                            else
                            {
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    _vm.ShowError(res.Message);
                                }));
                            }
                        }
                    }).DisposeWith(this);

            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<string>()
                .Where(x => x.EventName == "AddSyncFileEventRouter").Subscribe(
                    async e =>
                    {
                        var para = e.EventData;
                        var res = GlobalPara.webApis.AddSyncFile(DgSelectItem.FileID, para, para, lastSize);
                        if (res.Successful)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                _vm.ShowInformation("上传完成");
                            }));
                            this.Dispatcher.BeginInvoke((Action) RefreshFilesList);
                        }
                        else
                        {
                            //if (res.Code.Equals(Global.ERROR_TOKEN))
                            //{
                            //    MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<string>(null, "",
                            //        Global.ReLoginMSG);
                            //}
                            //else
                            //{
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    _vm.ShowError(res.Message);
                                }));
                            //}


                        }
                        await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    })
                .DisposeWith(this);
            //
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
                .Where(x => x.EventName == "RefreshFilesListEventRouter").Subscribe(
                    async e =>
                    {
                        this.Dispatcher.BeginInvoke((Action) RefreshFilesList);
                        await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    })
                .DisposeWith(this);

            //AllCheckedEventRouter
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<string>()
                .Where(x => x.EventName == "AllCheckedEventRouter").Subscribe(
                    async e =>
                    {
                        if (IsAllCheck) return;
                        IsAllCheck = !IsAllCheck;
                        FileBroswerData.ForEach(p => p.IsChecked = IsAllCheck);
                        await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    })
                .DisposeWith(this);

            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<string>()
                .Where(x => x.EventName == "AllUnCheckedEventRouter").Subscribe(
                    async e =>
                    {
                        if (!IsAllCheck) return;
                        IsAllCheck = !IsAllCheck;
                        FileBroswerData.ForEach(p => p.IsChecked = IsAllCheck);
                        await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    })
                .DisposeWith(this);

            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<MesWindowResModel>()
                .Where(x => x.EventName == "MesWindowOptResEventRouter").Subscribe(
                    async e =>
                    {
                        IsUIBusy = false;
                        this.Dispatcher.BeginInvoke((Action) MainWinGetFocus);
                        var para = e.EventData;
                        switch (para.WinType)
                        {
                            case MesWinType.LoginWin:
                                if (para.IsOk)
                                {
                                    if(para.ResData.Equals("true"))
                                    UserNick = Global.authToken.Username;
                                    //头像
                                    //HeadImageSource =
                                    //    $"{Global.ImageServerAdress}{string.Format(Global.authToken.Profile.FaceUrl, 40, 40, "c")}";
                                }
                                break;
                            case MesWinType.UploadWin:
                                if (para.IsOk)
                                {
                                    if (GlobalPara.UploadItems.Count > 0 && GlobalPara.rootTypeNow == 2)
                                    {
                                        var reqVm = new ChangeStatusWindow_Model();
                                        reqVm.CanStatusChange = false;
                                        reqVm.IsFromTopBar = true;
                                        if (GlobalPara.CatalogNow.pathInfo.Count < 2)
                                            reqVm.IsFromRoot = true;
                                        else
                                        {
                                            reqVm.IsFromRoot = false;
                                        }
                                        reqVm.StatusType = EnumDocStatusType.Share;
                                        reqVm.OpenSync = false;
                                        reqVm.ShareRange = new ObservableCollection<SelectItemModel>();
                                        reqVm.SyncRange = new ObservableCollection<SelectItemModel>();
                                        await this.StageManager.DefaultStage.Show(reqVm);
                                    }
                                    RefreshFilesList();
                                    GetCompanySotreStatus();
                                }
                                break;
                            case MesWinType.CataLogAddWin:
                                if (para.IsOk)
                                {
                                    CreatnewFolderWinPara paras = (CreatnewFolderWinPara) para.ResData;
                                    var res = CreateNewFolder(paras.newName);
                                    if (res.Successful)
                                    {
                                        if (paras.newStatus == EnumDocStatusType.Share)
                                        {
                                            SetShareRange(res.Data, paras.shareList, EnumDocType.Catalog);
                                            SetSyncRange(res.Data, paras.syncList, EnumDocType.Catalog);
                                        }
                                        SetNewStatus(res.Data, paras.newStatus);
                                        this.Dispatcher.BeginInvoke((Action) RefreshFilesList);
                                    }

                                }
                                break;
                            case MesWinType.DownSaveWin:
                                if (para.IsOk)
                                {
                                    DownSaveWinParaModel paras = (DownSaveWinParaModel) para.ResData;
                                    FTPClient fTPClient = new FTPClient(GlobalPara.SourceServerAdress,
                                        GlobalPara.SourceUserName, GlobalPara.SourcePwd, FTPModel.ASCII,
                                        Encoding.Default);
                                    fTPClient.OnCompleted += FTPClient_OnDownloadCompleted;
                                    fTPClient.OnProgressChanged += FTPClient_OnDownloadProgressChanged;
                                    fTPClient.Download(paras.Filepath,
                                        System.IO.Path.Combine(GlobalPara.SourceServerAdress, paras.Url));
                                }
                                break;
                            case MesWinType.RenameWin:
                                if (para.IsOk)
                                {
                                    RenameWinParaModel paras = (RenameWinParaModel) para.ResData;
                                    if (paras.IsFolder)
                                    {
                                        RenameFloder(paras.NewName, paras.CatalogId);
                                    }
                                    else
                                    {
                                        RenameFile(paras.NewName, paras.CatalogId);
                                    }

                                }
                                break;
                            case MesWinType.DelConfirmWin:
                                if (para.IsOk)
                                {
                                    DelConfirmWinParaModel paras = (DelConfirmWinParaModel) para.ResData;
                                    DocumentDeleteRequest req = new DocumentDeleteRequest
                                    {
                                        fileIds = paras.fileIds,
                                        folderIds = paras.folderIds
                                    };
                                    DeleteDoc(req);
                                }
                                break;
                            case MesWinType.StatusChangeWin:
                                if (para.IsOk)
                                {
                                    ChangeStatusWinPara paras = (ChangeStatusWinPara) para.ResData;
                                    if (!paras.IsFromTopBar)
                                    {
                                        if (paras.newStatus == EnumDocStatusType.Share)
                                        {
                                            SetShareRange(paras.shareList);
                                            SetSyncRange(paras.syncList);
                                        }
                                        SetNewStatus(paras.newStatus);
                                    }
                                    else
                                    {
                                        foreach (var item in GlobalPara.UploadItems)
                                        {
                                            if (paras.newStatus == EnumDocStatusType.Share)
                                            {
                                                SetShareRange(item, paras.shareList);
                                                SetSyncRange(item, paras.syncList);
                                            }
                                            SetNewStatus(item, paras.newStatus, item.type);
                                        }
                                        GlobalPara.UploadItems = new List<DocBaseInfoApiModel>();
                                    }

                                }
                                break;
                        }

                        await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    })
                .DisposeWith(this);

        }


        #region 绑定数据

        private void ClearData()
        {
            BookListData.Clear();
            EBookListData.Clear();
            FileBroswerData.Clear();
        }


        public bool IsAllCheck
        {
            get { return _IsAllCheckLocator(this).Value; }
            set
            {
                _IsAllCheckLocator(this).SetValueAndTryNotify(value);
                if (!value)
                    SetMainWinCkall();
            }
        }

        #region Property bool IsAllCheck Setup        

        protected Property<bool> _IsAllCheck = new Property<bool> {LocatorFunc = _IsAllCheckLocator};

        static Func<BindableBase, ValueContainer<bool>> _IsAllCheckLocator = RegisterContainerLocator<bool>(
            "IsAllCheck",
            model =>
                model.Initialize("IsAllCheck", ref model._IsAllCheck, ref _IsAllCheckLocator,
                    _IsAllCheckDefaultValueFactory));

        static Func<bool> _IsAllCheckDefaultValueFactory = () => false;

        #endregion


        /// <summary>
        /// 图书列表数据
        /// </summary>
        public ObservableCollection<BookListItem> BookListData
        {
            get { return _BookListDataLocator(this).Value; }
            set { _BookListDataLocator(this).SetValueAndTryNotify(value); }
        }

        #region Property ObservableCollection<BookListItem> BookListData Setup        

        protected Property<ObservableCollection<BookListItem>> _BookListData =
            new Property<ObservableCollection<BookListItem>> {LocatorFunc = _BookListDataLocator};

        static Func<BindableBase, ValueContainer<ObservableCollection<BookListItem>>> _BookListDataLocator =
            RegisterContainerLocator<ObservableCollection<BookListItem>>("BookListData",
                model =>
                    model.Initialize("BookListData", ref model._BookListData, ref _BookListDataLocator,
                        _BookListDataDefaultValueFactory));

        static Func<ObservableCollection<BookListItem>> _BookListDataDefaultValueFactory =
            () => new ObservableCollection<BookListItem>();

        #endregion

        /// <summary>
        /// 动画书列表数据
        /// </summary>
        public ObservableCollection<EBookListItem> EBookListData
        {
            get { return _EBookListDataLocator(this).Value; }
            set { _EBookListDataLocator(this).SetValueAndTryNotify(value); }
        }

        #region Property ObservableCollection<EBookListItem> EBookListData Setup        

        protected Property<ObservableCollection<EBookListItem>> _EBookListData =
            new Property<ObservableCollection<EBookListItem>> {LocatorFunc = _EBookListDataLocator};

        static Func<BindableBase, ValueContainer<ObservableCollection<EBookListItem>>> _EBookListDataLocator =
            RegisterContainerLocator<ObservableCollection<EBookListItem>>("EBookListData",
                model =>
                    model.Initialize("EBookListData", ref model._EBookListData, ref _EBookListDataLocator,
                        _EBookListDataDefaultValueFactory));

        static Func<ObservableCollection<EBookListItem>> _EBookListDataDefaultValueFactory =
            () => new ObservableCollection<EBookListItem>();

        #endregion

        /// <summary>
        /// FTP目录或文件列表
        /// </summary>
        public ObservableCollection<FTPListItem> FileBroswerData
        {
            get { return _FileBroswerDataLocator(this).Value; }
            set { _FileBroswerDataLocator(this).SetValueAndTryNotify(value); }
        }

        #region Property ObservableCollection<DocumentV1ApiModel> FileBroswerData Setup        

        protected Property<ObservableCollection<FTPListItem>> _FileBroswerData =
            new Property<ObservableCollection<FTPListItem>> {LocatorFunc = _FileBroswerDataLocator};

        static Func<BindableBase, ValueContainer<ObservableCollection<FTPListItem>>> _FileBroswerDataLocator =
            RegisterContainerLocator<ObservableCollection<FTPListItem>>("FileBroswerData",
                model =>
                    model.Initialize("FileBroswerData", ref model._FileBroswerData, ref _FileBroswerDataLocator,
                        _FileBroswerDataDefaultValueFactory));

        static Func<ObservableCollection<FTPListItem>> _FileBroswerDataDefaultValueFactory =
            () => new ObservableCollection<FTPListItem>();

        #endregion

        public FTPListItem DgSelectItem
        {
            get { return _DgSelectItemLocator(this).Value; }
            set
            {
                try
                {
                    _DgSelectItemLocator(this).SetValueAndTryNotify(value);
                    if (value != null)
                    {
                        var ent = FileBroswerData.FirstOrDefault(p => p.FileID == value.FileID);
                        ent.IsChecked = !ent.IsChecked;
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "");
                }

            }
        }
        #region Property FTPListItem DgSelectItem Setup        
        protected Property<FTPListItem> _DgSelectItem = new Property<FTPListItem> { LocatorFunc = _DgSelectItemLocator };
        static Func<BindableBase, ValueContainer<FTPListItem>> _DgSelectItemLocator = RegisterContainerLocator<FTPListItem>("DgSelectItem", model => model.Initialize("DgSelectItem", ref model._DgSelectItem, ref _DgSelectItemLocator, _DgSelectItemDefaultValueFactory));
        static Func<FTPListItem> _DgSelectItemDefaultValueFactory = () => default(FTPListItem);
        #endregion

        #endregion


        #region  命令


        //动画书列表中双击某一行
        public CommandModel<ReactiveCommand, String> CommandEBookBrowserLeftDoubleClick
        {
            get { return _CommandEBookBrowserLeftDoubleClickLocator(this).Value; }
            set { _CommandEBookBrowserLeftDoubleClickLocator(this).SetValueAndTryNotify(value); }
        }

        #region Property CommandModel<ReactiveCommand, String> CommandEBookBrowserLeftDoubleClick Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandEBookBrowserLeftDoubleClick =
            new Property<CommandModel<ReactiveCommand, String>>
            {
                LocatorFunc = _CommandEBookBrowserLeftDoubleClickLocator
            };

        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>>
            _CommandEBookBrowserLeftDoubleClickLocator =
                RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandEBookBrowserLeftDoubleClick",
                    model =>
                        model.Initialize("CommandEBookBrowserLeftDoubleClick",
                            ref model._CommandEBookBrowserLeftDoubleClick,
                            ref _CommandEBookBrowserLeftDoubleClickLocator,
                            _CommandEBookBrowserLeftDoubleClickDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>>
            _CommandEBookBrowserLeftDoubleClickDefaultValueFactory =
                model =>
                {
                    var state = Global.LoadFTPExplorerMSG; // Command state  
                    var commandId = Global.LoadFTPExplorerMSG;
                    var vm = CastToCurrentType(model);
                    var cmd = new ReactiveCommand(canExecute: true) {ViewModel = model}; //New Command Core

                    cmd.DoExecuteUITask(
                            vm,
                            async e =>
                            {
                                vm.IsUIBusy = true;
                                EBookListItem para = (EBookListItem) vm.dgSelectEBookItem;
                                if (para != null && !string.IsNullOrEmpty(para.id))
                                {
                                    vm.FillFilesFromFTPRoot(para.id, false, true);
                                }
                                vm.IsUIBusy = false;
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


        //图书列表中双击某一行
        public CommandModel<ReactiveCommand, String> CommandBookBrowserLeftDoubleClick
        {
            get { return _CommandBookBrowserLeftDoubleClickLocator(this).Value; }
            set { _CommandBookBrowserLeftDoubleClickLocator(this).SetValueAndTryNotify(value); }
        }

        #region Property CommandModel<ReactiveCommand, String> CommandBookBrowserLeftDoubleClick Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandBookBrowserLeftDoubleClick =
            new Property<CommandModel<ReactiveCommand, String>>
            {
                LocatorFunc = _CommandBookBrowserLeftDoubleClickLocator
            };

        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>>
            _CommandBookBrowserLeftDoubleClickLocator =
                RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandBookBrowserLeftDoubleClick",
                    model =>
                        model.Initialize("CommandBookBrowserLeftDoubleClick",
                            ref model._CommandBookBrowserLeftDoubleClick, ref _CommandBookBrowserLeftDoubleClickLocator,
                            _CommandBookBrowserLeftDoubleClickDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>>
            _CommandBookBrowserLeftDoubleClickDefaultValueFactory =
                model =>
                {
                    var state = Global.LoadFTPExplorerMSG; // Command state  
                    var commandId = Global.LoadFTPExplorerMSG;
                    var vm = CastToCurrentType(model);
                    var cmd = new ReactiveCommand(canExecute: true) {ViewModel = model}; //New Command Core

                    cmd.DoExecuteUITask(
                            vm,
                            async e =>
                            {
                                vm.IsUIBusy = true;
                                BookListItem para = (BookListItem) vm.dgSelectBookItem;
                                if (para != null && !string.IsNullOrEmpty(para.id))
                                {
                                    vm.FillFilesFromFTPRoot(para.id, true, true);
                                }
                                vm.IsUIBusy = false;
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

        public CommandModel<ReactiveCommand, String> CommandLoadFTP
        {
            get { return _CommandLoadFTPLocator(this).Value; }
            set { _CommandLoadFTPLocator(this).SetValueAndTryNotify(value); }
        }

        #region Property CommandModel<ReactiveCommand, String> CommandLoadFTP Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandLoadFTP =
            new Property<CommandModel<ReactiveCommand, String>> {LocatorFunc = _CommandLoadFTPLocator};

        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLoadFTPLocator =
            RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLoadFTP",
                model =>
                    model.Initialize("CommandLoadFTP", ref model._CommandLoadFTP, ref _CommandLoadFTPLocator,
                        _CommandLoadFTPDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLoadFTPDefaultValueFactory =
            model =>
            {
                var state = "CommandLoadFTP"; // Command state  
                var commandId = "CommandLoadFTP";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) {ViewModel = model}; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            vm.IsUIBusy = true;
                            //var req = new DownSavePathMesWindow_Model()
                            //{
                            //    FileName = vm.DgSelectItem.Name,
                            //    SavePath = GlobalPara.DefaultSavePath,
                            //    Size = vm.DgSelectItem.Size,
                            //    URL = vm.DgSelectItem.Url
                            //};
                            //GlobalPara.lastDownSize = vm.DgSelectItem.Size;
                            //if (req.FileName.Length > 10)
                            //{
                            //    req.FileName = req.FileName.Substring(0, 10) + "...";
                            //}
                            //req.FullFileName = vm.DgSelectItem.Name;
                            //vm.lastFTPRootID = vm.DgSelectItem.Name;

                            //await vm.StageManager.DefaultStage.Show(req);
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

        private string lastFTPRootID = "";

        #endregion


        public CommandModel<ReactiveCommand, String> CommandDownload
        {
            get { return _CommandDownloadLocator(this).Value; }
            set { _CommandDownloadLocator(this).SetValueAndTryNotify(value); }
        }

        #region Property CommandModel<ReactiveCommand, String> CommandDownload Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandDownload =
            new Property<CommandModel<ReactiveCommand, String>> {LocatorFunc = _CommandDownloadLocator};

        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandDownloadLocator =
            RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandDownload",
                model =>
                    model.Initialize("CommandDownload", ref model._CommandDownload, ref _CommandDownloadLocator,
                        _CommandDownloadDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandDownloadDefaultValueFactory =
            model =>
            {
                var state = "CommandDownload"; // Command state  
                var commandId = "CommandDownload";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) {ViewModel = model}; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            vm.IsUIBusy = true;
                            //var req = new DownSavePathMesWindow_Model()
                            //{
                            //    FileName = vm.DgSelectItem.FileName,
                            //    SavePath = GlobalPara.DefaultSavePath,
                            //    Size = vm.DgSelectItem.Size,
                            //    URL = vm.DgSelectItem.Url
                            //};
                            //GlobalPara.lastDownSize = vm.DgSelectItem;
                            //if (req.FileName.Length > 10)
                            //{
                            //    req.FileName = req.FileName.Substring(0, 10) + "...";
                            //}
                            //req.FullFileName = vm.DgSelectItem.FileName;
                            //vm.lastDownName = vm.DgSelectItem.FileName;

                            //await vm.StageManager.DefaultStage.Show(req);
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

        private string lastDownName = "";

        #endregion


        #endregion

        #region 事件处理

        /// <summary>
        /// 左侧导航栏选择改变
        /// </summary>
        public int TabCotrolSelectIndex
        {
            get { return _TabCotrolSelectIndexLocator(this).Value; }
            set
            {
                _TabCotrolSelectIndexLocator(this).SetValueAndTryNotify(value);
                if (GlobalPara.rootTypeNow == value) return;
                switch (value)
                {
                    //图书资源
                    case 0:
                        //BroswerPathStr = "图书资源";
                        TransVisibility = System.Windows.Visibility.Collapsed;
                        MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(null, true, Global.ShowBookListControlMSG);
                        break;
                    //动画书资源
                    case 1:
                        //BroswerPathStr = "图书资源";
                        TransVisibility = System.Windows.Visibility.Collapsed;
                        MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(null, true, Global.ShowEBookListControlMSG);
                        break;
                    //共享资源
                    case 2:
                        IsNotAllTab = true;
                        if (!isLockTab) GetRootFile(2, 1, 2);
                        //BroswerPathStr = "共享资源";
                        TransVisibility = Visibility.Collapsed;
                        GetUploadPath("");
                        break;
                    //私有资源
                    case 3:
                        IsNotAllTab = true;
                        if (!isLockTab) GetRootFile(3, 1, 2);
                        //BroswerPathStr = "私有资源";
                        TransVisibility = Visibility.Collapsed;
                        GetUploadPath("");
                        break;
                }
            }
        }

        #region Property int TabCotrolSelectIndex Setup        

        protected Property<int> _TabCotrolSelectIndex = new Property<int> {LocatorFunc = _TabCotrolSelectIndexLocator};

        static Func<BindableBase, ValueContainer<int>> _TabCotrolSelectIndexLocator =
            RegisterContainerLocator<int>("TabCotrolSelectIndex", model =>
                model.Initialize("TabCotrolSelectIndex", ref model._TabCotrolSelectIndex,
                    ref _TabCotrolSelectIndexLocator, _TabCotrolSelectIndexDefaultValueFactory));

        static Func<int> _TabCotrolSelectIndexDefaultValueFactory = () => -1;

        #endregion


        #endregion


        /// <summary>
        /// 获取指定ID下的文件
        /// </summary>
        ///<param name="CatalogId">文档ID</param>
        /// <param name="rootType">目录类型</param>
        /// <param name="isInlist">是否插入队列中（上一步下一步操作时，不需要插入）</param>
        private void GetFilesFromCatalogId(string CatalogId, int rootType, bool isInlist = true)
        {
            IsAllCheck = false;
            var res = GlobalPara.webApis.GetListQuery(CatalogId, "", rootType, 1, 2);
            if (res.Successful)
            {
                if (isInlist)
                {
                    inExcuList(new OpenFolderOptDataModel()
                    {
                        rootType = rootType,
                        CatalogId = res.Data.CatelogId
                    });
                }

                ConvertToPath(res.Data.pathInfo, 4);
                GlobalPara.CatalogNow = res.Data;
                try
                {
                    FileBroswerData.Clear();
                    foreach (var item in res.Data.Records)
                    {
                        FileBroswerData.Add(MapperUtil.Mapper.Map<FTPListItem>(item));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "");
                }

            }
        }

        /// <summary>
        ///从FTP站点获取指定ID下的文件或文件夹
        /// </summary>
        ///<param name="CatalogId">文档ID</param>
        /// <param name="IsBook">是否为图书资源</param>
        /// <param name="IsRootData">是否为根路径</param>
        private void FillFilesFromFTPRoot(string CatalogId, bool IsBook, bool IsRootData)
        {
            IsAllCheck = false;
            IsFTPRoot = IsRootData;
            try
            {
                var res = GlobalPara.webApis.FTPRoot(Global.authToken.Token, CatalogId, IsBook);
                if (res.Successful)
                {
                    ClearData();
                    if (res.Data != null && res.Data.Count > 0)
                    {
                        try
                        {

                            foreach (var item in res.Data)
                            {
                                FileBroswerData.Add(MapperUtil.Mapper.Map<FTPListItem>(item));
                            }
                            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(this, IsRootData, Global.LoadFTPExplorerMSG);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex + "");
                        }
                    }

                }
                else
                {
                    if (res.Code.Equals(Global.ERROR_TOKEN))
                    {
                        MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<string>(null, "",
                            Global.ReLoginMSG);
                    }
                    else
                    {
                        MessageBox.Show(res.Message);
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

         

        }
        /// <summary>
        ///从FTP站点获取指定ID下的文件或文件夹
        /// </summary>
        ///<param name="CatalogId">文档ID</param>
        /// <param name="rootType">目录类型</param>
        /// <param name="isInlist">是否插入队列中（上一步下一步操作时，不需要插入）</param>
        private void FillFilesFromFTP(string CatalogId, bool IsBook,bool IsRootData)
        {

            IsAllCheck = false;
            IsFTPRoot = IsRootData;
            try
            {
                var res = GlobalPara.webApis.FTPList(Global.authToken.Token, EnumFileInfoType.ALL, "", CatalogId);
                if (res.Successful)
                {
                    ClearData();
                    if (res.Data != null && res.Data.Count > 0)
                    {
                        try
                        {

                            foreach (var item in res.Data)
                            {
                                FileBroswerData.Add(MapperUtil.Mapper.Map<FTPListItem>(item));
                            }
                            //MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(this, IsRootData, Global.LoadFTPExplorerMSG);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex + "");
                        }
                    }
                }
                else
                {
                    if (res.Code.Equals(Global.ERROR_TOKEN))
                    {
                        MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<string>(null, "",
                            Global.ReLoginMSG);
                    }
                    else
                    {
                        MessageBox.Show(res.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
        }

        public CommandModel<ReactiveCommand, String> CommandSyncFiles
        {
            get { return _CommandSyncFilesLocator(this).Value; }
            set { _CommandSyncFilesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandSyncFiles Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandSyncFiles = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSyncFilesLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSyncFilesLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandSyncFiles", model => model.Initialize("CommandSyncFiles", ref model._CommandSyncFiles, ref _CommandSyncFilesLocator, _CommandSyncFilesDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSyncFilesDefaultValueFactory =
            model =>
            {
                var state = "CommandSyncFiles";           // Command state  
                var commandId = "CommandSyncFiles";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                            dialog.Multiselect = false;

                            if (dialog.ShowDialog() == true && dialog.FileNames.Length > 0)
                            {
                                string filename = dialog.FileNames[0];
                                System.IO.FileInfo fileInfo = new FileInfo(filename);
                                
                                vm.lastSize = Convert.ToInt32(fileInfo.Length / 1024.00);
                                if (vm.lastSize == 0) vm.lastSize = 1;
                                FTPClient fTPClient = new FTPClient(GlobalPara.SourceServerAdress,
                                GlobalPara.SourceUserName, GlobalPara.SourcePwd, FTPModel.ASCII, Encoding.Default);
                                fTPClient.OnCompleted += FTPClient_OnCompleted;
                                fTPClient.Upload(filename, GlobalPara.SourceServerAdress, GlobalPara.UploadPathNow);
                                
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

        private static void FTPClient_OnCompleted(string sessionID, string Url)
        {
            string[] arr = Url.Split(new string[] { sessionID }, StringSplitOptions.RemoveEmptyEntries);
            var url = GlobalPara.UploadPathNow + sessionID;
            if (arr.Length > 1)
                url = url + arr[1];
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<string>(null, url, "AddSyncFileEventRouter");
        }

        #endregion

        public bool isUiLoack = false;

        public CommandModel<ReactiveCommand, String> CommandUploadFileShow
        {
            get { return _CommandUploadFileShowLocator(this).Value; }
            set { _CommandUploadFileShowLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandUploadFileShow Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandUploadFileShow = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandUploadFileShowLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandUploadFileShowLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandUploadFileShow", model => model.Initialize("CommandUploadFileShow", ref model._CommandUploadFileShow, ref _CommandUploadFileShowLocator, _CommandUploadFileShowDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandUploadFileShowDefaultValueFactory =
            model =>
            {
                var state = "CommandUploadFileShow";           // Command state  
                var commandId = "CommandUploadFileShow";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            if (!Global.ArrowEditFile&&GlobalPara.rootTypeNow==1) return;
                            if (GlobalPara.rootTypeNow == 2)
                            {
                                if (GlobalPara.CatalogNow.pathInfo.Count < 2 ||
                                    (GlobalPara.CatalogNow.SynergyRange.departs.Any(p =>
                                         Global.DepartId.Contains(p.DepartmentId)) ||
                                     GlobalPara.CatalogNow.SynergyRange.users.Any(p =>
                                         p.ProfileId == Global.authToken.ID))||GlobalPara.CatalogNow.Creator.ProfileId == Global.authToken.ID)
                                {

                                }
                                else
                                {
                                    return;
                                }
                            }
                            vm.IsUIBusy = true;
                            await vm.StageManager.DefaultStage.Show(new UploadMesWindow_Model());
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





        public string NowFolderName
        {
            get { return _NowFolderNameLocator(this).Value; }
            set { _NowFolderNameLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string NowFolderName Setup        
        protected Property<string> _NowFolderName = new Property<string> { LocatorFunc = _NowFolderNameLocator };
        static Func<BindableBase, ValueContainer<string>> _NowFolderNameLocator = RegisterContainerLocator<string>("NowFolderName", model => model.Initialize("NowFolderName", ref model._NowFolderName, ref _NowFolderNameLocator, _NowFolderNameDefaultValueFactory));
        static Func<string> _NowFolderNameDefaultValueFactory = () => "";
        #endregion

        /// <summary>
        /// 将路径转化为浏览路径
        /// </summary>
        /// <param name="pathInfo">路径信息</param>
        /// <param name="count">需要省略的阀值</param>
        private void ConvertToPath(List<CatalogSimpleModel> pathInfo,int count)
        {
            StringBuilder sb = new StringBuilder();
            var res= pathInfo.OrderBy(p => p.Level).ToList();
            List<CatalogSimpleModel> tempt = new List<CatalogSimpleModel>();
            foreach (var item in res)
            {
                CatalogSimpleModel newitem = item;
                if (item.Name.Length > 6)
                {
                    newitem.Name = item.Name.Substring(0, 6) + "...";
                }
                tempt.Add(newitem);
            }
            var temp = tempt.OrderBy(p => p.Level);
            int count1 = temp.Count();
            if (count1 > count)
            {
                var temp1 = temp.Take(1);
                var temp2 = temp.Skip(temp.Count() - (count-1));
                sb.Append(temp1.ToList()[0].Name + " > ...");
                sb.Append(string.Join(" > ", temp2.Select(p => p.Name).ToArray()));
                BroswerPathStr = sb.ToString();
                return;
            }

            sb.Append(string.Join(" > ", temp.Select(p => p.Name).ToArray()));
            BroswerPathStr = sb.ToString();
            if (GlobalPara.rootTypeNow == 0)
            {
                BroswerPathStr= "全部" + BroswerPathStr.Substring(2);
            }
            NowFolderName = pathInfo[0].Name;
        }

        public void RefreshDownRec()
        {
            var res = INIOperationHelper.INIGetAllItems(GlobalPara.IniPath, Global.authToken.ID + "Downlist");
            DownRecordList.Clear();
            foreach (var item in res)
            {
                try
                {
                    var ret = item.Split('=');
                    
                    DownRecordList.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<DownRecordModel>(ret[1]));
                }
                catch
                {
                    continue;
                }
                
            }
        }

        /// <summary>
        /// 搜索文件
        /// </summary>
        private void SearchFiles()
        {
            IsAllCheck = false;
            var res = GlobalPara.webApis.GetListQuery(GlobalPara.CatalogNow.CatelogId, string.IsNullOrEmpty(SearchKeyStr)?"": SearchKeyStr, GlobalPara.rootTypeNow, 1, 2);
            
            if (res.Successful)
            {
                BroswerPathStr = "搜索结果";
                GlobalPara.CatalogNow = res.Data;
                FileBroswerData.Clear();
                foreach (var item in res.Data.Records)
                {
                    FileBroswerData.Add(MapperUtil.Mapper.Map<FTPListItem>(item));
                }
            }
        }

        private void inExcuList(OpenFolderOptDataModel model)
        {
            if (Global.RecordList.Count > indexnow+1)
            {
                Global.RecordList.RemoveRange(indexnow+1, Global.RecordList.Count - indexnow-1);
            }
            Global.RecordList.Add(model);
            indexnow++;
        }

        private bool isfirst = true;

        /// <summary>
        /// 获取根目录文件
        /// </summary>
        /// <param name="rootType">1-公司；2-共享；3-个人</param>
        /// <param name="ordertype">排序类型：1 时间，2文件名，3类型, 4大小</param>
        /// <param name="isasc">排序类型：1 升序，2 降序</param>
        private void GetRootFile(int rootType, int ordertype, int isasc)
        {
            IsAllCheck = false;
            var res = GlobalPara.webApis.GetRoot(rootType, ordertype, isasc);
            if (res.Successful)
            {
                inExcuList(new OpenFolderOptDataModel()
                {
                    rootType = rootType,
                    CatalogId = res.Data.CatelogId
                });
                if (isfirst)
                {
                    indexnow = 0;
                    isfirst = false;
                }
                ConvertToPath(res.Data.pathInfo, 4);
                GlobalPara.rootTypeNow = rootType;
                GlobalPara.CatalogNow = res.Data;
                FileBroswerData.Clear();
                foreach (var item in res.Data.Records)
                {
                    FileBroswerData.Add(MapperUtil.Mapper.Map<FTPListItem>(item));
                }
            }
        }

        private void DeleteDoc(DocumentDeleteRequest ent)
        {
            var res = GlobalPara.webApis.Delete(ent);
            if (res.Successful)
            {
                this.Dispatcher.BeginInvoke((Action)RefreshFilesList);
                this.Dispatcher.BeginInvoke((Action)GetCompanySotreStatus);
            }
            else
            {
                _vm.ShowInformation(res.Message);
            }
        }

        private void RenameFile(string name, string fileid)
        {
            var res = GlobalPara.webApis.RenameFile(name,fileid);
            if (res.Successful)
            {
                this.Dispatcher.BeginInvoke((Action) RefreshFilesList);
            }
            else
            {
                _vm.ShowInformation(res.Message);
            }
        }

        private void RenameFloder(string name, string folderid)
        {
            var res = GlobalPara.webApis.RenameCatalog(name, folderid);
            if (res.Successful)
            {
                this.Dispatcher.BeginInvoke((Action)RefreshFilesList);
            }
        }

        /// <summary>
        /// 获取存储情况
        /// </summary>
        private void GetCompanySotreStatus()
        {
            var res = GlobalPara.webApis.getCompanyStoreStatus();
            if (res.Successful)
            {
                StoreUseStatus = res.Data;
            }
        }
        /// <summary>
        /// 获取上传路径
        /// </summary>
        /// <param name="rootype">0：其他 1-公司；2-共享；3-个人</param>
        public void GetUploadPath(string DirectID)
        {
            var res = GlobalPara.webApis.GetUpaloadPath(Global.authToken.Token, DirectID);
            if (res.Successful)
            {
                GlobalPara.UploadPathNow = $"{res.Data.ip}:{res.Data.port}/{res.Data.path}";
            }
        }

        private Api.TheResult<string> CreateNewFolder(string name)
        {
            var res = GlobalPara.webApis.CreateDCatalog(GlobalPara.rootTypeNow,
                new DocumentCatalogV1Request()
                {
                    Name = name,
                    ParentId = GlobalPara.CatalogNow.CatelogId
                });
            return res;
        }




        private void SetMainWinCkall()
        {
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(null, true, "MainWinCkAllEventRouter");
        }

        private void MainWinGetFocus()
        {
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(null, true, "MainGetFoucusEventRouter");
        }
        private List<CoverDocResultApiModel> GetCoverNum(string targetCatalogId, List<DocBaseInfoApiModel> model)
        {
            var res = GlobalPara.webApis.GetCoverNum(targetCatalogId,model);
            if (res.Successful)
            {
                return res.Data;
            }
            else
            {
                return new List<CoverDocResultApiModel>();
            }
        }

        private bool IsOverRange(string sourceId, string targetCatalogId, EnumDocType type)
        {
            var res = GlobalPara.webApis.IsOverRange(sourceId, targetCatalogId, type);
            if (res.Successful)
            {
                return res.Data;
            }
            else
            {
                return false;
            }
        }

        private void MoveDocs(string sourceId, string sourceName, string targetCatalogId, EnumDocType type,
            bool isCover,bool isClear=false)
        {
            var res = GlobalPara.webApis.MoveDoc(sourceId,sourceName,targetCatalogId,type,isCover,isClear);
        }

        private void SetNewStatus(EnumDocStatusType newStatus)
        {
            var res = GlobalPara.webApis.SetNewStatus(DgSelectItem.FileID,
                !DgSelectItem.IsFile
                    ? EnumDocType.Catalog
                    : EnumDocType.File, newStatus);
            if (res.Successful)
            {
                this.Dispatcher.BeginInvoke((Action) RefreshFilesList);
            }
            else
            {
                _vm.ShowInformation(res.Message);
            }
        }

        private void SetSyncRange(StatusList model)
        {
            if (model.UserList == null) model.UserList = new List<string>();
            if (model.DeptList== null) model.DeptList = new List<string>();
            var res = GlobalPara.webApis.setSynergyRange(DgSelectItem.FileID,
                !DgSelectItem.IsFile
                    ? EnumDocType.Catalog
                    : EnumDocType.File, model);
        }

        private void SetShareRange(StatusList model)
        {
            if (model.UserList == null) model.UserList = new List<string>();
            if (model.DeptList == null) model.DeptList = new List<string>();
            var res = GlobalPara.webApis.setShareRange(DgSelectItem.FileID,
                !DgSelectItem.IsFile
                    ? EnumDocType.Catalog
                    : EnumDocType.File, model);
        }

        private void SetNewStatus(string docid,EnumDocStatusType newStatus)
        {
            var res = GlobalPara.webApis.SetNewStatus(docid,EnumDocType.Catalog, newStatus);
            if (res.Successful)
            {
                this.Dispatcher.BeginInvoke((Action)RefreshFilesList);
            }
            else
            {
                _vm.ShowInformation(res.Message);
            }
        }

        private void SetSyncRange(string docid,StatusList model, EnumDocType type)
        {
            var res = GlobalPara.webApis.setSynergyRange(docid,
                type, model);
        }

        private void SetShareRange(string docid,StatusList model, EnumDocType type)
        {
            var res = GlobalPara.webApis.setShareRange(docid, type, model);
        }

        private void SetNewStatus(DocBaseInfoApiModel item, EnumDocStatusType newStatus, EnumDocType type)
        {
            var res = GlobalPara.webApis.SetNewStatus(item.sourceId, type, newStatus);
            if (res.Successful)
            {
                this.Dispatcher.BeginInvoke((Action)RefreshFilesList);
            }
            else
            {
                _vm.ShowInformation(res.Message);
            }
        }

        private void SetSyncRange(DocBaseInfoApiModel item, StatusList model)
        {
            var res = GlobalPara.webApis.setSynergyRange(item.sourceId,
                item.type, model);
        }

        private void SetShareRange(DocBaseInfoApiModel item, StatusList model)
        {
            var res = GlobalPara.webApis.setShareRange(item.sourceId,
                item.type, model);
        }

        private void FTPClient_OnDownloadProgressChanged(string sessionID, double progress)
        {
            ProgressBarValue = progress;
        }

        private void FTPClient_OnDownloadCompleted(string sessionID, string Url)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                _vm.ShowInformation(lastDownName+" 下载完成");
            }));
            
            DownRecordModel req = new DownRecordModel()
            {
                Name = lastDownName,
                Size = GlobalPara.lastDownSize,
                SessionId=sessionID
            };
            if (!string.IsNullOrEmpty(req.Name))
            {
                INIOperationHelper.INIWriteValue(GlobalPara.IniPath, Global.authToken.ID + "Downlist", sessionID, Newtonsoft.Json.JsonConvert.SerializeObject(req));
            }
            
            ProgressBarValue = 0;
            var res = GlobalPara.webApis.AddDownloadTime(Url, 1);
            if (res.Successful)
            {
                this.Dispatcher.BeginInvoke((Action) RefreshFilesList);
            }

        }

        public BookListItem dgSelectBookItem
        {
            get { return _dgSelectBookItemLocator(this).Value; }
            set
            {
                try
                {
                    _dgSelectBookItemLocator(this).SetValueAndTryNotify(value);
                    if (value != null)
                    {
                        var ent = EBookListData.FirstOrDefault(p => p.id.Equals(value.id)  );
                        //ent.isChecked = !ent.isChecked;
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "");
                }

            }
        }
        #region Property BookListItem dgSelectBookItem Setup        
        protected Property<BookListItem> _dgSelectBookItem = new Property<BookListItem> { LocatorFunc = _dgSelectBookItemLocator };
        static Func<BindableBase, ValueContainer<BookListItem>> _dgSelectBookItemLocator = RegisterContainerLocator<BookListItem>("dgSelectBookItem", model => model.Initialize("dgSelectBookItem", ref model._dgSelectBookItem, ref _dgSelectBookItemLocator, _dgSelectBookItemDefaultValueFactory));
        static Func<BookListItem> _dgSelectBookItemDefaultValueFactory = () => default(BookListItem);
        #endregion

        public EBookListItem dgSelectEBookItem
        {
            get { return _dgSelectEBookItemLocator(this).Value; }
            set
            {
                try
                {
                    _dgSelectEBookItemLocator(this).SetValueAndTryNotify(value);
                    if (value != null)
                    {
                        var ent = EBookListData.FirstOrDefault(p => p.id.Equals(value.id));
                        //ent.isChecked = !ent.isChecked;
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "");
                }

            }
        }
        #region Property EBookListItem dgSelectEBookItem Setup        
        protected Property<EBookListItem> _dgSelectEBookItem = new Property<EBookListItem> { LocatorFunc = _dgSelectEBookItemLocator };
        static Func<BindableBase, ValueContainer<EBookListItem>> _dgSelectEBookItemLocator = RegisterContainerLocator<EBookListItem>("dgSelectEBookItem", model => model.Initialize("dgSelectEBookItem", ref model._dgSelectEBookItem, ref _dgSelectEBookItemLocator, _dgSelectEBookItemDefaultValueFactory));
        static Func<EBookListItem> _dgSelectEBookItemDefaultValueFactory = () => default(EBookListItem);
        #endregion




        public ObservableCollection<DownRecordModel> DownRecordList
        {
            get { return _DownRecordListLocator(this).Value; }
            set { _DownRecordListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<DownRecordModel> DownRecordList Setup        
        protected Property<ObservableCollection<DownRecordModel>> _DownRecordList = new Property<ObservableCollection<DownRecordModel>> { LocatorFunc = _DownRecordListLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<DownRecordModel>>> _DownRecordListLocator = RegisterContainerLocator<ObservableCollection<DownRecordModel>>("DownRecordList", model => model.Initialize("DownRecordList", ref model._DownRecordList, ref _DownRecordListLocator, _DownRecordListDefaultValueFactory));
        static Func<ObservableCollection<DownRecordModel>> _DownRecordListDefaultValueFactory = () => new ObservableCollection<DownRecordModel>();
        #endregion



        public bool isLockTab
        {
            get { return _isLockTabLocator(this).Value; }
            set { _isLockTabLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool isLockTab Setup        
        protected Property<bool> _isLockTab = new Property<bool> { LocatorFunc = _isLockTabLocator };
        static Func<BindableBase, ValueContainer<bool>> _isLockTabLocator = RegisterContainerLocator<bool>("isLockTab", model => model.Initialize("isLockTab", ref model._isLockTab, ref _isLockTabLocator, _isLockTabDefaultValueFactory));
        static Func<bool> _isLockTabDefaultValueFactory = () => false;
        #endregion





        public CommandModel<ReactiveCommand, String> CommandSwitchTransVis
        {
            get { return _CommandSwitchTransVisLocator(this).Value; }
            set { _CommandSwitchTransVisLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandSwitchTransVis Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandSwitchTransVis = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSwitchTransVisLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSwitchTransVisLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandSwitchTransVis", model => model.Initialize("CommandSwitchTransVis", ref model._CommandSwitchTransVis, ref _CommandSwitchTransVisLocator, _CommandSwitchTransVisDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSwitchTransVisDefaultValueFactory =
            model =>
            {
                var state = "CommandSwitchTransVis";           // Command state  
                var commandId = "CommandSwitchTransVis";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
           
                            switch(vm.TransVisibility)
                            {
                                case Visibility.Collapsed:
                                    vm.TransVisibility = Visibility.Visible;
                                    vm.RefreshDownRec();
                                    break;
                                case Visibility.Visible:
                                    vm.TransVisibility = Visibility.Collapsed;
                                    break;
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



        public List<string> CompanyList
        {
            get { return _CompanyListLocator(this).Value; }
            set { _CompanyListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property List<string> CompanyList Setup        
        protected Property<List<string>> _CompanyList = new Property<List<string>> { LocatorFunc = _CompanyListLocator };
        static Func<BindableBase, ValueContainer<List<string>>> _CompanyListLocator = RegisterContainerLocator<List<string>>("CompanyList", model => model.Initialize("CompanyList", ref model._CompanyList, ref _CompanyListLocator, _CompanyListDefaultValueFactory));
        static Func<List<string>> _CompanyListDefaultValueFactory = () => new List<string>();
        #endregion


        public CompanyStoreStatusApiModel StoreUseStatus
        {
            get { return _StoreUseStatusLocator(this).Value; }
            set { _StoreUseStatusLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CompanyStoreStatusApiModel StoreUseStatus Setup        
        protected Property<CompanyStoreStatusApiModel> _StoreUseStatus = new Property<CompanyStoreStatusApiModel> { LocatorFunc = _StoreUseStatusLocator };
        static Func<BindableBase, ValueContainer<CompanyStoreStatusApiModel>> _StoreUseStatusLocator = RegisterContainerLocator<CompanyStoreStatusApiModel>("StoreUseStatus", model => model.Initialize("StoreUseStatus", ref model._StoreUseStatus, ref _StoreUseStatusLocator, _StoreUseStatusDefaultValueFactory));
        static Func<CompanyStoreStatusApiModel> _StoreUseStatusDefaultValueFactory = () => new CompanyStoreStatusApiModel(){DocumentsSize=500,UsedDocumentsSize=0,OtherSize=500,UsedOtherSize=0};
        #endregion




        public Visibility TransVisibility
        {
            get { return _TransVisibilityLocator(this).Value; }
            set { _TransVisibilityLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Visibility TransVisibility Setup        
        protected Property<Visibility> _TransVisibility = new Property<Visibility> { LocatorFunc = _TransVisibilityLocator };
        static Func<BindableBase, ValueContainer<Visibility>> _TransVisibilityLocator = RegisterContainerLocator<Visibility>("TransVisibility", model => model.Initialize("TransVisibility", ref model._TransVisibility, ref _TransVisibilityLocator, _TransVisibilityDefaultValueFactory));
        static Func<Visibility> _TransVisibilityDefaultValueFactory = () => Visibility.Collapsed;
        #endregion



        //propvm tab tab string tab Title
        public String Title
        {
            get { return _TitleLocator(this).Value; }
            set { _TitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String Title Setup
        protected Property<String> _Title = new Property<String> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<String>> _TitleLocator = RegisterContainerLocator<String>("Title", model => model.Initialize("Title", ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
        static Func<String> _TitleDefaultValueFactory = () => "Title is Here";
        #endregion







        public string HeadImageSource
        {
            get { return _HeadImageSourceLocator(this).Value; }
            set { _HeadImageSourceLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string HeadImageSource Setup        
        protected Property<string> _HeadImageSource = new Property<string> { LocatorFunc = _HeadImageSourceLocator };
        static Func<BindableBase, ValueContainer<string>> _HeadImageSourceLocator = RegisterContainerLocator<string>("HeadImageSource", model => model.Initialize("HeadImageSource", ref model._HeadImageSource, ref _HeadImageSourceLocator, _HeadImageSourceDefaultValueFactory));
        static Func<string> _HeadImageSourceDefaultValueFactory = () => "";
        #endregion


        public string UserNick
        {
            get { return _UserNickLocator(this).Value; }
            set { _UserNickLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string UserNick Setup        
        protected Property<string> _UserNick = new Property<string> { LocatorFunc = _UserNickLocator };
        static Func<BindableBase, ValueContainer<string>> _UserNickLocator = RegisterContainerLocator<string>("UserNick", model => model.Initialize("UserNick", ref model._UserNick, ref _UserNickLocator, _UserNickDefaultValueFactory));
        static Func<string> _UserNickDefaultValueFactory = () => "未登录";
        #endregion


        public bool IsNotAllTab
        {
            get { return _IsNotAllTabLocator(this).Value; }
            set { _IsNotAllTabLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool IsNotAllTab Setup        
        protected Property<bool> _IsNotAllTab = new Property<bool> { LocatorFunc = _IsNotAllTabLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsNotAllTabLocator = RegisterContainerLocator<bool>("IsNotAllTab", model => model.Initialize("IsNotAllTab", ref model._IsNotAllTab, ref _IsNotAllTabLocator, _IsNotAllTabDefaultValueFactory));
        static Func<bool> _IsNotAllTabDefaultValueFactory = () => false;
        #endregion


        public bool SelectAll
        {
            get { return _SelectAllLocator(this).Value; }
            set
            {
                _SelectAllLocator(this).SetValueAndTryNotify(value);
            }
        }
        #region Property bool SelectAll Setup        
        protected Property<bool> _SelectAll = new Property<bool> { LocatorFunc = _SelectAllLocator };
        static Func<BindableBase, ValueContainer<bool>> _SelectAllLocator = RegisterContainerLocator<bool>("SelectAll", model => model.Initialize("SelectAll", ref model._SelectAll, ref _SelectAllLocator, _SelectAllDefaultValueFactory));
        static Func<bool> _SelectAllDefaultValueFactory = () => true;
        #endregion


        public string SearchKeyStr
        {
            get { return _SearchKeyStrLocator(this).Value; }
            set { _SearchKeyStrLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string  SearchKeyStr Setup        
        protected Property<string > _SearchKeyStr = new Property<string > { LocatorFunc = _SearchKeyStrLocator };
        static Func<BindableBase, ValueContainer<string >> _SearchKeyStrLocator = RegisterContainerLocator<string >("SearchKeyStr", model => model.Initialize("SearchKeyStr", ref model._SearchKeyStr, ref _SearchKeyStrLocator, _SearchKeyStrDefaultValueFactory));
        static Func<string > _SearchKeyStrDefaultValueFactory = () => default(string );
        #endregion








        public double ProgressBarValue
        {
            get { return _ProgressBarValueLocator(this).Value; }
            set { _ProgressBarValueLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int ProgressBarValue Setup        
        protected Property<double> _ProgressBarValue = new Property<double> { LocatorFunc = _ProgressBarValueLocator };
        static Func<BindableBase, ValueContainer<double>> _ProgressBarValueLocator = RegisterContainerLocator<double>("ProgressBarValue", model => model.Initialize("ProgressBarValue", ref model._ProgressBarValue, ref _ProgressBarValueLocator, _ProgressBarValueDefaultValueFactory));
        static Func<double> _ProgressBarValueDefaultValueFactory = () => 0.00;
        #endregion



        public string BroswerPathStr
        {
            get { return _BroswerPathStrLocator(this).Value; }
            set { _BroswerPathStrLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string BroswerPathStr Setup        
        protected Property<string> _BroswerPathStr = new Property<string> { LocatorFunc = _BroswerPathStrLocator };
        static Func<BindableBase, ValueContainer<string>> _BroswerPathStrLocator = RegisterContainerLocator<string>("BroswerPathStr", model => model.Initialize("BroswerPathStr", ref model._BroswerPathStr, ref _BroswerPathStrLocator, _BroswerPathStrDefaultValueFactory));
        static Func<string> _BroswerPathStrDefaultValueFactory = () => "浏览路径";
        #endregion


        public CommandModel<ReactiveCommand, String> CommandSelectAll
        {
            get { return _CommandSelectAllLocator(this).Value; }
            set { _CommandSelectAllLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandSelectAll Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandSelectAll = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSelectAllLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSelectAllLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandSelectAll", model => model.Initialize("CommandSelectAll", ref model._CommandSelectAll, ref _CommandSelectAllLocator, _CommandSelectAllDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSelectAllDefaultValueFactory =
            model =>
            {
                var state = "CommandSelectAll";           // Command state  
                var commandId = "CommandSelectAll";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            //Todo: Add SelectAll logic here, or
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


        public CommandModel<ReactiveCommand, String> CommandNewFolder
        {
            get { return _CommandNewFolderLocator(this).Value; }
            set { _CommandNewFolderLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandNewFolder Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandNewFolder = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandNewFolderLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandNewFolderLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandNewFolder", model => model.Initialize("CommandNewFolder", ref model._CommandNewFolder, ref _CommandNewFolderLocator, _CommandNewFolderDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandNewFolderDefaultValueFactory =
            model =>
            {
                var state = "CommandNewFolder";           // Command state  
                var commandId = "CommandNewFolder";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            if (!Global.ArrowEditFolder && GlobalPara.rootTypeNow == 1) return;
                            ViewModels.CreatenewFolderWindow_Model reqVm = new CreatenewFolderWindow_Model();
                            reqVm.DefalutName = "新建文件夹";
                            reqVm.OpenSync = false;
                            if (GlobalPara.rootTypeNow == 2)
                            {
                                if (GlobalPara.CatalogNow.pathInfo.Count < 2 ||
                                    (GlobalPara.CatalogNow.SynergyRange.departs.Any(p =>
                                         Global.DepartId.Contains(p.DepartmentId)) ||
                                     GlobalPara.CatalogNow.SynergyRange.users.Any(p =>
                                         p.ProfileId == Global.authToken.ID))||GlobalPara.CatalogNow.Creator.ProfileId == Global.authToken.ID)
                                    reqVm.CanStatusChange = true;
                                else
                                {
                                    return;
                                }
                            }
                            reqVm.StatusType = GlobalPara.rootTypeNow==1?EnumDocStatusType.Department:(GlobalPara.rootTypeNow == 2 ? EnumDocStatusType.Share:EnumDocStatusType.Personal);

                            vm.IsUIBusy = true;
                            await vm.StageManager.DefaultStage.Show(reqVm);
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


        public CommandModel<ReactiveCommand, String> CommandGoBack
        {
            get { return _CommandGoBackLocator(this).Value; }
            set { _CommandGoBackLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGoBack Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandGoBack = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGoBackLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGoBackLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGoBack", model => model.Initialize("CommandGoBack", ref model._CommandGoBack, ref _CommandGoBackLocator, _CommandGoBackDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGoBackDefaultValueFactory =
            model =>
            {
                var state = "CommandGoBack";           // Command state  
                var commandId = "CommandGoBack";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            vm.IsUIBusy = true;
                            vm.isLockTab = true;
                            if (vm.indexnow == 0)
                            {
                                vm.isLockTab = false;
                                vm.IsUIBusy = false;
                                return; 
                            }
                            var newidex = vm.indexnow - 1;
                            
                            var res = vm.GetOpenModel(newidex);
                            if (res == null)
                            {
                                vm.isLockTab = false;
                                return;
                            }
                            vm.indexnow--;
                            vm.GetFilesFromCatalogId(res.CatalogId, res.rootType, false);
                            
                            vm.TabCotrolSelectIndex = res.rootType;
                            vm.isLockTab = false;
                            vm.IsUIBusy = false;
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


        public int indexnow
        {
            get { return _indexnowLocator(this).Value; }
            set { _indexnowLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int indexnow Setup        
        protected Property<int> _indexnow = new Property<int> { LocatorFunc = _indexnowLocator };
        static Func<BindableBase, ValueContainer<int>> _indexnowLocator = RegisterContainerLocator<int>("indexnow", model => model.Initialize("indexnow", ref model._indexnow, ref _indexnowLocator, _indexnowDefaultValueFactory));
        static Func<int> _indexnowDefaultValueFactory = () => 0;
        #endregion

        private OpenFolderOptDataModel GetOpenModel(int index)
        {
            if (index == -1) return null;
            if (Global.RecordList.Count<=index)
            {
                return null;
            }
            return Global.RecordList[index];
        }

        public CommandModel<ReactiveCommand, String> CommandGoFront
        {
            get { return _CommandGoFrontLocator(this).Value; }
            set { _CommandGoFrontLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGoFront Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandGoFront = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGoFrontLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGoFrontLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGoFront", model => model.Initialize("CommandGoFront", ref model._CommandGoFront, ref _CommandGoFrontLocator, _CommandGoFrontDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGoFrontDefaultValueFactory =
            model =>
            {
                var state = "CommandGoFront";           // Command state  
                var commandId = "CommandGoFront";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            vm.isLockTab = true;
                            vm.IsUIBusy = true;
                            var newidex= vm.indexnow+1;
                            var res = vm.GetOpenModel(newidex);
                            if (res == null)
                            {
                                vm.IsUIBusy = false;
                                vm.isLockTab = false;
                                return;
                            }
                            vm.indexnow++;
                            vm.GetFilesFromCatalogId(res.CatalogId, res.rootType, false);
                            
                            vm.TabCotrolSelectIndex = res.rootType;
                            vm.isLockTab = false;
                            vm.IsUIBusy = false;
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


        public CommandModel<ReactiveCommand, String> CommandDeletFiles
        {
            get { return _CommandCommandDeletFilesLocator(this).Value; }
            set { _CommandCommandDeletFilesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandCommandDeletFiles Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandCommandDeletFiles = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandCommandDeletFilesLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandCommandDeletFilesLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandCommandDeletFiles", model => model.Initialize("CommandCommandDeletFiles", ref model._CommandCommandDeletFiles, ref _CommandCommandDeletFilesLocator, _CommandCommandDeletFilesDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandCommandDeletFilesDefaultValueFactory =
            model =>
            {
                var state = "CommandCommandDeletFiles";           // Command state  
                var commandId = "CommandCommandDeletFiles";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            var req = vm.FileBroswerData.Where(p => p.IsChecked == true).ToList();
                            if (req == null || req.Count<1) return;
                            if (GlobalPara.rootTypeNow == 1)
                            {
                                if (req.Any(p => p.IsFile))
                                {
                                    if (!Global.ArrowDeleteFile)
                                    {
                                        vm._vm.ShowInformation($"没有删除公司文件的权限！");
                                        return;
                                    }
                                }
                                if (req.Any(p => !p.IsFile ))
                                {
                                    if (!Global.ArrowDeleteFolder)
                                    {
                                        vm._vm.ShowInformation($"没有删除部门文件夹的权限！");
                                        return;
                                    }
                                }
                            }
   

                            var filename = req.FirstOrDefault().FileName;
                            if (filename.Length > 8)
                            {
                                filename = filename.Substring(0, 8) + "...";
                            }
                            DelconfirmWindow_Model data =
                                new DelconfirmWindow_Model { TipStr = $"确定要删除\"{filename}\"等 {req.Count}个文件（夹）吗？" };
                            req.ForEach(p =>
                            {
                                if (GlobalPara.rootTypeNow == 2)
                                    if (!GlobalPara.hasSyncRight(p))
                                    {
                                        return;
                                    }
                                if (!p.IsFile)
                                {
                                    data.folderIds.Add(p.FileID);
                                }
                                else
                                {
                                    data.fileIds.Add(p.FileID);
                                }
                            });
                            vm.IsUIBusy = true;
                            await vm.StageManager.DefaultStage.Show(data);
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


        public CommandModel<ReactiveCommand, String> CommandRefreshFiles
        {
            get { return _CommandRefreshFilesLocator(this).Value; }
            set { _CommandRefreshFilesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandRefreshFiles Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandRefreshFiles = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandRefreshFilesLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandRefreshFilesLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandRefreshFiles", model => model.Initialize("CommandRefreshFiles", ref model._CommandRefreshFiles, ref _CommandRefreshFilesLocator, _CommandRefreshFilesDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandRefreshFilesDefaultValueFactory =
            model =>
            {
                var state = "CommandRefreshFiles";           // Command state  
                var commandId = "CommandRefreshFiles";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            vm.IsUIBusy = true;
                            vm.RefreshFilesList();
                            vm.IsUIBusy = false;
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





        public CommandModel<ReactiveCommand, String> CommandSearchFiles
        {
            get { return _CommandSearchFilesLocator(this).Value; }
            set { _CommandSearchFilesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandSearchFiles Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandSearchFiles = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSearchFilesLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSearchFilesLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandSearchFiles", model => model.Initialize("CommandSearchFiles", ref model._CommandSearchFiles, ref _CommandSearchFilesLocator, _CommandSearchFilesDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandSearchFilesDefaultValueFactory =
            model =>
            {
                var state = "CommandSearchFiles";           // Command state  
                var commandId = "CommandSearchFiles";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            vm.SearchFiles();
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
        /// <summary>
        /// 刷新文件列表
        /// </summary>
        private void RefreshFilesList()
        {
            GetFilesFromCatalogId(GlobalPara.CatalogNow.CatelogId,GlobalPara.rootTypeNow,false);
        }


        public CommandModel<ReactiveCommand, String> CommandDeleteFile
        {
            get { return _CommandDeleteFileLocator(this).Value; }
            set { _CommandDeleteFileLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandDeleteFile Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandDeleteFile = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandDeleteFileLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandDeleteFileLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandDeleteFile", model => model.Initialize("CommandDeleteFile", ref model._CommandDeleteFile, ref _CommandDeleteFileLocator, _CommandDeleteFileDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandDeleteFileDefaultValueFactory =
            model =>
            {
                var state = "CommandDeleteFile";           // Command state  
                var commandId = "CommandDeleteFile";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            if (vm.DgSelectItem == null) return;
                            var filename = vm.DgSelectItem.FileName;
                            if (filename.Length > 10)
                            {
                                filename = filename.Substring(0, 10) + "...";
                            }
                            DelconfirmWindow_Model data =
                                new DelconfirmWindow_Model {TipStr = $"确定要删除\"{filename}\"吗？"};
                            if (!vm.DgSelectItem.IsFile)
                            {
                                data.folderIds.Add(vm.DgSelectItem.FileID);
                            }
                            else
                            {
                                data.fileIds.Add(vm.DgSelectItem.FileID);
                            }
                            vm.IsUIBusy = true;
                            await vm.StageManager.DefaultStage.Show(data);
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


        public CommandModel<ReactiveCommand, String> CommandShowRenameWin
        {
            get { return _CommandShowRenameWinLocator(this).Value; }
            set { _CommandShowRenameWinLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowRenameWin Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowRenameWin = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowRenameWinLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowRenameWinLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowRenameWin", model => model.Initialize("CommandShowRenameWin", ref model._CommandShowRenameWin, ref _CommandShowRenameWinLocator, _CommandShowRenameWinDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowRenameWinDefaultValueFactory =
            model =>
            {
                var state = "CommandShowRenameWin";           // Command state  
                var commandId = "CommandShowRenameWin";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            if (vm.DgSelectItem == null) return;
                            var filename = vm.DgSelectItem.FileName;
                            var extension = "";
                            if (vm.DgSelectItem.IsFile)
                            {
                                filename = vm.DgSelectItem.FileName.Substring(0, vm.DgSelectItem.FileName.LastIndexOf("."));
                                extension = vm.DgSelectItem.FileName.Substring(vm.DgSelectItem.FileName.LastIndexOf(".") + 1);
                            }
                            vm.IsUIBusy = true;
                            await vm.StageManager.DefaultStage.Show(new RenameWindow_Model { OriginName= filename, DefalutName = filename,DocId= vm.DgSelectItem.FileID,IsFolder= !vm.DgSelectItem.IsFile,Extension=extension,CataLogId= vm.DgSelectItem .FileID});
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



        public CommandModel<ReactiveCommand, String> CommandShowChangeStatus
        {
            get { return _CommandShowChangeStatusLocator(this).Value; }
            set { _CommandShowChangeStatusLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowChangeStatus Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowChangeStatus = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowChangeStatusLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowChangeStatusLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowChangeStatus", model => model.Initialize("CommandShowChangeStatus", ref model._CommandShowChangeStatus, ref _CommandShowChangeStatusLocator, _CommandShowChangeStatusDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowChangeStatusDefaultValueFactory =
            model =>
            {
                var state = "CommandShowChangeStatus";           // Command state  
                var commandId = "CommandShowChangeStatus";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            try
                            {
                                //if (vm.DgSelectItem == null) return;
                                ////var ShareRange = vm.DgSelectItem.ShareRange;
                                ////var SyncRange = vm.DgSelectItem.SynergyRange;
                                
                                //var reqVm = new ChangeStatusWindow_Model();
                                //if (!vm.DgSelectItem.IsFile)
                                //{
                                //    reqVm.IsFolder = true;
                                //}
                                //else
                                //{
                                //    reqVm.IsFolder = false;
                                //}
                                //reqVm.IsFromTopBar = false;
                                //if (ShareRange == null)
                                //{
                                //    ShareRange = new DocRangeItem();
                                //}
                                //if (SyncRange == null)
                                //{
                                //    SyncRange = new DocRangeItem();
                                //}
                                //if (ShareRange.departs == null)
                                //{
                                //    ShareRange.departs = new List<DepartmentApiModel>();
                                //}
                                //if (ShareRange.users == null)
                                //{
                                //    ShareRange.users = new List<ProfileApiModel>();
                                //}
                                //if (SyncRange.departs == null)
                                //{
                                //    SyncRange.departs = new List<DepartmentApiModel>();
                                //}
                                //if (SyncRange.users == null)
                                //{
                                //    SyncRange.users = new List<ProfileApiModel>();
                                //}
                                //foreach (var item in ShareRange.departs)
                                //{
                                //    if (GlobalPara.CatalogNow.pathInfo.Count == 1||GlobalPara.CatalogNow.ShareRange.departs.Any(p=>p.DepartmentId==item.DepartmentId)
                                //        )
                                //    {
                                //        SelectItemModel nowItem = new SelectItemModel()
                                //        {
                                //            Itemtype = PsAndDeptItemtype.Dept,
                                //            ItemId = item.DepartmentId,
                                //            Name = item.Title
                                //        };
                                //        reqVm.ShareRange.Add(nowItem);
                                //    }
                                //}
                                //foreach (var item in ShareRange.users)
                                //{
                                //    if (GlobalPara.CatalogNow.pathInfo.Count == 1 || GlobalPara.CatalogNow.ShareRange.users.Any(p=>p.ProfileId==item.ProfileId)||GlobalPara.CatalogNow.Creator.ProfileId==item.ProfileId)
                                //    {
                                //        SelectItemModel nowItem = new SelectItemModel()
                                //        {
                                //            Itemtype = PsAndDeptItemtype.Person,
                                //            ItemId = item.ProfileId,
                                //            Name = item.Fullname
                                //        };
                                //        reqVm.ShareRange.Add(nowItem);
                                //    }
                                //}
                                //foreach (var item in SyncRange.departs)
                                //{
                                //    if (GlobalPara.CatalogNow.pathInfo.Count == 1 || GlobalPara.CatalogNow.SynergyRange.departs.Any(p => p.DepartmentId == item.DepartmentId))
                                //    {
                                //        SelectItemModel nowItem = new SelectItemModel()
                                //        {
                                //            Itemtype = PsAndDeptItemtype.Dept,
                                //            ItemId = item.DepartmentId,
                                //            Name = item.Title
                                //        };
                                //        reqVm.SyncRange.Add(nowItem);
                                //    }
                                //}
                                //foreach (var item in SyncRange.users)
                                //{
                                //    if (GlobalPara.CatalogNow.pathInfo.Count == 1 || GlobalPara.CatalogNow.SynergyRange.users.Any(p => p.ProfileId == item.ProfileId) || GlobalPara.CatalogNow.Creator.ProfileId == item.ProfileId)
                                //    {
                                //        SelectItemModel nowItem = new SelectItemModel()
                                //        {
                                //            Itemtype = PsAndDeptItemtype.Person,
                                //            ItemId = item.ProfileId,
                                //            Name = item.Fullname
                                //        };
                                //        reqVm.SyncRange.Add(nowItem);
                                //    }
                                //}
                                //reqVm.StatusType = vm.DgSelectItem.StatusType;
                                //if (SyncRange.departs.Count + SyncRange.users.Count > 0)
                                //{
                                //    reqVm.OpenSync = true;
                                //}
                                //reqVm.CanStatusChange = true;
                                //if (vm.BroswerPathStr.Split('>').ToList().Count > 1&&GlobalPara.rootTypeNow==2)
                                //{
                                //    reqVm.IsFromRoot = false;
                                //}
                                //vm.IsUIBusy = true;
                                //await vm.StageManager.DefaultStage.Show(reqVm);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex + "");
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


        public CommandModel<ReactiveCommand, String> CommandShowIrWindow
        {
            get { return _CommandShowIrWindowLocator(this).Value; }
            set { _CommandShowIrWindowLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowIrWindow Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowIrWindow = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowIrWindowLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowIrWindowLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowIrWindow", model => model.Initialize("CommandShowIrWindow", ref model._CommandShowIrWindow, ref _CommandShowIrWindowLocator, _CommandShowIrWindowDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowIrWindowDefaultValueFactory =
            model =>
            {
                var state = "CommandShowIrWindow";           // Command state  
                var commandId = "CommandShowIrWindow";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            try
                            {
                                var para = (List<IterationItem>)e.EventArgs.Parameter;
                                var newreq = new ObservableCollection<IterationItem>();
                                foreach (var item in para)
                                {
                                    newreq.Add(item);
                                }
                                vm.IsUIBusy = true;
                                await vm.StageManager.DefaultStage.Show(new IrRecourdWindow_Model { IrData = newreq,FileNameStr=vm.DgSelectItem.FileName });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex + "");
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


        public CommandModel<ReactiveCommand, String> CommandTest
        {
            get { return _CommandTestLocator(this).Value; }
            set { _CommandTestLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandTest Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandTest = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandTestLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandTestLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandTest", model => model.Initialize("CommandTest", ref model._CommandTest, ref _CommandTestLocator, _CommandTestDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandTestDefaultValueFactory =
            model =>
            {
                var state = "CommandTest";           // Command state  
                var commandId = "CommandTest";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                
                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            
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


        public CommandModel<ReactiveCommand, String> CommandBrowserLeftDoubleClick
        {
            get { return _CommandBrowserLeftDoubleClickLocator(this).Value; }
            set { _CommandBrowserLeftDoubleClickLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandBrowserLeftDoubleClick Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommandBrowserLeftDoubleClick = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandBrowserLeftDoubleClickLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandBrowserLeftDoubleClickLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandBrowserLeftDoubleClick", model => model.Initialize("CommandBrowserLeftDoubleClick", ref model._CommandBrowserLeftDoubleClick, ref _CommandBrowserLeftDoubleClickLocator, _CommandBrowserLeftDoubleClickDefaultValueFactory));

        private static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandBrowserLeftDoubleClickDefaultValueFactory =
            model =>
            {
                var state = "CommandBrowserLeftDoubleClick";           // Command state  
                var commandId = "CommandBrowserLeftDoubleClick";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            vm.IsUIBusy = true;
                            FTPListItem para = (FTPListItem)vm.DgSelectItem;
                            if (para != null&&!para.IsFile)
                            {
                                vm.FillFilesFromFTP(para.FileID,true,false);
                                //vm.GetUploadPath(para.FileID);
                            }
                            vm.IsUIBusy = false;
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




        public DownRecordModel DownloadRecSelectItem
        {
            get { return _DownloadRecSelectItemLocator(this).Value; }
            set { _DownloadRecSelectItemLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property DownRecordModel DownloadRecSelectItem Setup        
        protected Property<DownRecordModel> _DownloadRecSelectItem = new Property<DownRecordModel> { LocatorFunc = _DownloadRecSelectItemLocator };
        static Func<BindableBase, ValueContainer<DownRecordModel>> _DownloadRecSelectItemLocator = RegisterContainerLocator<DownRecordModel>("DownloadRecSelectItem", model => model.Initialize("DownloadRecSelectItem", ref model._DownloadRecSelectItem, ref _DownloadRecSelectItemLocator, _DownloadRecSelectItemDefaultValueFactory));
        static Func<DownRecordModel> _DownloadRecSelectItemDefaultValueFactory = () =>  new DownRecordModel();
        #endregion


        public CommandModel<ReactiveCommand, String> CommandDeletRec
        {
            get { return _CommanddDeletRecLocator(this).Value; }
            set { _CommanddDeletRecLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommanddDeletRec Setup        

        protected Property<CommandModel<ReactiveCommand, String>> _CommanddDeletRec = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommanddDeletRecLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommanddDeletRecLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommanddDeletRec", model => model.Initialize("CommanddDeletRec", ref model._CommanddDeletRec, ref _CommanddDeletRecLocator, _CommanddDeletRecDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommanddDeletRecDefaultValueFactory =
            model =>
            {
                var state = "CommanddDeletRec";           // Command state  
                var commandId = "CommanddDeletRec";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core

                cmd.DoExecuteUITask(
                        vm,
                        async e =>
                        {
                            if (vm.DownloadRecSelectItem == null)
                                return;
                            else
                            {
                                var para = vm.DownloadRecSelectItem;
                              INIOperationHelper.INIDeleteKey(GlobalPara.IniPath, Global.authToken.ID+"Downlist", para.SessionId);
                                vm.RefreshDownRec();
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

        /// <summary>
        /// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        /// </summary>
        /// <param name="view">Set target</param>
        /// <param name="oldValue">Value before set.</param>
        /// <returns>Task awaiter</returns>
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

        //        /// <summary>
        //        /// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        //        /// </summary>
        //        /// <param name="view">View that firing Load event</param>
        //        /// <returns>Task awaiter</returns>
        //        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        //        {
        //            
        //            return base.OnBindedViewLoad(view);
        //        }

        /// <summary>
        /// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        /// </summary>
        /// <param name="view">View that firing Unload event</param>
        /// <returns>Task awaiter</returns>
        protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        {
            _vm.OnUnloaded();
            return base.OnBindedViewUnload(view);
        }

        /// <summary>
        /// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        /// </summary>
        /// <param name="view">View that firing Load event</param>
        /// <returns>Task awaiter</returns>
        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            _vm = Singleton<ToastInstance>.Instance;
            return base.OnBindedViewLoad(view);
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

