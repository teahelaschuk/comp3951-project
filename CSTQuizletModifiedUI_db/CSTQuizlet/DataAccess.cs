using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTQuizlet
{
    /// <summary>
    /// Static class simplifying access to courses in database.
    /// </summary>
    static class DataAccess
    {
        private static List<string> selectedTopics;
        private static string courseID;

        public static List<string> SelectedTopics
        {
            get { return selectedTopics; }
            set { selectedTopics = value; }
        }

        public static string CourseID
        {
            get { return courseID; }
            set { courseID = value; }
        }
        static DataAccess()
        {
            selectedTopics = new List<string>();
        }               
    }
}
