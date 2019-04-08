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
    public class Question
    {
        [System.Xml.Serialization.XmlText()]
        public string NameQuestions { get; set; }
        public string Text { get; set; }
        public TypeQuestion typeQuestion { get; set; }
        //private List<Answer> answers;
        public List<Answer> Answers { get; set; }
        public string Comment { get; set; }
    }
    public class Answer
    {
        public string Text { get; set; }
        public bool Trueness { get; set; }
    }
    public class Group
    {
        public string NameGroup { get; set; }
        public List<Question> QuestList { get; set; }
    }
}
