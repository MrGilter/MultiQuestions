using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Data.SQLite;


namespace MultiQuestions
{
    static class Loading
    {
        static private List<Group> groups;
        static private object lockr = new object();
        static private object load_obj = new object();
        static public List<Group> Groups
        {
            get
            {
                lock (lockr)
                {
                    return groups;
                }
            }
            set
            {
                lock (lockr)
                {
                    groups = value;
                    
                }
                
            }
        }
        public delegate void UpdateControl(); // сигнатура делегатта для хранения визова для 
        public static UpdateControl updateControlForm;
        static public Question SelectionQuestion { get; set; }

        /// <summary>
        /// Загрузка груп вопросов, каждая группа оддельная БД SQLite
        /// </summary>
        /// <param name="only">Данный параметр указывает прочитать полностью папку или только выбранную БД</param>
        /// <param name="nameGroup">Название группы которую нужно подгрузить</param>
        static public async void LoadGroup(bool only = false, string nameGroup = null)
        {

            DirectoryInfo directory = new DirectoryInfo(@".\Questions\");
            FileInfo[] info;
            object locker = new object();

            if (only)
            {
                if (nameGroup != null)
                {
                    info = directory.GetFiles($"{nameGroup}.db");
                }
                else
                    throw new Exception("Ошибка - параметры метода LoadGroup должны принимать либо оба параметра либо ниодного");
                
            }
            else
            {
                Groups = new List<Group>();
                info = directory.GetFiles("*.db");
            }

            foreach (var file in info)
            {
                //мб вернуть асинхронность и сделать обновление формы через делегат или событие
                Task taskLoad = new Task(() =>
                {
                    Group g = new Group() { NameGroup = file.Name, QuestList = new List<Question>() };
                    lock (locker)
                    {
                        using (SQLiteConnection con = new SQLiteConnection($@"Data Source=.\Questions\{file.Name}; Version = 3;"))
                        {
                            con.Open();
                            SQLiteCommand command = new SQLiteCommand("select * from Questions", con);

                            SQLiteDataReader reader = command.ExecuteReader();

                            // Чтение Question таблицы и заполнение Groups.Questions
                            if (reader.HasRows)
                            {


                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    string name_question = reader.GetString(1);
                                    string text = reader.GetString(2);
                                    TypeQuestion type;
                                    string buf = reader.GetString(3);

                                    if (buf == "CheckBox")
                                        type = TypeQuestion.CheckBox;
                                    else
                                        type = TypeQuestion.TextBox;

                                    string comment = reader.GetString(4);

                                    g.QuestList.Add(new Question()
                                    {
                                        ID = id,
                                        NameQuestions = name_question,
                                        Text = text,
                                        typeQuestion = type,
                                        Comment = comment,
                                        Answers = new List<Answer>()
                                    });

                                }
                            }
                            reader.Close();

                            // Перебор ИД Question и отправка запросов на поис связаных Answer
                            foreach (var q in g.QuestList)
                            {
                                command = new SQLiteCommand("select ID, Text, Trueness " +
                                    "from Answers " +
                                    "where id_question = @id", con);
                                SQLiteParameter id_parameter = new SQLiteParameter("@id", q.ID);
                                command.Parameters.Add(id_parameter);

                                reader = command.ExecuteReader();

                                if (reader.HasRows)
                                {
                                    q.Answers = new List<Answer>();
                                    while (reader.Read())
                                    {
                                        int id_answer = reader.GetInt32(0);
                                        string text = reader.GetString(1);
                                        bool trueness = reader.GetBoolean(2);

                                        q.Answers.Add(new Answer()
                                        {
                                            ID = id_answer,
                                            Text = text,
                                            Trueness = trueness
                                        });
                                    }
                                }
                                reader.Close();
                            }
                            con.Close();
                        }

                    }
                    Loading.Groups.Add(g);
                });
                taskLoad.Start();
                await taskLoad;
               
            }
            updateControlForm();

        }
        
        /// <summary>
        /// Создание БД в каталоге ".\Questions\"
        /// </summary>
        /// <param name="nameDB">Имя БД</param>
        static public async void CreateDB(string nameDB)
        {
            Task taskCreateDb = new Task(() =>
            {
                SQLiteConnection.CreateFile($@".\Questions\{nameDB}.db");
                using (SQLiteConnection connection = new SQLiteConnection($@"Data Source=.\Questions\{nameDB}.db; Version = 3;"))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("CREATE TABLE Questions (" +
                        "ID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL," +
                        "NameQuestions TEXT    NOT NULL," +
                        "Text          TEXT    NOT NULL," +
                        "TypeQuestions TEXT    NOT NULL," +
                        "Comment       TEXT); ", connection);
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE Answers (" +
                        "ID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL," +
                        "Text        TEXT NOT NULL," +
                        "Trueness BOOLEAN NOT NULL," +
                        "id_question INTEGER REFERENCES Questions(ID));";
                    command.ExecuteNonQuery();
                }
                
            });
            taskCreateDb.Start();
            await taskCreateDb;
            if(Loading.Groups.Exists(e => e.NameGroup==nameDB+".db")==false)
                Loading.LoadGroup(true, nameDB+".db");
            //LoadGroup();
        }//29.06

