using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace EnrolleeForms
{
    public partial class ViewEnrollee : Form
    {

        // список направлен подготовки
        List<TrainingDirection> trainingDirections;
        // список абитуриентов
        List<Enrollee> enrollees;

        // Строка подключения
        string connectionString;

        // конструктор
        internal ViewEnrollee(List<TrainingDirection> trainingDirections)
        {
            this.trainingDirections = trainingDirections;
            connectionString = Connection.ConnectionString;

            InitializeComponent();

            enrollees = Enrollee.ReadDataEnrollee( );
        }

        TrainingDirection trainingDirection=null;

        // конструктор
        internal ViewEnrollee(List<TrainingDirection> trainingDirections, TrainingDirection trainingDirection)
        {
            this.trainingDirections = trainingDirections;
            connectionString = Connection.ConnectionString;
            this.trainingDirection = trainingDirection;
            InitializeComponent();
        }

        // загрузка данных в datagv
        private void LoadData()
        {
            

            // если указана направл подготовки
            if (trainingDirection != null)
            {
                foreach (Enrollee e in enrollees)
                {
                    if (e.TrainingDirection.Id == trainingDirection.Id)
                    {
                        // id нов строки
                        int rowNumber = dataGridView1.Rows.Add();

                        dataGridView1.Rows[rowNumber].Cells[0].Value = e.Id;
                        dataGridView1.Rows[rowNumber].Cells[1].Value = e.TrainingDirection.Specialty_.ShortName;
                        dataGridView1.Rows[rowNumber].Cells[2].Value = e.TrainingDirection.FormStudy_.Name;
                        dataGridView1.Rows[rowNumber].Cells[3].Value = e.TrainingDirection.TrainingPeriod_.Name;
                        dataGridView1.Rows[rowNumber].Cells[4].Value = e.TrainingDirection.BudgetOrCharge_.Name;
                        dataGridView1.Rows[rowNumber].Cells[5].Value = e.LevelEducation.Name;
                        if (e.Concession != null)
                            dataGridView1.Rows[rowNumber].Cells[6].Value = e.Concession.Name;
                        dataGridView1.Rows[rowNumber].Cells[7].Value = e.DateOfRegistration;
                        dataGridView1.Rows[rowNumber].Cells[8].Value = e.Firstname;
                        dataGridView1.Rows[rowNumber].Cells[9].Value = e.Lastname;
                        dataGridView1.Rows[rowNumber].Cells[10].Value = e.Patronymic;
                        dataGridView1.Rows[rowNumber].Cells[11].Value = e.Sex;
                        dataGridView1.Rows[rowNumber].Cells[12].Value = e.DateOfBirth;
                        dataGridView1.Rows[rowNumber].Cells[13].Value = e.Passport.Series;
                        dataGridView1.Rows[rowNumber].Cells[14].Value = e.Passport.Number;
                        dataGridView1.Rows[rowNumber].Cells[15].Value = e.Passport.PersonalNumber;
                        dataGridView1.Rows[rowNumber].Cells[16].Value = e.Passport.IssuedBy;
                        dataGridView1.Rows[rowNumber].Cells[17].Value = e.Passport.DateOfIssue;
                        dataGridView1.Rows[rowNumber].Cells[18].Value = e.Passport.DateExpiry;
                        dataGridView1.Rows[rowNumber].Cells[19].Value = e.Address;
                        dataGridView1.Rows[rowNumber].Cells[20].Value = e.Postcode;
                        dataGridView1.Rows[rowNumber].Cells[21].Value = e.PhoneNumber;
                        dataGridView1.Rows[rowNumber].Cells[22].Value = e.Diploma.Education;
                        dataGridView1.Rows[rowNumber].Cells[23].Value = e.PlaceOfStudy.Name;
                        dataGridView1.Rows[rowNumber].Cells[24].Value = e.PlaceOfStudy.Address;
                        dataGridView1.Rows[rowNumber].Cells[25].Value = e.PlaceOfStudy.GraduationDate.ToShortDateString();
                        dataGridView1.Rows[rowNumber].Cells[26].Value = e.Diploma.Number;
                        dataGridView1.Rows[rowNumber].Cells[27].Value = e.Diploma.Points;
                        dataGridView1.Rows[rowNumber].Cells[28].Value = e.ScoreFirstTest;
                        dataGridView1.Rows[rowNumber].Cells[29].Value = e.ScoreSecondTest;
                        dataGridView1.Rows[rowNumber].Cells[30].Value = e.ScoreThirdTest;
                        dataGridView1.Rows[rowNumber].Cells[31].Value = e.Email;
                        dataGridView1.Rows[rowNumber].Cells[32].Value = e.AdditionalInformation;
                    }
                }
            }
            else   // если не указана направл подготовки
            {
                foreach (Enrollee e in enrollees)
                {
                    // id нов строки
                    int rowNumber = dataGridView1.Rows.Add();

                    dataGridView1.Rows[rowNumber].Cells[0].Value = e.Id;
                    dataGridView1.Rows[rowNumber].Cells[1].Value = e.TrainingDirection.Specialty_.ShortName;
                    dataGridView1.Rows[rowNumber].Cells[2].Value = e.TrainingDirection.FormStudy_.Name;
                    dataGridView1.Rows[rowNumber].Cells[3].Value = e.TrainingDirection.TrainingPeriod_.Name;
                    dataGridView1.Rows[rowNumber].Cells[4].Value = e.TrainingDirection.BudgetOrCharge_.Name;
                    dataGridView1.Rows[rowNumber].Cells[5].Value = e.LevelEducation.Name;
                    if (e.Concession != null)
                        dataGridView1.Rows[rowNumber].Cells[6].Value = e.Concession.Name;
                    dataGridView1.Rows[rowNumber].Cells[7].Value = e.DateOfRegistration;
                    dataGridView1.Rows[rowNumber].Cells[8].Value = e.Firstname;
                    dataGridView1.Rows[rowNumber].Cells[9].Value = e.Lastname;
                    dataGridView1.Rows[rowNumber].Cells[10].Value = e.Patronymic;
                    dataGridView1.Rows[rowNumber].Cells[11].Value = e.Sex;
                    dataGridView1.Rows[rowNumber].Cells[12].Value = e.DateOfBirth.ToShortDateString();
                    dataGridView1.Rows[rowNumber].Cells[13].Value = e.Passport.Series;
                    dataGridView1.Rows[rowNumber].Cells[14].Value = e.Passport.Number;
                    dataGridView1.Rows[rowNumber].Cells[15].Value = e.Passport.PersonalNumber;
                    dataGridView1.Rows[rowNumber].Cells[16].Value = e.Passport.IssuedBy;
                    dataGridView1.Rows[rowNumber].Cells[17].Value = e.Passport.DateOfIssue.ToShortDateString();
                    dataGridView1.Rows[rowNumber].Cells[18].Value = e.Passport.DateExpiry.ToShortDateString();
                    dataGridView1.Rows[rowNumber].Cells[19].Value = e.Address;
                    dataGridView1.Rows[rowNumber].Cells[20].Value = e.Postcode;
                    dataGridView1.Rows[rowNumber].Cells[21].Value = e.PhoneNumber;
                    dataGridView1.Rows[rowNumber].Cells[22].Value = e.Diploma.Education;
                    dataGridView1.Rows[rowNumber].Cells[23].Value = e.PlaceOfStudy.Name;
                    dataGridView1.Rows[rowNumber].Cells[24].Value = e.PlaceOfStudy.Address;
                    dataGridView1.Rows[rowNumber].Cells[25].Value = e.PlaceOfStudy.GraduationDate.ToShortDateString();
                    dataGridView1.Rows[rowNumber].Cells[26].Value = e.Diploma.Number;
                    dataGridView1.Rows[rowNumber].Cells[27].Value = e.Diploma.Points;
                    dataGridView1.Rows[rowNumber].Cells[28].Value = e.ScoreFirstTest;
                    dataGridView1.Rows[rowNumber].Cells[29].Value = e.ScoreSecondTest;
                    dataGridView1.Rows[rowNumber].Cells[30].Value = e.ScoreThirdTest;
                    dataGridView1.Rows[rowNumber].Cells[31].Value = e.Email;
                    dataGridView1.Rows[rowNumber].Cells[32].Value = e.AdditionalInformation;
                }
            }
        }
        
        // индекс удаляемой строки
        int? indexDel = null;

        // возв выдел абитур
        private Enrollee FlagEnrolle()
        {
            Enrollee EDel = null;
            try
            {
                // индекс строки а не id абитур
                int index = dataGridView1.CurrentRow.Index;           

                foreach (Enrollee e in enrollees)
                {
                    if ((int)dataGridView1[0, index].Value == e.Id)
                    {
                        EDel = e;
                        indexDel = index;
                    }
                }
            }
            catch (Exception)
            {
                return EDel;
            }

            return EDel;
        }


        // список выбора факультетов
        string[] arrayF;
        // посл выбр факультет
        string arrayFLast= "Все";

        // спис спец
        string[] arrayS;

        // посл спец
        string arraySLast= "Все";

        // список форм об
        string[] arrayFS;
        // посл выбор
        string arrayFSLast= "Все";

        // выбор поиска
        string[] arraySearch;

        // загрузка формы
        private void ViewEnrollee_Load(object sender, EventArgs e)
        {

            // сптсок абитуриентов
            enrollees = Enrollee.ReadDataEnrollee();

            LoadData();

            // загр данных выбор факульт
            List<Faculty> faculties = Faculty.ReadToEndDataInList();
            arrayF = new string[faculties.Count+1];
            arrayF[0] = "Все";

            for (int i = 1; i < arrayF.Length; i++)
            {
                arrayF[i] = faculties[i-1].ShortName;
            }

            toolStripComboBox1.Items.AddRange(arrayF);
            toolStripComboBox1.Text = arrayF[0];

            // загрузка выбора спец
            List<Specialty> specialties = Specialty.ReadToEndListSpec();
            arrayS = new string[specialties.Count+1];

            arrayS[0] = "Все";

            for (int i = 1; i < arrayS.Length; i++)
            {
                arrayS[i] = specialties[i-1].ShortName;
            }

            toolStripComboBox2.Items.AddRange(arrayS);
            toolStripComboBox2.Text = arrayS[0];

            // загрузка форм обучения
            List<FormStudy> formStudies = FormStudy.ReadToEndDataInList();

            arrayFS = new string[formStudies.Count+1];
            arrayFS[0] = "Все";


            for (int i = 1; i < arrayFS.Length; i++)
            {
                arrayFS[i] = formStudies[i-1].Name;
            }

            toolStripComboBox3.Items.AddRange(arrayFS);
            toolStripComboBox3.Text = arrayFS[0];

            arraySearch = new string[] {"Id","Фамилии","Дате рожд. до","Дате рожд после","Полу","Серии паспорта","Номеру паспорта","Дате рег. до","Дате рег. после" };
            toolStripComboBox4.Items.AddRange(arraySearch);

        }

        // удаление абитуриента
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // индекс выд строки
            int index = dataGridView1.CurrentRow.Index;
            // индекс выд абитур
            int? iDel = Convert.ToInt32(dataGridView1[0, index].Value);
           

            if (iDel!=null)
            {
                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту строку?", "Удаление строки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                switch (res)
                {
                    case DialogResult.OK:                      
                        try
                        {
                            // выз метод опред данные выдел абитур
                            FlagEnrolle().Delete();


                            if (indexDel != null)
                                dataGridView1.Rows.RemoveAt((int)indexDel);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // выз форму изменения данных абитуриента
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (FlagEnrolle() != null)
            {
                UpdateEnrollee updateEnrollee = new UpdateEnrollee(FlagEnrolle());
                updateEnrollee.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // обновление листа 
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            toolStripButton8_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // передаем выдел абитуриента
            DocForm documentsForm = new DocForm(FlagEnrolle());
            documentsForm.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            // передаем выдел абитуриента
            ContractForm contractForm = new ContractForm(FlagEnrolle());
            contractForm.Show();
        }

        // отк форму родственники абитур
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            // передаем выдел абитуриента
            RelativeForm relativeForm = new RelativeForm(FlagEnrolle());
            relativeForm.Show();
        }


        // id посл выдел абитур
        int idLastEnr = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            // строка инф (внизу)
            if (FlagEnrolle() != null)
            {

                if (idLastEnr != FlagEnrolle().Id)
                {
                    idLastEnr = FlagEnrolle().Id;

                    label2.Text = FlagEnrolle().FIO() + "; " + FlagEnrolle().AgePers();
                    label1.Text = FlagEnrolle().AmountOfPointsSt();
                }
            }
            else
            {
                label1.Text = "";
                label2.Text = "";
            }

            // отобр факульт
            if (arrayFLast != toolStripComboBox1.Text)
            {
                arrayFLast = toolStripComboBox1.Text;

                // удаление данных с datagv (обратный цикл)
                for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                }

                enrollees = Enrollee.SelectByFaculty(arrayFLast);

                // выз метод котор заполняет табл
                LoadData();
            }

            // отобр факульт
            if (arraySLast != toolStripComboBox2.Text)
            {
                arraySLast = toolStripComboBox2.Text;

                // удаление данных с datagv (обратный цикл)
                for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                }

                enrollees = Enrollee.SelectBySpecialty(arrayFLast, arraySLast);

                // выз метод котор заполняет табл
                LoadData();
            }



            // отобр факульт
            if (arrayFSLast != toolStripComboBox3.Text)
            {
                arrayFSLast = toolStripComboBox3.Text;

                // удаление данных с datagv (обратный цикл)
                for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                }

                enrollees = Enrollee.SelectByFormStudy(arrayFLast, arraySLast, arrayFSLast);

                // выз метод котор заполняет табл
                LoadData();
            }

            

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            // удаление данных с datagv (обратный цикл)
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            }
            arrayFLast = arraySLast = arrayFSLast = "Все";

            toolStripComboBox1.Text = toolStripComboBox2.Text = toolStripComboBox3.Text = arrayFLast;
            toolStripTextBox1.Text = "";
            toolStripComboBox4.Text = "";

            enrollees = Enrollee.ReadDataEnrollee();

            // выз метод котор заполняет табл
            LoadData();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

            if (toolStripComboBox4.Text != String.Empty)
            {
                if (toolStripTextBox1 != null)
                {
                    // удаление данных с datagv (обратный цикл)
                    for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                    {
                        dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                    }

                    //arraySearch = new string[] { "Id", "Фамилии", "Дате рожд. до", "Дате рожд после", "Полу", "Серии паспорта", "Номеру паспорта", "Дате рег. до", "Дате рег. после" };

                    if (toolStripComboBox4.Text == arraySearch[0])
                        enrollees = Enrollee.SearchById(enrollees, toolStripTextBox1.Text);

                    if (toolStripComboBox4.Text == arraySearch[1])
                        enrollees = Enrollee.SearchByLastname(enrollees, toolStripTextBox1.Text);

                    if (toolStripComboBox4.Text == arraySearch[2])
                        enrollees = Enrollee.SearchByDateOfBirthLow(enrollees, toolStripTextBox1.Text);

                    if (toolStripComboBox4.Text == arraySearch[3])
                        enrollees = Enrollee.SearchByDateOfBirthTop(enrollees, toolStripTextBox1.Text);

                    if (toolStripComboBox4.Text == arraySearch[4])
                        enrollees = Enrollee.SearchBySex(enrollees, toolStripTextBox1.Text);

                    if (toolStripComboBox4.Text == arraySearch[5])
                        enrollees = Enrollee.SearchByPassportSeries(enrollees, toolStripTextBox1.Text);

                    if (toolStripComboBox4.Text == arraySearch[6])
                        enrollees = Enrollee.SearchByPassportNumber(enrollees, toolStripTextBox1.Text);

                    if (toolStripComboBox4.Text == arraySearch[7])
                        enrollees = Enrollee.SearchByDateOfRegistrationLow(enrollees, toolStripTextBox1.Text);

                    if (toolStripComboBox4.Text == arraySearch[8])
                        enrollees = Enrollee.SearchByDateOfRegistrationTop(enrollees, toolStripTextBox1.Text);


                    // выз метод котор заполняет табл
                    LoadData();
                }
                else
                    MessageBox.Show("Введите информацию для поиска");
            }
            else
                MessageBox.Show("Выберите параметры поиска");

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
                MessageBox.Show(Inquiry.Read(Inquiry.enrolleeFormInfo), "Помощь");
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

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Inquiry.Read(Inquiry.programInfo), "Справка о программе");
            }
            catch (Exception)
            { }
        }
    }
}


