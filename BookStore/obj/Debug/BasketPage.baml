   M S B A M L   `   `   `  ����D  @BookStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:BookStore	BookStore  5          BookStore.BasterPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:BookStore     5        N� Title$  
BasterPage��5	      .��5      � ��5   
   �� 5       �� ImageSource$ Resources/Background.jpg��6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��DataTableBasket��5      $y�Hidden=�6*   $
��Left=�6?   $	��319��6Z   $��
153,76,0,0q�6g      
IsReadOnly . $	��Top=�6�   $	��613��6�   ��6�   Z� ��5      �� 5         Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Книга��6)      MaxWidth$ 53Q�68      CellTemplate 5      �� 5      .R�5      �� $	��100��6>   $��50��6J   ��6$   �� *Photos+6J   5      5        5         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6)   $! Названия книги��63    5      �� 5      .R�5      �� $��15(�6(   $��Center=�6Q   $��Center=�6n   ��66   �� *	Name_book+6n   5      5!        5"      $ Цена��6)    5#      �� 5$      .R�5%      �� $��15(�6(   $��Center=�6M   ��66   �� *Price+6M   5&      5(        5)      $ Количество��6)    5*      �� 5+      DataTemplate��.R�5,      ]� .+�5-   "   �� -   5        $��30��5-   )   $��30��65   $��+��6@   6L   �� 5.   "    ��Name$ CountProductLabel��6�   $��40��6(   $��40��64   $��14(�6?   $��Center=�6M   $��Center=�6o   $��Center=�6�   ��6�   �� *Count+6�   �� 5/   "   -   5        $��30��5/   )   $��30��65   $��-��6@   6L   50      51      53        55      $  Сумма к оплате��6)    56      �� 57      .R�58      �� $��15(�6(   $��Center=�66   ��6Q   �� *Total_price+59      5;        5<      $ 50 6)    5=      �� 5>      .R�5?      �� -   5        $��40��5?   @   	 J� OpacityMask	 5@   "   �� 5A   &   $I CC:\Users\иван\Desktop\курсовая\BookStore\img\Delete.png��61   
 j� Stretch$
 UniformToFill=�6w   ��5C   "   �� 5D   &   $I CC:\Users\иван\Desktop\курсовая\BookStore\img\Delete.png��61   $
 UniformToFill=�6w   5F      5G      5I      5K      �� 5L   
   -   5        $��ButtonUpdate��5L      $��
Товар��6%   $
��Left=�65   $��	0,101,0,0q�6P   $	��Top=�6c   $	��100��6{   	 5M      �� 5N      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $
 UniformToFill=�6i   ��5P      �� 5Q      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $
 UniformToFill=�6i   5S      �� 5T   
   -   5        $��
Назад��5T      $
��Left=�6!   $��	0,330,0,0q�6<   $	��Top=�6O   $	��100��6g   	 5U      �� 5V      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $
 UniformToFill=�6i   ��5X      �� 5Y      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $
 UniformToFill=�6i   5[      �� 5\   
   -   5        $��	BuyButton��5\      $��Купить��6"   $
��Left=�63   $��	0,126,0,0q�6N   $	��Top=�6a   $	��100��6y   	 5]      �� 5^      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $
 UniformToFill=�6i   ��5`      �� 5a      $O IC:\Users\иван\Desktop\курсовая\BookStore\img\LeftBookmark.png��6   $
 UniformToFill=�6i   5c      5e      5f      