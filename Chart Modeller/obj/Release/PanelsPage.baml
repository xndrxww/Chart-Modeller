   M S B A M L   `   `   `  ���� I  EChart Modeller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Chart_ModellerChart_Modeller      Chart_Modeller.PanelsPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:Chart_Modeller     S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  -     N� Title$  
PanelsPage��.��� .+��� $��
0,120,0,70q�.���� -    ��Name$ sp�� �� Orientation$ Vertical=��� -   $ stackPanelDefault��$��Center=�$��Center=�$
��1000��$y�Hidden=�.+��� -    ��Name$ defaultText��$��35(� �� TextAlignment$ Center=����#131C26�� $h��bДля создания панели нажмите на кнопку "Cоздать панель"��$��25(�$ Center=�$��0,10,0,0q����#696969� $��Bottom=����#131C26$��52�� K� ZIndex$ -1��.+��� -    ��Name$ addPanel��$
��Left=�$��Center=�$	��175��$��35��$��16(�$��30,0,0,0q����#131C26
�������$��Center=�.��� $��16��$��Center=�.+��� $��Center=�$��Center=�$��	50,0,24,0q�Создать панель�� $��/Images/plus.png��$��16��$��
25,0,146,0q�$��Center=�$
��Fill=��� -   $ 
exitButton��$��Right=�$��Center=�$��35��$��16(�$��Center=�$��0,0,30,0q�$	��100�� � ToolTip ��  �� Path$ 
ServerName���Z� .��� .+��� $��Center=�$��Center=�$��	31,0,15,0q�
Выход�� $��/Images/exit.png��$��16��$��Center=�$��9,0,67,0q�$  HandyControl.Controls.ComboBox  -   	  Name$	 dbBox��$��Center=�$	��200��
   SelectionBrush
 �	#FF131C26��Z� 