   M S B A M L   `   `   `  ����P  LEllaMakerTool.Message, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\Bclr-namespace:EllaMakerTool.Message;assembly=EllaMakerTool.MessageEllaMakerTool.Message  5       EllaMakerTool.WPF>   8EllaMakerTool.WPF.Controls.UserControls.SelectMoveTarget   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N JAy.MvcFramework.Wpf, Version=7.2.6.0, Culture=neutral, PublicKeyToken=nullW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      VmessageBclr-namespace:EllaMakerTool.Message;assembly=EllaMakerTool.Message     5        � 	Resources  5         MyHieraTemp �� DataType5   /EllaMakerTool.Message.DocumentTreeNodelApiModel �� ItemsSource �� Orientation��%�   &	        �� 5	   
   "  67    5
   #   �� *	Childrens+.R�5      �� $ 
Horizontal=�6   .+�5      �� $��Center=�6   ��6:   �� *Name+5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��36 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ tipPath��5      $��0 0 0 0q�6!   
�������$��12(�6J   $��Center=�5        WindowBackground#��[ 62   $��1 1 1 0q�5      $��移动到：��6*   R� 5   
   -   5        $��1��5      $��1q�6    
�������#��[  5      ��5   L   �� *
TreeSource+��5      �� 5        MyTreeViewFolderItem �� BasedOn# [ 6    �� 
TargetType" Q�6H   .�5      ��  �� Property �@Q�
IsExpanded	 �� Value	 . 63   5      5      5       5!      