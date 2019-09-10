using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // документы
    class Document
    {
        //id 
        protected int id;

        public int Id
        {
            get
            {
                return id;
            }
        }

        // id абитур    
        protected int idEnrollee;

        public int IdEnrollee
        {
            get
            {
                return idEnrollee;
            }
        }

        // название
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        // номер
        protected string number;

        public string Number
        {
            get
            {
                return number;
            }
        }

        // описание
        protected string description;

        public string Description
        {
            get
            {
                return description;
            }
        }

        // конструктор для счит данных
        public Document(int id, int idEnrollee, string name, string number, string description)
        {
            this.id = id;
            this.idEnrollee = idEnrollee;
            this.name = name;
            this.number = number;
            this.description = description;
        }

        // конструктор для созд и добавл новых данных
        public Document(int idEnrollee, string name, string number, string description)
        {
            this.idEnrollee = idEnrollee;
            this.name = name;
            this.number = number;
            this.description = description;
        }

        // конструктор для наследника
        protected Document(int id, int idEnrollee, string number, string description)
        {
            this.id = id;
            this.idEnrollee = idEnrollee;
            this.number = number;
            this.description = description;
        }

        // конструктор для наследника
        protected Document( int idEnrollee, string number, string description)
        {
            this.idEnrollee = idEnrollee;
            this.number = number;
            this.description = description;
        }

        // метод счит инф из бд
        public static List<Document> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;

            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM Documents", connectionString);
            adapter.Fill(dst, "Documents");

            List<Document> documents = new List<Document>();

            foreach (DataTable dt in dst.Tables)
            {

                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;

                    string s = null;
                    string s1 = null;
                    string s2 = null;

                    if (cells[2] != DBNull.Value)
                        s = (string)cells[2];
                    if (cells[3] != DBNull.Value)
                        s1 = (string)cells[3];
                    if (cells[4] != DBNull.Value)
                        s2 = (string)cells[4];


                    documents.Add(new Document((int)cells[0], (int)cells[1], s, s1, s2));
                }
            }


            return documents;
        }

        // обновление данных
        public virtual void Update()
        {
            // подключение
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(Connection.ConnectionString);
                sqlConnection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка подключения!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SqlCommand updataDocCommand = new SqlCommand("UPDATE [Documents] SET [NameDocument]=@NameDocument," +
                    " [NumberDocument]=@NumberDocument, " +
                    " [Description]=@Description" +
                    " WHERE [IdDocument]=@IdDocument", sqlConnection);   // Запрос на изменение по Id
            updataDocCommand.Parameters.AddWithValue("IdDocument", Id);

            if (Name != null)
                updataDocCommand.Parameters.AddWithValue("NameDocument", Name);
            else
                updataDocCommand.Parameters.AddWithValue("NameDocument", DBNull.Value);

            if (Number != null)
                updataDocCommand.Parameters.AddWithValue("NumberDocument", Number);
            else
                updataDocCommand.Parameters.AddWithValue("NumberDocument", DBNull.Value);

            if (Description != null)
                updataDocCommand.Parameters.AddWithValue("Description", Description);
            else
                updataDocCommand.Parameters.AddWithValue("Description", DBNull.Value);

            try
            {
                // обновление
                updataDocCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // добавление нов док
        public virtual void Add()
        {
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM Documents", Connection.ConnectionString);
            adapter.Fill(dst, "Documents");
            // Создаем пустую рабочую строку
            // строка добавления
            DataRow row = (dst.Tables["Documents"]).NewRow();
            row[1] = IdEnrollee;
            row[2] = Name;
            row[3] = Number;
            row[4] = Description;
            // Добавляем в таблицу рабочую строку
            dst.Tables["Documents"].Rows.Add(row);

            new SqlCommandBuilder(adapter);

            // Внести все изменения в базу !!!
            adapter.Update(dst, "Documents");
        }

        // удаление 
        public virtual void Delete()
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(Connection.ConnectionString);
                sqlConnection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка подключения!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SqlCommand deleteDocCommand = null;

            deleteDocCommand = new SqlCommand("DELETE FROM [Documents] WHERE [IdDocument]=@IdDocument", sqlConnection);
            deleteDocCommand.Parameters.AddWithValue("IdDocument", Id);
            deleteDocCommand.ExecuteNonQuery();
        }

        // возв id посл дока
        public static int ReturnLastId()
        {
            string connectionString = Connection.ConnectionString;
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;
            // знач абитуриента
            int id = 0;
            // соед с табл и выгрузка данных
            adapter = new SqlDataAdapter("SELECT * FROM Documents", connectionString);
            adapter.Fill(dst, "Documents");
            // поиск посл добавленного абит
            foreach (DataTable dt in dst.Tables)
            {
                DataRow dataRow = dt.Rows[dt.Rows.Count - 1];
                var cells = dataRow.ItemArray;

                id = (int)cells[0];
            }

            return id;
        }

        // возвращает список документов по idEnr
        public static List<Document> RenListByInEnrollee(int idEnrolle)
        {
            List<Document> allDoc = ReadToEndDataInList();
            List<Document> retDoc =new List<Document>();
            foreach (Document d in allDoc)
            {
                if (idEnrolle==d.IdEnrollee)
                {
                    retDoc.Add(d);
                }
            }
            return retDoc;
        }

        // поиск по id
        public static List<Document> SearchById(List<Document> documents, string s)
        {
            List<Document> documentsNew = new List<Document>();
            try
            {
                int id_ = Convert.ToInt32(s);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Укажите id целым числом");
                return documents;
            }

            foreach (Document d in documents)
            {
                if (d.Id == Convert.ToInt32(s))
                    documentsNew.Add(d);
            }
            return documentsNew;
        }

        // поиск по назв
        public static List<Document> SearchByName(List<Document> documents, string s)
        {
            List<Document> documentsNew = new List<Document>();
            foreach (Document d in documents)
            {
                if (d.Name!=null)
                {
                    if (d.Name.ToLower().Contains(s.ToLower()))
                        documentsNew.Add(d);
                }
            }
            return documentsNew;
        }

        // поиск по номеру
        public static List<Document> SearchByNumber(List<Document> documents, string s)
        {
            List<Document> documentsNew = new List<Document>();
            foreach (Document d in documents)
            {
                if (d.Number!=null)
                if (d.Number.ToLower().Contains(s.ToLower()))
                    documentsNew.Add(d);
            }

            return documentsNew;
        }

        // поиск по номеру
        public static List<Document> SearchByDescription(List<Document> documents, string s)
        {
            List<Document> documentsNew = new List<Document>();
            foreach (Document d in documents)
            {
                if (d.Description!=null)
                if (d.Description.ToLower().Contains(s.ToLower()))
                    documentsNew.Add(d);
            }
            return documentsNew;
        }
    }
}
