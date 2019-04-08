using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQuestions
{
    public static class Statistics
    {
        public static string thisQuestionStatus { get; set; }
        public static int answeredQuestion { get; set; }
        public static int missedQuestion { get; set; }
        public static int correctAnswers { get; set; }
        public static float correctAnswersPercent { get; set; }
      
    }
}
