using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnrolleeForms
{
    public partial class AddRelativ : Form
    {

        // абитуриент
        Enrollee enrollee = null;

        // прин в конструкторе абитуриента
        internal AddRelativ(Enrollee enrollee)
        {
            InitializeComponent();

            this.enrollee = enrollee;
           
        }

        private void AddRelativ_Load(object sender, EventArgs e)
        {
          // выбор пола в комбоксе
            comboBox1.Items.AddRange(new string[] { "мужской","женский"});

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (enrollee != null)
            {
             
                // созд объекты и добавл нов родст в бд
                try
                {
                    Passport passport = new Passport(
                   Convert.ToString(textBox31.Text),
                   Convert.ToString(textBox30.Text),
                   Convert.ToString(textBox29.Text),
                   Convert.ToString(textBox28.Text),
                   Convert.ToDateTime(textBox27.Text),
                   Convert.ToDateTime(textBox26.Text)
                   );

                Work work = new Work(Convert.ToString(textBox9.Text), Convert.ToString(textBox10.Text));

                // созд нов объект о бов в базу
                new Relative(
                Convert.ToString(textBox4.Text),
                Convert.ToString(textBox5.Text),
                Convert.ToString(textBox3.Text),
                 Convert.ToString(comboBox1.Text),
                Convert.ToDateTime(textBox1.Text),
                passport,
                Convert.ToString(textBox7.Text),
                Convert.ToString(textBox8.Text),
                enrollee.Id,
                Convert.ToString(textBox6.Text),
                 work
                ).Add();
                MessageBox.Show("Данные успешно добавлены");
                Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Заполните все поля правильно");
                }
            }
        }
    }
}
