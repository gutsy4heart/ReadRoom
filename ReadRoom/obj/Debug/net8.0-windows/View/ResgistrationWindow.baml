   M S B A M L   `   `   `  ����C  ?ReadRoom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ReadRoom.ViewReadRoom.View  5   	   '    !ReadRoom.View.ResgistrationWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ReadRoom.View     5   	     � Title$  Registration��5   	   $	��370��6   $	��300��6+   
�����_� � Icon$ /View/registration.png��6N   .��5
      � $��0,0,0,-6q�6   ��5   
   �� 5       �� ImageSource$ /View/background.jpg��6    j� Stretch$
 Fill=�6<    j� TileMode$
 None=�6K    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��1��6"   $��Center=�62   $��Center=�6O   $
��10,0q�6j   .+�5      �� $��40(�6   $��ReadRoom��6"   $��Edwardian Script ITC)�65   5      �� 5   
   $��1��6   $��2��6"   $��Center=�66   $��Center=�6Q   .+�5      �� $��25(�6   $��Registration��6"   $��Edwardian Script ITC)�69   $	��108��6[   �� 5      $��
Логин��6   
���   �6$   �� 5       $��Stretch=�6   $��Center=�64   $	��200��6O   $	��0,5q�6[   ��6h   �� *
User.Login+ �� UpdateSourceTrigger$ PropertyChanged=��� 5"      $��Пароль��6   
���   �6%   G� 5#      -   5        $	��200��5#      $	��0,5q�6&   63   �� 5%      $��Email��6   
���   �6$   �� 5&      $��Stretch=�6   $��Center=�64   $	��200��6O   $	��0,5q�6[   ��6h   �� *
User.Email+$ PropertyChanged=��� 5(      $��Завершить��6   $	��200��6)   $��5q�65   ��6@   �� *RegistrationCommand+5)      5+      5,      