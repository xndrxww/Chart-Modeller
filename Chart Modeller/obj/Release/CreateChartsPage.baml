   M S B A M L   `   `   `  ���� I  EChart Modeller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Chart_ModellerChart_Modeller  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf %    Chart_Modeller.CreateChartsPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:Chart_Modeller     S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  -     N� Title$  CreateChartsPage��.��� .+��� -    ��Name$ 
backButton��$
��Left=�$	��Top=�$��	20,60,0,0q����#131C26��Z� .��� $��Center=�.+��� $��Center=�$��7,0,-7,0q�
Назад�� $��/Images/back.png��$��Center=�$��
-18,0,18,0q�$��14��&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��$	��Top=�$
��Left=�$	��900��$	��310��$��
50,110,0,0q�   
IsReadOnly . ��Z�   LiveCharts.Wpf.PieChart  -     Name$ pieChart��$��Bottom=�$��225,0,500,95q�$	��300��&   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�#  LiveCharts.Wpf.CartesianChart  -     Name$ chart��$	��900��$	��360��$��Bottom=�$
��Left=�$��	50,0,0,80q�   AxisX   LiveCharts.Wpf.Axis     Labels �� *
LabelsList+�� $��Right=�$	��250��$��Center=�	 K� ZIndex$	 1��$��0,112,25,118q�.+��� .+��� $��Center=�$	��Top=�$��15(�
�������Значение X$  HandyControl.Controls.ComboBox  -   
  Name$
 
columnsBox��$	��Top=� �� SelectedIndex$ -1��'  !HandyControl.Controls.InfoElement   Placeholder$ Значение X��   SelectionBrush �	#FF131C26$��
35,15,35,0q���Z� �� $��0,40,0,0q�.+��� $��Center=�$	��Top=�$��15(�
�������Значение Y  -   $

 xBox��$	��Top=�$ -1��$ Значение Y�� �	#FF131C26$��
35,15,35,0q���Z� �� $��0,40,0,0q�.+��� $��Center=�$	��Top=�$��15(�
�������#!Название значения#  HandyControl.Controls.TextBox  -     Name$ lineNameTxt��$��Center=�$	��Top=�$	��180�� �� 	MaxLength$ 50��$' !Название значения��$��
35,15,35,0q���Z� '	  !HandyControl.Controls.ColorPicker	  -	   $��colorPicker��$��Center=�$��0,40,0,0q�$	��180���� $��0,40,0,0q�.+��� $��Center=�$	��Top=�$��15(�
�������!Название графика  -
   $ chartNameTxt��$��Center=�$	��Top=�$	��190��$ 40��$% Название графика��$��
35,15,25,0q���  �� Orientation$ 
Horizontal=�$��0,40,0,0q�$��Center=�.+��� -   $ 	addButton��$��Bottom=�$	��135�����#131C26
�������$
��Left=�$��0,0,15,0q�.��#!Добавить значения�� -   $ 
saveButton��$��Bottom=�$��85��
����������#131C26$��Right=�{�.  ��Z� .��Сохранить�� $��Right=�$	��250��$	��699��$��Center=� �� RadiusY$ 12�� �� RadiusX$ 12�����#131C26$��0,100,25,101q��� -   $ clearButton��$��Bottom=�$
��Left=�$	��130��$��
50,0,0,440q���Z� .��� .+��� $��Center=�$��8,0,-8,0q�Очистить график�� $��/Images/delete.png��$��Center=�$��
-11,0,89,0q�$��15���� -    ��Name$ 	chartName��$��Center=�$��25(�$��	0,0,270,5q� �� TextAlignment$ Center=�$��Center=�� $��Bottom=����#131C26$��52��$	 -1��.+�  -   $
 	tablesBox��$��Center=�$��Center=�$	��200����Z� 