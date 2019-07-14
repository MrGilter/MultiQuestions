using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MultiQuestions
{
    public partial class AddGroup_Form2 : Form
    {
        private Form1 Form1;
        public AddGroup_Form2()
        {
            InitializeComponent();
        }
        public void TargetForm1(Form1 f)
        {
            Form1 = f;
        }

        private void addGroup_button_Click(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (char invalid_ch in Path.GetInvalidFileNameChars())
            {
                for(int i = 0; i<addGroup_textBox.Text.Length; i++)
                {
                    if (addGroup_textBox.Text[i] == invalid_ch || addGroup_textBox.Text == "")
                    {
                        flag = false;
                        MessageBox.Show("Имена элементов и файлов не могут: \n" +
                            "- содержать любой из следующих символов: / ? : & " + (char)92 + " * " + (char)34 + "< > | # % \n" +
                            "- содержать управляющие символы Юникода \n " +
                            "- являться " + (char)34 + " . " + (char)34 + " или" + (char)34 + " , " + (char)34 + "\n" +
                            "- быть пустыми \n" +
                            "\n" +
                            "Введите допустимое имя!", "ERROR");
                    }
                }
            }

            //  проверка на существующий файл
            if (File.Exists($@".\Questions\{addGroup_textBox.Text}.db"))
            {
                MessageBox.Show("Группа тестов с таким названием уже существует! Создание группы с текущим именем невозможно!");
                flag = false;
            }
            

            if (flag)
            {

                Loading.CreateDB(addGroup_textBox.Text);

                this.Visible = false;
                this.Close();
                

                EditForm editForm = new EditForm();
                editForm.CloseForm1(Form1);
                
                editForm.ShowDialog();
                
            }
            else
            {
                addGroup_textBox.Text = "";
            }

        }
    }
}

