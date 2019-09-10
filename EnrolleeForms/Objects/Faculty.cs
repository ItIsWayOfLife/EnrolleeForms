using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // факультет
    class Faculty:StructuralUnit
    {
        private Faculty(int id, string fullName, string shortName) : base(id, fullName, shortName)
        {       }
        
        // метод счит все инф из табл Specialty бд и возвр список объектов Speciality
        public static List<Faculty> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM Faculty", connectionString);
            adapter.Fill(dst, "Faculty");
            List<Faculty> faculties = new List<Faculty>();

            foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    faculties.Add(new Faculty((int)cells[0], (string)cells[1], (string)cells[2]));
                }
            }
            return faculties;
        }

        // информация о факультете
        public override string Info()
        {
            List<Specialty> specialties = Specialty.ReadToEndListSpec();
            int countSpec = 0;
            foreach (Specialty s in specialties)
            {
                if (s.Faculty.Id == this.Id)
                    countSpec++;
            }
            return $"На факультете ({this.ShortName}) находятся ({countSpec}) специальности(ей)";
        }

    }
}
