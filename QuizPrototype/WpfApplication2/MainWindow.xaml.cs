using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Question> questions = new List<Question>();
        private List<string> answers = new List<string>();
        private List<RadioButton> possibleAnswers = new List<RadioButton>();
        private int questNum;
        private static int score;

        public MainWindow()
        {
            InitializeComponent();
            initQuestions();
            start();
        }

        private void start()
        {
            radioButton1.Content = answers[0];
            radioButton2.Content = answers[1];
            radioButton3.Content = answers[2];
            radioButton4.Content = answers[3];
            questionLabel.Content = questions[questNum].Quest;
            answerLabel.Content = questions[questNum].Answer;
            answerButton.IsEnabled = true;
            saveButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            

            
        }

        private void initQuestions()
        {
            possibleAnswers.Clear();
            possibleAnswers.Add(radioButton1);
            possibleAnswers.Add(radioButton2);
            possibleAnswers.Add(radioButton3);
            possibleAnswers.Add(radioButton4);
            

            answers.Add("Encapsulation");
            answers.Add("Properties");
            answers.Add("CLR");
            answers.Add("C#");

            Random rand = new Random();
            questions.Add(new Question("What is another term for data hiding?", answers[0]));
            questions.Add(new Question("The data stored in an object are commonly called fields or:)", answers[1]));
            questions.Add(new Question("What compiles ILR code into machine language?", answers[2]));



        }

        private void answerButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var radioBtn in possibleAnswers)
            {
                if(radioBtn.IsChecked == true)
                {
                    if(radioBtn.Content.ToString() == questions[questNum].Answer)
                    {
                        radioBtn.Background = Brushes.Green;
                        score++;
                    } else
                    {
                        radioBtn.Background = Brushes.Red;
                    }
                    break;

                }
                
            }
            answerButton.IsEnabled = false;
            saveButton.IsEnabled = true;
            nextButton.IsEnabled = true;
            answerLabel.Visibility = Visibility.Visible;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            questNum++;
            pointsLabel.Content = score;
            if (questNum >= questions.Count())
            {
                MessageBox.Show("Points: " + score);
                Close();
            } else
            {
                foreach (var radioBtn in possibleAnswers)
                {
                    radioBtn.Background = Brushes.White;
                    radioBtn.IsChecked = false;
                }
                answerLabel.Visibility = Visibility.Hidden;
                start();
            }
            
        }
    }

    public class Question
    {
        private string quest;
        private string answer;

        public Question(string question, string ans)
        {
            quest = question;
            answer = ans;
        }

        public string Quest
        {
            get
            {
                return quest;
            }
            set
            {
                quest = value;
            }
        }

        public string Answer
        {
            get
            {
                return answer;
            }

            set
            {
                answer = value;
            }
        }
        
    }
    
}
