using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EnrolleeForms
{
    public partial class MainForm : Form
    {
        // пользователь
        User user = null;
        internal MainForm(User user)
        {
            InitializeComponent();
            this.user = user;
            toolStripLabel3.Text = user.FIO();
        }
        // факультеты
        List<Faculty> faculties = Faculty.ReadToEndDataInList();
        // выбранный факультет
        Faculty faculty_ = null;
        string selectCB1 = null;
        string selectCB2 = null;
        bool reversBlCB2 = false;
        string selectCB3 = null;
        string selectCB31 = null;
        TrainingDirection tr = null;
       
        List<TrainingDirection> trainingDirections;

        // созд объектов (загрузка данныъ из бд)
        private void LoadData()
        {          
            ListViewItem listTD = null;

            foreach (TrainingDirection t in trainingDirections)
            {
                listTD = new ListViewItem(new string[]
                     {
                           Convert.ToString(t.Id),
                    Convert.ToString(t.Specialty_.FullName),
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

        // for CB2
        string[] st1 = new string[5] { "Id", "Специальности", "Форме обучения", "Периоду обучения", "Плат./бюдж." };
        string[] st2 = new string[3] { "Поиск","Id", "Специальности" };

        private  void Form1_Load(object sender, EventArgs e)
        {
            trainingDirections = TrainingDirection.ReadToEndListSpec();
            LoadData();

            // комбо бокс Факультет
            string[] st = new string[faculties.Count+1];
            st[0] = "Все";
            for (int i = 0; i < faculties.Count; i++)
            {
                st[i+1] = faculties[i].ShortName;
            }
            comboBox1.Items.AddRange(st);
            comboBox1.Text = comboBox1.Items[0].ToString();
            selectCB1 = comboBox1.Text;
          
            comboBox2.Items.AddRange(st1);
            comboBox2.Text = "Id";
            selectCB2 = comboBox2.Text;

            comboBox3.Items.AddRange(st2);
            comboBox3.Text = "Поиск";
            selectCB3 = comboBox3.Text;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddEnrollee_ addEnrollee = new AddEnrollee_(DefinitionTrainingDirection());
            addEnrollee.ShowDialog();
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
            catch(Exception)
            {
                return trd;
            }
            return trd;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if ((DefinitionTrainingDirection() != null))
                {
                AddEnrollee_ addEnrollee = new AddEnrollee_(DefinitionTrainingDirection());
                addEnrollee.ShowDialog();
            }
            else
                MessageBox.Show("Выберите направление подготовки");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if ((DefinitionTrainingDirection() != null))
            {
                MinutelyTrainingDirection minutelyTrainingDirection = new MinutelyTrainingDirection(DefinitionTrainingDirection());
            minutelyTrainingDirection.Show();
            }
            else
                MessageBox.Show("Выберите направление подготовки");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ViewEnrollee viewEnrollee = new ViewEnrollee(trainingDirections);
            viewEnrollee.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if ((DefinitionTrainingDirection() != null))
            {
                ViewEnrollee viewEnrollee = new ViewEnrollee(trainingDirections, DefinitionTrainingDirection());
            viewEnrollee.ShowDialog();
            }
            else
                MessageBox.Show("Выберите направление подготовки");
        }

        // прямая сортировка
        private void SortTR()
        {
            //  по id
            if (comboBox2.Text == st1[0])
                trainingDirections = TrainingDirection.SortById(trainingDirections);
            // по спец
            if (comboBox2.Text == st1[1])
                trainingDirections = TrainingDirection.SortBySpecFNama(trainingDirections);
            //по форм обуч
            if (comboBox2.Text == st1[2])
                trainingDirections = TrainingDirection.SortByFormSt(trainingDirections);
            //по форм пер об
            if (comboBox2.Text == st1[3])
                trainingDirections = TrainingDirection.SortByPerEd(trainingDirections);
            //по форм пл ил бюд
            if (comboBox2.Text == st1[4])
                trainingDirections = TrainingDirection.SortByBudOrCha(trainingDirections);
        }

        // обратная сортировка
        private void SortTRRevers()
        {
            //  по id
            if (comboBox2.Text == st1[0])
                trainingDirections = TrainingDirection.SortByIdRevers(trainingDirections);
            // по спец
            if (comboBox2.Text == st1[1])
                trainingDirections = TrainingDirection.SortBySpecFNamaRevers(trainingDirections);
            //по форм обуч
            if (comboBox2.Text == st1[2])
                trainingDirections = TrainingDirection.SortByFormStRevers(trainingDirections);
            //по форм пер об
            if (comboBox2.Text == st1[3])
                trainingDirections = TrainingDirection.SortByPerEdRevers(trainingDirections);
            //по форм пл ил бюд
            if (comboBox2.Text == st1[4])
                trainingDirections = TrainingDirection.SortByBudOrChaRevers(trainingDirections);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // план набора отобр снизу
            if (DefinitionTrainingDirection() != null && tr!= DefinitionTrainingDirection())
            {
               // выз мет инф
               label2.Text = (DefinitionTrainingDirection().Info());
                // выз мет кот возв фак и кафедр
                label10.Text = (DefinitionTrainingDirection().FacultAndDepart());

                tr = DefinitionTrainingDirection();
            }

            // выбор кафедры
            if (comboBox1.Text != selectCB1)
            {
                for (int i = listView1.Items.Count - 1; i >= 0; i--)
                    listView1.Items[i].Remove();

                trainingDirections = TrainingDirection.ReadToEndListSpec();

                if (comboBox1.Text == "Все")
                {
                    faculty_ = null;
                }
                else
                {
                    foreach (Faculty f in faculties)
                    {
                        if ((comboBox1.Text == f.ShortName))
                        {
                            faculty_ = f;
                        }
                    }
                }
                if (faculty_ != null)
                {
                    trainingDirections = TrainingDirection.ReadDataByIdFac(faculty_, trainingDirections);
                }

                LoadData();

                selectCB1 = comboBox1.Text;
            }

            // сортировка 
            if (comboBox2.Text != selectCB2)
            {
                for (int i = listView1.Items.Count - 1; i >= 0; i--)
                    listView1.Items[i].Remove();

                // сортируем
                SortTR();
                // пер остортир обр
                reversBlCB2 = true;

                LoadData();

                selectCB2 = comboBox2.Text;
            }
        }

        // обратные сортировки
        private void label8_Click(object sender, EventArgs e)
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
                listView1.Items[i].Remove();

            if (reversBlCB2 == false)
            {
                reversBlCB2 = true;
                SortTR();
            }
            else
            {
                SortTRRevers();
                reversBlCB2 = false;
            }
               
            LoadData();          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Поиск")
            {
                MessageBox.Show("Выберите по каким критериям будет вестись поиск");
            }
            selectCB31 = textBox1.Text;
            selectCB3 = comboBox3.Text;
            // удал строк
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
                listView1.Items[i].Remove();

            // если ищется по id
            if (comboBox3.Text == st2[1])
            {
                // поиск по Id
                trainingDirections = TrainingDirection.SearchById(Convert.ToInt32(textBox1.Text), trainingDirections);
            }
            else if (comboBox3.Text == st2[2])
            {
                // поиск по спец
                trainingDirections = TrainingDirection.SearchBySpecialty(textBox1.Text, trainingDirections);
            }
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // удал строк
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
                listView1.Items[i].Remove();

            label2.Text = "";

            comboBox2.Text = "Id";
            comboBox3.Text = "Поиск";
            comboBox1.Text = comboBox1.Items[0].ToString();
            textBox1.Text = "";
            label10.Text = ""; 

            trainingDirections = TrainingDirection.ReadToEndListSpec();
            LoadData();
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            if (user.AdminRights == AdminRights.Right)
            {
                SelectHistory selectHistory = new SelectHistory();
                selectHistory.Show();
            }
            else
            {
                MessageBox.Show("У вас нет прав администратора");
            }
       }

        // событие откт нач форму
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                user.HistoryUser.Update();
                Form loginForm = Application.OpenForms[0];
                loginForm.Show();
            }catch(Exception)
            { }
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
                MessageBox.Show(Inquiry.Read(Inquiry.mainFormInfoFile),"Помощь");
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
