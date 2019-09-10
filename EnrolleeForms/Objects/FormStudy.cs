using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // Форма обучения
    class FormStudy
    {
        // id
        int id;

        public int Id
        {
            get
            {
                return id;
            }
        }

        // название

        string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        // конструктор
        public FormStudy(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        // метод счит инф
        public static List<FormStudy> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter("SELECT * FROM FormStudy", connectionString);
            adapter.Fill(dst, "FormStudy");
            List<FormStudy> formStudies = new List<FormStudy>();
            foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    formStudies.Add(new FormStudy((int)cells[0], (string)cells[1]));
                }
            }
            return formStudies;
        }
    }
}

