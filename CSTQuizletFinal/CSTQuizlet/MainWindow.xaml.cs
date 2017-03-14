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
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CreateViewModel();
        }

        private void forumButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ForumViewModel();
        }

        private void upcomingButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UpcomingViewModel();
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UserViewModel();
        }

        private void quizSearchButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new QuizSearchViewModel();
        }
    }
}
