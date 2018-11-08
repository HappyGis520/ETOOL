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

namespace EllaMakerTool.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MVVMWindow
    {
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
            SubscribeCommand();
            this.SourceInitialized += delegate (object sender, EventArgs e)//执行拖拽
            {
                this._HwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            };
            this.MouseMove += new MouseEventHandler(Window_MouseMove);//鼠标移入到边缘收缩
        }


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

        private void InitLoginShowCommand()
        {
            ShowLoginCommand showLoginCommand = new ShowLoginCommand();
            showLoginCommand.Execute(null);
        }


        private void SubscribeCommand()
        {
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
                .Where(p => p.EventName == "MainGetFoucusEventRouter").Subscribe(
                    p =>
                    {
                        this.Focus();
                    });
            //MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<bool>()
            //    .Where(p => p.EventName == "MainWinCkAllEventRouter").Subscribe(
            //        p =>
            //        {
            //            ckall.IsChecked = false;
            //        });
        }

        private void ComChangeCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel(typeof(SelectionChangedEventArgs)).RaiseEvent("", "CompanySwitchEventRouter", e);
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
                this.Height= norheigh ;
                this.Width= norwid;
                this.Left= left;
                this.Top=top;
                isMax = false;
            }
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void FButton_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void ckall_Checked(object sender, RoutedEventArgs e)
        {
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<string>(null, "", "AllCheckedEventRouter");
        }

        private void ckall_Unchecked(object sender, RoutedEventArgs e)
        {
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent<string>(null, "", "AllUnCheckedEventRouter"); 
        }

        private void searchBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            searchBox.SelectionStart = searchBox.Text.Length;
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
    }
}
