   M S B A M L   `   `   `  ����I  EChart Modeller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Chart_ModellerChart_Modeller  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5   :   %    Chart_Modeller.CreateChartsPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Chart_Modeller     5      S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  6:   -   5          N� Title$  CreateChartsPage��5
      .��5      � .+�5   
   �� -   5         ��Name$ 
backButton��5      $
��Left=�6%   $	��Top=�6@   $��	20,60,0,0q�6X   ���#131C26��6�   Z� .��5      � $��Center=�6   .+�5      �� $��Center=�6   $��7,0,-7,0q�68   
Назад6Q   �� 5      $��/Images/back.png��6   $��Center=�62   $��
-18,0,18,0q�6M   $��14��6a   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $	��Top=�6%   $
��Left=�6=   $	��900��6X   $	��310��6d   $��
50,110,0,0q�6t      
IsReadOnly . ��6�   Z�   LiveCharts.Wpf.PieChart  5   
   -   5          Name$ pieChart��5      $��Bottom=�6)   $��225,0,500,95q�6E   $	��300��6[   &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�6h   5      #  LiveCharts.Wpf.CartesianChart  5   
   -   5          Name$ chart��5      $	��900��6,   $	��360��68   $��Bottom=�6E   $
��Left=�6`   $��	50,0,0,80q�6{      AxisX 5        LiveCharts.Wpf.Axis     Labels 6   �� *
LabelsList+5      �� 5   
   $��Right=�6   $	��250��61   $��Center=�6>   	 K� ZIndex$	 1��6Y   $��0,112,25,118q�6j   .+�5      �� .+�5      �� $��Center=�6   $	��Top=�69   $��15(�6Q   
�������Значение X6~   $  HandyControl.Controls.ComboBox  5      -   5        
  Name$
 
columnsBox��5      $	��Top=�62    �� SelectedIndex$ -1��6J   '  !HandyControl.Controls.InfoElement   Placeholder$ Значение X��6t      SelectionBrush �	#FF131C26$��
35,15,35,0q�6�   ��6]   Z� 6�   5      �� 5       $��0,40,0,0q�6   .+�5!      �� $��Center=�6   $	��Top=�69   $��15(�6R   
�������Значение Y6     5"      -   5        $

 xBox��5"      $	��Top=�6,   $ -1��6D   $ Значение Y��6n    �	#FF131C26$��
35,15,35,0q�6�   ��6W   Z� 6�   5#      �� 5$      $��0,40,0,0q�6   .+�5%      �� $��Center=�6   $	��Top=�69   $��15(�6Q   
�������#!Название значения6�   #  HandyControl.Controls.TextBox  5&      -   5          Name$ lineNameTxt��5&      $��Center=�62   $	��Top=�6O   $	��180��6g    �� 	MaxLength$ 50��6s   $' !Название значения��6�   $��
35,15,35,0q�6�   ��6�   Z� 6�   5'      '	  !HandyControl.Controls.ColorPicker	  5)      -	   5        $��colorPicker��5)      $��Center=�60   $��0,40,0,0q�6M   $	��180��6_   �� 5+      $��0,40,0,0q�6   .+�5,      �� $��Center=�6   $	��Top=�69   $��15(�6Q   
�������!Название графика6�     5-      -
   5        $ chartNameTxt��5-      $��Center=�63   $	��Top=�6P   $	��190��6h   $ 40��6t   $% Название графика��6�   $��
35,15,25,0q�6�   6�   5.      �� 50       �� Orientation$ 
Horizontal=�6   $��0,40,0,0q�62   $��Center=�6D   .+�51      �� -   5        $ 	addButton��51      $��Bottom=�6,   $	��135��6G   ���#131C26
�������$
��Left=�6{   $��0,0,15,0q�6�   .��6�   #!Добавить значения6�   �� 52      -   5        $ 
saveButton��52      $��Bottom=�6-   $��85��6H   
����������#131C26$��Right=�6{   {�.  ��6�   Z� .��6�   Сохранить6�   53      54      �� 55   
   $��Right=�6   $	��250��60   $	��699��6<   $��Center=�6I    �� RadiusY$ 12��6d    �� RadiusX$ 12��6q   ���#131C26$��0,100,25,101q�6�   �� 57   
   -   5        $ clearButton��57      $��Bottom=�6&   $
��Left=�6A   $	��130��6\   $��
50,0,0,440q�6h   ��6�   Z� .��58      � .+�59      �� $��Center=�6   $��8,0,-8,0q�68   Очистить график6[   �� 5:      $��/Images/delete.png��6   $��Center=�64   $��
-11,0,89,0q�6P   $��15��6d   5;      5<      �� 5=   
   -   5         ��Name$ 	chartName��5=      $��Center=�6'   $��25(�6B   $��	0,0,270,5q�6P    �� TextAlignment$ Center=�6c   $��Center=�6z   6�   � 5>   
   $��Bottom=�6   ���#131C26$��52��6?   $	 -1��6K   .+�5?        -   5        $
 	tablesBox��5?      $��Center=�6.   $��Center=�6I   $	��200��6f   ��6�   Z� 5@      5A      5B      