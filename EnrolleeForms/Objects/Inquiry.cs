using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnrolleeForms
{
    // класс хранит справочную информацию
    static class Inquiry
    {
        // справ. файлы 

        // помощь
        //
        public static string mainFormInfoFile = @"Info\MainFormInfo.txt";

        public static string contractFormInfo = @"Info\ContractFormInfo.txt";

        public static string docFormInfo = @"Info\DocFormInfo.txt";

        public static string enrolleeFormInfo = @"Info\EnrolleeFormInfo.txt";

        public static string relativeFormInfo = @"Info\RelativeFormInfo.txt";
        //
        // справка о прогр
        public static string programInfo = @"Info\ProgramInfo.txt";

        // справка о разр
        public static string aboutTheDeveloperInfo = @"Info\AboutTheDeveloperInfo.txt";

        // прочитать данные (s=назв файла)
        public static string Read(string s)
        {
            // строка подключения
            string stInfo = null;
            try
            {
                // счит данные с файла
                StreamReader reader = new StreamReader(s);
                stInfo = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show($"Файл {s} не найден");
            }
            // возв строки инф
            return stInfo;
        }

    }
}
