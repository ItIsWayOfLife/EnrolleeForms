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
    public partial class HistoryUserForm : Form
    {
        public HistoryUserForm()
        {
            InitializeComponent();
        }

        List<HistoryUser> historyUsers = null;


        // загр данны
        public void LoadData()
        {
            if (historyUsers != null)
            {

                foreach (HistoryUser h in historyUsers)
                {
                    // id нов строки
                    int rowNumber = dataGridView1.Rows.Add();
                    // запись в столбцы
                    dataGridView1.Rows[rowNumber].Cells[0].Value = h.Id;
                    dataGridView1.Rows[rowNumber].Cells[1].Value = h.IdUser;
                    dataGridView1.Rows[rowNumber].Cells[2].Value = h.FIOUser;
                    dataGridView1.Rows[rowNumber].Cells[3].Value = h.CreateAt;
                    if (h.ReleaseDate!=null)
                    dataGridView1.Rows[rowNumber].Cells[4].Value = h.ReleaseDate.Value;

                }
               
            }

        }

        string[] arraySearch;

        private void HistoryUserForm_Load(object sender, EventArgs e)
        {
            historyUsers = HistoryUser.ReadToEndDataInList();

            LoadData();


            arraySearch = new string[] { "Id","Id пользователя","ФИО пользователя", "Время входа до", "Время входа", "Время входа после", "Время выхода до",
            "Время выхода","Время выхода после"};

            toolStripComboBox1.Items.AddRange(arraySearch);
        }


        // возв выдел родственника
        private HistoryUser FlagHistoryUser()
        {
            HistoryUser history = null;
            try
            {

                // индекс строки а не id абитуп
                int index = dataGridView1.CurrentRow.Index;

                history = HistoryUser.ReturnRelByIdHistoryUser(Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value));

            }
            catch (Exception)
            {
                return history;
            }

            return history;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }

            toolStripComboBox1.Text = "";
            toolStripTextBox1.Text = "";

            historyUsers = HistoryUser.ReadToEndDataInList();

            LoadData();


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
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

                    arraySearch = new string[] { "Id","Id пользователя","Фио", "Время входа до", "Время входа", "Время входа после", "Время выхода до",
                            "Время выхода после", "Время выхода"};

                    if (toolStripComboBox1.Text == arraySearch[0])
                        historyUsers = HistoryUser.SearchById(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[1])
                        historyUsers = HistoryUser.SearchByIdUser(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[2])
                        historyUsers = HistoryUser.SearchByFIO(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[3])
                        historyUsers = HistoryUser.SearchByCreateAtLow(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[4])
                        historyUsers = HistoryUser.SearchByCreateAtTop(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[5])
                        historyUsers = HistoryUser.SearchByCreateAt(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[6])
                        historyUsers = HistoryUser.SearchByReleaseDateLow(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[7])
                        historyUsers = HistoryUser.SearchByReleaseDateTop(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[8])
                        historyUsers = HistoryUser.SearchByReleaseDate(toolStripTextBox1.Text);


                    LoadData();
                }
                else
                    MessageBox.Show("Заполните поисковую строку");

            }
            else
                MessageBox.Show("Выберите критерии поиска");

        }

        // id посл аб
        int idLastHU = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (FlagHistoryUser() != null)
            {
                if (idLastHU != FlagHistoryUser().Id)
                {
                    idLastHU = FlagHistoryUser().Id;
                    label1.Text = FlagHistoryUser().CountTime();
                }
            }
            else
                label1.Text = "";
        }

        private void импортВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
           
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


    }
}
