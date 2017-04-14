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
using CSTQuizlet.ViewModels;

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
        private string course;
        private string topic;

        public CreateView()
        {
            InitializeComponent();
            type = "";
            radioSelected = false;
            populateClassComboBox();
        }

        /// <summary>
        /// Chooses the type of question to be created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the submission of data into the database when the button is clicked. 
        /// Multiple choice:
        /// Maps each potential answer to the corresponding text box, and iterates through it
        /// to find the correct answer.it is stored, and the remaining choices are added to a list.
        /// the data is passed to the WriteMC method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    //string courseID, string question, string topic, int difficulty, int weight, string correctAnswer, List<string> altAnswers
                    WriteMC(course, question, topic, 1, 1, correctAnswer, altAnswers);
                    break;
                case "1":
                    //string courseID, string question, string topic, int difficulty, int weight, string answer
                    WriteSA(course, question, topic, 1, 1, shortAnswerTextBox.Text);
                    break;
                case "2":
                    //string courseID, string question, string topic, int difficulty, int weight
                    WriteTF(course, question, topic, 1, 1);
                    break;
                default:
                    break;
            }

            hideAndClear();

        }

        /// <summary>
        /// Clear values and hide fields
        /// </summary>
        private void hideAndClear()
        {
            topicComboBox.Items.Clear();
            textBox.Clear();
            shortAnswerTextBox.Clear();
            trueButton.IsChecked = false;
            falseButton.IsChecked = false;
            answer1.IsChecked = false;
            answer2.IsChecked = false;
            answer3.IsChecked = false;
            answer4.IsChecked = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            multiChoiceView.Visibility = Visibility.Collapsed;
            shortAnswerView.Visibility = Visibility.Collapsed;
            trueFalseView.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Add all courses from database into classComboBox.
        /// </summary>
        private void populateClassComboBox()
        {
            SqlDataReader reader;
            try
            {
                using (SqlConnection connection = MainWindow.getConnection())
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT courseID FROM Course", connection))
                    {
                        classComboBox.Items.Clear();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            System.Diagnostics.Debug.WriteLine(reader.GetString(reader.GetOrdinal("courseID")));
                            classComboBox.Items.Add(reader.GetString(reader.GetOrdinal("courseID")));

                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to the database. Please retry or contact the administrator");
            }
        }

        /// <summary>
        /// Queries all classes from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void classComboBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string course = classComboBox.SelectedValue.ToString();
                System.Diagnostics.Debug.WriteLine("Selected Course");
                System.Diagnostics.Debug.WriteLine(course);
                this.course = course;
                populateTopicsComboBox();
            } catch(NullReferenceException)
            {
                System.Diagnostics.Debug.WriteLine("No Course Selected");
            }
            

        }

        /// <summary>
        /// Queries all topics from specified class and adds to topicsComboBox
        /// </summary>
        private void populateTopicsComboBox()
        {
            SqlDataReader reader;
            string command = "SELECT topic FROM CourseTopic WHERE courseID LIKE '%" + course + "%'";
            try
            {
                using (SqlConnection connection = MainWindow.getConnection())
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(command, connection))
                    {
                        topicComboBox.Items.Clear();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            System.Diagnostics.Debug.WriteLine(reader.GetString(reader.GetOrdinal("topic")));
                            topicComboBox.Items.Add(reader.GetString(reader.GetOrdinal("topic")));

                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to the database. Please retry or contact the administrator");
            }
        }

        /// <summary>
        /// Select topic from topicComboBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void topicComboBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string topic = topicComboBox.SelectedValue.ToString();
                System.Diagnostics.Debug.WriteLine("Selected Topic");
                System.Diagnostics.Debug.WriteLine(course);
                this.topic = topic;
            } catch (NullReferenceException)
            {
                System.Diagnostics.Debug.WriteLine("No Topic Selected");
            }
            
        }

        /// <summary>
        /// Maps each radio button to the corresponding textbox.
        /// </summary>
        /// <param name="choices"></param>
        void mapAnswers(ref Dictionary<RadioButton, TextBox> choices)
        {
            choices.Add(answer1, textBox1);
            choices.Add(answer2, textBox2);
            choices.Add(answer3, textBox3);
            choices.Add(answer4, textBox4);
        }

        /// <summary>
        /// Writes the question, the correct answer, and the alternate answers to the database. 
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="question"></param>
        /// <param name="topic"></param>
        /// <param name="difficulty"></param>
        /// <param name="weight"></param>
        /// <param name="correctAnswer"></param>
        /// <param name="altAnswers"></param>
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

        /// <summary>
        /// Inserts the question and answer into the database for short answer style questions
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="question"></param>
        /// <param name="topic"></param>
        /// <param name="difficulty"></param>
        /// <param name="weight"></param>
        /// <param name="answer"></param>
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
                    InsertAnswer(getNextAnswerID(), questionID, "True", true);
                else
                    InsertAnswer(getNextAnswerID(), questionID, "False", true);
            }
            catch
            {
                MessageBox.Show("CreateView: cannot connect to the database.", "Error Occurred");
            }
        }

        /// <summary>
        /// Inserts a single answer into the database.
        /// </summary>
        /// <param name="answerID"></param>
        /// <param name="questionID"></param>
        /// <param name="answer"></param>
        /// <param name="correct"></param>
        private void InsertAnswer(int answerID, int questionID, string answer, bool correct)
        {
            try
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
            catch (Exception)
            {
                MessageBox.Show("Error connecting to the database. Please retry or contact the administrator");
            }
        }

        /// <summary>
        /// Inserts a single question into the database.
        /// </summary>
        /// <param name="questionID"></param>
        /// <param name="courseID"></param>
        /// <param name="question"></param>
        /// <param name="topic"></param>
        /// <param name="type"></param>
        /// <param name="weight"></param>
        /// <param name="difficulty"></param>
        private void InsertQuestion(int questionID, string courseID, string question, string topic, string type, int weight, int difficulty)
        {
            try
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
            catch (Exception)
            {
                MessageBox.Show("Error connecting to the database. Please retry or contact the administrator");
            }
        }


        /// <summary>
        /// Counts the number of values in the answer table. This
        /// number is used as the answerID when inputting new values.
        /// </summary>
        /// <returns></returns>
        private int getNextAnswerID()
        {
            try
            {
                using (SqlConnection connection = MainWindow.getConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 answerID FROM Answers ORDER BY answerID DESC; ", connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar()) + 1;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to the database. Please retry or contact the administrator");
                return -1;
            }
        }

        /// <summary>
        /// Counts the number of values in the testbank table.
        /// </summary>
        /// <returns></returns>
        private int getNextQuestionID()
        {
            try
            {
                using (SqlConnection connection = MainWindow.getConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 questionID FROM TestBank ORDER BY questionID DESC; ", connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar()) + 1;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to the database. Please retry or contact the administrator");
                return -1;
            }
        }

        /// <summary>
        /// Toggles the radioSelected boolean when a user clicks on a true or false radio button in the true/false view
        /// </summary>
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
