   M S B A M L   `   `   `  ����C  ?ReadRoom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ReadRoom.ViewReadRoom.View  5   	   $    ReadRoom.View.SearchBookWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ReadRoom.View     5   	     � Title$  SearchBookWindow��5   	   $	��320��6"   $	��600��6/   
���  �� � Icon$ /View/search.png��6P   .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��2.5* �6   �� 5      $
��2.5* �6   .+�5   
   �� $��0��6   $��0,0,0,0q�6"   .+�5      �� $��Поиск книг��6   $��18(�6*   $
��Bold"�68   
�������$��0,0,0,10q�6]   �� 5      $"��Название книги:��6   
�������6/   �� 5      -   5         ��Name$ SearchTitleTextBox��5      $��2q�6t   ��62   �� *SearchTitle+ �� UpdateSourceTrigger$ PropertyChanged=�6t   �� 5      $��Имя автора:��6   
�������6+   �� 5      -   5        $ SearchAuthorNameTextBox��5      $��2q�6~   ��67   �� *SearchAuthorName+$ PropertyChanged=�6~   �� 5      $"��Фамилия автора:��6   
�������6/   �� 5      -   5        $  SearchAuthorSurnameTextBox��5      $��2q�6�   ��6:   �� *SearchAuthorSurname+$ PropertyChanged=�6�   �� 5      $��	Жанр:��6   
�������6%   �� 5      -   5        $ SearchGenreTextBox��5      $��2q�6t   ��62   �� *SearchGenre+$ PropertyChanged=�6t   �� 5      $��
Найти��6   $��	0,10,0,10q�6G   $	��150��6Z   $��25��6f   ���#2196F3
���������6%   �� *SearchCommand+6�   �� 5      $��
Выйти��6   $	��150��6%   $��25��61   $��0,0,0,10q�6=   ���#F44336
���������6w   �� *ChangeWindowCommand+5      �� 5   
   $��1��6   $��10q�6%   .+�5      �� $	��250��6   $��0,0,0,0q�6$   ��65   �� *Books+��5      �� 5       .R�5!      �� ���#DDDDDD$��1q�67    �� CornerRadius$ 5��6K   $��5q�6\    �� Padding$ 10q�6g   ._�5"      ��  �� Orientation$ Vertical=�6)   .+�5#   "   �� $
��Bold"�6C   $��16(�6U   $��0,0,0,5q�6c   ��6,   �� *Title+6c   �� 5$   "   $��Italic$�6H   $��14(�6[   $��0,0,0,3q�6i   ��6,   �� *
AuthorName+6i   �� 5%   "   $��Italic$�6K   $��14(�6^   $��0,0,0,3q�6l   ��6,   �� *AuthorSurname+6l   �� 5&   "   $��14(�6G   $��0,0,0,3q�6U   ��6,   �� *	Publisher+6U   �� 5'   "   $��14(�6C   $��0,0,0,3q�6Q   ��6,   �� *Pages+6Q   �� 5(   "   $��14(�6C   $��0,0,0,3q�6Q   ��6,   �� *Genre+6Q   �� 5)   "   $��14(�6M   $��0,0,0,3q�6[   ��6,   �� *PublicationYear+6[   �� 5*   "   $��14(�6X   $��0,0,0,3q�6f   ��6,   �� *	CostPrice+ �� StringFormat$ C0��6f   �� 5+   "   $
��Bold"�6X   $��14(�6j   $��0,0,0,3q�6x   ��6,   �� *	SalePrice+$ C0��6x   �� 5,   "   $��14(�6L   $��0,0,0,3q�6Z   ��6,   �� *IsContinuation+6Z   �� 5-   "   ���#757575$��14(�6c   $��0,0,0,3q�6q   ��6,   �� *PrequelBookTitle+6q   5.      5/      50      52      53      54      55      