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
    public partial class UpdateEnrollee : Form
    {
        // изменяемый абитуриент
        Enrollee enrollee = null;


        // счит данные о напрп подготовки
        List<TrainingDirection> trainingDirections = TrainingDirection.ReadToEndListSpec();


        internal UpdateEnrollee(Enrollee enrollee)
        {
            InitializeComponent();
            this.enrollee = enrollee;

          
        }


        private void DrawingDataEnrollee()
        {
            // информ о паспорте 
            textBox1.Text = enrollee.Passport.Series;
            textBox2.Text = enrollee.Passport.Number;
            textBox3.Text = enrollee.Passport.PersonalNumber;
            textBox4.Text = enrollee.Passport.IssuedBy;
            textBox5.Text = enrollee.Passport.DateOfIssue.ToShortDateString();
            textBox6.Text = enrollee.Passport.DateExpiry.ToShortDateString();

            // инф о дипломе(аттестате)
            textBox22.Text = enrollee.Diploma.Education;
            textBox21.Text = enrollee.Diploma.Number;
            textBox13.Text = enrollee.Diploma.Points.ToString();

            // инф о посл мест учебы
            textBox12.Text = enrollee.PlaceOfStudy.Name;
            textBox11.Text = enrollee.PlaceOfStudy.Address;
            textBox10.Text = enrollee.PlaceOfStudy.GraduationDate.ToString();

            //баллы вступ испыт
            textBox25.Text = enrollee.ScoreFirstTest.ToString();
            textBox24.Text = enrollee.ScoreSecondTest.ToString();
            textBox23.Text = enrollee.ScoreThirdTest.ToString();

            // основная информация
            comboBox1.Text = enrollee.LevelEducation.Name;
            textBox8.Text = enrollee.Lastname;
            textBox9.Text = enrollee.Firstname;
            textBox7.Text = enrollee.Patronymic;
            comboBox3.Text = enrollee.Sex;
            textBox14.Text = enrollee.DateOfBirth.ToShortDateString();
            textBox18.Text = enrollee.Address;

            //доп инф
            if (enrollee.Concession!=null)
            comboBox2.Text = enrollee.Concession.Name;
            textBox17.Text = enrollee.PhoneNumber;
            textBox16.Text = enrollee.Postcode;
            textBox20.Text = enrollee.Email;
            textBox19.Text = enrollee.AdditionalInformation;

            //// направление подготовки
            //comboBox4.Text = enrollee.TrainingDirection.Specialty_.FullName;
            //comboBox5.Text = enrollee.TrainingDirection.FormStudy_.Name;
            //comboBox6.Text = enrollee.TrainingDirection.TrainingPeriod_.Name;
            //comboBox7.Text = enrollee.TrainingDirection.BudgetOrCharge_.Name;

            //отобр данный абитур
            label45.Text = enrollee.Id.ToString();
            label46.Text = enrollee.Lastname;
            label47.Text = enrollee.Firstname;
            label34.Text = enrollee.TrainingDirection.Id.ToString();
           

            // запись документов
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

            // запись контрактов
            foreach (Contract d in enrollee.Contracts)
            {
                // id нов строки
                int rowNumber = dataGridView2.Rows.Add();
                // запись в столбцы
                dataGridView2.Rows[rowNumber].Cells[0].Value = d.Id;
                dataGridView2.Rows[rowNumber].Cells[1].Value = d.Number;
                dataGridView2.Rows[rowNumber].Cells[2].Value = d.ImprisonmentDate;
                dataGridView2.Rows[rowNumber].Cells[3].Value = d.Validity;
                dataGridView2.Rows[rowNumber].Cells[4].Value = d.Description;
            }

            // запись родственников
            foreach (Relative r in enrollee.Relatives)
            {
                // id нов строки
                int rowNumber = dataGridView3.Rows.Add();
                // запись в столбцы
                dataGridView3.Rows[rowNumber].Cells[0].Value = r.Id;
                dataGridView3.Rows[rowNumber].Cells[1].Value = r.Degree;
                dataGridView3.Rows[rowNumber].Cells[2].Value = r.Firstname;
                dataGridView3.Rows[rowNumber].Cells[3].Value = r.Lastname;
                dataGridView3.Rows[rowNumber].Cells[4].Value = r.Patronymic;
                dataGridView3.Rows[rowNumber].Cells[5].Value = r.Sex;
                dataGridView3.Rows[rowNumber].Cells[6].Value = r.DateOfBirth;
                dataGridView3.Rows[rowNumber].Cells[7].Value = r.Passport.Series;
                dataGridView3.Rows[rowNumber].Cells[8].Value = r.Passport.Number;
                dataGridView3.Rows[rowNumber].Cells[9].Value = r.Passport.PersonalNumber;
                dataGridView3.Rows[rowNumber].Cells[10].Value = r.Passport.IssuedBy;
                dataGridView3.Rows[rowNumber].Cells[11].Value = r.Passport.DateOfIssue;
                dataGridView3.Rows[rowNumber].Cells[12].Value = r.Passport.DateExpiry;
                dataGridView3.Rows[rowNumber].Cells[13].Value = r.Address;
                dataGridView3.Rows[rowNumber].Cells[14].Value = r.PhoneNumber;
                dataGridView3.Rows[rowNumber].Cells[15].Value = r.Work_.PlaceOfWork;
                dataGridView3.Rows[rowNumber].Cells[16].Value = r.Work_.Post;



            }

        }


        // метод выпадания блоков
        private void ComboBoxData()
        {
            // выпадающ блоки
            // счит данных из табл уров образования
           List<LevelEducation> levelEducations = LevelEducation.ReadToEndDataInList();
         

            string[] st = new string[levelEducations.Count];
            for (int i = 0; i < st.Length; i++)
            {
                st[i] = levelEducations[i].Name;
            }
            comboBox1.Items.AddRange(st);


            // счит данных из табл уров льготы
            List<Concession> concessions = Concession.ReadToEndDataInList();
            

            string[] st1 = new string[concessions.Count];
            for (int i = 0; i < st1.Length; i++)
            {
                st1[i] = concessions[i].Name;
            }
            comboBox2.Items.AddRange(st1);

            // выбор пола
            string[] st2 = new string[] { "мужской", "женский" };

            comboBox3.Items.AddRange(st2);


            //// формы обучения 
            //List<TrainingDirection> trainingDirections = TrainingDirection.ReadToEndListSpec();

            //// выбор специальности
            //List<Specialty> specialties = Specialty.ReadToEndListSpec();
            //string[] st3 = new string[specialties.Count];
            //for (int i = 0; i < st3.Length; i++)
            //{
            //    st3[i] = specialties[i].ShortName;
            //}
            //comboBox4.Items.AddRange(st3);

            //// выбор формы
            //if (comboBox4.Text != "")
            //{

            //}


        }

        private void ReadBaseDataTrainingEn()
        {


           


            ListViewItem listTD = null;

         



            foreach (TrainingDirection t in trainingDirections)
            {
                listTD = new ListViewItem(new string[]
                     {
                           Convert.ToString(t.Id),
                    Convert.ToString(t.Specialty_.ShortName),
                    Convert.ToString(t.FormStudy_.Name),
                    Convert.ToString(t.TrainingPeriod_.Name),
                    Convert.ToString(t.BudgetOrCharge_.Name)
                     });
                listView1.Items.Add(listTD);
            }


            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;

        }


        private void UpdateEnrollee_Load(object sender, EventArgs e)
        {
            DrawingDataEnrollee();
            ComboBoxData();
            ReadBaseDataTrainingEn();
            
        }











        // метод созд. доплом (аттестат) (возв нов дипл)
        private Diploma NewDiploma()
        {
            Diploma diploma = null;
            try
            {
                string number = textBox21.Text;
            double points = Convert.ToDouble(textBox13.Text);
            string education = textBox22.Text;

            diploma = new Diploma(number, points, education);
        }
            catch (Exception)
            {
                MessageBox.Show("Данные аттестата/диплома должны быть полностью и правильно заполнены");
            }
            return diploma;
        }

        // метод созд. паспорт (возв нов паспорт)
        private Passport NewPass()
        {
            Passport passport = null;

            //try
            //{
            string series = textBox1.Text;
            string number = textBox2.Text;
            string personalNumber = textBox3.Text;
            string issuedBy = textBox4.Text;
            DateTime dateOfIssue = new DateTime();
            DateTime dateExpiry = new DateTime();
            try
            {
                 dateOfIssue = DateTime.Parse(textBox5.Text.ToString());
                 dateExpiry = DateTime.Parse(textBox6.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("wwwwwwwwwww");
            }

            try
            {
                passport = new Passport(series, number, personalNumber, issuedBy, dateOfIssue, dateExpiry);
            }
            catch (Exception)
            {
                MessageBox.Show("Заполните паспортные данные");
            }
           


            return passport;

        }

        // метод созд. посл места обучения (возвр нов посл место обуч)
        private PlaceOfStudy NewPlaceOfStudy()
        {

            PlaceOfStudy placeOfStudy = null;
            //try
            //{
            string name = textBox12.Text; ;
            string address = textBox11.Text;
            DateTime gradDate = new DateTime();
            try
            {
                 gradDate = DateTime.Parse(textBox10.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("qqq");
            }

            placeOfStudy = new PlaceOfStudy(name, address, gradDate);
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Данные последнего места учебы должны быть полностью заполнены");
            //}
            return placeOfStudy;
        }



        // определение напрв подготовки (выбор выбранной строки преобр в напр подготовки)
        private TrainingDirection DefinitionTrainingDirection()
        {
            TrainingDirection trd = null;
            try
            {
                int idTr = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);



                foreach (TrainingDirection t in trainingDirections)
                    if (idTr == t.Id)
                        trd = t;
            }
            catch (Exception)
            {
                return trd;
            }
            return trd;
        }

        // метод созд абит
        private Enrollee CreateNewEnrolee()
        {
            // ур образов
            List<LevelEducation> levelEducations = LevelEducation.ReadToEndDataInList();

            // льготы
            List<Concession> concessions = Concession.ReadToEndDataInList();

            TrainingDirection trainingDirection = TrainingDirection.ReturnTDById(Convert.ToInt32(label34.Text));

           

            string firstname = null;
            string lastname = null;
            string sex = null;
            DateTime dateOfBirth = new DateTime();
            string address = null;

            string phoneNumber = null;
            string postcode = null;
            string patronymic = null;
            string email = null;
            string additionalInformation = null;

            double? scoreThirdTest = null;
            double? scoreFirstTest = null;
            double? scoreSecondTest = null;

            firstname = textBox9.Text;
            lastname = textBox8.Text;
            sex = comboBox3.Text;

            address = textBox18.Text;
            try
            {
                
            dateOfBirth = DateTime.Parse(textBox14.Text.ToString());
           
            }
            catch (Exception)
            {
                MessageBox.Show("Заполните основную информацию");
            }




            if (textBox17.Text != String.Empty)
                phoneNumber = textBox17.Text;

            if (textBox16.Text != String.Empty)
                postcode = textBox16.Text;

            if (textBox7.Text != String.Empty)
                patronymic = textBox7.Text;

            if (textBox20.Text != String.Empty)
                email = textBox20.Text;

            if (textBox19.Text != String.Empty)
                additionalInformation = textBox19.Text;

            if (textBox23.Text != String.Empty)
                scoreThirdTest = Convert.ToDouble(textBox23.Text);

            if (textBox25.Text != String.Empty)
                scoreFirstTest = Convert.ToDouble(textBox25.Text);

            if (textBox24.Text != String.Empty)
                scoreSecondTest = Convert.ToDouble(textBox24.Text);


            LevelEducation lvEd = null;
            Concession con = null;

            for (int i = 0; i < levelEducations.Count; i++)
            {
                if (comboBox1.Text == levelEducations[i].Name)
                    lvEd = levelEducations[i];
            }

            for (int i = 0; i < concessions.Count; i++)
            {
                if (comboBox2.Text == concessions[i].Name)
                    con = concessions[i];
            }

            Enrollee enrollee_ = new Enrollee(Convert.ToInt32(label45.Text), firstname, lastname, patronymic, sex, dateOfBirth,
                 NewPass(), address, phoneNumber, trainingDirection, lvEd, con, DateTime.Now,
                 postcode, NewPlaceOfStudy(), NewDiploma(), scoreFirstTest, scoreSecondTest,
                 scoreThirdTest, email, additionalInformation);


          

            enrollee = enrollee_;


            return enrollee_;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // обновляем данные абитуриента
                CreateNewEnrolee().Update();
                MessageBox.Show("Данные успешно изменены");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте указанные данные");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // ВЫЗ МЕТ. КОТОР ВОЗВРАЩАЕТ ID ВЫБРАННОГО НАПРАВЛЕНИЯ
            label34.Text = DefinitionTrainingDirection().Id.ToString();
        }

       


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // создаем нов список документов список документов
                List<Document> documents = new List<Document>();

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {

                    documents.Add(new Document(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value),
                        enrollee.Id,
                        Convert.ToString(dataGridView1.Rows[i].Cells[1].Value),
                        Convert.ToString(dataGridView1.Rows[i].Cells[2].Value),
                        Convert.ToString(dataGridView1.Rows[i].Cells[3].Value)));

                }

                // обновляем документы
                foreach (Document d in documents)
                {
                    d.Update();
                }

                MessageBox.Show("Данные успешно изменены");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте указанные данные");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // создаем нов список документов список документов
                List<Contract> contracts = new List<Contract>();

                for (int i = 0; i < dataGridView2.RowCount; i++)
                {

                    contracts.Add(new Contract(Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value),
                        enrollee.Id,
                        Convert.ToString(dataGridView2.Rows[i].Cells[1].Value),
                        Convert.ToString(dataGridView2.Rows[i].Cells[4].Value),
                          Convert.ToDateTime(dataGridView2.Rows[i].Cells[2].Value),
                        Convert.ToDateTime(dataGridView2.Rows[i].Cells[3].Value)));

                }

                // одновляем контракты
                foreach (Contract c in contracts)
                {
                    c.Update();
                }
                MessageBox.Show("Данные успешно изменены");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте указанные данные");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // созд нов список родственников
                List<Relative> relatives = new List<Relative>();

                // заполняем список
                for (int i = 0; i < dataGridView3.RowCount; i++)
                {
                    Passport p = new Passport(
                    Convert.ToString(dataGridView3.Rows[i].Cells[7].Value),
                    Convert.ToString(dataGridView3.Rows[i].Cells[8].Value),
                    Convert.ToString(dataGridView3.Rows[i].Cells[9].Value),
                    Convert.ToString(dataGridView3.Rows[i].Cells[10].Value),
                    Convert.ToDateTime(dataGridView3.Rows[i].Cells[11].Value),
                    Convert.ToDateTime(dataGridView3.Rows[i].Cells[12].Value)
                    );

                    Work w = new Work(Convert.ToString(dataGridView3.Rows[i].Cells[15].Value), Convert.ToString(dataGridView3.Rows[i].Cells[16].Value));

                    relatives.Add(new Relative(
                        Convert.ToInt32(dataGridView3.Rows[i].Cells[0].Value),
                        Convert.ToString(dataGridView3.Rows[i].Cells[2].Value),
                        Convert.ToString(dataGridView3.Rows[i].Cells[3].Value),
                        Convert.ToString(dataGridView3.Rows[i].Cells[4].Value),
                        Convert.ToString(dataGridView3.Rows[i].Cells[5].Value),
                        Convert.ToDateTime(dataGridView3.Rows[i].Cells[6].Value),
                        p,
                        Convert.ToString(dataGridView3.Rows[i].Cells[13].Value),
                        Convert.ToString(dataGridView3.Rows[i].Cells[14].Value),
                        enrollee.Id,
                        Convert.ToString(dataGridView3.Rows[i].Cells[1].Value),
                        w));
                }


                // одновляем родственников
                foreach (Relative r in relatives)
                {
                    r.Update();
                }
                MessageBox.Show("Данные успешно изменены");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте указанные данные");
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
