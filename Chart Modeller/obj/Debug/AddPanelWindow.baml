   M S B A M L   `   `   `  ����I  EChart Modeller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Chart_ModellerChart_Modeller  5   	   #    Chart_Modeller.AddPanelWindow   5   	   S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Chart_Modeller     5   	     � Title$  Chart Modeller��5   	   $	��400��6    $	��680��6-    � WindowStartupLocation$ CenterScreen=�69    � 
ResizeMode$ NoResize=�6^    � AllowsTransparency .  � WindowStyle$
 None=�6�    #� VisualTextRenderingMode$ 	ClearType=�6�   .��5
      � .+�5   
   �� ���#131C266'   � 5   
   $	��Top=�6   $
��Left=�6'   $��35��6B    K� ZIndex$ 1��6N   .+�5      �� $��Center=�6   $
��Left=�64   $��40,0,0,0q�6O   
�������Chart Modeller6�   �� 5      $��/Images/line_chart.ico��6   $��18��64   $��Center=�6?   $
��Left=�6Z   $��15,0,0,0q�6u   5      � 5   
   -   5        $	��Top=�5      $��35��6'   ���#131C26.+�5      �� -   5         ��Name$ closeAppButton��5      $��Right=�6-   $��Center=�6I   ��6d   Z� ��6z   Z� .��5      �� $��/Images/close.png��6   5      5      �� 5   
   $��Center=�6   $	��Top=�61   $��0,90,0,0q�6I   $#��Создание панели��6[   $��30(�6r   #  HandyControl.Controls.TextBox  5   
   -   5          Name$ panelNameTxt��5      $��Center=�6+   $��Center=�6F   $	��300��6c   $��50��6o   $��25(�6{   	 �� 	MaxLength$	 50��6�   '  !HandyControl.Controls.InfoElement
   Placeholder$
 Название��6�   �� 5   
   -   5        $ okButton��5      $��Bottom=�6#   $��Center=�6>   $	��300��6[   $��50��6g   $��0,0,0,50q�6s   $��28(�6�   ���#131C26.��6�   Создать6�   5      5      