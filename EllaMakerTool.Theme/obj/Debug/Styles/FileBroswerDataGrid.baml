   M S B A M L   `   `   `  ����N  JEllaMakerTool.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:EllaMakerTool.ThemeEllaMakerTool.Theme  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N JAy.MvcFramework.Wpf, Version=7.2.6.0, Culture=neutral, PublicKeyToken=nullR NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   TabItem.Selected.Background �� EndPoint �� 
StartPoint � Color � Offset # DefaultColumnHeaderGripperStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 �� Padding  DefaultDataGridColumnHeader=   7System.Windows.Controls.Primitives.DataGridColumnHeader  UserInfoForeground  
FontFamily  	LineBrush  WindowBackground � ColumnDefinitions ��Name ��Name J� SnapsToDevicePixels ��Name n�Name �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName P� Property P� Value  DefaultDataGridRow)  #System.Windows.Controls.DataGridRow �� Triggers  ItemSelectedForeground ^� 
Conditions �� Binding �� Path �� RelativeSource 	 ItemMouseOverForeground  
 DefaultRowHeaderGripperStyle  DefaultDataGridRowHeader:  4System.Windows.Controls.Primitives.DataGridRowHeader  DefaultDataGridCell*  $System.Windows.Controls.DataGridCell �� ContentStringFormat �� ContentStringFormat  FileBroswerDataGridStyle&   System.Windows.Controls.DataGrid  ControlBorderBrush  HeaderBorderBrush ��Name  ��Name! � RowDefinitionsG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter"  Name7  1System.Windows.Controls.DataGridHeadersVisibility#   Column$ �� ConverterParameter!%   HeadersVisibilityConverter& �� 	Converter' �� AncestorType( ��Name) �� CanContentScroll* � Cursor+ ��Name, �� ScrollableHeight- � Maximum. �� Orientation/ �� ViewportHeight0 �� ViewportSize1 �� VerticalOffset2 � Value*3 �� #ComputedVerticalScrollBarVisibility4 J� Opacity5 �� ScrollableWidth6 �� HorizontalOffset7 �� ViewportWidth,8 �� %ComputedHorizontalScrollBarVisibility  DisableOpacity�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      +local!clr-namespace:EllaMakerTool.Theme  5        5      �� 5   
   $ Colors.xaml?�6   �� 5   
   $ DefaultScrollBar.xaml?�6   %=   &	        &	 �     &	 �    7  7  7  7  7  7  &	 e
    7  7   7  7   7  7   7 	 &	
 �    &	     7 
 7 
 &	     7  &	 �    7  7  7  7  7  7  7  7  �� 5      $	 0,1&�6>   $	 0,0&�6M   .��5	   
   � $ #f7f7f7��6   $	 0.0Q�6'   � 5
   
   $ #f7f7f7��6   $	 1.0Q�6'   5      �� 5      " n�64   .�5   
   ��  � ��$ 8��6"   �� 5   
    � ��
 ���� 6'   �� 5   
    �@�Cursor$ SizeWE��6#   �� 5   
    � �� 5      �� 5      "	 n�6"   .R�5      �� #
 z��6   
������ $��0 0 0 2q�6Z   ._�5      �� #��z��6$   $��1��6g   #��z��5   $   5      5      5      5      �� 5      "   60   .�5   
   ��  �@J�SnapsToDevicePixels . 60   �� 5    
    � ��$ 5��6%   �� 5!   
    � ��$ 25��6&   �� 5"   
    � ��$ 40��6#   �� 5#   
    � ��8   6'   �� 5$   
    � ��$
 Left=�67   �� 5%   
    � ��$ Center=�65   �� 5&   
    � ��$ 10,4,4,7q�6$   �� 5'   
    � ��$ 0,0,0,0q�6#   �� 5(   
    � ��8  6'   �� 5)   
    � ��$ 14(�6%   �� 5*   
    � ��$ 0,0,0,1q�6,   �� 5+   
    � ��8  6(   �� 5,   
    � ��8  6'   �� 5-   
    � �� 5.      �� 5/      "	   6"   .R�50      �  51      �� 52      $��* �6/   �� 53      $
��auto �6/   .+�55      �� $ BackgroundBorder��6!   #��z��6;   $��2��56   !   #��z��65   #��z��57   !   �� 58      $ HeaderContent��6+   #��z��59   +   #��z��5:   +   #��z��5;   +   #��z��5<   +   #��z��5=   +   ��. # z 5?   +   �� 5@      $ 	SortArrow��6$   $	����67   $��1��6G   $��20��6W   $y�	Collapsed=�5A   $   $��16(�6;   $��1,1,3,1q�6I   n� 5C      $ PART_LeftHeaderGripper��6    $
��Left=�6@   $
��Left=�5D       8�� 5E       n� 5G      $ PART_RightHeaderGripper��6    $��Right=�6A   $��Right=�5H       
������ 8�� $��1��5I   Y   5J       5K      ]� 5M       5N      �� 5O   "    �@J�IsMouseOver . 6C   �� 5P   "    6E   Z�  �@  SortDirection�� 5Q   "    �@  CanUserSort . 6C   ./�5S      �� $ 	SortArrow��6%    � y�$ Visible=�6R   5T      P� 5V       �@  CanUserSort . . �5W      ��  �@�Cursor$
 Hand��67   5X      P� 5Z       �@  SortDirection$ 	AscendingX�6;   . �5[      �� $ 	SortArrow��6%    � y�$ Visible=�6R   5\      P� 5^       �@  SortDirection$ 
DescendingX�6;   . �5_      �� $ 	SortArrow��6%    � y�$ Visible=�6R   �� 5`      $ 	SortArrow��6%    � ��$	 ��6L   5a      P� 5c       �@  DisplayIndex$ 2��6:   . �5d      �� $ PART_LeftHeaderGripper��6M    � y�$ 	Collapsed=�6;   6M   5e      5g      5i      5j      �� 5m      "  6'   .�5n   
   ��  � ��8   6'   �� 5o   
    � �� �#FFFFFF6'   �� 5p   
    � ��$ 0,0,0,0q�6#    5r   
   P� 5v       �@ 
