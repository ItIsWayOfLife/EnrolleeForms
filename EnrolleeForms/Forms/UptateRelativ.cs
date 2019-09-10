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
    public partial class UptateRelativ : Form
    {
        Relative relative = null;

        internal UptateRelativ(Relative relative)
        {
            InitializeComponent();

            this.relative = relative;
     
        }

        private void UptateRelativ_Load(object sender, EventArgs e)
        {
            // заполнение боксов данными 

            // паспорт
            textBox31.Text = relative.Passport.Series;
            textBox30.Text = relative.Passport.Number;
            textBox29.Text = relative.Passport.PersonalNumber;
            textBox28.Text = relative.Passport.IssuedBy;
            textBox27.Text = Convert.ToString( relative.Passport.DateOfIssue );
            textBox26.Text = Convert.ToString( relative.Passport.DateExpiry );


            textBox6.Text = relative.Degree;
            textBox5.Text = relative.Lastname;
            textBox4.Text = relative.Firstname;
            textBox3.Text = relative.Patronymic;
            textBox1.Text = Convert.ToString(relative.DateOfBirth );
            textBox7.Text = relative.Address;
            textBox8.Text = relative.PhoneNumber;
            textBox9.Text = relative.Work_.PlaceOfWork;
            textBox10.Text = relative.Work_.Post;

            comboBox3.Text = relative.Sex;

        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                    relative.Id,
                Convert.ToString(textBox4.Text),
                Convert.ToString(textBox5.Text),
                Convert.ToString(textBox3.Text),
                 Convert.ToString(comboBox3.Text),
                Convert.ToDateTime(textBox1.Text),
                passport,
                Convert.ToString(textBox7.Text),
                Convert.ToString(textBox8.Text),
                relative.IdEnrollee,
                Convert.ToString(textBox6.Text),
                 work
                ).Update();


                MessageBox.Show("Данные успешно изменены");
                Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Заполните все поля, проверьте введенные данные");
            }
        }
    }
}
