   M S B A M L   `   `   `  ����Q  MEllaMakerTool.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Dclr-namespace:EllaMakerTool.Controls;assembly=EllaMakerTool.ControlsEllaMakerTool.Controls  5      R NEllaMakerTool.Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:EllaMakerTool.Converter;assembly=EllaMakerTool.ConverterEllaMakerTool.Converter 5      L HEllaMakerTool.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:EllaMakerTool.WPF.ViewModelsEllaMakerTool.WPF.ViewModels 5      G CMVVMSidekick, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM6clr-namespace:MVVMSidekick.Views;assembly=MVVMSidekickMVVMSidekick.Views 5      -   'EllaMakerTool.WPF.CreatenewFolderWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N JAy.MvcFramework.Wpf, Version=7.2.6.0, Culture=neutral, PublicKeyToken=nullW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,	controls1http://www.ayjs.net/controls  5      ScontrolsDclr-namespace:EllaMakerTool.Controls;assembly=EllaMakerTool.Controls  5      RlocalFclr-namespace:EllaMakerTool.Converter;assembly=EllaMakerTool.Converter  5      3vm*clr-namespace:EllaMakerTool.WPF.ViewModels  5      Amvvm6clr-namespace:MVVMSidekick.Views;assembly=MVVMSidekick  5        � Title$  新建文件夹��5	       � AllowsTransparency .  � WindowStyle$
 None=�5
   ,    � 
ResizeMode$ NoResize=�6?   
������  � WindowStartupLocation$ CenterOwner=�6n   $	��440��5       � SizeToContent$ Height=�6      Window_style#��[  65    � 	Resources 5      >  8EllaMakerTool.WPF.ViewModels.CreatenewFolderWindow_Model  DesignVM��%   &	         5   
   6)   5      .��5      � -   5         �Name$ LayOut��5       � DataContext# [ 6   .+�5   
   �� $��	20 0 20 0q�6   ._�5      �� 	 �� Orientation$	 Vertical=�6   .+�5      ]� -   5        $	��Top=�5      $��36��61   $��5 0 5 0q�6=   .+�5      -  'Ay.Framework.WPF.Controls.AyImageButton  $��Right=�6.   $��40��6E   $��30��6P   $��
0 -5 -25 0q�6\   $��Center=�6p   $��Right=�5   .     AyImageCloseButtonStyle#���  6J   $��0��5   U   ��6.   �� *CommandCloseWindow+.��5      �� $��15��6    $��Uniform=�6+   ��5       �� 
 �� ConverterParameter$
 pc_Button_Close.svg��(  "EllaMakerTool.Converter.XConverter   SvgToXamlConverter �� 	Converter# Z  �� Mode$ OneWay=�5      �� 5      $��新建文件夹��6   $
��Bold"�6,   
������ $
��Left=�6W   $��Center=�6y   $��	-15 0 0 0q�6�   5      �� 5      .+�5      �  � ColumnDefinitions 5      �� 5      $	��100 �6/   6=   �� 5      $��* �6/   6;   .+�5!      �� .+�5"      �� $��	30 20 0 0q�6$   $��	文件名��67   
�����w�6c   �� 5#      $��	30 30 0 0q�6$   $��状态��67   
�����w�y�6D   �� *CanStatusChange+#   BooleanToVisibilityConverter# Z 6�   �� 5$      $��	30 15 0 0q�6$   $��共享范围��67   
�����w�y�6G   �� *
StatusType+-   &EnumDocStatusTypeToVisibilityConverter# Z 6�   �� 5%      $��
30 155 0 0q�6$   $��协同范围��68   
�����w�y�6H   �� *
OpenSync+# Z 6�   5&      �� 5'      $	 Vertical=�6%   $��1��6<   $
��Left=�6L   $��0 20 0 0q�6g   .+�5(      )  #Ay.Framework.WPF.Controls.AyTextBox  -   5          Name$ FilenameBox��5(   2   $	��280��6y   $��32��6�      AyTextCorner$ 2��6�   $��14(�6�   $��Center=�6�    �� 	MaxLength$ 30��6�   
���������6�   �� *DefalutName+.��6  5)      �� 5*      
���  ��$��0 1 0 1q�65   $��25��6F   $
��Left=�6S   ��6u   �� *TipStr+6�   �� 5+      $��0 0 0 0q�6)   $	 
Horizontal=�6:   .+�5,   "   �� $
��Left=�6)    �� CornerRadius$ 3��6D   $��30��6U   $��1q�6a   $	��110��6u   
�������y�6�   �� *CanStatusChange+# Z ._�5-   &   ��  �� 
IsEditable .    WindowBackground#��[ 6B     ComboBoxStyle#��[ 6q   $	��100��6�   $��14(�6�   $��Center=�6�   
�������$��1q�6�    �� DisplayMemberPath$ Value��6O   �� SelectedValuePath$	 Key��6i  ��6  �� *ComBoxItemSource+ �� SelectedValue 6*  �� *
StatusType+5.   '   5/   #   �� 51   "   $��50 1 0 0q�6�   $��Center=�6�   $��Right=�6�   $	 
Horizontal=�6�   y�6-   �� *
StatusType+# Z .+�52   &   �� $��0 0 2 2q�6-   $��30��6>   $��60��6J     NormalBtnBorder#��[ 6U   $��1q�6�     NormalBtnBg#��[ 6�   
�������$��添加部门��6  ��6�   �� *CommandShowShareDeptWin+6  �� 53   &   $��0 0 2 2q�6-   $��30��6>   $��60��6J   #��[ 6U   $��1q�6�   #��[ 6�   
�������$��	添加人��6  ��6�   �� *CommandShowSharePersonWin+6  54   #   55      �� 57      $ 3��6�   $��0 10 0 0q�6�   $	��288��6�   
�������$��1q�6�   y�6&   �� *
StatusType+# Z ._�58   "   �� .+�59   &   �� 
������ $	��120��6H   $	��288��6U   $��Disabled=�6a   $
��Auto=�6�   ��. ��6�   �� *
ShareRange+��5:   *   �� 5;   .   .R�5<   2   � $	��280��6<    K� IsItemsHost .  � Orientation$ 
Horizontal=�6[   5=   /   ��5?   *   �� 5@   .   .R�5A   2   �� $��12(�69   
�������$��1q�6_   $��50��6s   $��Center=�6�   $��Center=�6  $��30��6$  #��[ 60  
���   ���6�   �� *Name+   StrAppendConverter# Z $	
  X ����6r  �� *#!DataContext.CommandDeletShareItem+ �� ElementName$ LayOut����6�  �� 5B   /   5D   '   5E   #   5F      �� 5G      $	 
Horizontal=�6)   $��0 10 0 0q�6B   .+�5H   "   �� $��0 0 0 0q�6�   $��Center=�6�     MyCheckBoxStyle#��[ 6�   
�������y�6,   �� *
StatusType+# Z  d� 	IsChecked 6�   �� *
OpenSync+.��6(  启用协同办公60  �� 5I   "   $��68 1 0 0q�6�   $��Center=�6�   $��Right=�6�   $	 
Horizontal=�6�   y�6-   �� *
OpenSync+# Z .+�5J   &   �� $��0 0 2 2q�6-   $��30��6>   $��60��6J   #��[ 6U   $��1q�6�   #��[ 6�   
�������$��添加部门��6   ��6�   �� *CommandShowSyncDeptWin+6   �� 5K   &   $��0 0 2 2q�6-   $��30��6>   $��60��6J   #��[ 6U   $��1q�6�   #��[ 6�   
�������$��	添加人��6  ��6�   �� *CommandShowSyncPersonWin+6  5L   #   5M      �� 5O      $ 3��6�   $��0 10 0 0q�6�   $	��288��6�   
�������$��1q�6�   y�6%   �� *
OpenSync+# Z ._�5P   "   �� .+�5Q   &   �� 
������ $	��120��6H   $	��288��6U   $��Disabled=�6a   $
��Auto=�6�   ��. ��6�   �� *	SyncRange+��5R   *   �� 5S   .   .R�5T   2   � $	��280��6<    . $ 
Horizontal=�6[   5U   /   ��5W   *   �� 5X   .   .R�5Y   2   �� $��12(�69   
�������$��1q�6_   $��50��6s   $��Center=�6�   $��Center=�6  $��30��6$  #��[ 60  
���   ���6�   �� *Name+# Z $	
  X ����6r  �� *" DataContext.CommandDeletSyncItem+$ LayOut����6�  �� 5Z   /   5\   '   5]   #   5^      5_      5`      5a      �� 5b      $��	0 20 0 20q�6   $��Center=�60   $��Center=�6K   $	 
Horizontal=�6h   .+�5c      $   EllaMakerTool.Controls.FButton    FButton_Transparency#��[ 6'   $��0 0 30 0q�6U   $��30��6g   $��80��6s   #��[ 6~   ���#FFFFFF$��确定��6�   $��1��6�   ��6�   �� *CommandCloseWindow+6�     5d      $��80��6'   $��30��62   #��[ 6>   #��[ 6l   #��[ 6�   $��0��6�   $��取消��6  ��6�   �� *CommandCloseWindow+6  5e      5f      5g      5h      5i      