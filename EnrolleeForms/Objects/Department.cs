using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // кафедра
    class Department:StructuralUnit
    {
        // конструктор
        private Department(int id, string fullName, string shortName) : base(id, fullName, shortName)
        {    }

        // метод счит все инф из табл Specialty бд и возвр список объектов Speciality
        public static List<Department> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM Department", connectionString);
            adapter.Fill(dst, "Department");
            List<Department> departments = new List<Department>();
            foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;                   
                    departments.Add(new Department((int)cells[0], (string)cells[1], (string)cells[2]));
                }
            }
            return departments;
        }

        // переопределение метода информация
        public override string Info()
        {
            List<Specialty> specialties = Specialty.ReadToEndListSpec();
            int countDep = 0;
            foreach (Specialty s in specialties)
            {
                if (s.Department.Id == this.Id)
                    countDep++;
            }
            return $"За кафедрой ({ShortName}) закреплены ({countDep}) специальности(ей)";
        }
    }
}
