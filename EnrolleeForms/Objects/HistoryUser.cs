using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EnrolleeForms
{
    // история входов
    class HistoryUser
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

        // id user
        int idUser;

        public int IdUser
        {
            get
            {
                return idUser;
            }
        }

        // fio user
        private string fIOUser;

        public string FIOUser
        {
            get
            {
                return fIOUser;
            }
        }

        // дата входа
        private DateTime createAt;

        public DateTime CreateAt
        {
            get
            {
                return createAt;
            }
        }

        // дата выхода
        private DateTime? releaseDate;

        public DateTime? ReleaseDate
        {
            get
            {
                return releaseDate;
            }
        }

        // констр для созд
        public HistoryUser(int idUser, string fIOUser)
        {
            this.idUser = idUser;
            this.fIOUser = fIOUser;
            
        }
       
        // констр для счит данных
        public HistoryUser(int id, int idUser, string fIOUser, DateTime createAt, DateTime? releaseDate)
        {
            this.id = id;
            this.idUser = idUser;
            this.fIOUser = fIOUser;
            this.createAt = createAt;
            this.releaseDate = releaseDate;
        }

        // методы

        // счит данных
        public static List<HistoryUser> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter("SELECT * FROM HistoryUser", connectionString);
            adapter.Fill(dst, "HistoryUser");
            List<HistoryUser> histories = new List<HistoryUser>();
            foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;

                    DateTime? lastD = null;

                    if (cells[4] != DBNull.Value)
                        lastD = (DateTime?)cells[4];

                    histories.Add(new HistoryUser((int)cells[0], (int)cells[1], (string)cells[2], (DateTime)cells[3],  lastD ));
                }
            }
            return histories;
            
        }

        // запись входа
        public void Add()
        {           
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM HistoryUser", Connection.ConnectionString);
            adapter.Fill(dst, "HistoryUser");

            // Создаем пустую рабочую строку
            // строка добавления
            DataRow row = (dst.Tables["HistoryUser"]).NewRow();
            row[1] = idUser;
            row[2] = fIOUser;
            row[3] = DateTime.Now;
            row[4] = DBNull.Value;
            // Добавляем в таблицу рабочую строку
            dst.Tables["HistoryUser"].Rows.Add(row);
            new SqlCommandBuilder(adapter);
            // Внести все изменения в базу !!!
            adapter.Update(dst, "HistoryUser");
        }

        // запись времени выхода
        // обновление данных
        public void Update()
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
            SqlCommand updataDocCommand = new SqlCommand("UPDATE [HistoryUser] SET [ReleaseDate]=@ReleaseDate" +
                    " WHERE [IdHistoryUser]=@IdHistoryUser", sqlConnection);   // Запрос на изменение по Id
            updataDocCommand.Parameters.AddWithValue("IdHistoryUser", IdLast());
                updataDocCommand.Parameters.AddWithValue("ReleaseDate",DateTime.Now);
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

        // возв посл id
        private static int IdLast()
        {
            List<HistoryUser> historyUsers = HistoryUser.ReadToEndDataInList();

           

          

            int idLast= historyUsers[0].Id;

            foreach (HistoryUser h in historyUsers)
            {
                if (idLast < h.Id)
                    idLast = h.Id;
            }
            return idLast;

        }

        // возв объект по указ id
        public static HistoryUser ReturnRelByIdHistoryUser(int id_)
        {
            List<HistoryUser> historyUsers = HistoryUser.ReadToEndDataInList();

            HistoryUser newH = null;

            foreach (HistoryUser h in historyUsers)
            {
                if (h.id == id_)
                    return h;
            }
           return newH;
        }

        // метод поиск по id
        public static List<HistoryUser> SearchById(string st)
        {
            List<HistoryUser> histories = HistoryUser.ReadToEndDataInList();
            List<HistoryUser> newHis = new List<HistoryUser>();

            try
            {
                int id_ = Convert.ToInt32(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Укажите id целым числом");
                return histories;
            }

            foreach (HistoryUser h in histories)
            {
                if (h.Id == Convert.ToInt32(st))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по id user
        public static List<HistoryUser> SearchByIdUser(string st)
        {
            List<HistoryUser> histories = HistoryUser.ReadToEndDataInList();
            List<HistoryUser> newHis = new List<HistoryUser>();

            try
            {
                int id_ = Convert.ToInt32(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Укажите id целым числом");
                return histories;
            }

            foreach (HistoryUser h in histories)
            {
                if (h.idUser == Convert.ToInt32(st))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по lastname
        public static List<HistoryUser> SearchByFIO(string st)
        {
            List<HistoryUser> histories = HistoryUser.ReadToEndDataInList();
            List<HistoryUser> newHis = new List<HistoryUser>();



            foreach (HistoryUser h in histories)
            {
                if (h.fIOUser.ToLower().Contains(st.ToLower()))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по дате входа до
        public static List<HistoryUser> SearchByCreateAtLow(string st)
        {
            List<HistoryUser> histories = HistoryUser.ReadToEndDataInList();
            List<HistoryUser> newHis = new List<HistoryUser>();

            try
            {
                DateTime id_ = Convert.ToDateTime(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return histories;
            }

            foreach (HistoryUser h in histories)
            {
                if (h.CreateAt <= Convert.ToDateTime(st))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по дате входа посл
        public static List<HistoryUser> SearchByCreateAtTop(string st)
        {
            List<HistoryUser> histories = HistoryUser.ReadToEndDataInList();
            List<HistoryUser> newHis = new List<HistoryUser>();

            try
            {
                DateTime id_ = Convert.ToDateTime(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return histories;
            }

            foreach (HistoryUser h in histories)
            {
                if (h.CreateAt >= Convert.ToDateTime(st))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по дате входа 
        public static List<HistoryUser> SearchByCreateAt(string st)
        {
            List<HistoryUser> histories = HistoryUser.ReadToEndDataInList();
            List<HistoryUser> newHis = new List<HistoryUser>();

            try
            {
                DateTime id_ = Convert.ToDateTime(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return histories;
            }

            foreach (HistoryUser h in histories)
            {
                if (h.CreateAt == Convert.ToDateTime(st))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по дате выхода до
        public static List<HistoryUser> SearchByReleaseDateLow(string st)
        {
            List<HistoryUser> histories = HistoryUser.ReadToEndDataInList();
            List<HistoryUser> newHis = new List<HistoryUser>();

            try
            {
                DateTime id_ = Convert.ToDateTime(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return histories;
            }

            foreach (HistoryUser h in histories)
            {
                if (h.releaseDate <= Convert.ToDateTime(st))
                    newHis.Add(h);
            }

            return newHis;

        }

        // метод поиск по дате выхода после
        public static List<HistoryUser> SearchByReleaseDateTop(string st)
        {
            List<HistoryUser> histories = HistoryUser.ReadToEndDataInList();
            List<HistoryUser> newHis = new List<HistoryUser>();
            try
            {
                DateTime id_ = Convert.ToDateTime(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return histories;
            }

            foreach (HistoryUser h in histories)
            {
                if (h.releaseDate >= Convert.ToDateTime(st))
                    newHis.Add(h);
            }
            return newHis;

        }

        // метод поиск по дате выхода после
        public static List<HistoryUser> SearchByReleaseDate(string st)
        {
            List<HistoryUser> histories = HistoryUser.ReadToEndDataInList();
            List<HistoryUser> newHis = new List<HistoryUser>();
            try
            {
                DateTime id_ = Convert.ToDateTime(st);

            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return histories;
            }

            foreach (HistoryUser h in histories)
            {
                if (h.releaseDate == Convert.ToDateTime(st))
                    newHis.Add(h);
            }

            return newHis;
        }

        // кол минут в системе
        public string CountTime()
        {
            if (ReleaseDate != null)
            {
                int sec1 = ReleaseDate.Value.Second + ReleaseDate.Value.Minute * 60 + ReleaseDate.Value.Hour * 60 * 60;
                int sec2 = CreateAt.Second + CreateAt.Minute * 60 + CreateAt.Hour * 60 * 60;
                int c = sec1 - sec2;
                c = c / 60;            
                return $"Пользователь находился в системе: {c}: минут";
            }
            return null;
        }
    }
}
