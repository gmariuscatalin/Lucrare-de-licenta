   M S B A M L   `   `   `  ����C  ?NewBank2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:NewBank2.ViewNewBank2.View  5      7 clr-namespace:NewBank2.ViewModelNewBank2.ViewModel  5      %    NewBank2.View.DeleteAccountView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:NewBank2.View     5      6	viewmodel clr-namespace:NewBank2.ViewModel     5      X TFontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5        � 	Resources  5          BooleanToVisibilityConverter  LargeRadioButton �� 
TargetType �� Property �� Value �� 
TargetType � ColumnDefinitions � RowDefinitions A�Name �� Triggers	 P� Property
 P� Value �� 
TargetName��%�  &	        &	      �� 5   
   6'   �� 5   
   " 	�6)   .�5      ��  � �� 5      �� 5      " 	�6&   .R�5      � 
������  5      �� 5   "   $
��Auto �63   �� 5   "   $��* �63    5      �� 5   "   $
��Auto �60   .+�5      A� $ outerEllipse��6&   $��20��6<   $��20��6G   
������ #��z��6f   $��2��6�   A� 5      $ innerEllipse��6&   $��10��6<   $��10��6G   #��z��6S   $y�	Collapsed=�6w   �� 5      $��1��6/   $��4,0,0,0q�6?   $��Center=�6P   $
��Left=�6k   #��z��6�   5       5      P� 5      	 �@d�	IsChecked$

 TrueY�6;   . �5   "   �� $ innerEllipse��6)    � y�$ Visible=�6Y   5       P� 5!      	 �@J�IsMouseOver$

 True��6=   . �5"   "   �� $ outerEllipse��6)    � �� �#28AEED6U   �� 5#   "   $ innerEllipse��6)    � �� �#28AEED6S   5$      5&      5(      5)      5*      .��5+      � .+�5,   
   �� ��5-      �� 5.       �� 
StartPoint$	 1,1&�6&    �� EndPoint$	 0,0&�67   .��5/      �   primaryBackColor1 � Color# [ 6#    � Offset$ 0Q�6N   � 50        primaryBackColor2# [ 6#   $ 1Q�6N   51      ._�53      � .+�54      �� -   5         ��Name$ btnBack��54      $��0q�55       � Cursor$
 Hand��56      $��0,0,0,0q�57      $
��Left=�58      $	��Top=�59      $��0q�5;      
������ ��5=      �� 5>      " ��6*   .R�5?      �� #��z��6/   #��z��5@   #   #��z��5A   #   #��z��5B   #   5C      .��5E      �� ��5F      !  FontAwesome.Sharp.IconImage  5G      �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ ArrowLeftLong=�6+     plainTextColor1   
Foreground# [ 5H   3   $��40��5I   3   $��40��5J   3   ��5K   "   �� 5L   &   "  6,   .�5M   *   ��  �@J�Opacity$ 1Q�6D    �� Triggers 5N   *   P� 5O   .   	 �@J�IsMouseOver$

 True��6M   . �5P   2   ��  �@J�Opacity$	 0.8Q�6L   5Q   /   P� 5R   .   	 � {�$
 False��6K   . �5S   2   ��  �@J�Opacity$	 0.5Q�6L   5T   /   5V   '   5X      5Z      5[      � 5]       5^      �� 5_      $��* �6+   �� 5`      $��* �6+   �� 5a      $��* �6+   .+�5c      �� $��1��6!   $��Center=�61   .+�5d      �� $��CHOOSE THE CURRENCY��6$     titleColor3#��[ 5e   (   $��16(�5f   (   $��
Montserrat)�5g   (   $��Medium"�5h   (   $��0,30,0,0q�5i   (   $��Center=�5j   $   �� 5k      $��0,40,0,0q�6%   .+�5l      	� -   5         	�Name$ ronRadioButton��5l   *   $	��RON��6B   #��[ 6�   #��[ 6�   $��
-21,10,0,0q�6�   $��Center=�6�    d� 	IsChecked 6P   �� *IsRonSelected+y�6  �� *IsRonAvailable+ �� 	Converter# [  	� 5m      -   5        $ euroRadioButton��5m   *   $	��EUR��6C   #��[ 6�   #��[ 6�   $��
-25,10,0,0q�6  $��Center=�6    6Q   �� *IsEuroSelected+{�6v   �� *IsEuroAvailable+y�6=  �� *IsEuroAvailable+# [  	� 5n      -   5        $ dollarRadioButton��5n   *   $	��USD��6E   #��[ 6�   #��[ 6�   $��
-25,10,0,0q�6  $��Center=�6(   6S   �� *IsDollarSelected+{�6z   �� *IsDollarAvailable+y�6E  �� *IsDollarAvailable+# [  5p      �� 5q       
 color4#���  6B   $��0,60,0,0q�6h   $��Center=�6z   $
��Wrap=�6�   $
��Auto��6�    �� TextAlignment$ Center=�6�   ��6$   �� *ErrorMessage+6�   �� 5r      -   5        $ btnDeleteAccount��5r   !   $��0q�5s   !   $��Delete Account��5t   !     panelActiveColor#��[ 5u   !   $��12(�5v   !   $��
Montserrat)�5w   !   $
 Hand��5x   !   $��0,10,0,0q�5y   !   $��Center=�5{   !   ��5z   !   �� *DeleteAccountCommand+��5}      �� 5~   "   " ��6(   .�5   &   ��  � �� 
 color2# [ 6C    5�   &   P� 5�   *   	 �@J�IsMouseOver$

 True��6I   . �5�   .   ��  � �� �#28AEED6K   5�   +   5�   #   ��5�      �� 5�   "   " ��62   .R�5�   &   �� $	��100��6-   $��30��69    �� CornerRadius$ 15��5�   -   #��z��5�   -   ._�5�   *   �� $��Center=�6;   $��Center=�5�   ;   5�   '   5�   #   5�      5�      5�      � 5�       5�      �� 5�      $��* �6+   �� 5�      $��* �6+   �� 5�      $��* �6+   5�      5�      5�      5�      5�      