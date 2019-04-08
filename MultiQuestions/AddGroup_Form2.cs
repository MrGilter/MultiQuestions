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
        public AddGroup_Form2()
        {
            InitializeComponent();
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

            // запилить проверку на существующий файл

            if (flag)
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
                xmlDoc.AppendChild(xmlDoc.CreateElement("Questions"));
                string path = string.Format("./Questions/{0}.xml", addGroup_textBox.Text);
                xmlDoc.Save(path);

                Group g = new Group();
                g.NameGroup = string.Format("{0}.xml", addGroup_textBox.Text);
                // передаем в конструктор тип класса
                XmlSerializer serializer = new XmlSerializer(g.GetType());
                // получаем поток, куда будем записывать сериализованный объект
                
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, g);
                }
                this.Visible = false;
                this.Close();

                EditForm editForm = new EditForm();
                editForm.ShowDialog();
                
            }
            else
            {
                addGroup_textBox.Text = "";
            }

        }
    }
}

