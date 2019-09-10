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
    public partial class DocForm : Form
    {
        // абитуриент
        Enrollee enrollee = null;

        // конструктор
        internal DocForm( Enrollee enrollee)
        {
            InitializeComponent();

            this.enrollee = enrollee;
            toolStripLabel2.Text = enrollee.Id.ToString();
        }

      

        // поиск по
        string[] arraySearch;

        // метод заполнения данными
        private void DrawData()
        {
            foreach (Document d in enrollee.Documents)
            {
                // id нов строки
                int rowNumber = dataGridView1.Rows.Add();
                // запись в столбцы
                dataGridView1.Rows[rowNumber].Cells[0].Value = d.Id;
                dataGridView1.Rows[rowNumber].Cells[1].Value = d.Name;
                dataGridView1.Rows[rowNumber].Cells[2].Value = d.Number;
                dataGridView1.Rows[rowNumber].Cells[3].Value = d.Description;
            }
        }

        // загрузка формы
        private void DocForm_Load(object sender, EventArgs e)
        {
            DrawData();

            arraySearch = new string[] { "Id", "Названию", "Номеру", "Описанию"};
            toolStripComboBox1.Items.AddRange(arraySearch);

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        // выз форму добавлен документа
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddDoc addDoc = new AddDoc(enrollee);
            addDoc.ShowDialog();
        }

       // обновить данные
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }

            toolStripComboBox1.Text = "";
            toolStripTextBox1.Text = "";
            enrollee.Documents = null;
            enrollee.Documents = Document.RenListByInEnrollee(enrollee.Id);
            DrawData();
        }


        // возв выдел док
        private Document FlagDoc()
        {
            Document doc = null;

            try
            {
                // индекс строки а не id абитуп
                int index = dataGridView1.CurrentRow.Index;

                doc = new Document(
                    Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value),
                    enrollee.Id,
                    Convert.ToString(dataGridView1.Rows[index].Cells[1].Value),
                    Convert.ToString(dataGridView1.Rows[index].Cells[2].Value),
                    Convert.ToString(dataGridView1.Rows[index].Cells[3].Value)
                    );
            }
            catch (Exception)
            {
                return doc;
            }
            return doc;
        }

        // выз форму обновление документа
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            UpdateDoc updateDoc = new UpdateDoc(FlagDoc());
            updateDoc.ShowDialog();
        }

        // удаление строки
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // индекс выд строки
            int index = dataGridView1.CurrentRow.Index;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(Connection.ConnectionString);
                sqlConnection.OpenAsync();
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
                        FlagDoc().Delete();
                        dataGridView1.Rows.RemoveAt((int)index);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender,e);
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
                        enrollee.Documents = Document.SearchById(enrollee.Documents, toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[1])
                        enrollee.Documents = Document.SearchByName(enrollee.Documents, toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[2])
                        enrollee.Documents = Document.SearchByNumber(enrollee.Documents, toolStripTextBox1.Text);

                    if (toolStripComboBox1.Text == arraySearch[3])
                        enrollee.Documents = Document.SearchByDescription(enrollee.Documents, toolStripTextBox1.Text);

                    DrawData();

                }
                else
                    MessageBox.Show("Укажите данные для поиска");
            }
            else
            {
                MessageBox.Show("Выберите критерии поиска");
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
                MessageBox.Show(Inquiry.Read(Inquiry.docFormInfo), "Помощь");
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
