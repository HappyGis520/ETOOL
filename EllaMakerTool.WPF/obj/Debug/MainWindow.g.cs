﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7E36130B7632F974BF3AB43601A3472DE33D151E"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Ay.Framework.WPF.Controls;
using Ay.MvcFramework.AyMarkupExtension;
using EllaMakerTool.Controls;
using EllaMakerTool.Converter;
using EllaMakerTool.CustomControls;
using EllaMakerTool.WPF.ViewModels;
using MVVMSidekick.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace EllaMakerTool.WPF {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : MVVMSidekick.Views.MVVMWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal EllaMakerTool.WPF.MainWindow mVVMWindow;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutMain;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MaxBtnImage;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal EllaMakerTool.Controls.ExtendTextBox searchBox;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border headerObj;
        
        #line default
        #line hidden
        
        
        #line 205 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComChangeCbx;
        
        #line default
        #line hidden
        
        
        #line 269 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgfileBrowser;
        
        #line default
        #line hidden
        
        
        #line 293 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ckall;
        
        #line default
        #line hidden
        
        
        #line 602 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ResizeBottomRight;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EllaMakerTool.WPF;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.mVVMWindow = ((EllaMakerTool.WPF.MainWindow)(target));
            
            #line 14 "..\..\MainWindow.xaml"
            this.mVVMWindow.SizeChanged += new System.Windows.SizeChangedEventHandler(this.mVVMWindow_SizeChanged);
            
            #line default
            #line hidden
            
            #line 14 "..\..\MainWindow.xaml"
            this.mVVMWindow.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LayoutMain = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 44 "..\..\MainWindow.xaml"
            ((Ay.Framework.WPF.Controls.AyImageButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FButton_Min_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 48 "..\..\MainWindow.xaml"
            ((Ay.Framework.WPF.Controls.AyImageButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FButton_Max_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MaxBtnImage = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            
            #line 51 "..\..\MainWindow.xaml"
            ((Ay.Framework.WPF.Controls.AyImageButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FButton_Close_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.searchBox = ((EllaMakerTool.Controls.ExtendTextBox)(target));
            
            #line 76 "..\..\MainWindow.xaml"
            this.searchBox.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.searchBox_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.headerObj = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.ComChangeCbx = ((System.Windows.Controls.ComboBox)(target));
            
            #line 210 "..\..\MainWindow.xaml"
            this.ComChangeCbx.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComChangeCbx_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dgfileBrowser = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            this.ckall = ((System.Windows.Controls.CheckBox)(target));
            
            #line 297 "..\..\MainWindow.xaml"
            this.ckall.Checked += new System.Windows.RoutedEventHandler(this.ckall_Checked);
            
            #line default
            #line hidden
            
            #line 297 "..\..\MainWindow.xaml"
            this.ckall.Unchecked += new System.Windows.RoutedEventHandler(this.ckall_Unchecked);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ResizeBottomRight = ((System.Windows.Controls.Border)(target));
            
            #line 602 "..\..\MainWindow.xaml"
            this.ResizeBottomRight.MouseMove += new System.Windows.Input.MouseEventHandler(this.ResizePressed);
            
            #line default
            #line hidden
            
            #line 603 "..\..\MainWindow.xaml"
            this.ResizeBottomRight.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ResizePressed);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

