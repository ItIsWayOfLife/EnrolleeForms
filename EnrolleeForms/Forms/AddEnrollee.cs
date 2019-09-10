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
    public partial class AddEnrollee_ : Form
    {

        // направление подготовки
        TrainingDirection trainingDirection = null;

        // нов абитуриент
        Enrollee enrollee = null;


        // ур образов
        List<LevelEducation> levelEducations;

        // льготы
        List<Concession> concessions;




        // конструктор
        internal AddEnrollee_(TrainingDirection trainingDirection)
        {
            InitializeComponent();
            this.trainingDirection = trainingDirection;
        }

        // показывает напр подготовки
        private void DrawInfoTranDir()
        {
            label34.Text = trainingDirection.Specialty_.FullName;
            label35.Text = trainingDirection.TrainingPeriod_.Name;
            label39.Text = trainingDirection.FormStudy_.Name;
            label40.Text = trainingDirection.BudgetOrCharge_.Name;
        }

        // загрузка формы
        private void AddEnrollee_Load(object sender, EventArgs e)
        {
            DrawInfoTranDir();
            ComboBoxData();
        }


        // метод выпадания блоков
        private void ComboBoxData()
        {
            // выпадающ блоки
            // счит данных из табл уров образования
            levelEducations = LevelEducation.ReadToEndDataInList();



            string[] st = new string[levelEducations.Count];
            for (int i = 0; i < st.Length; i++)
            {
                st[i] = levelEducations[i].Name;
            }
            comboBox1.Items.AddRange(st);


            // счит данных из табл уров льготы
            concessions = Concession.ReadToEndDataInList();

            string[] st1 = new string[concessions.Count];
            for (int i = 0; i < st1.Length; i++)
            {
                st1[i] = concessions[i].Name;
            }
            comboBox2.Items.AddRange(st1);

            // выбор пола
            string[] st2 = new string[] { "мужской", "женский" };

            comboBox3.Items.AddRange(st2);
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

            try
            {
                string series = textBox1.Text;
                string number = textBox2.Text;
                string personalNumber = textBox3.Text;
                string issuedBy = textBox4.Text;
                DateTime dateOfIssue = Convert.ToDateTime(textBox5.Text);
                DateTime dateExpiry = Convert.ToDateTime(textBox6.Text);


                passport = new Passport(series, number, personalNumber, issuedBy, dateOfIssue, dateExpiry);

            }
            catch (Exception)
            {
                MessageBox.Show("Данные паспорта должны быть полностью заполнены");
            }


            return passport;

        }

        // метод созд. посл места обучения (возвр нов посл место обуч)
        private PlaceOfStudy NewPlaceOfStudy()
        {

            PlaceOfStudy placeOfStudy = null;
            try
            {
                string name = textBox12.Text; ;
                string address = textBox11.Text; ;
                DateTime gradDate = Convert.ToDateTime(textBox10.Text);

                placeOfStudy = new PlaceOfStudy(name, address, gradDate);
            }
            catch (Exception)
            {
                MessageBox.Show("Данные последнего места учебы должны быть полностью заполнены");
            }
            return placeOfStudy;
        }

        // метод созд нов абитур
        private Enrollee CreateNewEnrolee()
        {
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




            try
            {
                firstname = textBox8.Text;
                lastname = textBox9.Text;
                sex = comboBox3.Text;
                dateOfBirth = Convert.ToDateTime(textBox14.Text);
                address = textBox18.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Заполните правильно основную информацию");
            }




            if (textBox17.Text != "")
                phoneNumber = textBox17.Text;

            if (textBox16.Text != "")
                postcode = textBox16.Text;

            if (textBox7.Text != "")
                patronymic = textBox7.Text;

            if (textBox20.Text != "")
                email = textBox20.Text;

            if (textBox19.Text != "")
                additionalInformation = textBox19.Text;

            if (textBox23.Text != "")
                scoreThirdTest = Convert.ToDouble(textBox23.Text);

            if (textBox25.Text != "")
                scoreFirstTest = Convert.ToDouble(textBox25.Text);

            if (textBox24.Text != "")
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


            try
            {
                // созд объект конструктором для созд нов объектов
                enrollee = new Enrollee(firstname, lastname, patronymic, sex, dateOfBirth,
                    NewPass(), address, phoneNumber, trainingDirection, lvEd, con, DateTime.Now,
                    postcode, NewPlaceOfStudy(), NewDiploma(), scoreFirstTest, scoreSecondTest,
                    scoreThirdTest, email, additionalInformation);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Укажите верные данные");
            }

            return enrollee;
        }

        // очистка текстбоксов
        private void TextBoxClear()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            comboBox3.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";
            textBox21.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            textBox25.Text = "";
        }

        // добавить абит
        private void button1_Click(object sender, EventArgs e)
        {
            // прин нов абитур и бодавбл данные в бд
            try
            {

                CreateNewEnrolee().Add();
                MessageBox.Show("Данные успешно добавлены");


                SearchNewEnrolle();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления данных, проверте правильность вводимых значений!");
            }

        }

        // метод поиска добавлен абитуриента
        private void SearchNewEnrolle()
        {
            label45.Text = enrollee.ReturnLastId().ToString();
            label46.Text = enrollee.Firstname;
            label47.Text = enrollee.Lastname;

        }

        // добавление документации
        private void AddDocument()
        {
            if (enrollee != null)
            {

                // нов список документов
                List<Document> documents_ = new List<Document>();

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    documents_.Add(new Document(
                        Convert.ToInt32(label45.Text),
                         dataGridView1[0, i].Value.ToString(),
                          dataGridView1[1, i].Value.ToString(),
                           dataGridView1[2, i].Value.ToString()
                        ));
                }


                // добавление в бд
                foreach (Document d in documents_)
                {
                    d.Add();
                }
            }
            else
                MessageBox.Show("Добавьте абитуриента");

        }

        // добавл договоры
        private void AddContract()
        {
            if (enrollee != null)
            {

                List<Contract> contracts_ = new List<Contract>();
               

                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    contracts_.Add(new Contract(
                        Convert.ToInt32(label45.Text),
                        dataGridView2[0, i].Value.ToString(),
                          dataGridView2[3, i].Value.ToString(),
                         Convert.ToDateTime(dataGridView2[1, i].Value),
                         Convert.ToDateTime(dataGridView2[2, i].Value)));
                }

                // добавление в бд
                foreach (Contract d in contracts_)
                {
                    d.Add();
                }
            }
            else
                MessageBox.Show("Добавьте абитуриента");
        }

        // добавляет родственников
        private void AddRelative()
        {
            if (enrollee != null)
            {


                List<Relative> relatives_ = new List<Relative>();

                // Создаем пустую рабочую строку

                for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                {

                    Passport p = new Passport(textBox31.Text, textBox30.Text, textBox29.Text, textBox28.Text,Convert.ToDateTime( textBox27.Text),
                        Convert.ToDateTime( textBox26.Text));

                    Work w = new Work(dataGridView3[8, i].Value.ToString(), dataGridView3[9, i].Value.ToString());

                    relatives_.Add(new Relative(dataGridView3[0, i].Value.ToString(), dataGridView3[1, i].Value.ToString(),
                        dataGridView3[2, i].Value.ToString(), dataGridView3[3, i].Value.ToString(), Convert.ToDateTime(dataGridView3[4, i].Value),
                        p, dataGridView3[6, i].Value.ToString(), dataGridView3[7, i].Value.ToString(),
                        Convert.ToInt32(label45.Text), dataGridView3[5, i].Value.ToString(), w));

                }

                // добавление в бд
                foreach (Relative d in relatives_)
                {
                    d.Add();
                }
            }
            else
                MessageBox.Show("Добавьте абитуриента");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                AddDocument();
                MessageBox.Show("Данные успешно добавлены");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте введенные данные");
            }

            }

        private void button3_Click(object sender, EventArgs e)
        {
            TextBoxClear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                AddContract();
                MessageBox.Show("Данные успешно добавлены");
            }
            catch (Exception)
            { 
            MessageBox.Show("Ошибка! Проверьте введенные данные");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                AddRelative();
                MessageBox.Show("Данные успешно добавлены");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте введенные данные");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                AddDocument();
            AddContract();
            AddRelative();
                MessageBox.Show("Данные успешно добавлены");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте введенные данные");
            }
        }

        private void RelativePassClear()
        {
            textBox26.Text = "";
            textBox27.Text = "";
            textBox28.Text = "";
            textBox29.Text = "";
            textBox30.Text = "";
            textBox31.Text = "";
        }

        // очистить все
        private void button7_Click(object sender, EventArgs e)
        {
            TextBoxClear();
            RelativePassClear();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
        }
    }
}
