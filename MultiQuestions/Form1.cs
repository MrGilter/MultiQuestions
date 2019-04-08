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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Loading.LoadGroup();
            LoadItemsStripMenu();
            DefStat();
        }

        /// <summary>
        /// Обнуление статистики
        /// </summary>
        private void DefStat()
        {
            Statistics.thisQuestionStatus = "None";
            Statistics.answeredQuestion = 0;
            Statistics.missedQuestion = 0;
            Statistics.correctAnswers = 0;
            Statistics.correctAnswersPercent = 0;
        }

        /// <summary>
        /// Добавление в question menu групп и подписываеи элементы на собитие клика
        /// </summary>
        private void LoadItemsStripMenu()
        {
            int i = -1;
            foreach(Group g in Loading.Groups)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(g.NameGroup);
                questionToolStripMenuItem.DropDownItems.Add(g.NameGroup);
                questionToolStripMenuItem.DropDownItems[++i].Click += new EventHandler(this.checkedItemQuestionMenu_Click);
               
            }
            
        }

        /// <summary>
        /// Обработка события нажатия елемента в question menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedItemQuestionMenu_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            if(item.Checked)
                item.Checked = false;
            else
                item.Checked = true;

        }

        /// <summary>
        /// Обработка события нажатия на Setting -> Add new group of questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGroup_Form2 form2 = new AddGroup_Form2();
            form2.ShowDialog();
        }

        /// <summary>
        /// Обработка события нажатия на Setting -> Edit questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editQuestionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm();
            editForm.ShowDialog();
        }

        /// <summary>
        /// Обработка события нажатия на Setting -> Delete group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteGroupForm form3 = new DeleteGroupForm();
            form3.Show();
            
        }

        /// <summary>
        /// Обработка события нажатия на кнопку Next
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_button_Click(object sender, EventArgs e)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            List<int> list = new List<int>();
            for(int i = 0; i<questionToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (((ToolStripMenuItem)questionToolStripMenuItem.DropDownItems[i]).Checked)
                {
                    list.Add(i);
                }
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Не выбранна не одна группа вопросов");
            }
            else
            {
                
                int r = random.Next(0, list.Count);
                PrintQuestion(Loading.Groups[list[r]]);
                
            }
        }
        /// <summary>
        /// Дополнительный метод обработки события кнопки Next, данный метод вызывает дополнительные методы Print и PrintStatistic.
        /// А также выполняет изменение статистики
        /// </summary>
        /// <param name="g">Группа вопросов с которой будет выбираться вопрос для вывода пользователю</param>
        private void PrintQuestion(Group g)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                // если строка пустая то в статистике не учитивать первую пустую ворму (нулевой вопрос), иначе - считать что вопрос активный
                Print(g);
                PrintStatistic();
            }
            else
            {
                switch (Statistics.thisQuestionStatus)
                {
                    case "None":
                        Statistics.missedQuestion++;
                        Print(g);
                        PrintStatistic();
                        break;

                    case "True":
                        Statistics.thisQuestionStatus = "None";
                        Statistics.answeredQuestion++;
                        Statistics.correctAnswers++;
                        Statistics.correctAnswersPercent = ((float)Statistics.correctAnswers / (float)Statistics.answeredQuestion)*100;
                        Print(g);
                        PrintStatistic();
                        break;

                    case "False":
                        Statistics.thisQuestionStatus = "None";
                        Statistics.answeredQuestion++;
                        Statistics.correctAnswersPercent = ((float)Statistics.correctAnswers / (float)Statistics.answeredQuestion)*100;
                        Print(g);
                        PrintStatistic();
                        break;
                }
            }
           
                
        }
        /// <summary>
        /// Метод выбора случайного вопроса из группы, а также вывода в форму елементов обьекта вопроса 
        /// </summary>
        /// <param name="g">Группа вопросов с которой выбираеться вопрос для вывода пользователю</param>
        private void Print(Group g)
        {
            groupBox1.Controls.Clear();

            Random random = new Random((int)DateTime.Now.Ticks);
            int r = random.Next(0, g.QuestList.Count);
            richTextBox1.Text = g.QuestList[r].Text;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;

            Loading.SelectionQuestion = g.QuestList[r];

            if(g.QuestList[r].typeQuestion == TypeQuestion.CheckBox)
            {
                groupBox1.Controls.Add(new CheckedListBox { Name = "CheckList", Dock = DockStyle.Fill });
                CheckedListBox c = (CheckedListBox)groupBox1.Controls[0];
                c.ScrollAlwaysVisible = true;
                c.CheckOnClick = true;
                

                foreach (Answer a in g.QuestList[r].Answers)
                {
                    c.Items.Add(a.Text);
                }
            }
            else if(g.QuestList[r].typeQuestion == TypeQuestion.TextBox)
            {
                groupBox1.Controls.Add(new RichTextBox { Name = "answer_RichTextBox", Dock = DockStyle.Fill });
            }
            
            
        }
        /// <summary>
        /// Вывод статистики за сессию в groupBox2
        /// </summary>
        private void PrintStatistic()
        {
            groupBox2.Controls.Clear();
            
            groupBox2.Controls.Add(new Label { Text = "thisQuestionStatus:", AutoSize = true, Location = new Point(5, 15) });
            groupBox2.Controls.Add(new Label { Text = Statistics.thisQuestionStatus, AutoSize = true, Location = new Point(5, 30) });
            groupBox2.Controls.Add(new Label { Text = "answeredQuestion:", AutoSize = true, Location = new Point(5, 45) });
            groupBox2.Controls.Add(new Label { Text = Convert.ToString(Statistics.answeredQuestion), AutoSize = true, Location = new Point(5, 60) });
            groupBox2.Controls.Add(new Label { Text = "correctAnswers:", AutoSize = true, Location = new Point(5, 75) });
            groupBox2.Controls.Add(new Label { Text = Convert.ToString(Statistics.correctAnswers), AutoSize = true, Location = new Point(5, 90) });
            groupBox2.Controls.Add(new Label { Text = "correctAnswersPercent:", AutoSize = true, Location = new Point(5, 105) });
            groupBox2.Controls.Add(new Label { Text = Convert.ToString(Statistics.correctAnswersPercent), AutoSize = true, Location = new Point(5, 120) });
            groupBox2.Controls.Add(new Label { Text = "missedQuestion:", AutoSize = true, Location = new Point(5, 135) });
            groupBox2.Controls.Add(new Label { Text = Convert.ToString(Statistics.missedQuestion), AutoSize = true, Location = new Point(5, 150) });
        }

        /// <summary>
        /// Обработка события нажатия на кнопку Reply, которое проверяет правильность ответа пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reply_button_Click(object sender, EventArgs e)
        {
            if (Statistics.thisQuestionStatus != "None" || string.IsNullOrEmpty(richTextBox1.Text))
            {
                return;
            }

            if(Loading.SelectionQuestion.typeQuestion == TypeQuestion.CheckBox)
            {
                
                CheckedListBox c = (CheckedListBox)groupBox1.Controls[0];
                List<Answer> answers = new List<Answer>();

                for (int i = 0; i < c.Items.Count; i++)
                {
                    bool check = false;
                    if (c.CheckedIndices.Contains(i))
                        check = true;
                    answers.Add(new Answer { Text = Convert.ToString(c.Items[i]), Trueness = check });
                }

                foreach(Answer a in Loading.SelectionQuestion.Answers)
                {
                    bool flag = false;
                    foreach( Answer copy_listBox_a in answers)
                    {
                        if(a.Text == copy_listBox_a.Text && a.Trueness == copy_listBox_a.Trueness)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if(flag == false)
                    {
                        Statistics.thisQuestionStatus = "False";
                        PrintStatistic();
                        return;
                    }
                }
                Statistics.thisQuestionStatus = "True";
                PrintStatistic();
                return;
            }
            else if(Loading.SelectionQuestion.typeQuestion == TypeQuestion.TextBox)
            {
                foreach(Answer a in Loading.SelectionQuestion.Answers)
                {
                    if(a.Trueness && a.Text == ((RichTextBox)groupBox1.Controls[0]).Text)
                    {
                        Statistics.thisQuestionStatus = "True";
                        PrintStatistic();
                        return;
                    }
                    else
                    {
                        Statistics.thisQuestionStatus = "False";
                        PrintStatistic();
                    }
                }
            }
        }

        /// <summary>
        /// Вызов ответа коментария по вопросу 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comment_button_Click(object sender, EventArgs e)
        {
            if(Statistics.thisQuestionStatus=="True"|| Statistics.thisQuestionStatus == "False")
            {
                MessageBox.Show(Loading.SelectionQuestion.Comment);
            }
        }
    }
}
