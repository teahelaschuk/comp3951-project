using System;
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
using System.Data.SqlClient;
using System.Data;

      /*
       * Table columns, for reference
       * TestBank: questionID, courseID, question, topic, type, difficulty, weight
       * Answers:  answerID, questionID, answer, correct
       * Course:   courseID, courseName, cstLevel
       */

namespace CSTQuizlet.Views
{
    /// <summary>
    /// Interaction logic for CreateView.xaml
    /// </summary>
    public partial class CreateView : UserControl
    {
        private string type;
        private bool radioSelected;     

        public CreateView()
        {
            InitializeComponent();
            type = "";
            radioSelected = false;
        }


        private void answerTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // 0 = Multiple Choice
            // 1 = Short Answer
            // 2 = True/False

            switch (answerTypeComboBox.SelectedIndex.ToString())
            {
                case "0":
                    type = "MC";
                    submitQuestionButton.IsEnabled = true;
                    multiChoiceView.Visibility = Visibility.Visible;
                    shortAnswerView.Visibility = Visibility.Collapsed;
                    trueFalseView.Visibility = Visibility.Collapsed;

                    break;
                case "1":
                    type = "SA";
                    submitQuestionButton.IsEnabled = true;
                    multiChoiceView.Visibility = Visibility.Collapsed;
                    shortAnswerView.Visibility = Visibility.Visible;
                    trueFalseView.Visibility = Visibility.Collapsed;
                    break;
                case "2":
                    type = "TF";
                    multiChoiceView.Visibility = Visibility.Collapsed;
                    shortAnswerView.Visibility = Visibility.Collapsed;
                    trueFalseView.Visibility = Visibility.Visible;
                    break;
                default:
                    type = "";
                    submitQuestionButton.IsEnabled = false;
                    multiChoiceView.Visibility = Visibility.Collapsed;
                    shortAnswerView.Visibility = Visibility.Collapsed;
                    trueFalseView.Visibility = Visibility.Collapsed;
                    break;

            }
        }

        /* handles the submission of data into the database when the button is clicked. 
         * multiple choice:
         *      maps each potential answer to the corresponding text box, and iterates through it
         *      to find the correct answer. it is stored, and the remaining choices are added to a list. 
         *      the data is passed to the WriteMC method. */
        private void submitQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sent!");
            string question = textBox.Text;
            string correctAnswer = "n/a";
            List<string> altAnswers = new List<string>();

