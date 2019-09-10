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
    public partial class AddContract : Form
    {
        // объект абируриент
        Enrollee enrollee = null;

        // приним в констр абитур
        internal AddContract(Enrollee enrollee)
        {
            InitializeComponent();

            this.enrollee = enrollee;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // созд нов контрак и добавл в базу
                 new Contract(
                    enrollee.Id,
                    textBox1.Text,
                    textBox3.Text,
                    Convert.ToDateTime(textBox2.Text),
                    Convert.ToDateTime(textBox4.Text)).Add();
                MessageBox.Show("Контракт успешно добавлен");
                Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Данные указаны неверно");
            }
            }

        private void AddContract_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
