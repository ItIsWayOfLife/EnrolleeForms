using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // договор
    class Contract:Document
    {
        // дата заключения
        private DateTime imprisonmentDate;

        public DateTime ImprisonmentDate
        {
            get
            {
                return imprisonmentDate;
            }
        }

        // годен до
        private DateTime validity;

        public DateTime Validity
        {
            get
            {
                return validity;
            }
        }

        // конструктор для счит
        public Contract(int id, int idEnrollee, string number, string description, DateTime imprisonmentDate, DateTime validity)
            : base(id, idEnrollee, number, description)
        {
            this.imprisonmentDate = imprisonmentDate;
            this.validity = validity;
        }

        // конструктор для созд
        public Contract(int idEnrollee, string number, string description, DateTime imprisonmentDate, DateTime validity)
            : base( idEnrollee, number, description)
        {
            this.imprisonmentDate = imprisonmentDate;
            this.validity = validity;
        }

        // метод счит все инф из табл Concession бд и возвр список объектов Speciality
        new public static List<Contract> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM ContractEn", connectionString);
            adapter.Fill(dst, "ContractEn");

            List<Contract> contracts = new List<Contract>();

            foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;

                    contracts.Add(new Contract((int)cells[0], (int)cells[1], (string)cells[2], (string)cells[5], (DateTime)cells[3], (DateTime)cells[4]));
                }
            }
            return contracts;
        }

        // добавление нов контр
        public override void Add()
        {
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter("SELECT * FROM ContractEn", Connection.ConnectionString);
            adapter.Fill(dst, "ContractEn");

            // Создаем пустую рабочую строку
            // строка добавления
            DataRow row = (dst.Tables["ContractEn"]).NewRow();
            row[1] = IdEnrollee;
            row[2] = Number;
            row[3] = ImprisonmentDate;
            row[4] = Validity;
            row[5] = Description;
            // Добавляем в таблицу рабочую строку
            dst.Tables["ContractEn"].Rows.Add(row);

            new SqlCommandBuilder(adapter);         
            // Внести все изменения в базу !!!
            adapter.Update(dst, "ContractEn");
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

            SqlCommand upContCommand = new SqlCommand("UPDATE [ContractEn] SET [NumberContract]=@NumberContract," +
                    " [ImprisonmentDateContract]=@ImprisonmentDateContract, " +
                      " [ValidityContract]=@ValidityContract, " +
                    " [DescriptionContract]=@DescriptionContract" +
                    " WHERE [IdContract]=@IdContract", sqlConnection);   // Запрос на изменение по Id

            upContCommand.Parameters.AddWithValue("IdContract", Id);

            if (Number != null)
                upContCommand.Parameters.AddWithValue("NumberContract", Number);
            else
                upContCommand.Parameters.AddWithValue("NumberContract", DBNull.Value);

            if (Description != null)
                upContCommand.Parameters.AddWithValue("DescriptionContract", Description);
            else
                upContCommand.Parameters.AddWithValue("DescriptionContract", DBNull.Value);

            if (ImprisonmentDate != null)
                upContCommand.Parameters.AddWithValue("ImprisonmentDateContract", ImprisonmentDate);
            else
                upContCommand.Parameters.AddWithValue("ImprisonmentDateContract", DBNull.Value);

            if (Validity != null)
                upContCommand.Parameters.AddWithValue("ValidityContract", Validity);
            else
                upContCommand.Parameters.AddWithValue("ValidityContract", DBNull.Value);

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

        // удаление 
        public override void Delete()
        {
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

            SqlCommand deleteConCommand = null;

            deleteConCommand = new SqlCommand("DELETE FROM [ContractEn] WHERE [IdContract]=@IdContract", sqlConnection);
            deleteConCommand.Parameters.AddWithValue("IdContract", Id);
            deleteConCommand.ExecuteNonQuery();
        }

        // возвращает список контрактов по idEnr
        new public static List<Contract> RenListByInEnrollee(int idEnrolle)
        {
            List<Contract> allCon = ReadToEndDataInList();
            List<Contract> retCon = new List<Contract>();
            foreach (Contract c in allCon)
            {
                if (idEnrolle == c.IdEnrollee)
                {
                    retCon.Add(c);
                }
            }
            return retCon;
        }

        // поиск по id
       public static List<Contract> SearchById(List<Contract> contracts, string s)
        {
            List<Contract> newC = new List<Contract>();
            try
            {
                int id_ = Convert.ToInt32(s);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Укажите id целым числом");
                return contracts;
            }

            foreach (Contract c in contracts)
            {
                if (c.Id == Convert.ToInt32(s))
                    newC.Add(c);
            }
          return newC;
        }

        // поиск по номеру
        public static List<Contract> SearchByNumber(List<Contract> contracts, string s)
        {
            List<Contract> newC = new List<Contract>();
            foreach (Contract d in contracts)
            {
                if (d.Number != null)
                    if (d.Number.ToLower().Contains(s.ToLower()))
                        newC.Add(d);
            }
            return newC;
        }

        // поиск по описанию
        public static List<Contract> SearchByDescription(List<Contract> contracts, string s)
        {
            List<Contract> newC = new List<Contract>();
            foreach (Contract d in contracts)
            {
                if (d.Number != null)
                    if (d.Description.ToLower().Contains(s.ToLower()))
                        newC.Add(d);
            }
            return newC;
        }

        // поиск по дате подп до
        public static List<Contract> SearchByImprisonmentDateLow(List<Contract> contracts, string s)
        {
            List<Contract> newC = new List<Contract>();

            try
            {
                DateTime dt = Convert.ToDateTime(s);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Дата указана неверно");
                return contracts;
            }

            foreach (Contract c in contracts)
            {
                if (c.ImprisonmentDate <= Convert.ToDateTime(s))
                    newC.Add(c);
            }
            return newC;
        }

        // поиск по дате подп после
        public static List<Contract> SearchByImprisonmentDateTop(List<Contract> contracts, string s)
        {
            List<Contract> newC = new List<Contract>();

            try
            {
                DateTime dt = Convert.ToDateTime(s);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Дата указана неверно");
                return contracts;
            }

           foreach (Contract c in contracts)
            {
                if (c.ImprisonmentDate >= Convert.ToDateTime(s))
                    newC.Add(c);
            }

            return newC;
        }

        // поиск по дате годен
        public static List<Contract> SearchByValidityLow(List<Contract> contracts, string s)
        {
            List<Contract> newC = new List<Contract>();

            try
            {
                DateTime dt = Convert.ToDateTime(s);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Дата указана неверно");
                return contracts;
            }

            foreach (Contract c in contracts)
            {
                if (c.Validity <= Convert.ToDateTime(s))
                    newC.Add(c);
            }

            return newC;
        }

        // поиск по дате не годн
        public static List<Contract> SearchByValidityTop(List<Contract> contracts, string s)
        {
            List<Contract> newC = new List<Contract>();

            try
            {
                DateTime dt = Convert.ToDateTime(s);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Дата указана неверно");
                return contracts;
            }

            foreach (Contract c in contracts)
            {
                if (c.Validity >= Convert.ToDateTime(s))
                    newC.Add(c);
            }

            return newC;
        }

        // мет опред скок месяцев действует контракт
        public string MonthWork()
        {

            DateTime nowDate = DateTime.Now;

            var d3 = (nowDate.Month - ImprisonmentDate.Month) + 12 * (nowDate.Year - ImprisonmentDate.Year);

            return $"С даты подписания прошло: {d3.ToString()} месяцев";
        }

        // мет опред скок месяцев осталось действовать контракту
        public string MonthWorkLast()
        {
            DateTime nowDate = DateTime.Now;

            var d3 = (nowDate.Month - Validity.Month) + 12 * (nowDate.Year - Validity.Year);

            if ( Convert.ToInt32(d3)>=0)
                return $"Контракт истек: {Math.Abs(d3).ToString()} месяцев назад";

            return $"Контракту осталось действовать: {Math.Abs(d3).ToString()} месяцев";
        }
    }
}
