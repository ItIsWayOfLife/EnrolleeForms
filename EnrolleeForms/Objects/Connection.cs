using System;
using System.Windows.Forms;
using System.IO;

namespace EnrolleeForms
{
    // статичесткий класс подключения к бд
   public static class  Connection
    {
        // строка подключения
        public static string ConnectionString { get; set; } = ReadConnectionString();


        // записать строку подкл
        public static void WriteConnectionString(string connectionString)
        {
            try
            {
                File.WriteAllText("Connection.txt", connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        // прочитать строку подкл
        public static string ReadConnectionString()
        {
            // строка подключения
            string connectionString = null;
            try
            {
                // счит данные с файла
                StreamReader reader = new StreamReader("Connection.txt");
                connectionString = reader.ReadLine();
                reader.Close();
            }
            catch (Exception ex)
            {
                string se = "Ошибка, файл подключения Connection.txt";
                MessageBox.Show(se +"\n" +ex);
            }
            // возв строку подкл
            return connectionString;
        }
    }
}
