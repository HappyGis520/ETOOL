   M S B A M L   `   `   `  ����Q  MEllaMakerTool.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:EllaMakerTool.Controls;assembly=EllaMakerTool.ControlsEllaMakerTool.Controls  5      L HEllaMakerTool.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:EllaMakerTool.WPF.ViewModelsEllaMakerTool.WPF.ViewModels 5      G CMVVMSidekick, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM6clr-namespace:MVVMSidekick.Views;assembly=MVVMSidekickMVVMSidekick.Views 5      R NEllaMakerTool.Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:EllaMakerTool.Converter;assembly=EllaMakerTool.ConverterEllaMakerTool.Converter 5      *   $EllaMakerTool.WPF.ChangeStatusWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N JAy.MvcFramework.Wpf, Version=7.2.6.0, Culture=neutral, PublicKeyToken=nullW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      YcontrolsDclr-namespace:EllaMakerTool.Controls;assembly=EllaMakerTool.Controls     5      9vm*clr-namespace:EllaMakerTool.WPF.ViewModels     5      Gmvvm6clr-namespace:MVVMSidekick.Views;assembly=MVVMSidekick     5      ,	controls1http://www.ayjs.net/controls  5      V	converterFclr-namespace:EllaMakerTool.Converter;assembly=EllaMakerTool.Converter  5        � Title$  修改文件状态��5	       � AllowsTransparency .  � WindowStyle$
 None=�5
   ,    � 
ResizeMode$ NoResize=�6?   
������  � WindowStartupLocation$ CenterOwner=�5      $	��440��5       � SizeToContent$ Height=�6      Window_style#��[  65    � 	Resources 5      ;  5EllaMakerTool.WPF.ViewModels.ChangeStatusWindow_Model  DesignVM��%   &	         5   
   6&   5      .��5      � -   5         �Name$ LayOut��5       � DataContext# [ 6   .+�5   
   �� $��	20 0 20 0q�6   ._�5      �� 	 �� Orientation$	 Vertical=�6   .+�5      ]� -   5        $	��Top=�5      $��36��61   $��5 0 5 0q�6=   .+�5      -  'Ay.Framework.WPF.Controls.AyImageButton  $��Right=�6.   $��40��6E   $��30��6P   $��
0 -5 -25 0q�6\   $��Center=�6p   $��Right=�5   '     AyImageCloseButtonStyle#���  6C   $��0��5   N   ��6'   �� *CommandCloseWindow+.��5      �� $��15��6    $��Uniform=�6+   ��5   $   �� 
 �� ConverterParameter$
 pc_Button_Close.svg��(  "EllaMakerTool.Converter.XConverter   SvgToXamlConverter �� 	Converter# Z  �� Mode$ OneWay=�5      �� 5      $��修改文件状态��6   $
��Bold"�6-   
������ $
��Left=�5      $��Center=�6>   $��	-15 0 0 0q�6`   5      �� 5      .+�5      �  � ColumnDefinitions 5       �� 5!      $	��100 �6/   �� 5"      $��* �6/   .+�5$      �� .+�5%      �� $��	30 20 0 0q�6$   $��状态��67   
�����w�y�5&   $   �� *CanStatusChange+   BoolToVisHideConverter# Z �� 5'      $��	30 15 0 0q�6$   $��共享范围��67   
�����w�y�5(   $   �� *
StatusType+-   &EnumDocStatusTypeToVisibilityConverter# Z 5)   $   �� 5*      $��
30 155 0 0q�6$   $��协同范围��68   
�����w�y�5+   $   �� *
OpenSync+#   BooleanToVisibilityConverter# Z 5,   $   5-      �� 5.      $	 Vertical=�6%   $��1��6<   $
��Left=�6L   $��0 20 0 0q�6g   .+�50      �� $	 
Horizontal=�6)   .+�51   "   �� $
��Left=�53   %    �� CornerRadius$ 3��6@   $��30��6Q   $��1q�6]   $	��110��54   %   
�������y�52   %   �� *CanStatusChange+# Z ._�55   &   ��  �� 
IsEditable .    WindowBackground#��[ 6B     ComboBoxStyle#��[ 56   /   $	��100��6V   $��14(�6b   $��Center=�57   /   
�������$��1q�58   /    �� DisplayMemberPath$ Value��59   T    �� SelectedValuePath$	 Key��5:   /   ��58   C   �� *ComBoxItemSource+ �� SelectedValue 59   /   �� *
StatusType+5:   /   5;   #   �� 5<   "   $��50 1 0 0q�5>   %   $��Center=�67   $��Right=�6R   $	 
Horizontal=�5?   %   y�5=   %   �� *
StatusType+# Z .+�5@   &   �� $��0 0 2 2q�6-   $��30��6>   $��60��6J     NormalBtnBorder#��[ 5A   -   $��1q�6\     NormalBtnBg#��[ 5B   -   
�������$��添加部门��5D   -   ��5C   -   �� *CommandShowShareDeptWin+5D   -   �� 5E   &   $��0 0 2 2q�6-   $��30��6>   $��60��6J   #��[ 5F   -   $��1q�6\   #��[ 5G   -   
�������$��	添加人��5I   -   ��5H   -   �� *CommandShowSharePersonWin+5I   -   5J   #   5K      �� 5M      $ 3��5O   !   $��0 10 0 0q�62   $	��288��6D   
�������$��1q�5P   !   y�5N   !   �� *
StatusType+# Z ._�5Q   "   �� .+�5R   &   �� 
������ $	��120��6H   $	��288��6U   $��Disabled=�5S   .   $
��Auto=�5T   .   ��. ��5U   S   �� *
ShareRange+��5V   *   �� 5W   .   .R�5X   2   � $	��280��6<    K� IsItemsHost .  � Orientation$ 
Horizontal=�6[   5Y   /   ��5[   *   �� 5\   .   .R�5]   2   �� $��12(�69   
�������$��1q�6_   $��50��5^   9   $��Center=�5`   9   $��Center=�6T   $��30��5a   9   #��[ 6E   
���   ���5_   9   �� *Name+   StrAppendConverter# Z $	
  X ����5c   9   �� *#!DataContext.CommandDeletShareItem+ �� ElementName$ LayOut����5d   9   �� 5e   /   5g   '   5h   #   5i      �� 5j      $	 
Horizontal=�6)   $��0 10 0 0q�6B   .+�5k   "   �� $��0 0 0 0q�5m   D   $��Center=�6U     MyCheckBoxStyle#��[ 5n   %   
�������y�5l   %   �� *
StatusType+# Z  d� 	IsChecked 5m   %   �� *
OpenSync+.��5n   `   启用协同办公5p   #   �� 5q   "   $��68 1 0 0q�5s   %   $��Center=�67   $��Right=�6R   $	 
Horizontal=�5t   %   y�5r   %   �� *
OpenSync+# Z .+�5u   &   �� $��0 0 2 2q�6-   $��30��6>   $��60��6J   #��[ 5v   -   $��1q�6\   #��[ 5w   -   
�������$��添加部门��5y   -   ��5x   -   �� *CommandShowSyncDeptWin+5y   -   �� 5z   &   $��0 0 2 2q�6-   $��30��6>   $��60��6J   #��[ 5{   -   $��1q�6\   #��[ 5|   -   
�������$��	添加人��5~   -   ��5}   -   �� *CommandShowSyncPersonWin+5~   -   5   #   5�      �� 5�      $ 3��5�   !   $��0 10 0 0q�62   $	��288��6D   
�������$��1q�5�   !   y�5�   !   �� *
OpenSync+# Z ._�5�   "   �� .+�5�   &   �� 
������ $	��120��6H   $	��288��6U   $��Disabled=�5�   .   $
��Auto=�5�   .   ��. ��5�   S   �� *	SyncRange+��5�   *   �� 5�   .   .R�5�   2   � $	��280��6<    . $ 
Horizontal=�6[   5�   /   ��5�   *   �� 5�   .   .R�5�   2   �� $��12(�69   
�������$��1q�6_   $��50��5�   9   $��Center=�5�   9   $��Center=�6T   $��30��5�   9   #��[ 6E   
���   ���5�   9   �� *Name+# Z $	
  X ����5�   9   �� *" DataContext.CommandDeletSyncItem+$ LayOut����5�   9   �� 5�   /   5�   '   5�   #   5�      5�      5�      5�      �� 5�      $��	0 20 0 20q�6   $��Center=�60   $��Center=�6K   $	 
Horizontal=�5�      .+�5�      $   EllaMakerTool.Controls.FButton  $��0 0 30 0q�6'   $��30��69   $��80��6E     FButton_Transparency#��[ 5�   '   $��确定��5�   N   $��1��6[   ���#FFFFFF��6'   �� *CommandCloseWindow+5�   '     5�      $��80��6'   $��30��62   #��[ 6>   #��[ 5�   '   #��[ 5�   '   $��0��5�   N   $��1q�6c   $��取消��5�   '   ��5�   '   �� *CommandCloseWindow+5�   '   5�      5�      5�      5�      5�      