   M S B A M L   `   `   `  ����C  ?NewBank2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:NewBank2.ViewNewBank2.View  5      1clr-namespace:NewBank2.ModelsNewBank2.Models  5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
      &     NewBank2.View.BankAccountControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:NewBank2.View     5      0modelsclr-namespace:NewBank2.Models     5	      6sys&clr-namespace:System;assembly=mscorlib     5
        � 	Resources  5      5   /NewBank2.Models.NoDecimalIfWholeNumberConverter #  NoDecimalIfWholeNumberConverter/   )NewBank2.Models.CurrencyToSymbolConverter  CurrencyToSymbolConverter3   -NewBank2.Models.TextLengthToFontSizeConverter ! TextLengthToFontSizeConverter.   (NewBank2.Models.GreaterThanZeroConverter  GreaterThanZeroConverter��%t   &	        &	      &	 &     &	 9       5   
   61     5   
   6+     5   
   6/     5   
   6*   5      .��5      ��  �� CornerRadius$ 5��6   
������  �� Padding$ 10q�67   ._�5   
   � ��6   �� *Index+ � RowDefinitions 5      �� 5      $	��150 �6    �� 5      $��* �6    .+�5      �� $ 5��6    
 color5#��[ 6&   $ 10q�6K   $��0,0,0,5q�6X   ._�5      � ��6   �� *Index+ 5      �� 5      $
��Auto �6(   �� 5      $��* �6(   .+�5      �� $
��Bold"�6B   $��20(�6T   ��6    �� *Account.Currency+6T   �� 5      $��1��6!   .+�5      �� $��	Balance: ��6$   $
��Bold"�65   $��16(�6G   �� 5        �� Orientation$ 
Horizontal=�6%   $��Center=�6>   $��Center=�6[   .+�5!      �� $
��Bold"�6�   ��6(   �� *Account.Balance+ �� 	Converter# [  ��6�   �� *Account.Balance+# [ �� 5"      $
��Bold"�6�   $��5,0,0,0q�6�   ��6(   �� *Account.Currency+# [ ��6�   �� *Account.Balance+# [ 6�   5#      5$      5%      5&      �� 5'      $��1��6   $ 5��6"   #��[ 63   $ 10q�6X   $��0,5,0,0q�6e   ._�5(      �  5)      �� 5*      $
��Auto �6(   �� 5+      $��* �6(   .+�5-      � $��0��6    � ColumnDefinitions 5.      �� 5/      $
��Auto �6/   �� 50      $��* �6/   .+�53      �� $��0��6$   $��Transactions:��64   $
��Bold"�6I   $��16(�6[   �� 54      $��1��6!   $��5,0,0,0q�55   !   $ 15��56   !   $��1q�57   !   
������ ���#B4B7FF._�5:      �� -   5         ��Name$ SearchTextBox��5:   &   $��13(�5=   &   $��25��5>   &   $��Center=�5?   &    �� TextAlignment$
 Left=�5@   &   $��Center=�5A   &   $��0q�5B   &   
������ $��13,0,0,0q�5C   ?   ��5<   &   �� *
SearchText+	 �� UpdateSourceTrigger$	 PropertyChanged=�5C   ?   5D      �� 5E      -   5        
 ��Name$
 SearchLabel��5E   0   $��1��6    $��   Search...��6E    J� IsHitTestVisible$ False��6\   $��5,0,0,0q�6u   
������ $��23��6�   $��12(�6�   $	��Top=�6�   5F      �� 5G      $��1��6   
������ $��0q�6r   $��Stretch=�6�   $
��Auto=�6�   $��Disabled=�6�   ��6,   �� *FilteredTransactions+��5H      �� 5I       �� 
TargetType" ��6$   .�5J   "   ��  �� Property �@J�IsHitTestVisible �� Value$ False��6E   �� 5K   "    � |�$ False��6>   5L        5N        CustomScrollBarStyle �� 
TargetType ]�Name ]� Thumb  CustomScrollThumbStyle��%�  &	       &	 �    �� 5O      " ��6$   .�5P   "   ��  � ��$ 10��6:   �� 5Q   "    � ��
 ���� 6?   �� 5R   "    � �� 5S   &   �� 5T   *   " ��6:   .R�5U   .   �  5V   2   �� 5W   6   $��* �6D   .+�5Y   2   �� $��0��69   #��z��6F   $ 5��6p   ]� 5Z   2   $ 
PART_Track��6E   $��0��68   $
��True��6Y    5[   6   n� 5\   :   #���  6@   5^   3   5_   /   5`   +   5b   #   5c      �� 5d      " n�6C   .�5e   "   ��  � �� �#B4B7FF6?   �� 5f   "    � �� 5g   &   �� 5h   *   " n�6:   .R�5i   .   �� $��10��65   #��z��6@   $ 5��6j   5j   +   5l   #   5m      5n      ��5o      �� 5p      " ��6$   .�5q   "   ��  �@�OverridesDefaultStyle$
 True��6J   �� 5r   "    � �� 5s   &   �� 5t   *   " ��6:   .R�5u   .   �� #��[B��6;   #��z��6j   #��z��6�   .��5v   2   ��   5w   2    �� BasedOn��%0   '��       7  �� 5x   6   " ��6<   8   6S   5y   3   5z   /   5{   +   5}   #   5~      ��5�      �� 5�      .R�5�   "   �� 
������ ���#B4B7FF$��1q�6X   $ 5��6l   $��0,0.5q�6}   ._�5�   &   �  5�   *   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<    5�   *   �� 5�   .   $
��Auto �6?   �� 5�   .   $��* �6?   .+�5�   *   �� $��0��64   $��0��6A   
���   �$��
Montserrat)�6�   $��Medium"�6�   $
��Left=�6�   $��3,3,0,0q�6�   ��6Q   �� *TransactionDate+ �� StringFormat$ {0:MM/dd/yyyy}��6�   �� 5�   *   $��0��64   $��1��6A   
���   �$��
Montserrat)�6   $��Medium"�6�   $��Right=�6�   $��0,3,3,0q�6�   ��6Q   �� *
Category+6�   �� 5�   *   $��1��64   $��1��6A   
���   �$��
Montserrat)�6�   $��Medium"�6�   $��Right=�6�   $��0,0,3,3q�6�   ��6Q   �� *
UsernameTr+6�   �� 5�   *   $��1��64   $��0��6A   $��
Montserrat)�6�   $
��Bold"�6�   $
��Left=�6�   $��3,0,0,3q�6  ��6Q   �� *
AmountTr+$ {0:+0.00;-0.00;0}��# [  ��5�   .   �� 5�   2   " ��68   .�5�   6   ��  � ��
 �   �6S    �� Triggers 5�   6   �� 5�   :    �� Binding 6F   �� *
AmountTr+# [  �� Value	 False6�   .a�5�   >   ��  � ��
 �  ��6[   5�   ;   �� 5�   :    6F   �� *
AmountTr+# [  True6�   .a�5�   >   ��  � ��
 � d �6[   5�   ;   5�   3   5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      