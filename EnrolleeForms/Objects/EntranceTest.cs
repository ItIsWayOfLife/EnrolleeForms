using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // вступительное испытание
    class EntranceTest
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
        public EntranceTest(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        // метод счит информацию
        public static List<EntranceTest> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter("SELECT * FROM EntranceTests", connectionString);
            adapter.Fill(dst, "EntranceTests");
            List<EntranceTest> entranceTests = new List<EntranceTest>();
           foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    entranceTests.Add(new EntranceTest((int)cells[0], (string)cells[1]));
                }
            }
           return entranceTests;
        }
    }
}
