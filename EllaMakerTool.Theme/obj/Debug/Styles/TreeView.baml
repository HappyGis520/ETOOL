   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      R NEllaMakerTool.Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:EllaMakerTool.Converter;assembly=EllaMakerTool.ConverterEllaMakerTool.Converter 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N JAy.MvcFramework.Wpf, Version=7.2.6.0, Culture=neutral, PublicKeyToken=nullR NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   	TreeArrow  TreeViewArrowButtonStyle  �� 
TargetType �� Property �� Value �� 
TargetType �� Padding F�Name �� Angle �� CenterY �� CenterX	 �� Triggers
 P� Property P� Value �� 
TargetName  TreeViewFolderButtonStyle � ColumnDefinitions �� ConverterParameter(   "EllaMakerTool.Converter.XConverter    SvgToXamlConverter �� 	Converter �� Mode  MyTreeViewItem � RowDefinitions ��Name J� SnapsToDevicePixels d�Name �� 	ClickMode d� 	IsChecked �� RelativeSource ��Name ��Name ]� 
Conditions �� Property �� Value  MyTreeViewFolderItem  MyTreeViewListItem K� IsItemsHost $� IsVirtualizing  $� VirtualizationMode�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      1System&clr-namespace:System;assembly=mscorlib  5      U
converter1Fclr-namespace:EllaMakerTool.Converter;assembly=EllaMakerTool.Converter  5      %|  &	        &	 *     7   &	 �    7   &	 7	    7  &	 �    7  &	 �    'R� �    B� 5      $��M0,0 L0,6 L6,0 zC�6%   �� 5      "  d�6-   .�5   
   ��  � |� .  6&   �� 5   
    � ��$ 16��6"   �� 5	   
    � ��$ 16��6#   �� 5
   
    � �� 5      �� 5      " d�6"   .R�5      �� 
������ $��16��66   $ 5,5,5,5q�6B   $��16��6T   ._�5      F� $ 
ExpandPath��6   8��  
������ ���	#FF989898z�5      �� 5   "   $	 135Q�62   $ 3Q�6>   $ 3Q�6J   5      5      	 5      P� 5      
 �@J�IsMouseOver . . �5      �� $ 
ExpandPath��67    � �� �	#FF1BBBFA6O   �� 5      $ 
ExpandPath��65    � ��
 ���� 6M   5      P� 5      
 �@d�	IsChecked$
 TrueY�67   . �5      �� $ 
ExpandPath��6@    � z� 5   "   �� 5   &   $	 180Q�66   $ 3Q�6B   $ 3Q�6N   5      �� 5       $ 
ExpandPath��65    � �� �#ffffff6M   �� 5!      $ 
ExpandPath��67    � �� �	#FF2626266O   5"      5$      5&      5'      �� 5(      "  d�6.   .�5)   
   ��  � |� .  6&   �� 5*   
    � ��$ 40��6"   �� 5+   
    � ��$ 30��6#   �� 5,   
    � �� 5-      �� 5.      " d�6"   .R�5/      �� 
������ $ 5,5,5,5q�66   $��40��6H   ._�50      �  51      �� 52   "   $��16 �63   �� 53   "   $��20 �63   .+�55      F� $ 
ExpandPath��6>   $��Center=�6#   8��  
������ ���	#FF989898z�57   "   �� 58   &   $	 135Q�66   $ 3Q�6B   $ 3Q�6N   5:      �� 5;      $	��pic��6a   $
��Left=�6$   $��-5 0 0 0q�6?   $��1��6Q   $��20��6l   $��16��5<   $   $��UniformToFill=�60   ��5=   $   �� $ Icon_Type_folder.svg��# Z $ OneWay=�5>      5@      	 5A      P� 5B      
 �@J�IsMouseOver . . �5C      �� $ 
ExpandPath��67    � �� �	#FF1BBBFA6O   �� 5D      $ 
ExpandPath��65    � ��
 ���� 6M   5E      P� 5F      
 �@d�	IsChecked$
 TrueY�67   . �5G      �� $ 
ExpandPath��6@    � z� 5H   "   �� 5I   &   $	 180Q�66   $ 3Q�6B   $ 3Q�6N   5K      �� 5L      $ 
ExpandPath��65    � �� �	#FF5959596M   �� 5M      $ 
ExpandPath��67    � �� �	#FF2626266O   �� 5N      $	 pic��6%    5O   %   �� $ Icon_Type_folderHigh.svg��# Z $ OneWay=� � ��5P      5R      5T      5U      �� 5V      "  Q�6#   .�5W   
   ��  � �� 5X      �� 5Y      " Q�6"   .R�5Z      �  5[      �� 5\      $��16 �6/   �� 5]       5_      �� 5`      �� 5a      .+�5c      �� $ Bd��6J   $��2��6!   $��-1600,0,0,0q�65   #��z��5d   !   #��z��5e   !   #��z��5f   !    . 6K   d� 5g      $ Expander��6C   $��30��6'   $��0��63   8��  $ Press=�5j   '    5i   '   �� *
IsExpanded+ �� *TemplatedParent+5j   '   �� 5k      $ PART_Header��6;   $��1��6+   $��Header��6P   #��z��5l   +   # z 5m   +   �� 5n      $ 	ItemsHost��6)   $��2��6<   $��1��6P   $��1��6`   5p      	 5q      P� 5r      
 �@Q�
IsExpanded .  . �5s      �� $ 	ItemsHost��6;    � y�$ 	Collapsed=�6R   5t      P� 5u      
 �@��HasItems .  . �5v      �� $ Expander��6;    � y�$ Hidden=�6Q   5w      P� 5x      
 �@Q�
IsSelected . . �5y      �� $ Bd��6;    � �� �#eaf0fb6K   �� 5z      $ Expander��6;    � �� �#eaf0fb6Q   5{      ]� 5|       5}      �� 5~   "    �@Q�
IsSelected . 6B   �� 5   "    �@Q�IsSelectionActive .  6I   ./�5�      �� $ Bd��6;    � �� �#eaf0fb6K   �� 5�      $ Expander��6;    � �� �#eaf0fb6Q   5�      P� 5�      
 � {� .  . �5�      �� # � ��5�   %    � ��5�      5�      5�      5�      �� 5�      "  Q�6)   .�5�   
   ��  � �� 5�      �� 5�      " Q�6"   .R�5�      �  5�      �� 5�      $��50 �6/   �� 5�       5�      �� 5�      �� 5�      .+�5�      �� $ Bd��6V   $��30��6!   $��2��6-   $��-1600,0,0,0q�6A   #��z��5�   !   #��z��5�   !   #��z��5�   !    . 6K   d� 5�      $ Expander��6R   $��0��6'   $
��Left=�67   8��  $ Press=�5�   '    5�   '   �� *
IsExpanded+ �� *TemplatedParent+5�   '   �� 5�      $ PART_Header��6N   $��	-15 0 0 0q�6+   $��1��6>   $��Header��5�   +   #��z��5�   +   # z 5�   +   �� 5�      $ 	ItemsHost��6)   $��2��6<   $��1��6P   $��1��6`   5�      	 5�      P� 5�      
 �@Q�
IsExpanded .  . �5�      �� $ 	ItemsHost��6;    � y�$ 	Collapsed=�6R   5�      P� 5�      
 �@��HasItems .  . �5�      �� $ Expander��6;    � y�$ Visible=�6Q   5�      P� 5�      
 �@Q�
IsSelected . . �5�      �� $ Bd��6;    � �� �#eaf0fb6K   �� 5�      $ Expander��6;    � �� �#eaf0fb6Q   5�      ]� 5�       5�      �� 5�   "    �@Q�
IsSelected . 6B   �� 5�   "    �@Q�IsSelectionActive .  6I   ./�5�      �� $ Bd��6;    � �� �#eaf0fb6K   �� 5�      $ Expander��6;    � �� �#eaf0fb6Q   5�      P� 5�      
 � {� .  . �5�      �� # � ��5�   %    � ��5�      5�      5�      5�      �� 5�      "  Q�6'   .�5�   
   ��  � �� 5�      �� 5�      " Q�6"   .R�5�      �  5�      �� 5�      $��0 �6/   �� 5�       5�      �� 5�      �� 5�      .+�5�      �� $ Bd��6J   $��2��6!   $��-1600,0,0,0q�65   #��z��5�   !   #��z��5�   !   #��z��5�   !    . 6K   �� 5�      $ PART_Header��6;   $��1��6+   $��Header��6P   #��z��5�   +   # z 5�   +   �� 5�      $ 	ItemsHost��6)   $��2��6<   $��1��6P   $��1��6`   5�      	 5�      P� 5�      
 �@Q�
IsExpanded .  . �5�      �� $ 	ItemsHost��6;    � y�$ 	Collapsed=�6R   5�      P� 5�      
 �@Q�
IsSelected . . �5�      �� $ Bd��6;    � �� �#eaf0fb6K   5�      ]� 5�       5�      �� 5�   "    �@Q�
IsSelected . 6B   �� 5�   "    �@Q�IsSelectionActive .  6I   ./�5�      �� $ Bd��6;    � �� �#eaf0fb6K   5�      P� 5�      
 � {� .  . �5�      �� # � ��5�   %    � ��5�      5�      5�      5�      �� 5�      "  R�6   .�5�   
   ��  � �� . 6:   �� 5�   
    �@$�IsVirtualizing . 6B   �� 5�   
    �@$�VirtualizationMode$ 	Recycling=�6F   �� 5�   
   " �@��IsDeferredScrollingEnabled .  6D   �� 5�   
    � �� 5�      �� 5�      .R�5�      $�  .  . $  	Recycling=�6V   $��0q�5�   -   5�      5      5     5     