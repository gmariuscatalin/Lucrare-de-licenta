   M S B A M L   `   `   `  ����C  ?NewBank2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:NewBank2.ViewNewBank2.View  5      A%clr-namespace:NewBank2.CustomControlsNewBank2.CustomControls  5          NewBank2.View.ProfileView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:NewBank2.View     5      'vmusing:NewBank2.ViewModel     5      @customcontrols%clr-namespace:NewBank2.CustomControls     5      .��5      �� ��5   
   �� 5        �� 
StartPoint$	  1,1&�6"    �� EndPoint$	 0,0&�63   .��5      �    primaryBackColor1 � Color# [  6    � Offset$ 0Q�6J   � 5        primaryBackColor2# [ 6   $ 1Q�6J   5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��268 �6#   �� 5      $��* �6#   .+�5      �� $��0��6   ._�5      �� $��Center=�6   .+�5      �� $��CHANGE PASSWORD��6      titleColor3#��[ 5   (   $��16(�5   (   $��
Montserrat)�5   (   $��Medium"�5   (   $��
55,30,55,0q�5    (   �� 5!      $��New password��6    
�������$��14(�5#       $��Medium"�5$       $��
Montserrat)�5%       $��	10,30,0,0q�5&       1   +NewBank2.CustomControls.BindablePasswordBox  5'      -   5          Name$ NewPasswordBox��5)   9   $��28��5(   9   $��	10,5,10,0q�5*   9      Password 5'   9   �� *NewPassword+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5+      �� 5,      $��Confirm new password��6    
�������$��14(�5.       $��Medium"�5/       $��
Montserrat)�50       $��	10,30,0,0q�51         52      -   5        $ ConfirmNewPasswordBox��54   9   $��28��53   9   $��	10,5,10,0q�55   9    52   9   �� *ConfirmNewPassword+$ TwoWay=�$ PropertyChanged=�56      �� 57       
 color4#���  6>   $��0,32,0,0q�6d   $��Center=�6v   $
��Wrap=�6�   $
��Auto��6�   	 �� TextAlignment$	 Center=�6�   ��6    �� *ErrorMessage+6�   �� 58      -   5        
 ��Name$
 btnChangePassword��58      $��0q�59   !   $��CHANGE PASSWORD��5:   !     panelActiveColor#��[ 5;   !   $��12(�5<   !   $��
Montserrat)�5=   !    � Cursor$
 Hand��5>   !   $��0,10,0,0q�5?   !   ��5A      �� *ChangePasswordCommand+��5B      �� 5C       �� 
TargetType" ��6$   .�5D   "   ��  �� Property � �� 
 color2 �� Value# [ 6?    �� Triggers 5E   "   P� 5F   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5G   *   ��  � �� �#28AEED6G   5H   '   5J      ��5L      �� 5M       �� 
TargetType" ��6.   .R�5N   "   �� $	��130��6)   $��30��65    �� CornerRadius$ 15��5O   -   #��z��5P   -   ._�5Q   &   �� $��Center=�67   $��Center=�5R   ;   5T   #   5U      5X      5Y      5Z      �� 5[      $��1��6   ._�5]      �� $��Center=�6   .+�5^      �� $��CHANGE PROFILE PICTURE��6    #��[ 5_   (   $��16(�5`   (   $��
Montserrat)�5a   (   $��Medium"�5b   (   $��
35,30,35,0q�5c   (   A� 5d      $	��130��6   $	��130��6+   #��[ 5e   &   $��2��5f   &   $��
10,15,10,0q�5g   &   ��5h      �� 5i       �� ImageSource 6)   �� *ProfilePictureBitmap+5l      �� 5m      $;��5It may take some time for the changes to take effect.��6    
�������$��10(�5o       $��Medium"�5p       $��
Montserrat)�5q       $��	10,15,0,0q�5r       �� 5s      -   5        $
 btnChangeProfilePicture��5s      $��0q�5t   !   $��CHANGE PICTURE��5u   !   #��[ 5v   !   $��12(�5w   !   $��
Montserrat)�5x   !   $
 Hand��5y   !   $��0,30,0,0q�5z   !   ��5|      �� *ProfilePictureCommand+��5}      �� 5~      " ��6$   .�5   "   ��  � ��# [ 6?    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   ��  � �� �#28AEED6G   5�   '   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $	��120��6)   $��30��65   $ 15��5�   -   #��z��5�   -   ._�5�   &   �� $��Center=�67   $��Center=�5�   ;   5�   #   5�      5�      5�      5�      5�      5�      5�      