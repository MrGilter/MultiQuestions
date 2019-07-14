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
        private Form1 form1 { get; set; }
        public EditForm()
        {
            InitializeComponent();

            Loading.updateControlForm += LoadData;
            Loading.SelectionQuestion = null;
            Loading.LoadGroup();
        }
        public void CloseForm1(Form1 f)
        {
            form1 = f;
            f.Visible=false;
        }

        /// <summary>
        /// Загрузка данных, заполнение treeView
        /// </summary>
        private void LoadData()  
        {
            editForm_treeView.Nodes.Clear();
            ClearForm();

            List<Group> groups = Loading.Groups;

            for (int i = 0; i < groups.Count; i++)
            {
                editForm_treeView.Nodes.Add(groups[i].NameGroup, groups[i].NameGroup);
                List<Question> questions = groups[i].QuestList;
                for (int y = 0; y < questions.Count; y++)
                {
                    editForm_treeView.Nodes[groups[i].NameGroup].Nodes.Add(questions[y].NameQuestions);
                }                
            }
            
            questionType_comboBox.Items.Clear();
            questionType_comboBox.Items.Add("CheckBox");
            questionType_comboBox.Items.Add("TextBox");

            
        } // 21.06

        /// <summary>
        /// Обработка события двойного щелчка по ноду из treeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editForm_treeView_MouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            editQuestion_button.BackColor = Control.DefaultBackColor;
            addQuestion_button.BackColor = Control.DefaultBackColor;

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
                ClearForm();
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
            if (Loading.SelectionQuestion != null)//что бы при создании нового вопроса не жмякали редактирование и не вызывали исключение
            {
                Question q = Loading.SelectionQuestion;
                if (listAnsrwers_comboBox.Items.Count > 0)
                {
                    if (listAnsrwers_comboBox.SelectedItem != null)
                    {
                        for (int i = 0; i < q.Answers.Count; i++)
                        {
                            if (q.Answers[i].Text == listAnsrwers_comboBox.SelectedItem.ToString())
                            {
                                q.Answers[i].Text = response_richTextBox.Text;
                                q.Answers[i].Trueness = trueness_checkBox.Checked;
                                listAnsrwers_comboBox.SelectedText = q.Answers[i].Text;
                                listAnsrwers_comboBox.SelectedValue = q.Answers[i].Text;
                            }
                        }
                        if (Loading.SelectionQuestion.ID != null)
                            editQuestion_button.BackColor = Color.Green;
                        else
                            addQuestion_button.BackColor = Color.Green;
                    }
                    else
                    {
                        MessageBox.Show("Не выбран ответ который необходимо редактировать");
                    }
                }
                else
                {
                    MessageBox.Show("Редактирование недопустимо - количество элемнтов ответов равно нулю");
                }
                
                
            }
            else
            {
                MessageBox.Show("Невозможно редактировать ответ, не создан обьект вопроса ");
            }
        } //06.07

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
            if (!string.IsNullOrEmpty(response_richTextBox.Text))
            {
                Loading.SelectionQuestion.Answers.Add(new Answer { Text = response_richTextBox.Text, Trueness = trueness_checkBox.Checked });
                listAnsrwers_comboBox.Items.Add(response_richTextBox.Text);
                response_richTextBox.Text = "";
                trueness_checkBox.Checked = false;

                if (Loading.SelectionQuestion.ID != null)
                    editQuestion_button.BackColor = Color.Green;
                else
                    addQuestion_button.BackColor = Color.Green;
            }
            else
            {
                MessageBox.Show("Прежде чем добавить ответ заполните его текстовое поле");
            }
            

        }//06.07

        /// <summary>
        /// Удаление ответа из текущего вопроса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteSelect_button_Click(object sender, EventArgs e)
        {
            if (listAnsrwers_comboBox.Items.Count>0)
            {
                if (listAnsrwers_comboBox.SelectedItem != null)
                {
                    for (int i = 0; i < Loading.SelectionQuestion.Answers.Count; i++)
                    {
                        if (Loading.SelectionQuestion.Answers[i].Text == listAnsrwers_comboBox.SelectedItem.ToString())
                        {
                            if (editForm_treeView.SelectedNode.Parent != null)//возможно нужно придумать другой механизм, во избежание сбоя таргета
                            {
                                Loading.DeleteAnswer(Loading.SelectionQuestion.Answers[i].ID, editForm_treeView.SelectedNode.Parent.Name);
                                Loading.SelectionQuestion.Answers.Remove(Loading.SelectionQuestion.Answers[i]);
                                MessageBox.Show("Completed");
                            }

                        }
                    }

                    listAnsrwers_comboBox.Items.Remove(listAnsrwers_comboBox.SelectedItem);
                    response_richTextBox.Text = "";
                    trueness_checkBox.Checked = false;
                }
                else
                {
                    MessageBox.Show("Ошибка - не выбран ответ на удаление");
                }
                
            }
            else
            {
                MessageBox.Show("Ошибка - список ответов пуст");
            }
            
        } //6.07

        /// <summary>
        /// Добавление вопроса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addQuestion_button_Click(object sender, EventArgs e)
        {
            
            if (editForm_treeView.SelectedNode!=null && editForm_treeView.SelectedNode.Parent == null)
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
                        Question q = (Question)Loading.SelectionQuestion.Clone();
                        q.NameQuestions = nameTest_textBox.Text;
                        q.Text = question_richTextBox.Text;

                        if (questionType_comboBox.SelectedItem.ToString() == "CheckBox")
                            q.typeQuestion = TypeQuestion.CheckBox;
                        if (questionType_comboBox.SelectedItem.ToString() == "TextBox")
                            q.typeQuestion = TypeQuestion.TextBox;

                        
                        q.Comment = comment_richTextBox.Text;

                        Loading.CreateQuestion((Question)q.Clone(), editForm_treeView.SelectedNode.Name);

                        Loading.SelectionQuestion = new Question();
                        addQuestion_button.BackColor = Control.DefaultBackColor;
                        //LoadData();
                    }
                }

            }
            else
            {
                MessageBox.Show("Выбирите в трее группу для добавления вопроса.");
            }
        } //21.06

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

                if (editForm_treeView.SelectedNode.Parent != null)
                {
                    Loading.EditQuestion(q, editForm_treeView.SelectedNode.Parent.Name);

                    Loading.SelectionQuestion = new Question();
                    editQuestion_button.BackColor = Control.DefaultBackColor;
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Ошибка! Сбой таргета выделенного вопроса на редактирование (в трее)! " +
                        "Выделете вопрос заново и посторите попытку.");
                }

                
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

        /// <summary>
        /// Удаление выбраного вопроса 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteQuestion_button_Click(object sender, EventArgs e)
        {
            if (Loading.SelectionQuestion!=null && Loading.SelectionQuestion.ID != null)
            {
                if (editForm_treeView.SelectedNode.Parent != null)
                {
                    Loading.DeleteSelectedQuestion(editForm_treeView.SelectedNode.Parent.Name, Loading.SelectionQuestion);
                    editForm_treeView.SelectedNode.Remove();
                   // LoadData();
                    MessageBox.Show("Completed");
                }
            }
            else
                MessageBox.Show("Не загружен вопрос который необходимо удалить (загрузить - 2 ЛКМ по нужному вопросу)");
        }//6.07

        private void ClearForm()
        {
            //editForm_treeView.Nodes.Clear();
            Loading.SelectionQuestion = null;
            nameTest_textBox.Text = "";
            questionType_comboBox.Items.Clear();
            questionType_comboBox.Items.Add("CheckBox");
            questionType_comboBox.Items.Add("TextBox");
            question_richTextBox.Text = "";
            comment_richTextBox.Text = "";
            listAnsrwers_comboBox.Items.Clear();
            response_richTextBox.Text = "";
        }
        private void editF_fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            //e.Cancel = true;
            form1.Visible = true;
            form1.Show();
            Loading.updateControlForm();
            e.Cancel = false;
        }
    }
}
