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
    public partial class UpdateDoc : Form
    {

        Document document = null;

        internal UpdateDoc(Document document)
        {
            InitializeComponent();

            this.document = document;
        }

        private void UpdateDoc_Load(object sender, EventArgs e)
        {
            textBox1.Text = document.Name;
            textBox2.Text = document.Number;
            textBox3.Text = document.Description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (document != null)
            {
                try
                {
                    // созд новой док и обновляем данные
                    new Document(document.Id, document.IdEnrollee, textBox1.Text, textBox2.Text, textBox3.Text).Update();
                    MessageBox.Show("Данные успешно изменены");
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Данные указаны неверно");
                }
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
