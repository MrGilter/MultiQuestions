using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiQuestions 
{
    public partial class EditForm : Form 
    {
        public EditForm()
        {
            InitializeComponent();
            LoadData();
        }

        /// <summary>
        /// Загрузка данных, заполнение treeView
        /// </summary>
        private void LoadData()  
        {
            Loading.LoadGroup();
            Loading.SelectionQuestion = new Question();
            List<Group> groups = Loading.Groups;
            foreach(Group g in groups)
            {
                editForm_treeView.Nodes.Add(g.NameGroup, g.NameGroup);
                List<Question> questions = g.QuestList;                    
                foreach(Question q in questions)
                {
                    editForm_treeView.Nodes[g.NameGroup].Nodes.Add(q.NameQuestions);
                }
            }
        }

        /// <summary>
        /// Обработка события двойного щелчка по ноду из treeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editForm_treeView_MouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                foreach(Group g in Loading.Groups)
                {
                    if (e.Node.Parent.Text == g.NameGroup)
                    {
                        foreach(Question q in g.QuestList)
                        {
                            if (e.Node.Text == q.NameQuestions)
                            {
                                Loading.SelectionQuestion = q; // получаем сылку на обьект для дальнейшего редактирования

                                nameTest_textBox.Text = e.Node.Text;
                                questionType_comboBox.Items.Clear();
                                questionType_comboBox.Items.Add("CheckBox");
                                questionType_comboBox.Items.Add("TextBox");

                                if (q.typeQuestion == TypeQuestion.CheckBox)
                                    questionType_comboBox.SelectedIndex = 0;
                                else if (q.typeQuestion == TypeQuestion.TextBox)
                                    questionType_comboBox.SelectedIndex = 1;

                                question_richTextBox.Text = q.Text;
                                comment_richTextBox.Text = q.Comment;

                                listAnsrwers_comboBox.Items.Clear();
                                foreach(Answer a in q.Answers)
                                {
                                    listAnsrwers_comboBox.Items.Add(a.Text);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Loading.SelectionQuestion = new Question();
                nameTest_textBox.Text = "";
                questionType_comboBox.Items.Clear();
                questionType_comboBox.Items.Add("CheckBox");
                questionType_comboBox.Items.Add("TextBox");
                question_richTextBox.Text = "";
                comment_richTextBox.Text = "";
                listAnsrwers_comboBox.Items.Clear();
                response_richTextBox.Text = "";


            }
        }

        /// <summary>
        /// Обработка события выбора елемента comboBox_listAnsvers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_listAnsvers_SelectedValueChanged(object sender, EventArgs e)
        {
            response_richTextBox.Text = listAnsrwers_comboBox.SelectedItem.ToString();
            trueness_checkBox.Checked = false;

            foreach(Answer a in Loading.SelectionQuestion.Answers)
            {
                if (a.Text == listAnsrwers_comboBox.SelectedItem.ToString())
                {
                    trueness_checkBox.Checked = a.Trueness;
                }
            }
            
        }

        /// <summary>
        /// Редактирование ответа в текущем вопросе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editAnswer_button_Click(object sender, EventArgs e) 
        {
            Question q = Loading.SelectionQuestion;
            for(int i =0; i < q.Answers.Count; i++)
            {
                if (q.Answers[i].Text == listAnsrwers_comboBox.SelectedIndex.ToString())
                {
                    q.Answers[i].Text = response_richTextBox.Text;
                    q.Answers[i].Trueness = trueness_checkBox.Checked;
                    listAnsrwers_comboBox.SelectedText = q.Answers[i].Text;
                    listAnsrwers_comboBox.SelectedValue = q.Answers[i].Text;
                }
            }
            
        } 

        /// <summary>
        /// Добавление ответа в текущий вопрос
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addAnswer_button_Click(object sender, EventArgs e)
        {
            if(Loading.SelectionQuestion == null)
            {
                Loading.SelectionQuestion = new Question();
                Loading.SelectionQuestion.Answers = new List<Answer>();
            }
            if (Loading.SelectionQuestion.Answers == null)
            {
                Loading.SelectionQuestion.Answers = new List<Answer>();
            }
            Loading.SelectionQuestion.Answers.Add(new Answer { Text = response_richTextBox.Text, Trueness = trueness_checkBox.Checked });
            listAnsrwers_comboBox.Items.Add(response_richTextBox.Text);
            response_richTextBox.Text = "";
            trueness_checkBox.Checked = false;

        }

        /// <summary>
        /// Удаление ответа из текущего вопроса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteSelect_button_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < Loading.SelectionQuestion.Answers.Count; i++)
            {
                if (Loading.SelectionQuestion.Answers[i].Text == listAnsrwers_comboBox.SelectedItem.ToString())
                {
                    Loading.SelectionQuestion.Answers.Remove(Loading.SelectionQuestion.Answers[i]);
                }
            }

            listAnsrwers_comboBox.Items.Remove(listAnsrwers_comboBox.SelectedItem);
            response_richTextBox.Text = "";
            trueness_checkBox.Checked = false;
        } 

        /// <summary>
        /// Добавление вопроса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addQuestion_button_Click(object sender, EventArgs e)
        {
            
            if (editForm_treeView.SelectedNode.Parent == null)
            {
                if (CheckNullOrEmpty())
                {
                    MessageBox.Show("Вы не заполнили одно из обязательных текстовых полей, не выбрали тип вопроса или же не добавили ни единого ответа на вопрос");
                }
                else
                {
                    if (CheckQuestionNameExists())
                    {
                        MessageBox.Show("Вопрос с таким именем уже существует");
                    }
                    else
                    {
                        Question q = Loading.SelectionQuestion;
                        q.NameQuestions = nameTest_textBox.Text;
                        q.Text = question_richTextBox.Text;

                        if (questionType_comboBox.SelectedItem.ToString() == "CheckBox")
                            q.typeQuestion = TypeQuestion.CheckBox;
                        if (questionType_comboBox.SelectedItem.ToString() == "TextBox")
                            q.typeQuestion = TypeQuestion.TextBox;

                        
                        q.Comment = comment_richTextBox.Text;

                        foreach(Group g in Loading.Groups)
                        {
                            if (g.NameGroup == editForm_treeView.SelectedNode.Name)
                                g.QuestList.Add(q);
                        }


                        Loading.SaveGroup();
                        Loading.SelectionQuestion = new Question();
                        editForm_treeView.Nodes.Clear();
                        LoadData();
                    }
                }

            }
            else
            {
                MessageBox.Show("Выбирите в трее группу для добавления вопроса.");
            }
        }

        /// <summary>
        /// Српавочная информация по использованию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void help_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("   Для редактирования вопроса дважды используйте ЛКМ на необходимом вопросе в treeView порте, заполните элементы формы и нажмите кнопку Edit question. \n" +
                "   Для добавления вопроса дважды используйте ЛКМ на необходимой группе вопросов в treeView порте, заполните элементы формы и нажмите кнопку Add question. \n" +
                "\n" +
                "   Для редактирования или создания вопроса должны быть заполнены все боксы и контроллеры формы такие как Name test, Qustion type, Question, List of answers, Comment. \n" +
                "\n" +
                "   Для заполнения List of answers введите текст ответа в Response text, наличием флажка (True) в Trueness будет отмечено правильный ли это ответ. " +
                "После чего нажмите кнопку Add answer, после этого ответ должет появиться в выпадающем списке List of answers \n" +
                "   Для редактирования ответа в List of answers выбирите его из выпадающего меню после чего текст и состояние (True) в Trueness будут активны для редактирования." +
                "После редактирования ответа нажмите на кнопку Edit answer \n" +
                "   Для удаления ответа из списка снчала выбирите его List of answers после чего нажмите на кнопку Delete selected answer from the list \n" +
                "\n" +
                "   Выпадающий список Qustion type отвечает за тип (структуру) вопроса задаваемого пользователю. Возможны следующие элементы:\n" +
                "   CheckBox - Структура подается так что выводиться множество вариантов и правильные ответы необходимо отметить галочками\n" +
                "   TextBox - Структура подается так что пользователю необходимо самому ввести правельный ответ");
        }


        
        /// <summary>
        /// Редактирование текущего вопроса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editQuestion_button_Click(object sender, EventArgs e)   
        {
            if (CheckNullOrEmpty())
            {
                MessageBox.Show("Вы не заполнили одно из обязательных текстовых полей, не выбрали тип вопроса или же не добавили ни единого ответа на вопрос");
            }
            else
            {
                Question q = Loading.SelectionQuestion;
                q.NameQuestions = nameTest_textBox.Text;
                q.Text = question_richTextBox.Text;

                if (questionType_comboBox.SelectedItem.ToString() == "CheckBox")
                    q.typeQuestion = TypeQuestion.CheckBox;
                if (questionType_comboBox.SelectedItem.ToString() == "TextBox")
                    q.typeQuestion = TypeQuestion.TextBox;

                q.Comment = comment_richTextBox.Text;

                Loading.SaveGroup();
                Loading.SelectionQuestion = new Question();
                editForm_treeView.Nodes.Clear();
                LoadData();
            }
            

        }

        /// <summary>
        /// Проверка на заполненность элементов формы
        /// </summary>
        /// <returns></returns>
        private bool CheckNullOrEmpty()
        {
            if (string.IsNullOrEmpty(nameTest_textBox.Text))
                return true;
            if (string.IsNullOrEmpty(question_richTextBox.Text))
                return true;
            if (string.IsNullOrEmpty(comment_richTextBox.Text))
                return true;
            if (listAnsrwers_comboBox.Items.Count == 0)
                return true;
            if (questionType_comboBox.SelectedItem == null)
                return true;

            return false;
        }

        /// <summary>
        /// Проверка на существуещий вопрос (совпадение имен)
        /// </summary>
        /// <returns></returns>
        private bool CheckQuestionNameExists()
        {

            foreach(Group g in Loading.Groups)
            {
                if(g.NameGroup == editForm_treeView.SelectedNode.Name)
                    foreach(Question q in g.QuestList)
                    {
                        if (q.NameQuestions == nameTest_textBox.Text)
                            return true;
                    }
            }
            return false;
        }
    }
}