IsSelected . . �5w      ��  � ��8  6/   �� 5x       � ��8  6/   5y      ]� 5z       5{      �� 5|       �@ 
IsSelected . 66   �� 5}       �@��IsSelectionActive . 6F   ./�5      ��  � ��8  6/   �� 5�       � ��8  6/   5�      ^� 5�       5�      �� 5�       6    �� $ IsMouseOver� �� *Self+ True6k   .0�5�      ��  � ��8  6/   �� 5�       � ��8  6/   5�      5�      �� 5�      " n�61   .�5�   
   ��  � ��$ 6��6#   �� 5�   
    � ��
 ���� 6'   �� 5�   
    �@�Cursor$ SizeNS��6#   �� 5�   
    � �� 5�      �� 5�      "	 n�6"   .R�5�      �� #
 z��6   
������ 6A   5�      5�      5�      �� 5�      "  6-   .�5�   
   ��  � ��$ Stretch=�67   �� 5�   
    � ��$ Center=�65   �� 5�   
    � ��
 ���� 6'   �� 5�   
    � ��
 ���� 6(   �� 5�   
    � ��$ 0,0,1,0q�6,   �� 5�   
    � ��$ 0,0,0,0q�6#   �� 5�   
    � �� 5�      �� 5�      "	  6"   .R�5�      � .+�5�      �� #��z��6!   #��z��5�   !   #��z��5�   !   #
 z��5�   !   #��z��5�   !    . ._�5�      �� # z 6/   #��z��5�   /   #��z��5�   /   5�      n� 5�      $ PART_TopHeaderGripper��6    $	��Top=�6?   $	��Top=�5�       
������ 8��  5�       n� 5�      $ PART_BottomHeaderGripper��6    $��Bottom=�6B   $��Bottom=�5�       8�� 6;   5�      5�      5�      5�      �� 5�      "  5�      .�5�   
   ��  � ��
 ���� 6'   �� 5�   
    � ��
 ���� 6(   �� 5�   
    � ��$ Stretch=�67   �� 5�   
    � ��$ Center=�65   �� 5�   
    �@J�SnapsToDevicePixels . 60   �� 5�   
    � ��$ 0q�6$   �� 5�   
    � �� 5�      �� 5�      "	  6"   .R�5�      �� #��z��6   #��z��5�      #��z��5�      # z 5�      ._�5�      �� #��z��6+   #��z��5�   +   # z 5�   +   #��z��5�   +   # z 5�   +   #��z��5�   +   #��z��5�   +   5�       5�      P� 5�       �@ 
IsSelected . . �5�      �� 8    � ��6;   5�      5�      5�      5�      �� 5�      "  6-   .�5�   
   ��  �@ MinRowHeight$ 25Q�6)   �� 5�   
    � ��8   6'   �� 5�   
    � ��8  6(   �� 5�   
    � ��$ 1q�6,   �� 5�   
     �@ HorizontalGridLinesBrush �#e6e6e665   �� 5�   
    �@ VerticalGridLinesBrush �#e6e6e663   �� 5�   
    �@ ColumnHeaderStyle8  6.   �� 5�   
    �@ RowHeaderStyle8  6+   �� 5�   
    �@ 	CellStyle8  6&   �� 5�   
    �@ RowStyle8  6%   �� 5�   
    �@ HeadersVisibility$	 All=�6.   �� 5�   
    �@ EnableRowVirtualization . 64   �� 5�   
   " �@ EnableColumnVirtualization .  67   �� 5�   
    �@ AutoGenerateColumns .  60   �� 5�   
    �@ 
IsReadOnly . 6'   �� 5�   
    �@ SelectionMode$ Single=�6*   �� 5�   
    �@ SelectionUnit$ FullRow=�6*   �� 5�   
    �@ GridLinesVisibility$	 All=�60   �� 5�   
    �@��AlternationCount$ 2��6-   �� 5�   
    � �� . 6:   �� 5�   
    �@$�IsVirtualizing . 6B   �� 5�   
    �@$�VirtualizationMode$ 	Recycling=�6F   �� 5�   
   " �@��IsDeferredScrollingEnabled .  6D   �� 5�   
   " �@ DropLocationIndicatorStyle 5�      �� 5�      " ��6   .�5�      ��  � ��8  63   �� 5�       � ��$	 2.5��6.   �� 5�       � �� 5�      �� 5�      "	 ��6.   .R�5�   "   �� #��z��6,   #��z��6P   #��z��5   ,   5     5     5     5     �� 5  
    � �� 5	     �� 5
     "	  6"   .R�5     �� $ border��5  Q   #��z��5     #��z��5     #��z��5     #
 z��6G    . ._�5     �� $ DG_ScrollViewer��5  '   $��1��5  '   $��Stretch=�64   $��Stretch=�6P   |�.  ��5     �� 5  "   "	 ��62   .R�5  &   �� ._�5  *   �  5  .   �� 5  2   $  col_rowheader��6C   $��1 �6Z   �� 5  2   $��* �6C   ! 5  .   �� 5  2   $
��Auto �6@   �� 5  2   $��* �6@   .+�5  .     $!" PART_ColumnHeadersPresenter��6M   $��1��5  M   $��2��6]   y�5  M   �� *HeadersVisibility+#$ Z# #& Z%  �� "'  � 5   .   $��1��63   $��2��6@   .+�5!  2   �� $!( PART_ScrollContentPresenter��6I   #) z��5"  I   $��2��5#  I   #* z* 5$  I   #��z��5%  I   #��z��5&  I   $��Stretch=�5'  I   $��Stretch=�5(  I   �� 5)  2   $+ PART_VerticalScrollBar��6<   $��Right=�5*  <   #- z, 5+  <   $. Vertical=�5,  <   #0 z/ 5-  <   #2 z1 5.  <   #y�z3 5/  <   $	4 0.5Q�50  <   �� 51  2   $+ PART_HorizontalScrollBar��6<   #- z5 52  <   $. 
Horizontal=�53  <   $��Bottom=�54  <   #2 z6 55  <   #0 z7 56  <   #y�z8 57  <   $	4 0.5Q�58  <   59  /   5:  +   5;  '   5<  #   .��5>     �� # z 6-   5?     5@      5A     P� 5B      � {� .  . �5C     �� 8  $ border��6`    �@J�Opacity5D     5F     5H      5I  
   P� 5J      �@��
IsGrouping . . �5K     ��  � �� .  6B   5L     5N     5O     