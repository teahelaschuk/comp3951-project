   M S B A M L   `   `   `  ���� E  ACSTQuizlet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:CSTQuizlet.ViewsCSTQuizlet.Views  !    CSTQuizlet.Views.CreateView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:CSTQuizlet.Views     $��512.643��$��644.049��.��� $��-10,0,0,-0.5q����	#FFC0D541$��Right=�$	��654��$	��513��$	��Top=�  � RowDefinitions  �� �� �� �� ��  � ColumnDefinitions �� �� �� �� �� .+��� -    ��Name$ label��$��Create a Question��$
��Left=�$��
107,27,0,0q�$	��Top=�
�������$
��Bold"�$��3��$	��178��$��20(�$��1���� -   $ label1��$��	Question:��$
��Left=�$��	9.5,0,0,0q�$��1��$	��Top=�$��16(�$��84��$
��Bold"�
�������$��1���� -    ��Name$ textBox��$��3��$��1��$
��Left=�$��23��$��106.5,8,0,0q�$��1�� �� TextWrapping$
 Wrap=�$��Write Question Here...��$	��Top=�$	��238���� -   $ label2��$��Answer Type:��$
��Left=�$��
112,47,0,0q�$��1��$	��Top=�$
��Bold"�
�������$��16(�$��2���� -    ��Name$ answerTypeComboBox�� �� Text$ Choose Answer Type��$��3��$��1��$
��Left=�$��106.5,57,0,0q�$��1��$	��Top=�$	��151������  �� EndPoint$	 0,1&� �� 
StartPoint$	 0,0&�.��� 	 � Color$	 	#FFF0F0F0��
 � Offset$
 0Q�� $	 White��$
 1Q�.@��� -   $��multipleChoiceItem��.��Multiple Choice�� -   $��shortAnswerItem��.��Short Answer�� -   $��trueFalseItem��.��True or False�� -	    ��Name$ submitQuestionButton��$��Submit��$��2��$
��Left=�$��	24,0,0,47q�$��4��$��78�� J� RenderTransformOrigin$ 0.849,0.452&�$��46��$��Bottom=��� -
   $��multiChoiceView��$��2��$
��Left=�$	��178��$��40,83.5,0,0q�$��1��$��3��$	��Top=�$	��221��$��1��$y�	Collapsed=�.+��� -   $ posAnswersLabel��$��Possible Answers:��$
��Left=�$	��Top=�$ 0.21,-0.319&�
�������$
��Bold"�$��16(� �� Left$ 16��
 �� Top$ 9���� -   $ textBox1��$
��Left=�$��22��$
 Wrap=�$��Answer��$	��Top=�$	��120��$ 85��$ 42���� -   $ textBox2��$
��Left=�$��24��$
 Wrap=�$��Answer��$	��Top=�$	��120��$ 85��$ 72���� -   $ textBox3��$
��Left=�$��22��$
 Wrap=�$��Answer��$	��Top=�$	��120��$ 85��$	 106���� -   $ textBox4��$
��Left=�$��22��$
 Wrap=�$��Answer��$��Bottom=�$	��120��$ 85��$	 141��	� -    	�Name$ answer1��$
��Left=�$	��Top=�$ 62��$ 48��	� -   $ answer2��$
��Left=�$	��Top=�$ 62��$ 78��	� -   $ answer3��$
��Left=�$	��Top=�$ 0.216,-0.086&�$ 62��$	 109��	� -   $ answer4��$
��Left=�$	��Top=�$ 62��$	 141���� -   $��shortAnswerView��$��1��$
��Left=�$��44��$��1��$	��Top=�$	��324��$��3��$��21,89.5,0,0q�$��2��$y�	Collapsed=�.+��� -   $ shortAnswerLabel��$��Answer:��$
��Left=�$	��Top=�
�������$
��Bold"�$��16(�$ 2���� -   $ shortAnswerTextBox��$
��Left=�$��23��$
 Wrap=�$��Answer��$	��Top=�$	��238��$ 86��$ 10���� -   $��trueFalseView��$��1��$
��Left=�$	��106��$��106,83.5,0,0q�$��1��$��2��$	��Top=�$	��121��$��2��$y�	Collapsed=�.+��� -   $ trueFalseLabel��$��Answer:��$
��Left=�$	��Top=�$
��Bold"�$��16(�
�������$ 6��$ 8��	� -   $ 
trueButton��$
��True��$
��Left=�$	��Top=�
�������$
��Bold"�$��16(�$ 26��$ 44��	� -   $ falseButton��$��False��$
��Left=�$	��Top=�$��16(�$
��Bold"�
�������$ 26��$ 70��