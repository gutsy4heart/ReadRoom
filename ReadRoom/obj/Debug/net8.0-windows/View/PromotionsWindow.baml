   M S B A M L   `   `   `  ����C  ?ReadRoom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ReadRoom.ViewReadRoom.View  5   	   $    ReadRoom.View.PromotionsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ReadRoom.View     5   	     � Title$  PromotionsWindow��5   	   $	��650��6"   $	��500��6/   
���  �� � WindowStartupLocation$ CenterScreen=�6P   .��5	      � $��20q�6   .+�5
   
   �� $��Center=�6   $��Center=�60   .+�5      �� $"��Скидка для книг��6   $��18(�6.   $
��Bold"�6<   
�������$��Center=�6a   $��0,0,0,10q�6~   �� 5      $$��Название скидки:��6   
�������$��0,10,0,0q�6B   �� 5      $	��250��6k   $��0,0,0,10q�6w   ��6   �� * CurrentPromotion.PromotionName+ �� UpdateSourceTrigger$ PropertyChanged=�6w   �� 5      $6��0Выберите книгу для скидки:��6   
�������$��0,10,0,0q�6L   �� 5      $	��300��6[   $	��150��6g   $��0,0,0,10q�6t   ��6   �� *Books+ �� SelectedItem 65   �� *SelectedBook+��5      �� 5      .R�5      �� $
��Bold"�6;   
���   ���6$   �� *Title+6M   5      5      �� 5      $1��+Введите процент скидки:��6   
�������$��0,10,0,0q�6I   �� 5      $	��100��6~   $��0,0,0,10q�6�   ��6   �� *" CurrentPromotion.DiscountPercent+$ PropertyChanged=� �� StringFormat$ N2��6�   �� 5      $��Дата начала:��6   
�������$��0,10,0,0q�6>   (  "System.Windows.Controls.DatePicker  5      $	��200��6r   $��0,0,0,10q�6~      SelectedDate 6   �� *CurrentPromotion.StartDate+$ PropertyChanged=�6~   �� 5      $"��Дата окончания:��6   
�������$��0,10,0,0q�6A     5       $	��200��6p   $��0,0,0,10q�6|    6   �� *CurrentPromotion.EndDate+$ PropertyChanged=�6|   �� 5"      $	��150��6   $��25��6!   ���#2196F3
�������$��Применить��5#      ��6)   �� *ApplyDiscountCommand+�� 5$      $	��150��6   $��25��6!   
���  ��
�������$��
Назад��5%      $��Center=�6K   ��6%   �� *BackToShopCommand+6K   5&      5'      5(      