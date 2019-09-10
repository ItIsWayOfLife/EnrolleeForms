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
    public partial class UpdateContract : Form
    {
        Contract contract = null;

        internal UpdateContract(Contract contract)
        {
            InitializeComponent();

            this.contract = contract;
        }

        private void UpdateContract_Load(object sender, EventArgs e)
        {
            textBox1.Text = contract.Number;
            textBox2.Text = Convert.ToString( contract.ImprisonmentDate );
            textBox4.Text = Convert.ToString (contract.Validity);
            textBox3.Text = contract.Description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                new Contract(contract.Id, contract.IdEnrollee, textBox1.Text, textBox3.Text, Convert.ToDateTime(textBox2.Text), Convert.ToDateTime(textBox4.Text)).Update();
                MessageBox.Show("Данные успешно изменены");
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Данные указаны неверно");
            }

            }
    }
}
