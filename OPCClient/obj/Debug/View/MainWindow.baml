   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	   5clr-namespace:Client.PropertiesClient.Properties  5   	       Client.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   &localclr-namespace:Client     5   	   /resclr-namespace:Client.Properties     5   	   !   Client.Properties.Resources    WINDOW_TITLE � Title# Z  5   	   $	��650��5   	   $
��1200��6   $	��750��6#   $	��500��62    � WindowStartupLocation$ CenterScreen=�5   	   $��0q�5	   	   
������� � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ ../StyleTemplate.xaml?�6%   %    5      .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5      � 	 � Color$	 	#FF0691E0��6   
 � Offset$
 1Q�61   � 5      $	 	#FFFAFAFA��6   � 5      $	 	#FF811048��6   5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��30 �6   .+�5    
   � $��0��6    � ColumnDefinitions 5!      �� 5"      $	��300 �6#   �� 5#      $��* �6#   .+�5%      �� $��0��6   $��
10,10,5,10q�6%    �� CornerRadius$ 8,8,8,8��69   ��5&      �� 5'      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5(      � $	 	#FFDDE8E4��6'   $
 1Q�69   � 5)      $	 	#FFA2D6C5��6'   5*      ._�5,      � $��0,0,0,0q�6   
������ .+�5-      ��    CONTROL_PANEL#��Z 6   $��Center=�6N   $��50��6r   $	��Top=�6~   $��5,5,5,0q�6�   
�������$��Center=�5.      $��22(�66   $
��Bold"�6D   �� 5/         SET_SIMPLING_PERIOD � ToolTip# Z 6   $��12(�50      $��Center=�6%   $��35��6G   $��
190,80,5,0q�6S   $	��Top=�6g   ��5/   V   �� *SamplingPeriodItem+ �� SelectedIndex 6�   �� *SamplingPeriodIndex+50   g   �� 51         SINUSOIDAL_SIGNAL#��Z 6      SIM_SIN# Z 6T      CheckBoxTemplate#��[  6�   $
��Left=�52      $��22��62   $	��Top=�6>   $	��200��6V   $��
20,180,0,0q�6b    d� 	IsChecked 51      �� *Signal_Y1_Input+ �� Mode$ TwoWay=�52   b   �� 53         SQUARE_SIGNAL#��Z 6      SIM_SQU# Z 6P   #��[  6�   $
��Left=�54      $��22��62   $	��Top=�6>   $	��200��6V   $��
20,205,0,0q�6b    53   {   �� *Signal_Y2_Input+$ TwoWay=�54   b   �� 55         TRIANGLE_SIGNAL#��Z 6      SIM_TRI# Z 6R   #��[  6�   $
��Left=�56      $��22��62   $	��Top=�6>   $	��200��6V   $��
20,230,0,0q�6b    55   }   �� *Signal_Y3_Input+$ TwoWay=�56   b   �� 57         START_STOP_TIP# Z 6   $��5,0,5,70q�6O   $��55��6a   $��Bottom=�6m     Start_StopButtonTemplate#��[ 58   ;   ��6   �� *Start_StopCommand+6;   �� 5:         SAVE_TIP# Z 6     SaveButtonTemplate#��[ 5;      $��5,0,5,5q�6D   $��55��6V   $��Bottom=�6b   {�5:   I   �� *SaveEnabled+��6k   �� *SaveCommand+5;   b   �� 5=         SIMPLING_PERIOD#��Z 6   $��35��6O   $��18(�6[   $��
10,80,77,0q�6i   $	��Top=�6}   $
��Bold"�6�   �� 5>         INPUT_SIGNALS#��Z 6   $
��Left=�6M   $��35��6h   $��
10,140,0,0q�6t   $	��Top=�6�   $	��200��6�   $��18(�6�   $
��Bold"�6�   5@      5A      �� 5B      $��1��6   $��
5,10,10,10q�6%   $ 8,8,8,8��69   ��5C      �� 5D      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5E      � $	 	#FFDDE8E4��6'   $
 1Q�69   � 5F      $	 	#FFA2D6C5��6'   5G      ._�5I      � $��0,0,0,0q�6   
������ .+�5J      �� 
������ $��0,0,0,0q�6:    5K       �� 
TargetType �� Property  �� Value��%V   '��       �� 5L      " ��6$   .�5M   "   ��  � ��$  Center=�6H   5N      5O      .@�5P      �� !   SIGNAL_CHANGE_CH#��Z! 6"   "   SHOW_SIGNAL_CHANGE# Z" 6U   $��50��6�   $
��Bold"�6�   $��18(�6�   ���	#FF9ACBBB.��5Q      � 
�������$��0,0,0,0q�6<   .+�5R   "      Client.ChartControl  $��0,0,0,0q�65   #   InputSimplingPoint# 6F   �� *SignalSimplingPoint+5S      5T      �� 5U      $   SIGNAL_VALUE_GA#��Z$ 6"   %   SHOW_SIGNAL_VALUE# Z% 6T   $��50��6�   $
��Bold"�6�   $��18(�6�   ���	#FF9ACBBB.��5V      � $��0,0,0,0q�6#   ��5W   "   �� 5X   &   $ 0.5,1&�6:   $ 0.5,0&�6K   .��5Y   *   � $	 	#FFDDE8E4��67   $
 1Q�6I   � 5Z   *   $	 	#FFA2D6C5��67   5[   '    5]   "   �� 5^   &   $��1* �67   �� 5_   &   $��1* �67   �� 5`   &   $��1* �67   .+�5b   "   � $��0��6'   $��0,0,5,0q�68   ��5c   &   �� 5d   *   $ 0.5,1&�6>   $ RelativeToBoundingBox=�6O   $ 0.5,0&�6s   .��5e   .   � $	 Black��6;   � 5f   .   $	 White��6;   $
 0.435Q�6I   5g   +    5i   &   �� 5j   *   $��* �68   �� 5k   *   $��30 �68   �� 5l   *   $��40 �68   .+�5n   &   �� #��Z 6,   $��2��6a   $��Center=�6o   $��Center=�6�   $��22(�6�   $
��Bold"�6�   �� 5o   &   -   5        & ��Name$& Label1��5o   ,   
���  ��$��1��6M   $��Center=�5p       $��Center=�6;   $��18(�6_   ��5o   Z   �� *SignalSimplingPoint.Signal_Y1+5p   _      Client.GaugeControl  5q   &   $��5,5,5,5q�69   $��0��6J   '   
InputValue' 6W   �� *SignalSimplingPoint.Signal_Y1+5r   #   � 5s   "   $��1��6'   $��5,0,5,0q�67   ��5t   &   �� 5u   *   $ 0.5,1&�6>   $ RelativeToBoundingBox=�6O   $ 0.5,0&�6s   .��5v   .   � $	 Black��6;   � 5w   .   $	 White��6;   $
 0.435Q�6I   5x   +    5z   &   �� 5{   *   $��* �68   �� 5|   *   $��30 �68   �� 5}   *   $��40 �68   .+�5   &   �� #��Z 6,   $��2��6]   $��Center=�6k   $��Center=�6�   $��22(�6�   $
��Bold"�6�   �� 5�   &   -   5        $& Label2��5�   ,   
���  ��$��1��6M   $��Center=�5�       $��Center=�6;   $��18(�6_   ��5�   Z   �� *SignalSimplingPoint.Signal_Y2+5�   _     5�   &   $��5,5,5,5q�69   $��0��6J   ' 6W   �� *SignalSimplingPoint.Signal_Y2+5�   #   � 5�   "   $��2��6'   $��5,0,0,0q�67   ��5�   &   �� 5�   *   $ 0.5,1&�6>   $ RelativeToBoundingBox=�6O   $ 0.5,0&�6s   .��5�   .   � $	 Black��6;   � 5�   .   $	 White��6;   $
 0.435Q�6I   5�   +    5�   &   �� 5�   *   $��* �68   �� 5�   *   $��30 �68   �� 5�   *   $��40 �68   .+�5�   &   �� #��Z 6,   $��2��6_   $��Center=�6m   $��Center=�6�   $��22(�6�   $
��Bold"�6�   �� 5�   &   -   5        $& Label3��5�   ,   
���  ��$��1��6M   $��Center=�5�       $��Center=�6;   $��18(�6_   ��5�   Z   �� *SignalSimplingPoint.Signal_Y3+5�   _     5�   &   $��5,5,5,5q�69   $��0��6J   ' 6W   �� *SignalSimplingPoint.Signal_Y3+5�   #   5�      5�      5�      5�      5�      5�      �� 5�   
   $��1��6.   $��0,0,0,0q�6;   $��14(�6L   $
��Bold"�6Z   $��Center=�6l   
����������	#FF28B9F0��6   �� *	StatusTip+6�   5�      5�      