   M S B A M L   `   `   `  ����Q  MEllaMakerTool.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:EllaMakerTool.Controls;assembly=EllaMakerTool.ControlsEllaMakerTool.Controls  5      R NEllaMakerTool.Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:EllaMakerTool.Converter;assembly=EllaMakerTool.ConverterEllaMakerTool.Converter 5      L HEllaMakerTool.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:EllaMakerTool.WPF.ViewModelsEllaMakerTool.WPF.ViewModels 5      G CMVVMSidekick, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM6clr-namespace:MVVMSidekick.Views;assembly=MVVMSidekickMVVMSidekick.Views 5      '   !EllaMakerTool.WPF.IrRecourdWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N JAy.MvcFramework.Wpf, Version=7.2.6.0, Culture=neutral, PublicKeyToken=nullW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      YcontrolsDclr-namespace:EllaMakerTool.Controls;assembly=EllaMakerTool.Controls     5      XlocalFclr-namespace:EllaMakerTool.Converter;assembly=EllaMakerTool.Converter     5      9vm*clr-namespace:EllaMakerTool.WPF.ViewModels     5      Gmvvm6clr-namespace:MVVMSidekick.Views;assembly=MVVMSidekick     5      ,	controls1http://www.ayjs.net/controls  5        � Title$  更新记录��5	       � AllowsTransparency .  � WindowStyle$
 None=�5
   ,    � 
ResizeMode$ NoResize=�6?   
������  � WindowStartupLocation$ CenterOwner=�5      $	��440��5      $	��396��6      Window_style#��[  6+    � 	Resources 5      8  2EllaMakerTool.WPF.ViewModels.IrRecourdWindow_Model  DesignVM��%   &	         5   
   6#   5      .��5      � -   5         �Name$
 grid��5       � DataContext# [ 6   .+�5   
   ��  �� CornerRadius$ 3��6     	LineBrush#��[ 6"   $��1q�6K     WindowBackground#��[ 5      ._�5      �� 	 �� Orientation$	 Vertical=�6   .+�5      �� -   5        $	 
Horizontal=�5      $��36��66   $��5 0 5 0q�6B   .+�5      �� $��更新记录��6   $
��Bold"�6+   $
��Left=�6=   $��Center=�5      $	��390��6>   -  'Ay.Framework.WPF.Controls.AyImageButton  5      $��Right=�6.   $��40��6E   $��30��6P   $��
0 -5 -25 0q�6\   $��Center=�6p   $��Right=�5   .     AyImageCloseButtonStyle#���  6J   $��0��5   U   ��6.   �� *CommandCloseWindow+.��5      �� $��15��6    $��Uniform=�6+   ��5       �� 
 �� ConverterParameter$
 pc_Button_Close.svg��(  "EllaMakerTool.Converter.XConverter   SvgToXamlConverter �� 	Converter# Z  �� Mode$ OneWay=�5      5      &   System.Windows.Controls.DataGrid  5         	RowHeight$ 24Q�6   $��	30 0 30 0q�6*   #��[ 6=   $��1q�5          CanUserResizeColumns .     CanUserAddRows .    FileBroswerDataGridStyle#��[ 5!   2   $	��315��6d   $   HorizontalScrollBarVisibility$ Hidden=�5"      "   VerticalScrollBarVisibility$
 Auto=�6B      GridLinesVisibility$ 
Horizontal=�5#         MinRowHeight$ 44Q�6<   ��5    /   �� *IrData+   Columns 5$      4  .System.Windows.Controls.DataGridTemplateColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 61      MinWidth$	 100Q�6>      Header$ 作者��6M      CellTemplate 5'      �� 5(   "   .R�5)   &   �� $��5 0 0 0q�61   $	 
Horizontal=�6B   $��Center=�6[   $
��Left=�5*   1   .+�5+   *   �� $��50��60   $
��Left=�5,   0   ��5+   >   �� *CreatorName+5,   0   5-   '   5.   #   50        52      $ * 61   $	 100Q�6;   $ 时间��6J    53      �� 54   "   .R�55   &   �� $��5 0 0 0q�61   $	 
Horizontal=�6B   $��Center=�6[   $
��Left=�56   1   .+�57   *   �� $	��100��60   $
��Left=�59   0   ��58   0   �� *
CreateTime+    DataTimeTostringConverter# Z $
 yyyy-MM-dd HH:mm��59   0   5:   '   5;   #   5=        5?      $ 50 61   $ 20Q�6<   $  ��6J    5@      �� 5A   "   .R�5B   &     $��50��6>     AyImageTransButtonStyle#���  6I   ��5C   7   �� *#!DataContext.CommandDownLoadSingle+ �� ElementName$
 grid����5D   7   �� .��5E   *   �� $��15��60   $��Uniform=�6;   ��5F   4   �� $
 pc_Button_Download.svg��# Z $ OneWay=�5G   '   5H   #   5J      5M      �� 5N      $��0 5 0 0q�6   $��Center=�6.   $��Center=�6I   $	 
Horizontal=�5O      .+�5P      $   EllaMakerTool.Controls.FButton  $��80��6'   $��30��62     FButton_Transparency#��[ 6>     NormalBtnBg#��[ 5Q   '   #��[ 5R   '   $��1��5S   N   $��1q�6c   $��关闭��5T   '   ��5S   '   �� *CommandCloseWindow+5T   '   5U      5V      5W      5X      5Y      