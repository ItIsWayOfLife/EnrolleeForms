using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnrolleeForms
{
    public partial class RelativeForm : Form
    {

        // абитуриент
        Enrollee enrollee = null;

        internal RelativeForm(Enrollee enrollee)
        {
            InitializeComponent();
            this.enrollee = enrollee;
            toolStripLabel2.Text = enrollee.Id.ToString();
        }

        //метод заполнения данными
        private void DrawData()
        {
            foreach (Relative r in enrollee.Relatives)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = r.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = r.Degree;
                dataGridView1.Rows[rowNumber].Cells[2].Value = r.Firstname;
                dataGridView1.Rows[rowNumber].Cells[3].Value = r.Lastname;
                dataGridView1.Rows[rowNumber].Cells[4].Value = r.Patronymic;
                dataGridView1.Rows[rowNumber].Cells[5].Value = r.Sex;
                dataGridView1.Rows[rowNumber].Cells[6].Value = r.DateOfBirth.ToShortDateString();
                dataGridView1.Rows[rowNumber].Cells[7].Value = r.Passport.Series;
                dataGridView1.Rows[rowNumber].Cells[8].Value = r.Passport.Number;
                dataGridView1.Rows[rowNumber].Cells[9].Value = r.Passport.PersonalNumber;
                dataGridView1.Rows[rowNumber].Cells[10].Value = r.Passport.IssuedBy;
                dataGridView1.Rows[rowNumber].Cells[11].Value = r.Passport.DateOfIssue.ToShortDateString();
                dataGridView1.Rows[rowNumber].Cells[12].Value = r.Passport.DateExpiry.ToShortDateString();
                dataGridView1.Rows[rowNumber].Cells[13].Value = r.Address;
                dataGridView1.Rows[rowNumber].Cells[14].Value = r.PhoneNumber;
                dataGridView1.Rows[rowNumber].Cells[15].Value = r.Work_.PlaceOfWork;
                dataGridView1.Rows[rowNumber].Cells[16].Value = r.Work_.Post;

            }
        }

        string[] arraySearch;

        private void RelativeForm_Load(object sender, EventArgs e)
        {
            DrawData();


            arraySearch = new string[] { "Id","Фамилии","Степени родства","Серии паспорта","Номеру паспорта","Месту работы"};
            toolStripComboBox1.Items.AddRange(arraySearch);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }
            enrollee.Relatives = null;
            enrollee.Relatives = Relative.RenListByInEnrollee(enrollee.Id);
            DrawData();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddRelativ addRelativ = new AddRelativ(enrollee);
            addRelativ.Show();
        }

        // возв выдел родственника
        private Relative FlagRel()
        {
            Relative relative = null;
            try
            {
              
                // индекс строки а не id абитуп
                int index = dataGridView1.CurrentRow.Index;

                relative = Relative.ReturnRelByIdRel(Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value));

            }
            catch (Exception)
            {
                return relative;
            }

            return relative;
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            UptateRelativ uptateRelativ = new UptateRelativ(FlagRel());
            uptateRelativ.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // индекс выд строки
            int index = dataGridView1.CurrentRow.Index;
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

            DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту строку?", "Удаление строки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            switch (res)
            {
                case DialogResult.OK:
                    try
                    {
                        // выз метод котор возвращает выделенного абитуриента и удаляем
                        FlagRel().Delete();
                        dataGridView1.Rows.RemoveAt((int)index);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text != String.Empty)
            {
                if (toolStripTextBox1.Text != String.Empty)
                {

                    // удаление данных с datagv (обратный цикл)
                    for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                    {
                        dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                    }

                    arraySearch = new string[] { "Id", "Фамилии", "Степени родства", "Серии паспорта", "Номеру паспорта", "Месту работы" };

                    if (toolStripComboBox1.Text == arraySearch[0])
                        enrollee.Relatives = Relative.SearchById(enrollee.Relatives,toolStripTextBox1.Text);


                    if (toolStripComboBox1.Text == arraySearch[1])
                        enrollee.Relatives = Relative.SearchByLastname(enrollee.Relatives, toolStripTextBox1.Text);


                    if (toolStripComboBox1.Text == arraySearch[2])
                        enrollee.Relatives = Relative.SearchByDegree(enrollee.Relatives, toolStripTextBox1.Text);


                    if (toolStripComboBox1.Text == arraySearch[3])
                        enrollee.Relatives = Relative.SearchByPassportSeries(enrollee.Relatives, toolStripTextBox1.Text);


                    if (toolStripComboBox1.Text == arraySearch[4])
                        enrollee.Relatives = Relative.SearchByPassportNumber(enrollee.Relatives, toolStripTextBox1.Text);


                    if (toolStripComboBox1.Text == arraySearch[5])
                        enrollee.Relatives = Relative.SearchByPlaceOfWork(enrollee.Relatives, toolStripTextBox1.Text);


                    DrawData();
                }
                else
                    MessageBox.Show("Заполните поисковую строку!");
            }
            else
                MessageBox.Show("Выберите критерии поиска");
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender, e);
            toolStripComboBox1.Text = "";
            toolStripTextBox1.Text = "";
            label1.Text = "";
        }

        int idLast = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (FlagRel() != null)
            {

                if (FlagRel().Id != idLast)
                {
                    idLast = FlagRel().Id;

                    label1.Text = FlagRel().FIO() + "; " + FlagRel().AgePers();
                }
            }
            else
            {
                label1.Text = "";
            }
        }

        private void экспортВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataExport.ExportToExcel(dataGridView1);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            switch (res)
            {
                case DialogResult.OK:
                    try
                    {
                        Application.Exit();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }



        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Inquiry.Read(Inquiry.relativeFormInfo), "Помощь");
            }
            catch (Exception)
            { }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Inquiry.Read(Inquiry.programInfo), "Справка о программе");
            }
            catch (Exception)
            { }
        }

        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Inquiry.Read(Inquiry.aboutTheDeveloperInfo), "Справка о разработчике");
            }
            catch (Exception)
            { }
        }
    }
}
