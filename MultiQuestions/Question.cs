using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQuestions
{
    public enum TypeQuestion
    {
        CheckBox,
        TextBox
    }
    public class Question : ICloneable
    {
        public int? ID { get; set; }
        public string NameQuestions { get; set; }
        public string Text { get; set; }
        public TypeQuestion typeQuestion { get; set; }
        
        public List<Answer> Answers { get; set; }
        public string Comment { get; set; }

        public object Clone()
        {
            Question newQuestion = new Question
            {
                ID = this.ID,
                NameQuestions = this.NameQuestions,
                typeQuestion = this.typeQuestion,
                Text = this.Text,
                Comment = this.Comment,
                Answers = new List<Answer>(this.Answers.Count)
            };
            this.Answers.ForEach((answr) => 
            {
                newQuestion.Answers.Add((Answer)answr.Clone());
            });
            return newQuestion;
        }
    }
    public class Answer : ICloneable
    {
        public int? ID { get; set; }
        public string Text { get; set; }
        public bool Trueness { get; set; }
        public int? id_Question { get; set; }

        public object Clone()
        {
            return new Answer
            {
                ID = this.ID,
                Text = this.Text,
                Trueness = this.Trueness,
                id_Question = this.id_Question
            };
        }
    }

    /// <summary>
    /// Сопоставляющая таблица, но пока в контексте приложения не используеться. Для будущих расширений.
    /// </summary>
    public class Question_Answer
    {
        public int ID_Question { get; set; }
        public int ID_Answer { get; set; }
    }
    public class Group  
    {
        public string NameGroup { get; set; }
        public List<Question> QuestList { get; set; }

    }
}
