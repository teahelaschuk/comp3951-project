   M S B A M L   `   `   `  ����N  JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibraryCustomChromeLibrary  5   	   E ACSTQuizlet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CSTQuizlet
CSTQuizlet 5   	   =#clr-namespace:CSTQuizlet.ViewModelsCSTQuizlet.ViewModels 5	   	   3clr-namespace:CSTQuizlet.ViewsCSTQuizlet.Views 5
   	      CSTQuizlet.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   R NMicrosoft.Windows.Shell, Version=3.0.1.0, Culture=neutral, PublicKeyToken=nullKshell?http://schemas.microsoft.com/winfx/2006/xaml/presentation/shell  5   	   Hccl>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibrary  5   	   $localclr-namespace:CSTQuizlet  5   	   4
viewmodels#clr-namespace:CSTQuizlet.ViewModels  5	   	   *viewsclr-namespace:CSTQuizlet.Views  5
   	     � Title$  CST Quizlet��5   	    � WindowStartupLocation$ CenterScreen=�6   ���	#FF808B9B$	��600��6Y   $	��800��6i   *  $Microsoft.Windows.Shell.WindowChrome   WindowChrome 5        5   
      ResizeBorderThickness$ 6q�5         CaptionHeight$ 20Q�5         CornerRadius$ 0,0,0,0��5         GlassFrameThickness$ 0q�5      5       � 	Resources 5       �� DataType+ %CSTQuizlet.ViewModels.CreateViewModel!  CSTQuizlet.Views.CreateView	 � DataContext/ )CSTQuizlet.ViewModels.QuizSearchViewModel%  CSTQuizlet.Views.QuizSearchView) #CSTQuizlet.ViewModels.QuizViewModel  CSTQuizlet.Views.QuizView��%R  (��       5   3   *, +)(�� W     5   7   *, +)(�� �     5   1   *, +)�� 5   
   createViewTemplate��"  63   .R�5        	 6   �� 5      �� 5   
   quizSearchViewTemplate��"  67   .R�5        	 6#   �� 5      �� 5   
   quizViewTemplate��"  61   .R�5         	 6   �� 5!      5#      .��5%      � ��5&   
   �� 5'      
 �� 
StartPoint$
 0.5,0&�6"    �� EndPoint$ 0.5,1&�65   .��5(      �  � Color$ 	#FF2C3542��6    � Offset$ 0Q�61   � 5)      $ 	#FF3E496C��6   $ 1Q�61   5*       � ColumnDefinitions 5-   
   �� 5.      $��16 �6   �� 5/      $��* �6   �� 50      $��* �6   �� 51      $��* �6   �� 52      $��* �6   �� 53      $��* �6   �� 54      $��* �6    � RowDefinitions 56   
   �� 57      $��20 �6   �� 58      $	��100��6   �� 59      �� 5:      �� 5;      �� 5<      �� 5=      .+�5A   
   ��  �� CornerRadius$ 1,1,1,1��6   $��7��6(   $��7��69   $��1q�6M   ���	#FF808B9B5B      (   "CustomChromeLibrary.CaptionButtons  5E   
   $��6��6   �� 5G   
   $��1��6   ���	#FF808B9B$��0��6;   $��1��6K   $��7��6X   ��5H      �� 5I      $ 0.5,1&�6&   $
 0.5,0&�67   .��5J      � $ 	#FF878DB0��6#   $ 0Q�65   � 5K      $ 	#FF727789��6#   $ 0.961Q�65   � 5L      $ 	#FF5D627C��6#   $ 0.452Q�65   � 5M      $ 	#FF5C6078��6#   $ 0.375Q�65   � 5N      $ 	#FF63677A��6#   $ 0.876Q�65   5O      5Q      �� 5S   
   -   5         ��Name$ 
bcitButton��5S      $
��BCIT��6%   $��1��64   $��
10,5,10,10q�6A   $��2��6U   6i   �� 5U   
   -   5        $ createButton��5U      $��Create��6'   $��1��68   $��6��6E   $��
10,5,10,10q�6U    J� RenderTransformOrigin$ 0.663,0.487&�6i   6�   �� 5`   
   $��2��5a      $��0��5b      $��7��5c      $��5��5d      $��Stretch=�5e      $��Stretch=�5f      ���	#FF4B5953}�. ��5g      �� 5i      �� 5j   
   -   5        $ takeQuizButton��5j      $��	Take Quiz��6)   $��1��6=   $��5��6J   $��10.333,5,10.333,9.667q�6Z   $ 0.663,0.487&�6y   6�   5k      5l      