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
using System.Data;

namespace CSTQuizlet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!TestDB())
            {
                Application.Current.Shutdown();
            }
        }

        private bool TestDB()
        {
            using (var connection = getConnection())
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected successfully.");
                }
                catch
                {
                    MessageBox.Show("Cannot connect to the database. Check yer wifi.", "Error Occurred");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Used globally to connect to the database. The calling method is repsonsible for exception handling.
        /// </summary>
        /// <returns>a connection to the database</returns>
        public static SqlConnection getConnection()
        {
            string connectionString = "Server=tcp:quizletserver.database.windows.net,1433;Database=quizlet_db;User ID=group3;Password=GroupThree!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(connectionString);
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CreateViewModel();
        }

        private void bcitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://learn.bcit.ca/");
        }

        private void takeQuizButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new QuizSearchViewModel();
        }

        // out of scope features:
        //private void forumButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DataContext = new ForumViewModel();
        //}

        //private void upcomingButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DataContext = new UpcomingViewModel();
        //}

        //private void userButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DataContext = new UserViewModel();
        //}
    }
}
