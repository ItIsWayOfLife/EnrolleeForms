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
    public partial class ContractForm : Form
    {

        // абитуриент
        Enrollee enrollee = null;
        
        // конструктор
        internal ContractForm(Enrollee enrollee)
        {
            InitializeComponent();

            this.enrollee = enrollee;
            toolStripLabel2.Text = enrollee.Id.ToString();
        }


        // метод заполнения данными
        private void DrawData()
        {
            foreach (Contract c in enrollee.Contracts)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = c.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = c.Number;
                dataGridView1.Rows[rowNumber].Cells[2].Value = c.ImprisonmentDate.ToShortDateString();
                dataGridView1.Rows[rowNumber].Cells[3].Value = c.Validity.ToShortDateString();
                dataGridView1.Rows[rowNumber].Cells[4].Value = c.Description;
            }
        }

        // поиск по
        string[] arraySearch;

        // загрузка формы
        private void ContractForm_Load(object sender, EventArgs e)
        {
            DrawData();

            arraySearch = new string[] { "Id","Номеру","Дате подписания до","Дате подписания после","Дате годных до","Дате годных после","Описанию"};
            toolStripComboBox1.Items.AddRange(arraySearch);
        }


        // возв выдел кон
        private Contract FlagCon()
        {
            Contract con = null;
            try
            {

                // индекс строки а не id абитуп
                int index = dataGridView1.CurrentRow.Index;

                con = new Contract(
                    Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value),
                    enrollee.Id,
                      Convert.ToString(dataGridView1.Rows[index].Cells[1].Value),
                    Convert.ToString(dataGridView1.Rows[index].Cells[4].Value),
                    Convert.ToDateTime(dataGridView1.Rows[index].Cells[2].Value),
                    Convert.ToDateTime(dataGridView1.Rows[index].Cells[3].Value)
                    );
            }
            catch (Exception)
            {
                return con;
            }
            return con;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddContract addContract = new AddContract(enrollee);
            addContract.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }
            enrollee.Contracts = null;
            toolStripComboBox1.Text = "";
            toolStripTextBox1.Text = "";
            label1.Text = "";

            enrollee.Contracts = Contract.RenListByInEnrollee(enrollee.Id);
            DrawData();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            UpdateContract updateContract = new UpdateContract(FlagCon());
            updateContract.Show();
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
                        FlagCon().Delete();
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

                   
                    if (toolStripComboBox1.Text == arraySearch[0])
                        enrollee.Contracts = Contract.SearchById(enrollee.Contracts, toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[1])
                        enrollee.Contracts = Contract.SearchByNumber(enrollee.Contracts, toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[2])
                        enrollee.Contracts = Contract.SearchByImprisonmentDateLow(enrollee.Contracts, toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[3])
                        enrollee.Contracts = Contract.SearchByImprisonmentDateTop(enrollee.Contracts, toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[4])
                        enrollee.Contracts = Contract.SearchByValidityLow(enrollee.Contracts, toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[5])
                        enrollee.Contracts = Contract.SearchByValidityTop(enrollee.Contracts, toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[6])
                        enrollee.Contracts = Contract.SearchByDescription(enrollee.Contracts, toolStripTextBox1.Text);

                    DrawData();
                }
                else
                    MessageBox.Show("Укажите данные для поиска");
            }
            else
                MessageBox.Show("Выберите критерии для поиска");


            
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender, e);

            
        }

        int idLastContr = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (FlagCon() != null)
            {
                if (idLastContr != FlagCon().Id)
                {
                    idLastContr = FlagCon().Id;

                    label1.Text = FlagCon().MonthWork() + "; " + FlagCon().MonthWorkLast();
                }
            }
            else
                label1.Text = "";
        }

        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show(Inquiry.Read(Inquiry.contractFormInfo), "Помощь");
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
