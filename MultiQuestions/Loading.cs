using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;


namespace MultiQuestions
{
    static class Loading
    {
        static public List<Group> Groups { get; set; }

        static public Question SelectionQuestion { get; set; }

        /// <summary>
        /// Десирализация вопосов с .xml файлов
        /// </summary>
        static public void LoadGroup()
        {
            Groups = new List<Group>();

            DirectoryInfo directory = new DirectoryInfo("./Questions/");
            FileInfo[] info = directory.GetFiles();

            XmlSerializer serializer = new XmlSerializer(typeof(Group));

            foreach (FileInfo file in info)
            {
                // десериализация
                using (FileStream fs = new FileStream(string.Format("./Questions/{0}", file.Name), FileMode.OpenOrCreate))
                {
                    Group newGroup = new Group();
                    newGroup = (Group)serializer.Deserialize(fs); // траблы
                    Groups.Add(newGroup);
                    fs.Close();
                }
            }
            

        }
        
        /// <summary>
        /// Сериализация вопросов в .xml
        /// </summary>
        static public void SaveGroup()
        {
            foreach (Group g in Groups)
            {
                // передаем в конструктор тип класса
                XmlSerializer serializer = new XmlSerializer(g.GetType());
                // получаем поток, куда будем записывать сериализованный объект
                string path = string.Format("./Questions/{0}", g.NameGroup);
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, g);
                }
            }
        }
    }
}
