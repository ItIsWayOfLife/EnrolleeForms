using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace EnrolleeForms
{
    // направление подготовки
    class TrainingDirection
    {
        // id
        int id;

        public int Id
        {
            get
            {
                return id;
            }
        }

        // специальность
        private Specialty specialty;

        public Specialty Specialty_
        {
            get
            {
                return specialty;
            }
        }

        // форма обучения
        private FormStudy formStudy;

        public FormStudy FormStudy_
        {
            get
            {
                return formStudy;
            }
        }

        // срок обучения
        private TrainingPeriod trainingPeriod;

        public TrainingPeriod TrainingPeriod_
        {
            get
            {
                return trainingPeriod;
            }
        }

        // плат или бюдж
        private BudgetOrCharge budgetOrCharge;

        public BudgetOrCharge BudgetOrCharge_
        {
            get
            {
                return budgetOrCharge;
            }
        }

        // 1й вступ испт
        private EntranceTest firstEntranceTest;

        public EntranceTest FirstEntranceTest
        {
            get
            {
                return firstEntranceTest;
            }
        }

        //2й вступ испт
        private EntranceTest secondEntranceTest;

        public EntranceTest SecondEntranceTest
        {
            get
            {
                return secondEntranceTest;
            }
        }

        //3й вступ испт
        private EntranceTest thirdEntranceTest;

        public EntranceTest ThirdEntranceTest
        {
            get
            {
                return thirdEntranceTest;
            }
        }

        //план набора
        private int admissionPlan;

        public int AdmissionPlan
        {
            get
            {
                return admissionPlan;
            }
        }

        // конструктор
        public TrainingDirection(int id, Specialty specialty, FormStudy formStudy, TrainingPeriod trainingPeriod, BudgetOrCharge budgetOrCharge,
            EntranceTest firstEntranceTest, EntranceTest secondEntranceTest, EntranceTest thirdEntranceTest, int admissionPlan)
        {
            this.id = id;
            this.specialty = specialty;
            this.formStudy = formStudy;
            this.trainingPeriod = trainingPeriod;
            this.budgetOrCharge = budgetOrCharge;
            this.firstEntranceTest = firstEntranceTest;
            this.secondEntranceTest = secondEntranceTest;
            this.thirdEntranceTest = thirdEntranceTest;
            this.admissionPlan = admissionPlan;
        }

        // метод счит все инф из табл TrainingDirection бд и возвр ее знач
        public static List<TrainingDirection> ReadToEndListSpec()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;

            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;
            
            // создаем списки необходимыъ объектов
            List<FormStudy> formStudies = FormStudy.ReadToEndDataInList();
            List<TrainingPeriod> trainingPeriods = TrainingPeriod.ReadToEndDataInList();
            List<BudgetOrCharge> budgetOrCharges = BudgetOrCharge.ReadToEndDataInList();
            List<EntranceTest> entranceTests = EntranceTest.ReadToEndDataInList();
            List<Specialty> specialties = Specialty.ReadToEndListSpec();

            List<TrainingDirection> trainingDirections = new List<TrainingDirection>();

            // счит инфон из бд табл НаправлениеПодготовки
            adapter = new SqlDataAdapter("SELECT * FROM TrainingDirection", connectionString);
            adapter.Fill(dst, "TrainingDirection");

            // поля для созд новых объектов
            Specialty specialty1=null;
            FormStudy formStudy1 = null;
            TrainingPeriod trainingPeriod1 = null;
            BudgetOrCharge budgetOrCharge1 = null;
            EntranceTest entranceTest1 = null;
            EntranceTest entranceTest2 = null;
            EntranceTest entranceTest3 = null;

            foreach (DataTable dt in dst.Tables)
            {
                // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;

                    int idSpec = (int)cells[1];
                    int idFormSt = (int)cells[2];
                    int idTraPer = (int)cells[3];
                    int idBudOrChan = (int)cells[4];
                    int idFirstEn = (int)cells[5];
                    int idSecondEn = (int)cells[6];

                    int? idThirdEn = null ;

                    try
                    {
                        int i = (int)cells[7];
                        idThirdEn = (int)cells[7];
                    }
                    catch (Exception)
                    {
                        idThirdEn = null;
                    }

                        foreach (Specialty f in specialties)
                        {
                            if (idSpec == f.Id)
                                specialty1 = f;
                        }

                    foreach (FormStudy f in formStudies)
                    {
                        if (idFormSt == f.Id)
                            formStudy1 = f;
                    }

                    foreach (TrainingPeriod f in trainingPeriods)
                    {
                        if (idTraPer == f.Id)
                            trainingPeriod1 = f;
                    }

                    foreach (BudgetOrCharge f in budgetOrCharges)
                    {
                        if (idBudOrChan == f.Id)
                            budgetOrCharge1 = f;
                    }

                    foreach (EntranceTest f in entranceTests)
                    {
                        if (idFirstEn == f.Id)
                            entranceTest1 = f;

                        if (idSecondEn == f.Id)
                            entranceTest2 = f;

                        if (idThirdEn != null && idThirdEn == f.Id)
                            entranceTest3 = f;
                    }

                    trainingDirections.Add(new TrainingDirection((int)cells[0], specialty1, formStudy1, trainingPeriod1,
                       budgetOrCharge1, entranceTest1, entranceTest2, entranceTest3, (int)cells[8]));
                }
            }   
           return trainingDirections;
        }

        // возвращает направление по Id
        public static TrainingDirection ReturnTDById(int id1)
        {
            // счит данные напрподготовки
            List<TrainingDirection> trainingDirections = TrainingDirection.ReadToEndListSpec();

            foreach (TrainingDirection t in trainingDirections)
            {
                if (t.Id == id1)
                {
                    return t;
                }
            }
            return null;
        }

        // возвращает список напр по данному факультету
        public static List<TrainingDirection> ReadDataByIdFac(Faculty faculty1, List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTr = new List<TrainingDirection>();

            foreach (TrainingDirection t in trainingDirections)
            {
                if (t.Specialty_.Faculty.Id == faculty1.Id)
                {
                    newTr.Add(t);                   
                }             
            }
            return newTr;
        }

        // сортировка по id 
        public static List<TrainingDirection> SortById(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.Id 
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // сортировка по id обр
        public static List<TrainingDirection> SortByIdRevers(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.Id descending
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // сортировка по пол наз спец
        public static List<TrainingDirection> SortBySpecFNama(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.Specialty_.FullName
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // сортировка по пол наз спец обр
        public static List<TrainingDirection> SortBySpecFNamaRevers(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.Specialty_.FullName descending
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // сортировка по форме обу
        public static List<TrainingDirection> SortByFormSt(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.FormStudy_.Name
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // сортировка по форме обу обр
        public static List<TrainingDirection> SortByFormStRevers(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.FormStudy_.Name descending
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // сортировка по пер обуч
        public static List<TrainingDirection> SortByPerEd(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.TrainingPeriod_.Name
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // сортировка по пер обуч обр
        public static List<TrainingDirection> SortByPerEdRevers(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.TrainingPeriod_.Name descending
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // сортировка по бюдж или плат
        public static List<TrainingDirection> SortByBudOrCha(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.BudgetOrCharge_.Name
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // сортировка по бюдж или плат обр
        public static List<TrainingDirection> SortByBudOrChaRevers(List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTrDir = new List<TrainingDirection>();
            // сортировка
            var sortedUsers = from u in trainingDirections
                              orderby u.BudgetOrCharge_.Name descending
                              select u;

            foreach (TrainingDirection u in sortedUsers)
                newTrDir.Add(u);

            return newTrDir;
        }

        // метод подсчит колич под заяв на место (конкурс)
        public double Competition()
        {
            // кол под заяв (выз стат мет из классса абитур и пер текуший id)
            double pz = Enrollee.RetCountTrDByIdTr(this.Id);

            return  Math.Round( pz / AdmissionPlan, 2);
        }

        // факульт и кафедра
        public string FacultAndDepart()
        {
            return "Факультет: " + Specialty_.Faculty.ShortName + "; Кафедра: " + Specialty_.Department.ShortName; 
        }

        // поиск по id
        public static List<TrainingDirection> SearchById(int id_, List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTr = new List<TrainingDirection>(); 

            foreach (TrainingDirection t in trainingDirections)
            {
                if (t.Id == id_)
                {
                    newTr.Add(t);
                }
            }
            return newTr;
        }

        // поиск по специальности
        public static List<TrainingDirection> SearchBySpecialty(string s, List<TrainingDirection> trainingDirections)
        {
            List<TrainingDirection> newTr = new List<TrainingDirection>();
         
            foreach (TrainingDirection t in trainingDirections)
            {
                // пров содержит ли строка подстроку
                if (t.Specialty_.FullName.ToLower().Contains(s.ToLower()))
                newTr.Add(t);
            }
       
            return newTr;
        }

        // инф и наборе, под заяв, конкурсе
        public string Info()
        {
            string s = $"набор: {AdmissionPlan}   подано заявлений: {Enrollee.RetCountTrDByIdTr(Id)}   конкурс: {Competition()}";
            return s;
        }
    }
}

