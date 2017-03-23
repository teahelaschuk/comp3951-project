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

namespace CSTQuizlet.Views
{
    /// <summary>
    /// Interaction logic for CreateView.xaml
    /// </summary>
    public partial class CreateView : UserControl
    {
        public CreateView()
        {
            InitializeComponent();
        }


        private void answerTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // 0 = Multiple Choice
            // 1 = Short Answer
            // 2 = True/False

            switch(answerTypeComboBox.SelectedIndex.ToString())
            {
                case "0":
                    multiChoiceView.Visibility = Visibility.Visible;
                    shortAnswerView.Visibility = Visibility.Collapsed;
                    trueFalseView.Visibility = Visibility.Collapsed;
                    break;
                case "1":
                    multiChoiceView.Visibility = Visibility.Collapsed;
                    shortAnswerView.Visibility = Visibility.Visible;
                    trueFalseView.Visibility = Visibility.Collapsed;
                    break;
                case "2":
                    multiChoiceView.Visibility = Visibility.Collapsed;
                    shortAnswerView.Visibility = Visibility.Collapsed;
                    trueFalseView.Visibility = Visibility.Visible;
                    break;
                default:
                    multiChoiceView.Visibility = Visibility.Collapsed;
                    shortAnswerView.Visibility = Visibility.Collapsed;
                    trueFalseView.Visibility = Visibility.Collapsed;
                    break;

            }
        }

        private void submitQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sent!");
        }
    }
}
