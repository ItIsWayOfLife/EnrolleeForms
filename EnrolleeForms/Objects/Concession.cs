using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // льгота
    class Concession
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
        private string name;
      
        public string Name
        {
            get
            {
                return name;
            }
        }
        
        // конструктор
        public Concession(int id, string name)
        {
            this.id = id;
            this.name = name;

        }
        
        // метод счит  инф из табл
        public static List<Concession> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;

            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM Concession", connectionString);
            adapter.Fill(dst, "Concession");

            List<Concession> concessions = new List<Concession>();
            foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    concessions.Add(new Concession((int)cells[0], (string)cells[1]));
                }
            }
            return concessions;
        }
    }
}
