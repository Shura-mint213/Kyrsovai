   M S B A M L   `   `   `  ����D  @BookStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:BookStore	BookStore  5          BookStore.ProductPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:BookStore     5        N� Title$  ProductPage��5	      .��5
      � ��5   
   �� 5       �� ImageSource$ Resources/Background.jpg��6    � RowDefinitions 5   
   �� 5       � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5        $��0q�5      $6��0Редактировать
свой пофиль��6&   $
��Left=�6N   $	��Top=�6i   $	��115��6�   $��37��6�    � Cursor$  ��6�    J� RenderTransformOrigin$	 0,0&�6�   $��0,76,0,0q�6�    J� OpacityMask 5      �� 5      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6    j� Stretch$ UniformToFill=�6i   ��5      �� 5      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $ UniformToFill=�6i   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DataTableProduct��5   -   	   HeadersVisibility$
	 None=�6   $y�Hidden=�6G   $
��Left=�6[   $	��342��6v   $	��Top=�6�   $	��630��6�   
   
IsReadOnly
 .    AutoGenerateColumns . $��
115,50,0,0q�6�   ��5      �� 5      ��5       �� 5!         Columns 5#      4  .System.Windows.Controls.DataGridTemplateColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Книга��6*      MaxWidth$ 53Q�69      CellTemplate 5%      �� 5&      .R�5'      �� $	��100��6>   $��50��6J   ��6$   �� *Photos+6J   5(      5*        5+         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6)   $! Названия книги��63    5,      �� 5-      .R�5.      �� $��15(�6)   $��Center=�6R   ��67   �� *	Name_book+6R   5/      51        52      $ Цена��6)    53      �� 54      .R�55      �� $��15(�6(   $��Center=�6M   ��66   �� *Price+6M   56      58        59      $ 54 6)    5:      �� 5;      .R�5<      �� -   5        $��50��5<   =    5=   "   �� 5>   &   $N HC:\Users\иван\Desktop\курсовая\BookStore\img\Shopin_cart.png��61   $ UniformToFill=�6|   ��5@   "   �� 5A   &   $N HC:\Users\иван\Desktop\курсовая\BookStore\img\Shopin_cart.png��61   $ UniformToFill=�6|   5C      5D      5F      5H      �� 5J   
   -   5        $��0q�5J      $��
Товар��6%   $
��Left=�65   $��0,50,0,0q�6P   $	��Top=�6b   $	��115��6z   $��21��6�   $  ��6�   $	 0,0&�6�   ��6�   Z�  5K      �� 5L      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $ UniformToFill=�6i   ��5N      �� 5O      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $ UniformToFill=�6i   5Q      �� 5R   
   -   5        $��
Выход��5R      $
��Left=�6!   $��	0,371,0,0q�6<   $	��Top=�6O   $	��115��6g   $��21��6�    5S      �� 5T      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $ UniformToFill=�6i   ��5V      �� 5W      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $ UniformToFill=�6i   5Y      �� 5Z   
   -   5        $��ListB��5Z      $
��Left=�6   $	��100��6:   $��
10,124,0,0q�6G   $y�Hidden=�6[   $	��Top=�6o   $	��100��6�   �� 5[   
   -   5        $��ButtonBaster��5[      $
��Left=�6%   $��
700,10,0,0q�6@   $	��Top=�6T   $��45��6l   $��41��6�   $ -1.209,0.488&�6�    5\      �� 5]      $N HC:\Users\иван\Desktop\курсовая\BookStore\img\Shopin_cart.png��6   $ UniformToFill=�6h   ��5_      �� 5`      $N HC:\Users\иван\Desktop\курсовая\BookStore\img\Shopin_cart.png��6   $ UniformToFill=�6h   5b      �� 5c   
   -   5        $��SearchTextBox��5c      $
��Left=�6'   $��26��6B   $��0,17,0,0q�6N    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��178��6�   �� 5d   
   -	   5        $
��🔎��5d      $
��Left=�6   $��
178,17,0,0q�69   $	��Top=�6M   $��30��6e   $��26��6p   $ 
1.96,0.435&�6�   
����������	#FFCFCFCF6�   �� 5e   
   -
   5        $��OrdersButton��5e   %   $��0q�6   $��Заказы��69   $
��Left=�6J   $��	0,118,0,0q�6e   $	��Top=�6x   $	��115��6�   $��20��6�    5f      �� 5g      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $ UniformToFill=�6i   ��5i      �� 5j      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $ UniformToFill=�6i   5l      5m      5n      