        /// <summary>
        /// Запись нового вопроса в БД
        /// </summary>
        /// <param name="question">Вопрос для создания в БД</param>
        /// <param name="group_name">Имя группы (бд) которой пренадлежит вопрос</param>
        static public async void CreateQuestion(Question question, string group_name)
        {
            Task taskCreateQuestion = new Task(() =>
            {
                using (SQLiteConnection connection = new SQLiteConnection($@"Data Source=.\Questions\{group_name}; Version = 3;"))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("insert into Questions(NameQuestions, Text, TypeQuestions, Comment) " +
                        "values (@name, @text, @type, @comment);", connection);
                    command.Parameters.AddWithValue("@name", question.NameQuestions);
                    command.Parameters.AddWithValue("@text", question.Text);
                    command.Parameters.AddWithValue("@type", question.typeQuestion.ToString());
                    command.Parameters.AddWithValue("@comment", question.Comment);
                    command.ExecuteNonQuery();
                    command.CommandText = "select ID from Questions where NameQuestions=@name_q;";
                    command.Parameters.AddWithValue("@name_q", question.NameQuestions);
                    question.ID = Convert.ToInt32(command.ExecuteScalar());
                    

                    foreach (Answer a in question.Answers)
                    {

                        //command.Parameters.AddWithValue("@name_q", question.NameQuestions);
                        a.ID = CreateAnswer(a, question.ID, group_name).Result;
                        //System.Windows.Forms.MessageBox.Show(Convert.ToString(a.ID));
                    }

                    foreach (Group g in Loading.Groups)
                    {
                        if (g.NameGroup == group_name)
                            g.QuestList.Add((Question)question.Clone());
                    }
                }
            });
            taskCreateQuestion.Start();
            await taskCreateQuestion;
            Loading.updateControlForm();
        } //29.06

        /// <summary>
        /// Создание ответа в БД
        /// </summary>
        /// <param name="answer">Ответ который неоходимо создат в БД</param>
        /// <param name="id_question">ИД вопроса к которому привязан ответ</param>
        /// <param name="group_name">Имя группы (название БД) в который адресуем запрос</param>
        /// <returns>Значение ИД созданного ответа в БД</returns>
        static public async Task<int> CreateAnswer(Answer answer, int? id_question, string group_name)
        {
            Task<int> taskCreateAnswer = new Task<int>(() =>
            {
                using (SQLiteConnection connection = new SQLiteConnection($@"Data Source=.\Questions\{group_name}; Version = 3;"))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("insert into Answers(Text, Trueness, id_question) " +
                        "values (@text2, @trueness, @id_quest);", connection);
                    
                    command.Parameters.AddWithValue("@text2", answer.Text);
                    command.Parameters.AddWithValue("@trueness", answer.Trueness);
                    answer.id_Question = id_question;
                    command.Parameters.AddWithValue("@id_quest", answer.id_Question);
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid();";
                    answer.ID= Convert.ToInt32(command.ExecuteScalar());

                    return Convert.ToInt32(answer.ID);
                }
            });
            taskCreateAnswer.Start();
            await taskCreateAnswer;

