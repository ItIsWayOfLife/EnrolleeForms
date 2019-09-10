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
    public partial class HistoryForm : Form
    {
        List<History> histories = null;

        public HistoryForm()
        {
            InitializeComponent();
        }


        private void DrawData()
        {
            foreach (History r in histories)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = r.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = r.IdEnrollee;
                dataGridView1.Rows[rowNumber].Cells[2].Value = r.Operation;
                dataGridView1.Rows[rowNumber].Cells[3].Value = r.CreateAt;
               

            }
        }


        string[] arraySearch;

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            histories = History.ReadToEndDataInList();

            arraySearch = new string[] { "Id","Id абитуриенту","Операции","Дате до","Дате после","Дате"};

            toolStripComboBox1.Items.AddRange(arraySearch);
            DrawData();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            

            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }

            histories = History.ReadToEndDataInList();

            toolStripComboBox1.Text = "";
            toolStripTextBox1.Text = "";

            DrawData();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            

            if (toolStripComboBox1.Text != String.Empty)
            {
                if (toolStripTextBox1.Text != String.Empty)
                {
                    for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                    {
                        dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                    }


                    if (toolStripComboBox1.Text == arraySearch[0])
                        histories = History.SearchById(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[1])
                        histories = History.SearchByIdEnrolle(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[2])
                        histories = History.SearchByOperation(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[3])
                        histories = History.SearchByCreateAtLow(toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[4])
                        histories = History.SearchByCreateAtTop(toolStripTextBox1.Text);


                    if (toolStripComboBox1.Text == arraySearch[5])
                        histories = History.SearchByCreateAt(toolStripTextBox1.Text);

                    DrawData();
                }
                else
                    MessageBox.Show("Укажите данные для поиска");
            }
            else
                MessageBox.Show("Укажите критерии поиска");
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
    
    }
}
