   M S B A M L   `   `   `  ���� I  EChart Modeller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Chart_ModellerChart_Modeller  "    Chart_Modeller.ConnectDbPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:Chart_Modeller     S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    N� Title$  ConnectDbPage�� � 	Resources    cmbTemplate��%�   &	        �� .R�� $��0 5 0 5q�$��18��.+��� $��20��$��20��$
��Fill=�$��Center=�$��0,0,15,0q����� *Photo+�� $��Center=�$��Center=�$��16(����� *Name+.��� .+��� $.��(Подключение к серверу��$��40(�$
��Left=�$	��Top=�$��155,210,0,0q��� $
��Left=�$	��Top=�$��249,336,0,0q�$	��260��.+�#  HandyControl.Controls.TextBox  -     Name$ 	serverTxt��$��40��$��18(� �� TextAlignment$ Center=�'  !HandyControl.Controls.InfoElement   Placeholder$ Имя сервера��  -   $ loginTxt��$��40��$��18(�$ Center=�$ 
Логин��$��0,50,0,0q�  -   $ passwordTxt��$��40��$��18(�$ Center=�$ Пароль��$��0,50,0,0q�$  HandyControl.Controls.ComboBox  -     Name$ serverTypeBox��$��0,50,0,0q�#��[   �� SelectedValuePath$
 Name���� -    ��Name$ connectButton��$	��124��$��40��$��20(�$��
Войти��$��0,40,0,0q����#131C26
���������Z� � $��628,-387,-1078,107q� K� ZIndex$ -1��z��� 	 �� Angle$	 50Q�.+��� 
 �� Orientation$
 
Horizontal=�$��Right=�$&�� -63.757,106.684,-46.244,-106.684q�.+��� .+��� $	��800��$��50�����#3DDDC2$��	150,0,0,0q� �� CornerRadius$ 20���� $	��600��$��50�����#FC6586$��
150,15,0,0q�$ 20���� $	��700��$��50�����#335882$��
150,15,0,0q�$ 20���� .+��� $	��700��$��50�����#3DDDC2$��	150,0,0,0q�$ 20���� $	��900��$��50�����#FC6586$��
150,15,0,0q�$ 20���� $	��800��$��50�����#335882$��
150,15,0,0q�$ 20��