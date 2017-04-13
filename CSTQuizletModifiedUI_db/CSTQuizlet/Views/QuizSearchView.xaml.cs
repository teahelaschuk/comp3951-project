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
using CSTQuizlet.ViewModels;
using System.Data.SqlClient;

/// <summary>
/// to do: better logic for the stack panels
/// </summary>
namespace CSTQuizlet.Views
{

    /// <summary>
    /// Interaction logic for QuizSearchView.xaml
    /// </summary>
    public partial class QuizSearchView : UserControl
    {
        StackPanel col1, col2;
        public QuizSearchView()
        {
            InitializeComponent();
            col1 = new StackPanel { Orientation = Orientation.Vertical };
            col2 = new StackPanel { Orientation = Orientation.Vertical };
        }

        /// <summary>
        /// Validates input and navigates to the Take Quiz page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takeQuizButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> selectedTopics = new List<string>();
            foreach (CheckBox c in col1.Children)
                if (c.IsChecked == true)
                    selectedTopics.Add(c.Content.ToString());
            foreach (CheckBox c in col2.Children)
                if (c.IsChecked == true)
                    selectedTopics.Add(c.Content.ToString());

            if (selectedTopics.Count != 0)
            {
                DataAccess.SelectedTopics = selectedTopics;
                Application.Current.MainWindow.DataContext = new QuizViewModel();  
            }
            else
            {
                MessageBox.Show("Select at least 1 topic to be quizzed on.");
            }
        }


        /// <summary>
        /// Called when a course is selected from the side bar. Takes the selected label and uses it to 
        /// query the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveCourseID_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataAccess.CourseID = (sender as Label).Content.ToString();
                classTitleLabel.Content = DataAccess.CourseID;
                getTopics();
                label2.Visibility = Visibility.Visible;
                label3.Visibility = Visibility.Visible;
                takeQuizButton.IsEnabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error choosing course. Please select a different course.");
                return;
            }
        }
        /// <summary>
        /// Creates a list of checkboxes containing all topics covered in the selected course and sends the
        /// data to the updateCheckboxes method to create checkboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void getTopics()
        {
            try
            {
                col1.Children.Clear();
                col2.Children.Clear();
                List<string> topics = new List<string>();
                using (SqlConnection connection = MainWindow.getConnection())
                {
                    string query = "SELECT TOPIC FROM CourseTopic WHERE courseID = '" + DataAccess.CourseID + "'";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                topics.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                updateCheckboxes(ref topics);
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to course database. Please retry or contact the administrator. ");
            }
        }

        /// <summary>
        /// takes a list of course topics and dynamically creates checkboxes.
        /// </summary>
        /// <param name="topics"></param>
        public void updateCheckboxes(ref List<string> topics)
        {
            checkboxContainer.Children.Clear();
            checkboxContainer2.Children.Clear();

            int count = 0;
            foreach (var c in topics)
            {
                count++;
                CheckBox cb = new CheckBox();
                cb.Content = c;
                cb.Foreground = Brushes.White;
                cb.FontSize = 16;
                cb.Margin = new Thickness(10, 0, 0, 0);
                if (count < 15)
                    col1.Children.Add(cb);
                else
                    col2.Children.Add(cb);
            }
            checkboxContainer.Children.Add(col1);
            checkboxContainer2.Children.Add(col2);
        }
    }
}
