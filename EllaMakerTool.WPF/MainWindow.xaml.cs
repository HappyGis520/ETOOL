using EllaMakerTool.WPF.Helper.Command;
using MVVMSidekick.Views;
using Svg2Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using EllaMakerTool.Message;
using EllaMakerTool.Message.Data;
using EllaMakerTool.Models;
using MVVMSidekick.EventRouting;

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MVVMWindow
    {
        ucBookList _ucBookList = null;
        ucEBookList _ucEBookList = null;
        private ucFTPLIst _ucFTPList = null;

        public MainWindow()
        {
            InitializeComponent();

#if DEBUG
            if (!Debugger.IsAttached)
                Debugger.Launch();
#endif

             
            InitLoginShowCommand();
            maximge = SvgToimge("pc_Button_MaxBack.svg");
            normalimge = SvgToimge("pc_Button_ToMax.svg");
            norheigh = this.Height;
            norwid = this.Width;
            left = this.Left;
            top = this.Top;
            MaxBtnImage.Source = normalimge;
            SubscribeEvent();
            this.SourceInitialized += delegate (object sender, EventArgs e)//执行拖拽
            {
                this._HwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            };
            this.MouseMove += new MouseEventHandler(Window_MouseMove);//鼠标移入到边缘收缩
        }

        #region 窗体操作
        #region 初始化窗体可以缩放大小
        private const int WM_SYSCOMMAND = 0x112;
        private HwndSource _HwndSource;
        private Dictionary<ResizeDirection, Cursor> cursors = new Dictionary<ResizeDirection, Cursor>
        {
            {ResizeDirection.BottomRight, Cursors.SizeNWSE},
        };
        private enum ResizeDirection
        {
            BottomRight = 8,
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        #endregion
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element != null && !element.Name.Contains("Resize"))
                    this.Cursor = Cursors.Arrow;
            }

        }
  
        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_HwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }
        private void ResizePressed(object sender, MouseEventArgs e)
        {
            if (isMax) return;
            FrameworkElement element = sender as FrameworkElement;
            ResizeDirection direction = (ResizeDirection)Enum.Parse(typeof(ResizeDirection), element.Name.Replace("Resize", ""));
            this.Cursor = cursors[direction];
            if (e.LeftButton == MouseButtonState.Pressed)
                ResizeWindow(direction);
        }
        private void FButton_Max_Click(object sender, RoutedEventArgs e)
        {
            this.ResizeMode = ResizeMode.CanResizeWithGrip;
            if (!isMax)
            {
                Rect rc = SystemParameters.WorkArea;//获取工作区大小  
                this.Left = 0;//设置位置  
                this.Top = 0;
                this.Width = rc.Width;
                this.Height = rc.Height;
                MaxBtnImage.Source = maximge;
                isMax = true;
            }

            else
            {
                MaxBtnImage.Source = normalimge;
                this.Height = norheigh;
                this.Width = norwid;
                this.Left = left;
                this.Top = top;
                isMax = false;
            }
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void FButton_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }
        private void mVVMWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Rect rc = SystemParameters.WorkArea;//获取工作区大小 
            if (e.NewSize.Height > rc.Height && e.NewSize.Width > rc.Width)
            {

                this.Left = 0;//设置位置  
                this.Top = 0;
                this.Width = rc.Width;
                this.Height = rc.Height;
                MaxBtnImage.Source = maximge;
                isMax = true;
            }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (isMax) return;
                base.OnMouseLeftButtonDown(e);
                this.DragMove();
            }
            catch
            {

            }
        }
        #endregion


        private void InitLoginShowCommand()
        {
            ShowLoginCommand showLoginCommand = new ShowLoginCommand();
            showLoginCommand.Execute(false);
        }
        //路由事件订阅，响应
        private void SubscribeEvent()
        {
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
                .Where(p => p.EventName == "MainGetFoucusEventRouter").Subscribe(
                    p =>
                    {
                        this.Show();
                        this.Focus();
                    });
            //显示图书列表
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
                .Where(p => p.EventName == Global.ShowBookListControlMSG).Subscribe(LoadBookListControl);

            //显示动画书列表
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
                .Where(p => p.EventName == Global.ShowEBookListControlMSG).Subscribe(LoadEBookListControl);

            //加载FTP资源浏览器
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
                .Where(p => p.EventName == Global.LoadFTPExplorerMSG).Subscribe(LoadFTPExplorer);
            //重登录 
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
                .Where(p => p.EventName == Global.ReLoginMSG).Subscribe(ReLogin);
           
        }
        /// <summary>
        /// 显示图书列表
        /// </summary>
        /// <param name="param"></param>
        private void LoadBookListControl(RouterEventData<bool> param)
        {
            if (_ucBookList == null) _ucBookList = new ucBookList();
            if (grdDocker.Children.Count > 0)
            {
                if (!grdDocker.Children[0].GetType().Equals(typeof(ucBookList)))
                {
                    grdDocker.Children.Clear();
                    grdDocker.Children.Add(_ucBookList);
                }
            }
            else
            {
                grdDocker.Children.Add(_ucBookList);
            }
            BookListByPageParam _param = new BookListByPageParam()
            {
                pageIndex = 0,
                pageSize = 10,
                SearchAuthorName = "",
                SearchBookSetName = "",
                SearchPublisherName = "",
                token = Global.authToken.Token

            };
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(this, _param, Global.RefreshBookListData);
        }
        /// <summary>
        /// 加载FTP资源浏览器
        /// </summary>
        /// <param name="param"></param>
        private void LoadFTPExplorer(RouterEventData<bool> param)
        {
            bool _isRootData = (bool)param.EventData;
            if (_ucFTPList == null) _ucFTPList = new ucFTPLIst();
            if (grdDocker.Children.Count > 0)
            {
                if (!grdDocker.Children[0].GetType().Equals(typeof(ucFTPLIst)))
                {
                    grdDocker.Children.Clear();
                    grdDocker.Children.Add(_ucFTPList);
                }
            }
            else
            {
                grdDocker.Children.Add(_ucFTPList);
            }

        }
        /// <summary>
        /// 显示动画书列表
        /// </summary>
        /// <param name="param"></param>
        private void LoadEBookListControl(RouterEventData<bool> param)
        {
            if (_ucEBookList == null) _ucEBookList = new ucEBookList();
            if (grdDocker.Children.Count > 0)
            {
                if (!grdDocker.Children[0].GetType().Equals(typeof(ucEBookList)))
                {
                    grdDocker.Children.Clear();
                    grdDocker.Children.Add(_ucEBookList);
                }
            }
            else
            {
                grdDocker.Children.Add(_ucEBookList);
            }
            EBookListByPageParam _param = new EBookListByPageParam()
            {
                pageIndex = 0,
                pageSize = 10,
                token = Global.authToken.Token

            };
            EventRouter.Instance.RaiseEvent(this, _param, Global.RefreshEBookListData);

        }

        /// <summary>
        /// 重登录 
        /// </summary>
        /// <param name="param"></param>
        private void ReLogin(RouterEventData<bool> param)
        {
            this.Hide();
            ShowLoginCommand showLoginCommand = new ShowLoginCommand();
            showLoginCommand.Execute(true);
        }



        private void FButton_Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private DrawingImage maximge;
        private DrawingImage normalimge;

        private DrawingImage SvgToimge(string name)
        {
            DrawingImage svg_image;
            string file_name = AppDomain.CurrentDomain.BaseDirectory + @"\Resources\" + name.ToString();
            using (FileStream file_stream = new FileStream(file_name, FileMode.Open, FileAccess.Read))
                svg_image = SvgReader.Load(file_stream, new SvgReaderOptions(false));
            return svg_image;
        }

        private bool isMax = false;
        private double norwid;
        private double norheigh;
        private double left;
        private double top;

        private void searchBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            searchBox.SelectionStart = searchBox.Text.Length;
        }






    }
}
