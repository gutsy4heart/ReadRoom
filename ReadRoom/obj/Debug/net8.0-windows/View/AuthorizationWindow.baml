   M S B A M L   `   `   `  ����C  ?ReadRoom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ReadRoom.ViewModelReadRoom.ViewModel  5   	   "    ReadRoom.AuthorizationWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ReadRoom.ViewModel     5   	     � Title$  Authorization��5   	   $	��380��6   $	��400��6,    � Icon$ /View/authorization.png��68   ��5	      �� 5
   
    �� ImageSource$" /View/backgroundauthoriz.jpg��6   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��1��6"   $��Center=�62   $��Center=�6O   $
��10,0q�6j   .+�5      �� $��34(�6   $��ReadRoom��6"   $��Edwardian Script ITC)�65   
���������6j   Z� 5      �� 5   
   $��1��6   $��2��6"   $��Center=�66   $��Center=�6Q   .+�5      �� $��30(�6   $��Authorization��6"   $��Edwardian Script ITC)�6:   
�������$	��137��6o   ���	#FF2B93966{   �� 5       $��
Логин��6   
�������6$   �� 5!      $	��200��6W   $	��0,5q�6c   ��6   �� *
User.Login+ �� UpdateSourceTrigger$ PropertyChanged=�6c   �� 5"      $��Пароль��6   
�������6%   G� 5#      -   5        $	��200��5#      $	��0,5q�6&   63   �� 5$      $��
Войти��6   $	��200��6%   $��5q�61   ��6<   �� *LoginCommand+�� 5%      $?��9Нету аккаунта? Регистрируйтесь��6   $��Center=�6=   
�������6Z   �� 5&      $��Регистрация��6   $	��200��6+   $��5q�67   ��6B   �� *RegistrationOpenCommand+5'      5)      5*      