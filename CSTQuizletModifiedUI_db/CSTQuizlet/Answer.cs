using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTQuizlet
{
    /// <summary>
    /// Represents an answer to a quiz question created from the database.
    /// </summary>
    class QuizAnswer
    {
        private string answer;
        public string Answer
        {
            get { return answer; }
        }
        private bool correct;
        public bool Correct
        {
            get { return correct; }
        }
        public QuizAnswer(string answer, bool correct)
        {
            this.answer = answer;
            this.correct = correct;
        }
    }
}
