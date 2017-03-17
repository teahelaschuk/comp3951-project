using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTQuizlet.ViewModels
{
    class QuizViewModel
    {
        public string Type
        {
            get; set;
        }

        public QuizViewModel(string type)
        {
            Type = type;
        }

        private void displayQuestion()
        {
            switch(Type)
            {
                case "multiplechoice":
                    break;
                case "truefalse":
                    break;
                case "shortanswer":
                    break;
                default:
                    break;
            }
        }
        
    }
}