            return taskCreateAnswer.Result;
            
        }//29.06

        /// <summary>
        /// Метод отправляет запрос в БД на обновлние строки вопроса
        /// </summary>
        /// <param name="question">Вопрос который необходимо обновить в БД</param>
        /// <param name="group_name">Имя группы (бд) к которой пренадлежит вопрос</param>
        static public async void EditQuestion(Question question, string group_name)
        {
            Task taskEditQuestion = new Task(() =>
            {
                using (SQLiteConnection connection = new SQLiteConnection($@"Data Source=.\Questions\{group_name}; Version = 3;"))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("update Questions set NameQuestions=@name, Text=@text, TypeQuestions=@type, Comment=@comment " +
                        "where ID=@id_q", connection);
                    command.Parameters.AddWithValue("@name", question.NameQuestions);
                    command.Parameters.AddWithValue("@text", question.Text);
                    command.Parameters.AddWithValue("@type", question.typeQuestion.ToString());
                    command.Parameters.AddWithValue("@comment", question.Comment);
                    command.Parameters.AddWithValue("@id_q", question.ID);
                    command.ExecuteNonQuery();
                    
                    foreach (Answer a in question.Answers)
                    {
                        if (a.ID == null)
                            a.ID = CreateAnswer(a, question.ID, group_name).Result;

                        EditAnswer(a, group_name);
                    }
                }
            });
            taskEditQuestion.Start();
            await taskEditQuestion;
        }//29.06

        /// <summary>
        /// Метод отправляет запрос в БД на обновлние строки ответа
        /// </summary>
        /// <param name="answer">Ответ который необходимо обновить в БД</param>
        /// <param name="group_name">Имя группы (бд) к которой пренадлежит ответ</param>
        static public async void EditAnswer(Answer answer, string group_name)
        {
            Task taskEditAnswer = new Task(() =>
            {
                using (SQLiteConnection connection = new SQLiteConnection($@"Data Source=.\Questions\{group_name}; Version = 3;"))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("update Answers set Text=@text2, Trueness=@trueness " +
                        "where ID=@id_a;", connection);

                    command.Parameters.AddWithValue("@text2", answer.Text);
                    command.Parameters.AddWithValue("@trueness", answer.Trueness);
                    command.Parameters.AddWithValue("@id_a", answer.ID);
                    command.ExecuteNonQuery();

                }
            });
            taskEditAnswer.Start();
            await taskEditAnswer;
        }//29.06

        /// <summary>
        /// Удаление файла базы данных (группы вопросов)
        /// </summary>
        /// <param name="group_name">Имя группы (бд) к которою необходимо удалить</param>
        static public void DeleteDB(string group_name)
        {
            
            try
            {
                if (File.Exists($@".\Questions\{group_name}"))
                {
                    File.Delete($@".\Questions\{group_name}");
                    foreach(var g in Groups)
                    {
                        if (g.NameGroup == group_name)
                            Groups.Remove(g);
                    }
                    updateControlForm();
                    System.Windows.Forms.MessageBox.Show("Complited");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка данной группы (файла) не существует /n" +
                        "Введите коректное название группы. /n" +
                        "Так же вероятно вы ввели название группы с расширение файла ( .xml ), в таком случае пожалуйста введите только название группы (файла) без расширения.");
                }

            }
            
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Фиг пойми что случилось при удалении базы данных");
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            

        }

        /// <summary>
        /// Удаление выбраного вопроса с БД и памяти
        /// </summary>
        /// <param name="group_name">Имя группы к которой отноиться воппрос</param>
        /// <param name="selectQuestion">Вопрос который необходимо удалить</param>
        static public async void DeleteSelectedQuestion(string group_name, Question selectQuestion)
        {
            if (Loading.SelectionQuestion.ID == null)
            {
                throw new Exception("Вызвано исключение в DeleteSelectedQuestion, Loading.SelectionQuestion.ID = null");
            }
            else
            {
                Task taskDeleteQuestion = new Task(() =>
                {
                    using (SQLiteConnection connection = new SQLiteConnection($@"Data Source=.\Questions\{group_name}; Version = 3;"))
                    {
                        connection.Open();
                        SQLiteCommand command = new SQLiteCommand("delete from Questions where ID=@id_q", connection);
                        command.Parameters.AddWithValue("@id_q", selectQuestion.ID);
                        command.ExecuteNonQuery();

                        foreach (Answer a in selectQuestion.Answers)
                        {
                            DeleteAnswer(a.ID, group_name);
                        }
                    }

                    foreach (Group g in Loading.Groups)
                    {
                        if (g.NameGroup == group_name)
                        {
                            g.QuestList.Remove(selectQuestion);
                        }
                    }
                });
                taskDeleteQuestion.Start();
                await taskDeleteQuestion;
                Loading.updateControlForm();
            }
            
        }//04.07

        /// <summary>
        /// Удаление ответа с вопроса в БД и памяти
        /// </summary>
        /// <param name="id_answer">ИД ответа который необходимо удалить</param>
        /// <param name="group_name">Имя группы к которой относиться ответ</param>
        static public async void DeleteAnswer(int? id_answer, string group_name)
        {
            if (id_answer == null)
            {
                throw new Exception("Вызвано исключение в DeleteAnswer, id_answer = null");
            }
            else
            {
                Task taskDeleteAnswer = new Task(() =>
                {
                    using (SQLiteConnection connection = new SQLiteConnection($@"Data Source=.\Questions\{group_name}; Version = 3;"))
                    {
                        connection.Open();
                        SQLiteCommand command = new SQLiteCommand("delete from Answers where ID=@id_a", connection);
                        command.Parameters.AddWithValue("@id_a", id_answer);
                        command.ExecuteNonQuery();
                    }

                   
                });
                taskDeleteAnswer.Start();
                await taskDeleteAnswer;
            }
                
        }//04.07
    }
}
