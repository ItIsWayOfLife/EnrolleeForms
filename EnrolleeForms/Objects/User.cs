using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace EnrolleeForms
{
    // пользователь
    class User
    {
        // id
        private int id;

        public int Id
        {
            get
            {
                return id;
            }
        }

        // имя
        private string firstname;

        public string Firstname
        {
            get
            {
                return firstname;
            }
        }
        // фамилия
        private string lastname;

        public string Lastname
        {
            get
            {
                return lastname;
            }
        }

        // отчетсво
        private string patronymic;

        public string Patronymic
        {
            get
            {
                return patronymic;
            }
        }


        // логин
        private string login;

        public string Login
        {
            get
            {
                return login;
            }
        }

        // пароль
        private string password;

        // ад права
        private AdminRights adminRights;

        public AdminRights AdminRights
        {
            get
            {
                return adminRights;
            }
        }

        // история входов
        private HistoryUser historyUser;

        public HistoryUser HistoryUser
        {
            get
            {
                return historyUser;
            }
        }

        // конструктор   (Композиция)
        public User(int id, string login, string password, string lastname, string firstname, string patronymic, AdminRights adminRights)
        {
            this.id = id;
            this.lastname = lastname;
            this.firstname = firstname;
            this.patronymic = patronymic;
            this.login = login;
            this.password = password;
            this.adminRights = adminRights;
            historyUser = new HistoryUser(Id, FIO());
        }

        // вход
        public static User LoginToTheApp(string login_, string password_)
        {
            List<User> users = ReadToEndDataInList();
            User newUser = null;
            foreach (User u in users)
            {
                if (u.login == login_ && u.password == password_)
                    newUser = u;
            }
            if (newUser != null)
            {
                // запись
                newUser.historyUser.Add();
            }
            return newUser;
        }
    
        // метод счит все инф из табл Specialty бд и возвр список объектов Speciality
        private static List<User> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter("SELECT * FROM [User]", connectionString);
            adapter.Fill(dst, "[User]");
            List<User> users = new List<User>();
            foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    AdminRights ad = AdminRights.Usual;
                    if ((string)cells[6] == "+")
                        ad = AdminRights.Right;
                    users.Add(new User((int)cells[0], (string)cells[1], (string)cells[2], (string)cells[3], (string)cells[4],
                        (string)cells[5], ad));
                }
            }
            return users;
        }

        // метод возвращает фамилию и инициалы
        public string FIO()
        {
            string lastname_ = " ";
            string name_ = " ";
            string patr_ = " ";

            if (Lastname != null && Lastname.Length >= 1)
                lastname_ = Lastname;
            if (Firstname != null && Firstname.Length >= 1)
                name_ = Firstname;

            if (Patronymic != null && Patronymic.Length >= 1)
                patr_ = Patronymic;

            return lastname_ + " " + name_[0] + ". " + patr_[0] + ".";
        }    
    }
}
