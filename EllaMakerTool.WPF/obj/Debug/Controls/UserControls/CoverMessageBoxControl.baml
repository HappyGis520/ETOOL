   M S B A M L   `   `   `  ����Q  MEllaMakerTool.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:EllaMakerTool.Controls;assembly=EllaMakerTool.ControlsEllaMakerTool.Controls  5   	    EllaMakerTool.WPFD   >EllaMakerTool.WPF.Controls.UserControls.CoverMessageBoxControl   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N JAy.MvcFramework.Wpf, Version=7.2.6.0, Culture=neutral, PublicKeyToken=nullW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   YcontrolsDclr-namespace:EllaMakerTool.Controls;assembly=EllaMakerTool.Controls     5   	   -   5          � Title$  CoverMessageBox��5   	    � AllowsTransparency .  � WindowStyle$
 None=�6;    � ShowInTaskbar .  
������  � WindowStartupLocation$ CenterScreen=�5	   	    � SizeToContent$ WidthAndHeight=�5
   	    � 
ResizeMode$ 	CanResize=�6(   ��5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � ��	 �� Value	 5      �� 5      
 �� 
TargetType"
 �6&   .R�5      � $��2q�6   .+�5      �� #��� ��6(    �� RadiusX$ 3��5   (    �� RadiusY$ 3��64    J� Effect 5   "   3  -System.Windows.Media.Effects.DropShadowEffect  5   &      
BlurRadius$ 5Q�67      ShadowDepth$ 0Q�6F      Color$
 Gray��6V   5      �� 5      #��z��6%   #��z��5   %   #��z��5   %    �� Padding# z��5   %    J� SnapsToDevicePixels# z 5   %    �� CornerRadius$ 2��5   %   ._�5   "   �� 5      5      5       5"      5#      .��5%      � -   5         �Name$
 grid��5%      .+�5&   
   �� $��	20 0 20 0q�6      WindowBackground#��[  6$   ._�5'      ]� .+�5(      ]� -   5        $	��Top=�5(      $��36��61   $��5 0 5 0q�6=   .+�5*      �� $��
    提示��6   $
��Bold"�6-   
������ $��Center=�5+      $��Center=�6@   5,      �� 5-      -   5         ��Name$ TipStrLb��5-      $	��Top=�6*   $��30��6?   $��0 20 0 0q�6K   $��Center=�5.      �� 5/      -   5        $ 	MainStrLb��5/      $	��Top=�6+   $��30��6@   $��Center=�6L   
���   �$��16(�50   +   �� 51      $	��Top=�6   $��	0 30 0 30q�62   $��Center=�6E   $��Center=�52       �� Orientation$ Vertical=�6:   .+�53      $   EllaMakerTool.Controls.FButton  -   5          Name$ okBtn��53   '   $	��300��66   $��30��6B     FButton_Transparency#��[ 54   '   ���#FFFFFF55   '     56      -   5        $ 	cancelBtn��56   '   $	��300��6:   $��30��6F   $��0 10 0 0q�6R   #��[ 57   '     NormalBtnBg#��[ 58   '   #��[  59   '   6V   5:      5;      5<      5=      5>      