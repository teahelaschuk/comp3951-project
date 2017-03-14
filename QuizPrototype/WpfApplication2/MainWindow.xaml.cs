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
        //private List<string> answers = new List<string>();
        private List<RadioButton> possibleAnswers = new List<RadioButton>();
        private List<Quiz> savedQuizzes = new List<Quiz>();
        private int questNum;
        private static int score;
        private Quiz quiz;

        public MainWindow()
        {
            InitializeComponent();
            initQuiz();
            start();
        }

        private void start()
        {
            Question question = quiz.Questions[questNum];
            Random rnd = new Random();
            string[] answers = question.Answers.OrderBy(x => rnd.Next()).ToArray();

            radioButton1.Content = answers[0];
            radioButton2.Content = answers[1];
            radioButton3.Content = answers[2];
            radioButton4.Content = answers[3];

            questionLabel.Content = question.Quest;
            answerLabel.Content = question.Answer;
            answerButton.IsEnabled = true;
            nextButton.IsEnabled = false;

        }

        private void initQuiz()
        {
            List<string> keywords = new List<string>();
            keywords.Add("C#");
            quiz = new Quiz(keywords);
            List<Question> questions = new List<Question>();
            
            questions.Add(new Question(quiz, "What is another term for data hiding?", "Encapsulation"));
            List<string> answers1 = new List<string>();
            answers1.Add("Boxing");
            answers1.Add("Encapsulation");
            answers1.Add("Fielding");
            answers1.Add("Protected");
            questions[0].Answers = answers1;
            questions.Add(new Question(quiz, "The data stored in an object are commonly called fields or:", "Properties"));
            List<string> answers2 = new List<string>();
            answers2.Add("Properties");
            answers2.Add("Operators");
            answers2.Add("Destructors");
            answers2.Add("Value Type");
            questions[1].Answers = answers2;
            questions.Add(new Question(quiz, "What compiles ILR code into machine language?", "CLR"));
            List<string> answers3 = new List<string>();
            answers3.Add("JIT");
            answers3.Add("JTI");
            answers3.Add("Destructor");
            answers3.Add("CLR");
            questions[2].Answers = answers3;
            questions.Add(new Question(quiz, "What are the setters and getters for fields called?", "Accessors"));
            List<string> answers4 = new List<string>();
            answers4.Add("Properties");
            answers4.Add("Values");
            answers4.Add("Accessors");
            answers4.Add("Specifiers");
            questions[3].Answers = answers4;
            questions.Add(new Question(quiz, "Which of the following is the default access specifier of a class member variable?", "Private"));
            List<string> answers5 = new List<string>();
            answers5.Add("Protected");
            answers5.Add("Protected Internal");
            answers5.Add("Private");
            answers5.Add("Default");
            questions[4].Answers = answers5;

            quiz.Questions = questions;
            possibleAnswers.Add(radioButton1);
            possibleAnswers.Add(radioButton2);
            possibleAnswers.Add(radioButton3);
            possibleAnswers.Add(radioButton4);

        }

        private void answerButton_Click(object sender, RoutedEventArgs e)
        {
            Question question = quiz.Questions[questNum];
            foreach (var radioBtn in possibleAnswers)
            {
                if (radioBtn.IsChecked == true)
                {
                    if (radioBtn.Content.ToString() == question.Answer)
                    {
                        radioBtn.Background = Brushes.Green;
                        score++;
                    }
                    else
                    {
                        radioBtn.Background = Brushes.Red;
                    }
                    answerButton.IsEnabled = false;
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
            if (questNum >= quiz.Questions.Count())
            {
                MessageBox.Show("Points: " + score + " out of " + quiz.Questions.Count());
                Close();
            }
            else
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

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            savedQuizzes.Add(quiz);
            MessageBox.Show("Saved Quiz!");
            saveButton.Visibility = Visibility.Hidden;
        }
    }

    class Question
    {
        private string question;
        private string answer;
        private List<string> answers = new List<string>(); //list of all answers to question
        private List<string> keywords;
        string uniqueID; //holds question's ID for database 
        private Quiz quiz; //reference to current quiz
        private Image questionImage = null;

        public Question(Quiz quiz, string question, string answer, Image questionImage = null)
        {
            this.quiz = quiz;
            this.question = question;
            this.answer = answer;
        }

        public string Quest
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
            }
        }

        public string Answer
        {
            get
            {
                return answer;
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

    class Quiz
    {
        List<Question> questions = new List<Question>();
        public Quiz(List<string> keywords)
        {

        }

        public List<Question> Questions
        {
            get
            {
                return questions;
            }
            set
            {
                questions = value;
            }
        }
    }

}