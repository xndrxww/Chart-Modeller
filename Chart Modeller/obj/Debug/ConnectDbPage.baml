   M S B A M L   `   `   `  ����I  EChart Modeller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Chart_ModellerChart_Modeller  5      "    Chart_Modeller.ConnectDbPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Chart_Modeller     5      S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5        N� Title$  ConnectDbPage��5
       � 	Resources 5         cmbTemplate��%8  &	        �� 5   
   .R�5      � $��0 5 0 5q�6   $��18��6)   .+�5      �� $��20��6   $��20��6#   $
��Fill=�6/   $��Center=�6W   $��0,0,15,0q�6r   ��6>   �� *Photo+6r   �� 5      $��Center=�62   $��Center=�6M   $��16(�6j   ��6   �� *Name+6j   5      5      5      .��5      � .+�5   
   �� $.��(Подключение к серверу��6   $��40(�61   $
��Left=�6?   $	��Top=�6Z   $��155,210,0,0q�6r   �� 5   
   $
��Left=�6   $	��Top=�60   $��249,336,0,0q�6H   $	��260��6]   .+�5      #  HandyControl.Controls.TextBox  -   5          Name$ 	serverTxt��5      $��40��6-   $��18(�69    �� TextAlignment$ Center=�6G   '  !HandyControl.Controls.InfoElement   Placeholder$ Имя сервера��6^     5      -   5        $ loginTxt��5      $��40��6+   $��18(�67   $ Center=�6E   $ 
Логин��6\   $��0,50,0,0q�6     5      -   5        $ passwordTxt��5      $��40��6.   $��18(�6:   $ Center=�6H   $ Пароль��6_   $��0,50,0,0q�6�   $  HandyControl.Controls.ComboBox  5      -   5          Name$ serverTypeBox��5      $��0,50,0,0q�61   #��[  6C    �� SelectedValuePath$
 Name��6o   �� 5      -   5         ��Name$ connectButton��5      $	��124��6,   $��40��68   $��20(�6D   $��
Войти��6n   $��0,40,0,0q�6~   ���#131C26
���������6�   Z� 5      � 5"   
   $��628,-387,-1078,107q�6    K� ZIndex$ -1��6+   z�5$      �� 5%      	 �� Angle$	 50Q�6"   .+�5)      �� 
 �� Orientation$
 
Horizontal=�6   $��Right=�62   $&�� -63.757,106.684,-46.244,-106.684q�6N   .+�5*      �� .+�5+      �� $	��800��6   $��50��5,      ���#3DDDC2$��	150,0,0,0q�5.       �� CornerRadius$ 20��5/      �� 50      $	��600��6   $��50��51      ���#FC6586$��
150,15,0,0q�53      $ 20��54      �� 55      $	��700��6   $��50��56      ���#335882$��
150,15,0,0q�58      $ 20��59      5:      �� 5<      .+�5=      �� $	��700��6   $��50��5>      ���#3DDDC2$��	150,0,0,0q�5@      $ 20��5A      �� 5B      $	��900��6   $��50��5C      ���#FC6586$��
150,15,0,0q�5E      $ 20��5F      �� 5G      $	��800��6   $��50��5H      ���#335882$��
150,15,0,0q�5J      $ 20��5K      5L      5M      5N      5O      5P      