using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // специальность
    class Specialty
    {
        int id;

        public int Id
        {
            get
            {
                return id;
            }
        }


        // полное название
        string fullName;

        public string FullName
        {
            get
            {
                return fullName;
            }
        }


        // сокр назв
        string shortName;

        public string ShortName
        {
            get
            {
                return shortName;
            }
        }


        // факультет
        Faculty faculty;

        public Faculty Faculty
        {
            get
            {
                return faculty;
            }
        }


        // кафедра
        Department department;

        public Department Department
        {
            get
            {
                return department;
            }
        }


        // код специальности
        string codeSpecialty;

        public string CodeSpecialty
        {
            get
            {
                return codeSpecialty;
            }
        }


        // код специализации
        string codeSpecialization;

        public string CodeSpecialization
        {
            get
            {
                return codeSpecialization;
            }
        }
        // конструктор
        private Specialty(int id, Faculty faculty, Department department, string codeSpecialty, string codeSpecialization, 
            string fullName, string shortName )
        {
 
            this.id = id;
            this.fullName = fullName;
            this.shortName = shortName;
            this.faculty = faculty;
            this.department = department;
            this.codeSpecialty = codeSpecialty;
            this.codeSpecialization = codeSpecialization;

        }


        // метод счит всю инф из табл Specialty бд, созд объекты специальности и возвращает список объектов
        public static List<Specialty> ReadToEndListSpec()
        {

            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;

            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            // кафедры
            List<Department> departments = Department.ReadToEndDataInList();
            // факультеты
            List<Faculty> faculties = Faculty.ReadToEndDataInList();

            adapter = new SqlDataAdapter("SELECT * FROM Specialty", connectionString);
            adapter.Fill(dst, "Specialty");

            List<Specialty> specialties = new List<Specialty>();

            foreach (DataTable dt in dst.Tables)
            {                              // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;

                    Faculty fac = null;
                    foreach (Faculty f in faculties)
                    {
                        if (f.Id == Convert.ToInt32(cells[1]))
                            fac = f;
                    }

                    Department dep = null;
                    foreach (Department d in departments)
                    {
                        if (d.Id == Convert.ToInt32(cells[2]))
                            dep = d;
                    }

                        specialties.Add(new Specialty((int)cells[0], fac, dep, (string)cells[3],
                            (string)cells[4], (string)cells[5], (string)cells[6]));                 
                }
            }
            return specialties;
        }

    }
}
