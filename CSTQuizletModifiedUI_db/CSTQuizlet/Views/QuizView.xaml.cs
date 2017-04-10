using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using CSTQuizlet.ViewModels;

namespace CSTQuizlet.Views
{
    /// <summary>
    /// Interaction logic for QuizView.xaml
    /// </summary>
    public partial class QuizView : UserControl
    {
        private List<QuizQuestion> questions;
        private int currentQuestion, currentTotal;
        public QuizView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Runs when the quiz page is first displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Initialized(object sender, EventArgs e)
        {
            questions = new List<QuizQuestion>();
            currentQuestion = currentTotal = 0;
            if (DataAccess.SelectedTopics.Count > 0)
                runQuiz();
        }

        /// <summary>
        /// Starts the quiz.
        /// </summary>
        private void runQuiz()
        {
            getQuestions();
            if (questions.Count == 1)
                nextButton.IsEnabled = false;
            presentQuestion(questions.ElementAt(currentQuestion));
        }

        /// <summary>
        /// Updates the view to display a question in the quiz. The view adapts to accept user input
        /// appropriate for the type of the question.
        /// </summary>
        /// <param name="q"></param>
        private void presentQuestion(QuizQuestion q)
        {
            questionLabel.Text = q.Question;
            getAnswers(q);
            quizStatus.Content = (currentQuestion + 1) + "/" + questions.Count;
            correctStatus.Content = currentTotal + "/" + (currentQuestion);

            switch (q.Type)
            {
                case "SA":
                    setupSAView();
                    break;
                case "MC":
                    setupMCView();
                    radioButton1.Content = q.answers.ElementAt(0).Answer;
                    radioButton2.Content = q.answers.ElementAt(1).Answer;
                    radioButton3.Content = q.answers.ElementAt(2).Answer;
                    radioButton4.Content = q.answers.ElementAt(3).Answer;
                    break;
                case "TF":
                    setupTFView();
                    radioButton1.Content = "True";
                    radioButton2.Content = "False";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Hides all radio buttons and brings up a text box for short answer style questions.
        /// </summary>
        private void setupSAView()
        {
            inputTextBox.Visibility = Visibility.Visible;
            radioButton1.Visibility = Visibility.Hidden;
            radioButton2.Visibility = Visibility.Hidden;
            radioButton3.Visibility = Visibility.Hidden;
            radioButton4.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Makes all radio buttons visible for multiple choice style questions.
        /// </summary>
        private void setupMCView()
        {
            radioButton1.Visibility = Visibility.Visible;
            radioButton2.Visibility = Visibility.Visible;
            radioButton3.Visibility = Visibility.Visible;
            radioButton4.Visibility = Visibility.Visible;
            inputTextBox.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Makes two radio buttons visible for true/false style questions.
        /// </summary>
        private void setupTFView()
        {
            radioButton1.Visibility = Visibility.Visible;
            radioButton2.Visibility = Visibility.Visible;
            radioButton3.Visibility = Visibility.Hidden;
            radioButton4.Visibility = Visibility.Hidden;
            inputTextBox.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Queries the database to find all answers for a specific question.
        /// </summary>
        /// <param name="q"></param>
        private void getAnswers(QuizQuestion q)
        {
            using (SqlConnection connection = MainWindow.getConnection())
            {
                string query = "SELECT answer, correct FROM Answers WHERE questionID = " + q.Id;
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            q.answers.Add(new QuizAnswer(reader.GetString(0), reader.GetBoolean(1)));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets all relevant questions for the quiz.
        /// </summary>
        private void getQuestions()
        {
            using (SqlConnection connection = MainWindow.getConnection())
            {
                string query = getSQLQuery();
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(new QuizQuestion(reader.GetInt32(0),
                                                           reader.GetString(1),
                                                           reader.GetString(2),
                                                           reader.GetString(3),
                                                           reader.GetInt32(4),
                                                           reader.GetInt32(5)));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Writes an SQl query to find all questions relevant to the course and topics selected.
        /// </summary>
        /// <returns></returns>
        private string getSQLQuery()
        {
            if (DataAccess.SelectedTopics.Count == 1)
                return "SELECT questionID, question, topic, type, difficulty, weight FROM TestBank WHERE courseID = '" + DataAccess.CourseID + "' AND topic = '" + DataAccess.SelectedTopics.ElementAt(0) + "'";

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT questionID, question, topic, type, difficulty, weight FROM TestBank WHERE courseID = '" + DataAccess.CourseID + "' AND topic IN ('");
            for (int i = 0; i < DataAccess.SelectedTopics.Count; i++)
            {
                if (i == (DataAccess.SelectedTopics.Count - 1))
                    sb.Append(DataAccess.SelectedTopics.ElementAt(i) + "')");
                else
                    sb.Append(DataAccess.SelectedTopics.ElementAt(i) + "', '");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the next question.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            currentQuestion++;
            if (currentQuestion == questions.Count - 1)
            {
                nextButton.IsEnabled = false;
            }
            presentQuestion(questions.ElementAt(currentQuestion));
        }

        /// <summary>
        /// Checks the answer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            string correctAnswer = "";
            string userAnswer = "";

            // handling MC and TF questions
            if (questions.ElementAt(currentQuestion).Type != "SA")
            {
                userAnswer = getSelection();
                foreach (QuizAnswer a in questions.ElementAt(currentQuestion).answers)
                    if (a.Correct == true)
                        correctAnswer = a.Answer;
            }
            // handing SA questions
            else
            {
                userAnswer = inputTextBox.Text;
                correctAnswer = questions.ElementAt(currentQuestion).answers.ElementAt(0).Answer;
            }

            // comparing the user's answer to the database's answer
            if (userAnswer.Equals(correctAnswer, StringComparison.InvariantCultureIgnoreCase))
            {
                MessageBox.Show("correct");
                currentTotal++;
            }
            else
            {
                MessageBox.Show("correct answer is: \n" + correctAnswer, "incorrect");
            }

            //correctStatus.Content = currentTotal + "/" + (currentQuestion + 1);

            // if it is not the last question, go to the next. if it is the last question, 
            // and the last answer is submitted, call completeQuiz() to display the quiz summary.
            if (nextButton.IsEnabled == true)
                nextButton_Click(this, null);
            else if (currentQuestion == questions.Count-1)
                completeQuiz();

        }

        /// <summary>
        /// Cycles through radio buttons to find the user's selected answer for multiple choice and 
        /// true/false style questions
        /// </summary>
        /// <returns></returns>
        private string getSelection()
        {
            List<RadioButton> options = new List<RadioButton>();
            options.Add(radioButton1);
            options.Add(radioButton2);
            if (questions.ElementAt(currentQuestion).Type == "MC")
            {
                options.Add(radioButton3);
                options.Add(radioButton4);
            }

            foreach (RadioButton rb in options)
                if (rb.IsChecked == true)
                    return rb.Content.ToString();
            return "error";
        }

        /// <summary>
        /// Displays a pop up with a simple quiz summary
        /// </summary>
        private void completeQuiz()
        {
            double score = Math.Round(((double)currentTotal / (double)questions.Count) * 100, 2);
            string message = "Score: " + score + "%";
            correctStatus.Content = currentTotal + "/" + questions.Count;

            if (MessageBox.Show(message, "Quiz Completed", MessageBoxButton.OK) == MessageBoxResult.OK)
                Console.WriteLine("...");       // navigate away from quiz page
        }
    }
}
