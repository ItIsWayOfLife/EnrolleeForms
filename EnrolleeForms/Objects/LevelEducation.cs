using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // Уровень образования
    class LevelEducation
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
        public LevelEducation(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        // метод счит все инф из табл LevelEd бд и возвр список объектов Speciality
        public static List<LevelEducation> ReadToEndDataInList( )
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;

            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM LevelEducation", connectionString);
            adapter.Fill(dst, "LevelEducation");

            List<LevelEducation> levelEducations = new List<LevelEducation>();

            foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;

                    levelEducations.Add(new LevelEducation((int)cells[0], (string)cells[1]));
                }
            }
            return levelEducations;
        }
    }
}
