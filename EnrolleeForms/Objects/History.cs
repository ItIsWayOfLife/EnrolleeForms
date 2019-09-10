using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EnrolleeForms
{
    // история табл абитур
    class History
    {
        // id операции
        int id;

        public int Id
        {
            get
            {
                return id;
            }
        }

        // id абитуриента
        int idEnrollee;

        public int IdEnrollee
        {
            get
            {
                return idEnrollee;
            }
        }

        // операция

        string operation;

        public string Operation
        {
            get
            {
                return operation;

            }
        }
        // дата и время операции

        DateTime createAt;

        public DateTime CreateAt
        {
            get
            {
                return createAt;
            }
        }
       
        // конструктор
        public History(int id, int idEnrollee, string operation, DateTime createAt)
        {
            this.id = id;
            this.idEnrollee = idEnrollee;
            this.operation = operation;
            this.createAt = createAt;
        }

        // метод счит все инф из табл 
        public static List<History> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;


            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM History", connectionString);
            adapter.Fill(dst, "History");

            List<History> histories = new List<History>();

            foreach (DataTable dt in dst.Tables)
            {


                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;


                    histories.Add(new History((int)cells[0], (int)cells[1], (string)cells[2], (DateTime)cells[3]));
                }
            }


            return histories;
        }

        // метод поиск по id
        public static List<History> SearchById(string st)
        {
            List<History> histories = History.ReadToEndDataInList();
            List<History> newHis = new List<History>();

            try
            {
                int id_ = Convert.ToInt32(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Укажите id целым числом");
                return histories;
            }

            foreach (History h in histories)
            {
                if (h.Id == Convert.ToInt32(st))
                    newHis.Add(h);
            }

            return newHis;

        }
        
        // метод поиск по id абит
        public static List<History> SearchByIdEnrolle(string st)
        {
            List<History> histories = History.ReadToEndDataInList();
            List<History> newHis = new List<History>();

            try
            {
                int id_ = Convert.ToInt32(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Укажите id целым числом");
                return histories;
            }

            foreach (History h in histories)
            {
                if (h.IdEnrollee == Convert.ToInt32(st))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по опер
        public static List<History> SearchByOperation(string st)
        {
            List<History> histories = History.ReadToEndDataInList();
            List<History> newHis = new List<History>();

        

            foreach (History h in histories)
            {
                if (h.Operation.ToLower().Contains(st.ToLower()) )
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по дате
        public static List<History> SearchByCreateAtLow(string st)
        {
            List<History> histories = History.ReadToEndDataInList();
            List<History> newHis = new List<History>();

            try
            {
                DateTime id_ = Convert.ToDateTime (st);

            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return histories;
            }

            foreach (History h in histories)
            {
                if (h.CreateAt <= Convert.ToDateTime(st))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск дате 
        public static List<History> SearchByCreateAtTop(string st)
        {
            List<History> histories = History.ReadToEndDataInList();
            List<History> newHis = new List<History>();

            try
            {
                DateTime id_ = Convert.ToDateTime(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return histories;
            }

            foreach (History h in histories)
            {
                if (h.CreateAt >= Convert.ToDateTime(st))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по дате
        public static List<History> SearchByCreateAt(string st)
        {
            List<History> histories = History.ReadToEndDataInList();
            List<History> newHis = new List<History>();

            try
            {
                DateTime id_ = Convert.ToDateTime(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return histories;
            }

            foreach (History h in histories)
            {
                if (h.CreateAt == Convert.ToDateTime(st))
                    newHis.Add(h);
            }

            return newHis;

        }
    }
}
