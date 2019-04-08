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

namespace MultiQuestions
{
    public partial class DeleteGroupForm : Form
    {
        public DeleteGroupForm()
        {
            InitializeComponent();
        }

        private void deleteGroup_button_Click(object sender, EventArgs e)
        {
            string path = string.Format("./Questions/{0}.xml", DeleteGroup_textBox.Text);
            if (File.Exists(path))
            {
                File.Delete(path);
                MessageBox.Show("Файл удален!");
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Ошибка данной группы (файла) не существует /n" +
                    "Введите коректное название группы. /n" +
                    "Так же вероятно вы ввели название группы с расширение файла ( .xml ), в таком случае пожалуйста введите только название группы (файла) без расширения.");
            }
        }
    }
}
