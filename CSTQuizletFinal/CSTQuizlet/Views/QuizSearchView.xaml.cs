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

namespace CSTQuizlet.Views
{
    /// <summary>
    /// Interaction logic for QuizSearchView.xaml
    /// </summary>
    public partial class QuizSearchView : UserControl
    {
        public QuizSearchView()
        {
            InitializeComponent();
        }

        private void takeQuizButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new QuizViewModel();
        }
    }
}
