using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // бюджет или платно
    class BudgetOrCharge
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
        public BudgetOrCharge(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        // метод счит данные
        public static List<BudgetOrCharge> ReadToEndDataInList()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter("SELECT * FROM BudgetOrCharge", connectionString);
            adapter.Fill(dst, "BudgetOrCharge");
            List<BudgetOrCharge> budgetOrCharges = new List<BudgetOrCharge>();
            foreach (DataTable dt in dst.Tables)
            {
           // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    budgetOrCharges.Add(new BudgetOrCharge((int)cells[0], (string)cells[1]));
                }
            }
            return budgetOrCharges;
        }
    }
}
