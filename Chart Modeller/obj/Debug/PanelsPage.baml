   M S B A M L   `   `   `  ����I  EChart Modeller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Chart_ModellerChart_Modeller  5          Chart_Modeller.PanelsPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Chart_Modeller     5      S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      -   5          N� Title$  
PanelsPage��5
      .��5      � .+�5   
   �� $��
0,120,0,70q�6   .��5      �� -   5         ��Name$ sp��5       �� Orientation$ Vertical=�6%   5      �� 5   
   -   5        $ stackPanelDefault��5      $��Center=�60   $��Center=�6K   $
��1000��6h   $y�Hidden=�6u   .+�5      �� -   5         ��Name$ defaultText��5      $��35(�6-    �� TextAlignment$ Center=�6;   ���#131C266i   �� 5      $h��bДля создания панели нажмите на кнопку "Cоздать панель"��6   $��25(�6`   $ Center=�6n   $��0,10,0,0q�6�   ���#6969696�   5      � 5   
   $��Bottom=�6   ���#131C26$��52��6?    K� ZIndex$ -1��6K   .+�5      �� -   5         ��Name$ addPanel��5      $
��Left=�6'   $��Center=�6B   $	��175��6]   $��35��6i   $��16(�6�   $��30,0,0,0q�6�   ���#131C26
�������$��Center=�6�   .��5      � $��16��6   $��Center=�6#   .+�5      �� $��Center=�6!   $��Center=�6<   $��	50,0,24,0q�6Y   Создать панель6|   �� 5      $��/Images/plus.png��6   $��16��66   $��
25,0,146,0q�6A   $��Center=�6U   $
��Fill=�6p   5      5      �� 5      -   5        $ 
exitButton��5      $��Right=�6)   $��Center=�6E   $��35��6`   $��16(�6l   $��Center=�6z   $��0,0,30,0q�6�   $	��100��6   � ToolTip 6�   ��  �� Path$ 
ServerName���6�   Z� .��5      � .+�5      �� $��Center=�6!   $��Center=�6<   $��	31,0,15,0q�6Y   
Выход6s   �� 5       $��/Images/exit.png��6   $��16��66   $��Center=�6A   $��9,0,67,0q�6\   5!      5"      $  HandyControl.Controls.ComboBox  5#      -   5        	  Name$	 dbBox��5#      $��Center=�6)   $	��200��6D   
   SelectionBrush
 �	#FF131C26��6z   Z� 6�   5$      5%      5&      