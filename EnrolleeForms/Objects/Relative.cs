using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // родственник
    class Relative : Person
    {
        // id Абитур
        private int idEnrollee;

        public int IdEnrollee
        {
            get
            {
                return idEnrollee;
            }
        }

        // степень родства
        private string degree;

        public string Degree
        {
            get
            {
                return degree;
            }
        }

        // работа
        private Work work;

        public Work Work_
        {
            get
            {
                return work;
            }
        }

        // конструктор для счит данных
        public Relative(int id,  string firstname, string lastname, string patronymic, string sex, DateTime dateOfBirth, Passport passport,
            string address, string phoneNumber, int idEnrollee, string degree, Work work)
            :base(id, firstname, lastname, patronymic, sex, dateOfBirth, passport, address, phoneNumber)
        {
            
            this.idEnrollee = idEnrollee;
            this.degree = degree;
            this.work = work;
        }


        // конструктор для созд нов объектов и записи в бд
        public Relative( string firstname, string lastname, string patronymic, string sex, DateTime dateOfBirth, Passport passport,
            string address, string phoneNumber, int idEnrollee, string degree, Work work)
            : base( firstname, lastname, patronymic, sex, dateOfBirth, passport, address, phoneNumber)
        {

            this.degree = degree;
            this.work = work;
            this.idEnrollee = idEnrollee;
        }

        // метод счит все инф из табл Concession бд и возвр список объектов Speciality
        public static List<Relative> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;

            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM RelativeOrGuardian", connectionString);
            adapter.Fill(dst, "RelativeOrGuardian");

            List<Relative> relatives = new List<Relative>();

            foreach (DataTable dt in dst.Tables)
            {

                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;

                    
                    // знач работы
                    string w1 = null;
                    string w2 = null;

                    // созд паспорт
                    Passport p = new Passport((string)cells[8], (string)cells[9], (string)cells[10], (string)cells[11], (DateTime)cells[12], (DateTime)cells[13]);


                    if (cells[16] != DBNull.Value)
                        w1 = (string)cells[16];

                    if (cells[17] != DBNull.Value)
                        w2 = (string)cells[16];

                   Work w = new Work(w1, w2);

                    string s1, s2, s3, s4, s5, s6, s7, s8, s9;
                    s1 = s2 = s3 = s4 = s5 = s6 = s7 = s8 = s9 = null;
                    if (cells[3] != DBNull.Value)
                        s1 = (string)cells[3];
                    if (cells[4] != DBNull.Value)
                        s2 = (string)cells[4];
                    if (cells[5] != DBNull.Value)
                        s3 = (string)cells[5];
                    if (cells[6] != DBNull.Value)
                        s4 = (string)cells[6];

                    if (cells[14] != DBNull.Value)
                        s5 = (string)cells[14];
                    if (cells[15] != DBNull.Value)
                        s6 = (string)cells[15];

                    if (cells[2] != DBNull.Value)
                        s7 = (string)cells[2];
                    if (cells[16] != DBNull.Value)
                        s8 = (string)cells[16];
                    if (cells[17] != DBNull.Value)
                        s9 = (string)cells[17];

                    relatives.Add(new Relative((int)cells[0], s1, s2, s3,  s4, (DateTime)cells[7], p, 
                        s5, s6, (int)cells[1], s7, w ) );
                }
            }


            return relatives;
        }

        // обновление данных
         public override void Update()
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

            SqlCommand upContCommand = new SqlCommand("UPDATE [RelativeOrGuardian] SET [RelationDegree]=@RelationDegree," +
                    " [RelativeFirstname]=@RelativeFirstname, " +
                      " [RelativeLastname]=@RelativeLastname, " +
                       " [RelativePatronymic]=@RelativePatronymic, " +
                      " [RelativeSex]=@RelativeSex, " +
                       " [RelativeDateOfBirth]=@RelativeDateOfBirth, " +
                      " [RelativePassportSeries]=@RelativePassportSeries, " +
                       " [RelativePassportNumber]=@RelativePassportNumber, " +
                      " [RelativePassportPersonalNumber]=@RelativePassportPersonalNumber, " +
                       " [RelativePassportIssuedBy]=@RelativePassportIssuedBy, " +
                      " [RelativePassportDateOfIssue]=@RelativePassportDateOfIssue, " +
                       " [RelativePassportDateExpiry]=@RelativePassportDateExpiry, " +
                      " [RelativeAddress]=@RelativeAddress, " +
                       " [RelativePhoneNumber]=@RelativePhoneNumber, " +
                      " [RelativePlaceOfWork]=@RelativePlaceOfWork, " +
                        " [RelativePost]=@RelativePost " +
                    " WHERE [IdRelative]=@IdRelative", sqlConnection);   // Запрос на изменение по Id

            upContCommand.Parameters.AddWithValue("IdRelative", Id);

            if (Firstname != null)
                upContCommand.Parameters.AddWithValue("RelativeFirstname", Firstname);
            else
                upContCommand.Parameters.AddWithValue("RelativeFirstname", DBNull.Value);

            if (Lastname != null)
                upContCommand.Parameters.AddWithValue("RelativeLastname", Lastname);
            else
                upContCommand.Parameters.AddWithValue("RelativeLastname", DBNull.Value);

            if (Patronymic != null)
                upContCommand.Parameters.AddWithValue("RelativePatronymic", Patronymic);
            else
                upContCommand.Parameters.AddWithValue("RelativePatronymic", DBNull.Value);

            if (Sex != null)
                upContCommand.Parameters.AddWithValue("RelativeSex", Sex);
            else
                upContCommand.Parameters.AddWithValue("RelativeSex", DBNull.Value);

            if (DateOfBirth != null)
                upContCommand.Parameters.AddWithValue("RelativeDateOfBirth", DateOfBirth);
            else
                upContCommand.Parameters.AddWithValue("RelativeDateOfBirth", DBNull.Value);

            if (Degree != null)
                upContCommand.Parameters.AddWithValue("RelationDegree", Degree);
            else
                upContCommand.Parameters.AddWithValue("RelationDegree", DBNull.Value);

            if (Address != null)
                upContCommand.Parameters.AddWithValue("RelativeAddress", Address);
            else
                upContCommand.Parameters.AddWithValue("RelativeAddress", DBNull.Value);

            if (PhoneNumber != null)
                upContCommand.Parameters.AddWithValue("RelativePhoneNumber", PhoneNumber);
            else
                upContCommand.Parameters.AddWithValue("RelativePhoneNumber", DBNull.Value);

            if (Work_ != null)
            {
                if (Work_.PlaceOfWork != null)
                    upContCommand.Parameters.AddWithValue("RelativePlaceOfWork", Work_.PlaceOfWork);
                else
                    upContCommand.Parameters.AddWithValue("RelativePlaceOfWork", DBNull.Value);


                if (Work_.Post != null)
                    upContCommand.Parameters.AddWithValue("RelativePost", Work_.Post);
                else
                    upContCommand.Parameters.AddWithValue("RelativePost", DBNull.Value);
            }
            else
            {
                upContCommand.Parameters.AddWithValue("RelativePlaceOfWork", DBNull.Value);
                upContCommand.Parameters.AddWithValue("RelativePost", DBNull.Value);
            }

            if (Passport != null)
            {
                upContCommand.Parameters.AddWithValue("RelativePassportSeries", passport.Series);
                upContCommand.Parameters.AddWithValue("RelativePassportNumber", passport.Number);
                upContCommand.Parameters.AddWithValue("RelativePassportPersonalNumber", passport.PersonalNumber);
                upContCommand.Parameters.AddWithValue("RelativePassportIssuedBy", passport.IssuedBy);
                upContCommand.Parameters.AddWithValue("RelativePassportDateOfIssue", passport.DateOfIssue);
                upContCommand.Parameters.AddWithValue("RelativePassportDateExpiry", passport.DateExpiry);
            }
            else
            {
                upContCommand.Parameters.AddWithValue("RelativePassportSeries", DBNull.Value);
                upContCommand.Parameters.AddWithValue("RelativePassportNumber", DBNull.Value);
                upContCommand.Parameters.AddWithValue("RelativePassportPersonalNumber", DBNull.Value);
                upContCommand.Parameters.AddWithValue("RelativePassportIssuedBy", DBNull.Value);
                upContCommand.Parameters.AddWithValue("RelativePassportDateOfIssue", DBNull.Value);
                upContCommand.Parameters.AddWithValue("RelativePassportDateExpiry", DBNull.Value);
            }

            try
            {
                // обновление
                upContCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // добавл нов родственника
        public override void Add()
        {

            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            // подкл
            adapter = new SqlDataAdapter("SELECT * FROM RelativeOrGuardian", Connection.ConnectionString);
            adapter.Fill(dst, "RelativeOrGuardian");
            DataRow row = (dst.Tables["RelativeOrGuardian"]).NewRow();
            // паспорт
            row[8] = Passport.Series;
            row[9] = Passport.Number;
            row[10] = Passport.PersonalNumber;
            row[11] = Passport.IssuedBy;
            row[12] = Passport.DateOfIssue;
            row[13] = Passport.DateExpiry;
        
            row[1] = IdEnrollee;
            row[2] = Degree;
            row[3] = Firstname;
            row[4] = Lastname;
            row[5] = Patronymic;
            row[6] = Sex;
            row[7] = DateOfBirth;
            row[14] = Address;
            row[15] = PhoneNumber;
            row[16] = Work_.PlaceOfWork;
            row[17] = Work_.Post;
            // Добавляем в таблицу рабочую строку
            dst.Tables["RelativeOrGuardian"].Rows.Add(row);



            new SqlCommandBuilder(adapter);

            // Внести все изменения в базу !!!
            adapter.Update(dst, "RelativeOrGuardian");

        }

        // удаление
        public override void Delete()
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

            SqlCommand deleteRelCommand = null;

            deleteRelCommand = new SqlCommand("DELETE FROM [RelativeOrGuardian] WHERE [IdRelative]=@IdRelative", sqlConnection);
            deleteRelCommand.Parameters.AddWithValue("IdRelative", Id);
            deleteRelCommand.ExecuteNonQuery();
        }

        // возв родственника по id
        public static Relative ReturnRelByIdRel(int id_)
        {
            List<Relative> relatives = ReadToEndDataInList();

            foreach (Relative r in relatives)
            {
                if (r.Id==id_)
                {
                    return r;

                }
            }
            return null;
        }

        // возвращает список родственников по idEnr
         public static List<Relative> RenListByInEnrollee(int idEnrolle)
        {
            List<Relative> allRel = ReadToEndDataInList();
            List<Relative> retRel = new List<Relative>();
            foreach (Relative c in allRel)
            {
                if (idEnrolle == c.IdEnrollee)
                {
                    retRel.Add(c);
                }
            }
            return retRel;
        }

        // переопределение метода фио
        public override string FIO()
        {
            return "Родственник: " + base.FIO();
        }

        // метод получен возраста чел
        public override string AgePers()
        {
            // выз баз метод
            string year = base.AgePers();

            return $"Возраст: {year}";
        }

        // поиск по id
        public static List<Relative> SearchById(List<Relative> relatives, string s)
        {
            List<Relative> newR = new List<Relative>();

            try
            {
                int id_ = Convert.ToInt32(s);
            }
            catch (Exception)
            {
                return relatives;
            }

            foreach (Relative c in relatives)
            {
                if (c.Id == Convert.ToInt32(s))
                    newR.Add(c);
            }

            return newR;
        }

        // поиск по фамилии
        public static List<Relative> SearchByLastname(List<Relative> relatives, string s)
        {
            List<Relative> newR = new List<Relative>();
            foreach (Relative d in relatives)
            {
                if (d.Lastname != null)
                    if (d.Lastname.ToLower().Contains(s.ToLower()))
                        newR.Add(d);
            }
            return newR;
        }

        // поиск по степ родст
        public static List<Relative> SearchByDegree(List<Relative> relatives, string s)
        {
            List<Relative> newR = new List<Relative>();
            foreach (Relative d in relatives)
            {
                if (d.Lastname != null)
                    if (d.Degree.ToLower().Contains(s.ToLower()))
                        newR.Add(d);
            }
            return newR;
        }

        // поиск по сер пас
        public static List<Relative> SearchByPassportSeries(List<Relative> relatives, string s)
        {
            List<Relative> newR = new List<Relative>();
            foreach (Relative d in relatives)
            {
                if (d.Lastname != null)
                    if (d.Passport.Series.ToLower().Contains(s.ToLower()))
                        newR.Add(d);
            }
            return newR;
        }

        // поиск по numb pass
        public static List<Relative> SearchByPassportNumber(List<Relative> relatives, string s)
        {
            List<Relative> newR = new List<Relative>();
            foreach (Relative d in relatives)
            {
                if (d.Lastname != null)
                    if (d.Passport.Number.ToLower().Contains(s.ToLower()))
                        newR.Add(d);
            }
            return newR;
        }

        // поиск по place work
        public static List<Relative> SearchByPlaceOfWork(List<Relative> relatives, string s)
        {
            List<Relative> newR = new List<Relative>();
            foreach (Relative d in relatives)
            {
                if (d.Lastname != null)
                    if (d.Work_.PlaceOfWork.ToLower().Contains(s.ToLower()))
                        newR.Add(d);
            }
            return newR;
        }
    }
}
