   M S B A M L   `   `   `  ����E  ACSTQuizlet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CSTQuizlet
CSTQuizlet  5   	   =#clr-namespace:CSTQuizlet.ViewModelsCSTQuizlet.ViewModels  5   	   3clr-namespace:CSTQuizlet.ViewsCSTQuizlet.Views  5   	       CSTQuizlet.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:CSTQuizlet     5   	   :
viewmodels#clr-namespace:CSTQuizlet.ViewModels     5   	   0viewsclr-namespace:CSTQuizlet.Views     5   	     � Title$  CST QUizlet��5
   	   $��614.035��6   $��780.439��6.    � WindowStartupLocation$ CenterScreen=�6>    � 	Resources 5       �� DataType+  %CSTQuizlet.ViewModels.CreateViewModel��%M   (��       5   3   *, +)�� 6
   createViewTemplate��"  63   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5    
   ]� 
�������$��1��6*   $��0��67   $��5��6G   .+�5!      R� ���	#FFC0D541.@�5"      Q� $��Level 1��6   
�������.@�5#      �� $��COMP1100��6   �� 5$      $��COMP1111��6   �� 5%      $��COMP1113��6   �� 5&      $��COMM1116��6   �� 5'      $��COMP1536��6   �� 5(      $��COMP1510��6   �� 5)      $��BUSA2526��6   5*      Q� 5+      $��Level 2��6   
�������.@�5,      �� $��COMP2216��6   �� 5-      $��COMP2121��6   �� 5.      $��COMP2510��6   �� 5/      $��COMP2526��6   �� 50      $��COMP2721��6   �� 51      $��COMP2910��6   52      Q� 53      $��Level 3��6   
�������.@�54      �� $��COMP3512��6   �� 55      $��COMP3711��6   �� 56      $��COMP3721��6   �� 57      $��COMP3760��6   �� 58      $��COMP3900��6   59      Q� 5:      $��Level 4��6   
�������.@�5;      �� $��COMP2510��6   �� 5<      $��COMP2526��6   �� 5=      $��COMP2721��6   5>      5?      5@      �� 5A   
   $��0��6   $��0��6$   $��6��61   ���#05406A$��
0,0,-0.5,0q�6T   �� 5B   
   -   5         ��Name$ 
bcitButton��5B      $
��BCIT��6%   $��0��64   $��	10,6,10,9q�6A   �� 5C   
   -   5        $ createButton��5C      $��Create��6'   $��2��68   $��9.5,5,9.5,10q�6H    J� RenderTransformOrigin$ 0.663,0.487&�6^   �� 5D   
   -   5        $ forumButton��5D      $��Forum��6&   $��3��66   $��10.5,5,10,10q�6F   $ 0.663,0.487&�6\   �� 5E   
   -   5        $ upcomingButton��5E      $��Upcoming��6)   $��4��6<   $��	9,5,10,10q�6L   $ 0.663,0.487&�6_   �� 5F   
   -   5        $ 
userButton��5F      $
��User��6%   $��5��64   $��10,5,9.5,10q�6D   $ 0.663,0.487&�6Y   �� 5G   
   $��1��6   $��1��6&   $��5��66   $��5��6J   ��6[   �� 5H      5I      