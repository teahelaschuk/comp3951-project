using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTQuizlet
{
    class QuizQuestion
    {
        private int id;
        public int Id
        {
            get { return id; }
        }

        private string question;
        public string Question
        {
            get { return question; }
        }

        private string topic;

        private string type;
        public string Type
        {
            get { return type; }
        }

        private int difficulty;
        private int weight;

        public List<QuizAnswer> answers;

        public QuizQuestion(int id, string question, string topic, string type, int difficulty, int weight)
        {
            this.id = id;
            this.question = question;
            this.topic = topic;
            this.type = type;
            this.difficulty = difficulty;
            this.weight = weight;
            answers = new List<QuizAnswer>();
        }


    }
}