            switch (answerTypeComboBox.SelectedIndex.ToString())
            {
                case "0":
                    Dictionary<RadioButton, TextBox> choices = new Dictionary<RadioButton, TextBox>();
                    mapAnswers(ref choices);
                    foreach (KeyValuePair<RadioButton, TextBox> entry in choices)
                    {
                        if (entry.Key.IsChecked == true)
                            correctAnswer = entry.Value.Text;
                        else
                            altAnswers.Add(entry.Value.Text);
                    }
                    WriteMC("COMP0000", question, "Whatever", 1, 1, correctAnswer, altAnswers);
                    break;
                case "1":
                    WriteSA("COMP0000", question, "Whatever", 1, 1, shortAnswerTextBox.Text);
                    break;
                case "2":
                    WriteTF("COMP0000", question, "Whatever", 1, 1);
                    break;
                default:
                    break;
            }
        }

        public void topicComboBox_DropDownClosed(object sender, EventArgs e)
        {
            List<string> courses = new List<string>();
            SqlDataReader reader;
            using (SqlConnection connection = MainWindow.getConnection())
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand("SELECT courseID FROM Course WHERE cstLevel = 4", connection))
                {
                    reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        courses.Add(reader.GetString(reader.GetOrdinal("courseID")));
                        System.Diagnostics.Debug.WriteLine(reader.GetString(reader.GetOrdinal("courseID")));
                        classComboBox.Items.Add(reader.GetString(reader.GetOrdinal("courseID")));

                    }
                }
            }
        }

        /* Queries all classes from the database */
        public void classComboBox_DropDownClosed(object sender, EventArgs e)
        {

        }

        /* Queries all topics available based on class chosen */
        public void topicComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // Do Something
        }

        /* Maps each radio button to the corresponding textbox. */
        void mapAnswers(ref Dictionary<RadioButton, TextBox> choices)
        {
            choices.Add(answer1, textBox1);
            choices.Add(answer2, textBox2);
            choices.Add(answer3, textBox3);
            choices.Add(answer4, textBox4);
        }

        /* writes the question, the correct answer, and the alternate answers to the database. */
        private void WriteMC(string courseID, string question, string topic, int difficulty, int weight, string correctAnswer, List<string> altAnswers)
        {
            try
            {
                Console.WriteLine("CreateView: connected successfully");
                int questionID = getNextQuestionID();
                InsertQuestion(questionID, courseID, question, topic, type, weight, difficulty);
                InsertAnswer(getNextAnswerID(), questionID, correctAnswer, true);
                foreach (string s in altAnswers)
                    InsertAnswer(getNextAnswerID(), questionID, s, false);
            }
            catch
            {
                MessageBox.Show("CreateView: cannot connect to the database.", "Error Occurred");
                // quit?
            }
        }

        /* inserts the question and answer into the database for short answer style questions */
        private void WriteSA(string courseID, string question, string topic, int difficulty, int weight, string answer)
        {
            try
            {
                Console.WriteLine("CreateView: connected successfully");
                int questionID = getNextQuestionID();
                InsertQuestion(questionID, courseID, question, topic, type, weight, difficulty);
                InsertAnswer(getNextAnswerID(), questionID, answer, true);
            }
            catch
            {
                MessageBox.Show("CreateView: cannot connect to the database.", "Error Occurred");
            }
        }

        private void WriteTF(string courseID, string question, string topic, int difficulty, int weight)
        {
            try
            {
                Console.WriteLine("CreateView: connected successfully");
                int questionID = getNextQuestionID();
                InsertQuestion(questionID, courseID, question, topic, type, weight, difficulty);
                if(radioSelected == true)
                    InsertAnswer(getNextAnswerID(), questionID, "True", radioSelected);
                else
                    InsertAnswer(getNextAnswerID(), questionID, "False", radioSelected);
            }
            catch
            {
                MessageBox.Show("CreateView: cannot connect to the database.", "Error Occurred");
            }
        }

        /* inserts a single answer into the database */
        private void InsertAnswer(int answerID, int questionID, string answer, bool correct)
        {
            using (SqlConnection connection = MainWindow.getConnection())
            {
                string query = "INSERT INTO Answers (answerID, questionID, answer, correct) " +
                               "VALUES (@answerID, @questionID, @answer, @correct) ";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@answerID", SqlDbType.Int).Value = answerID;
                    cmd.Parameters.Add("@questionID", SqlDbType.Int).Value = questionID;
                    cmd.Parameters.Add("@answer", SqlDbType.Text).Value = answer;
                    cmd.Parameters.Add("@correct", SqlDbType.Bit).Value = correct;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /* inserts a single question into the database */
        private void InsertQuestion(int questionID, string courseID, string question, string topic, string type, int weight, int difficulty)
        {
            using (SqlConnection connection = MainWindow.getConnection())
            {
                string query = "INSERT INTO TestBank (questionID, courseID, question, topic, type, weight, difficulty) " +
                               "VALUES (@questionID, @courseID, @question, @topic, @type, @weight, @difficulty) ";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@questionID", SqlDbType.Int).Value = questionID;
                    cmd.Parameters.Add("@courseID", SqlDbType.Text).Value = courseID;
                    cmd.Parameters.Add("@question", SqlDbType.Text).Value = question;
                    cmd.Parameters.Add("@topic", SqlDbType.Text).Value = topic;
                    cmd.Parameters.Add("@type", SqlDbType.Text).Value = type;
                    cmd.Parameters.Add("@weight", SqlDbType.Int).Value = weight;
                    cmd.Parameters.Add("@difficulty", SqlDbType.Int).Value = difficulty;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /* counts the number of values in the answer table. this
         * number is used as the answerID when inputting new values. */
        private int getNextAnswerID()
        {
            using (SqlConnection connection = MainWindow.getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Answers;", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        /* counts the number of values in the testbank table. */
        private int getNextQuestionID()
        {
            using (SqlConnection connection = MainWindow.getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM TestBank;", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        /* toggles the radioSelected boolean when a user clicks on a true or false radio button in the true/false view */
        private void tf_Checked(object sender, RoutedEventArgs e)
        {
            submitQuestionButton.IsEnabled = true;
            if ((sender as RadioButton).Content.ToString() == "True")
                radioSelected = true;
            else
                radioSelected = false;
        }
    }

    
}
