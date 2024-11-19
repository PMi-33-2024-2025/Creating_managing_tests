using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTestExam.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; } 

        public Test Test { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }

    public enum QuestionType
    {
        MultipleChoice,
        Open,
        ShortAnswer,
        TrueFalse,
        SingleCorrectAnswer
    }
}
