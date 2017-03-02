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
        //private List<string> answers = new List<string>();
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
            Random rnd = new Random();
            string[] answers = questions[questNum].Answers.OrderBy(x => rnd.Next()).ToArray();
            
            radioButton1.Content = answers[0];
            radioButton2.Content = answers[1];
            radioButton3.Content = answers[2];
            radioButton4.Content = answers[3];

            questionLabel.Content = questions[questNum].Quest;
            answerLabel.Content = questions[questNum].CorrectAnswer;
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
            
            questions.Add(new Question("What is another term for data hiding?", "Encapsulation"));
            List<string> answers1 = new List<string>();
            answers1.Add("Boxing");
            answers1.Add("Encapsulation");
            answers1.Add("Fielding");
            answers1.Add("Protected");
            questions[0].Answers = answers1;
            questions.Add(new Question("The data stored in an object are commonly called fields or:", "Properties"));
            List<string> answers2 = new List<string>();
            answers2.Add("Properties");
            answers2.Add("Operators");
            answers2.Add("Destructors");
            answers2.Add("Value Type");
            questions[1].Answers = answers2;
            questions.Add(new Question("What compiles ILR code into machine language?", "CLR"));
            List<string> answers3 = new List<string>();
            answers3.Add("JIT");
            answers3.Add("JTI");
            answers3.Add("Destructor");
            answers3.Add("CLR");
            questions[2].Answers = answers3;
            questions.Add(new Question("What are the setters and getters for fields called?", "Accessors"));
            List<string> answers4 = new List<string>();
            answers4.Add("Properties");
            answers4.Add("Values");
            answers4.Add("Accessors");
            answers4.Add("Specifiers");
            questions[3].Answers = answers4;
            questions.Add(new Question("Which of the following is the default access specifier of a class member variable?", "Private"));
            List<string> answers5 = new List<string>();
            answers5.Add("Protected");
            answers5.Add("Protected Internal");
            answers5.Add("Private");
            answers5.Add("Default");
            questions[4].Answers = answers5;

        }

        private void answerButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var radioBtn in possibleAnswers)
            {
                if(radioBtn.IsChecked == true)
                {
                    if(radioBtn.Content.ToString() == questions[questNum].CorrectAnswer)
                    {
                        radioBtn.Background = Brushes.Green;
                        score++;
                    } else
                    {
                        radioBtn.Background = Brushes.Red;
                    }
                    answerButton.IsEnabled = false;
                    saveButton.IsEnabled = true;
                    nextButton.IsEnabled = true;
                    answerLabel.Visibility = Visibility.Visible;
                    break;

                }
                
            }
            
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
        private string correctAnswer;
        private List<string> answers = new List<string>();

        public Question(string question, string ans)
        {
            quest = question;
            correctAnswer = ans;
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

        public string CorrectAnswer
        {
            get
            {
                return correctAnswer;
            }

            set
            {
                correctAnswer = value;
            }
        }

        public List<string> Answers
        {
            get
            {
                return answers;
            }

            set
            {
                answers = value;
            }
        }
        
    }
    
}
