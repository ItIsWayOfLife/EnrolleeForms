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
    public partial class AddDoc : Form
    {
        // прин абитур
        internal AddDoc(Enrollee enrollee)
        {
            InitializeComponent();

            this.enrollee = enrollee;
        }

        // абитуриент
        Enrollee enrollee = null;



        private void AddDoc_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // созд и добавл док
                new Document(enrollee.Id, textBox1.Text, textBox2.Text, textBox3.Text).Add();
                MessageBox.Show("Документ успешно добавлен");
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Данные указаны неверно");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
