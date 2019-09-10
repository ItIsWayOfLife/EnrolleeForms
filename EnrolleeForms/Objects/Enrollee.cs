using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EnrolleeForms
{
    // абитуриент
    class Enrollee : Person
    {

        #region поля и св-ва 

        // направление подготовки
        private TrainingDirection trainingDirection;

        public TrainingDirection TrainingDirection
        {
            get
            {
                return trainingDirection;
            }
        }

        // уровень образования
        private LevelEducation levelEducation;

        public LevelEducation LevelEducation
        {
            get
            {
                return levelEducation;
            }
        }

        // льгота
        private Concession concession;

        public Concession Concession
        {
            get
            {
                return concession;
            }
        }

        // дата регестрации
        private DateTime dateOfRegistration;

        public DateTime DateOfRegistration
        {
            get
            {
                return dateOfRegistration;
            }
        }
        // почтов индекс
        private string postcode;

        public string Postcode
        {
            get
            {
                return postcode;
            }
        }

        // посл место учебы
        private PlaceOfStudy placeOfStudy;

        public PlaceOfStudy PlaceOfStudy
        {
            get
            {
                return placeOfStudy;
            }
        }

        // диплом (аттестат)
        private Diploma diploma;

        public Diploma Diploma
        {
            get
            {
                return diploma;
            }
        }

        // баллы
        // по 1му вст испт
        private double? scoreFirstTest;

        public double? ScoreFirstTest
        {
            get
            {
                return scoreFirstTest;
            }
        }

        // по 2му вст испт
        private double? scoreSecondTest;

        public double? ScoreSecondTest
        {
            get
            {
                return scoreSecondTest;
            }
        }

        // по 3му вст испт
        private double? scoreThirdTest;

        public double? ScoreThirdTest
        {
            get
            {
                return scoreThirdTest;
            }
        }

        // электр почта
        private string email;

        public string Email
        {
            get
            {
                return email;
            }
        }
        // доп инф
        private string additionalInformation;

        public string AdditionalInformation
        {
            get
            {
                return additionalInformation;
            }
        }

        // договоры
        public List<Contract> Contracts { get; set; }
        // документы
        public List<Document> Documents { get; set; }
        // родственники
        public List<Relative> Relatives { get; set; }

        #endregion

        #region Конструкторы

        // со спискоми (договоры, документы, родственники) для счит инф
        public Enrollee(int id, string firstname, string lastname, string patronymic, string sex, DateTime dateOfBirth, Passport passport,
            string address, string phoneNumber, TrainingDirection TrainingDirection, LevelEducation LevelEducation, Concession Concession,
           DateTime dateOfRegistration, string Postcode, PlaceOfStudy PlaceOfStudy, Diploma Diploma, double? ScoreFirstTest,
          double? ScoreSecondTest, double? ScoreThirdTest, string Email, string AdditionalInformation, List<Contract> Contracts,
           List<Document> Documents, List<Relative> Relatives)
            : base(id, firstname, lastname, patronymic, sex, dateOfBirth, passport, address, phoneNumber)
        {
            trainingDirection = TrainingDirection;
            levelEducation = LevelEducation;
            concession = Concession;
            this.dateOfRegistration = dateOfRegistration;
            postcode = Postcode;
            placeOfStudy = PlaceOfStudy;
            diploma = Diploma;
            scoreFirstTest = ScoreFirstTest;
            scoreSecondTest = ScoreSecondTest;
            scoreThirdTest = ScoreThirdTest;
            email = Email;
            additionalInformation = AdditionalInformation;
            this.Contracts = Contracts;
            this.Documents = Documents;
            this.Relatives = Relatives;
        }

        // без списков (договоры, документы, родственники) для созд
        public Enrollee(int id, string firstname, string lastname, string patronymic, string sex, DateTime dateOfBirth, Passport passport,
             string address, string phoneNumber, TrainingDirection TrainingDirection, LevelEducation LevelEducation, Concession Concession,
            DateTime dateOfRegistration, string Postcode, PlaceOfStudy PlaceOfStudy, Diploma Diploma, double? ScoreFirstTest,
           double? ScoreSecondTest, double? ScoreThirdTest, string Email, string AdditionalInformation)
             : base(id, firstname, lastname, patronymic, sex, dateOfBirth, passport, address, phoneNumber)
        {
            trainingDirection = TrainingDirection;
            levelEducation = LevelEducation;
            concession = Concession;
            this.dateOfRegistration = dateOfRegistration;
            postcode = Postcode;
            placeOfStudy = PlaceOfStudy;
            diploma = Diploma;
            scoreFirstTest = ScoreFirstTest;
            scoreSecondTest = ScoreSecondTest;
            scoreThirdTest = ScoreThirdTest;
            email = Email;
            additionalInformation = AdditionalInformation;
        }

        // со спискоми (договоры, документы, родственники) для счит
        public Enrollee(string firstname, string lastname, string patronymic, string sex, DateTime dateOfBirth, Passport passport,
            string address, string phoneNumber, TrainingDirection TrainingDirection, LevelEducation LevelEducation, Concession Concession,
           DateTime dateOfRegistration, string Postcode, PlaceOfStudy PlaceOfStudy, Diploma Diploma, double? ScoreFirstTest,
          double? ScoreSecondTest, double? ScoreThirdTest, string Email, string AdditionalInformation, List<Contract> Contracts,
           List<Document> Documents, List<Relative> Relatives)
            : base(firstname, lastname, patronymic, sex, dateOfBirth, passport, address, phoneNumber)
        {
            trainingDirection = TrainingDirection;
            levelEducation = LevelEducation;
            concession = Concession;
            this.dateOfRegistration = dateOfRegistration;
            postcode = Postcode;
            placeOfStudy = PlaceOfStudy;
            diploma = Diploma;
            scoreFirstTest = ScoreFirstTest;
            scoreSecondTest = ScoreSecondTest;
            scoreThirdTest = ScoreThirdTest;
            email = Email;
            additionalInformation = AdditionalInformation;
            this.Contracts = Contracts;
            this.Documents = Documents;
            this.Relatives = Relatives;
        }


        // без списков (договоры, документы, родственники) для созд нов объектов и записи в бд
        public Enrollee(string firstname, string lastname, string patronymic, string sex, DateTime dateOfBirth, Passport passport,
             string address, string phoneNumber, TrainingDirection TrainingDirection, LevelEducation LevelEducation, Concession Concession,
            DateTime dateOfRegistration, string Postcode, PlaceOfStudy PlaceOfStudy, Diploma Diploma, double? ScoreFirstTest,
           double? ScoreSecondTest, double? ScoreThirdTest, string Email, string AdditionalInformation)
             : base(firstname, lastname, patronymic, sex, dateOfBirth, passport, address, phoneNumber)
        {
            trainingDirection = TrainingDirection;
            levelEducation = LevelEducation;
            concession = Concession;
            this.dateOfRegistration = dateOfRegistration;
            postcode = Postcode;
            placeOfStudy = PlaceOfStudy;
            diploma = Diploma;
            scoreFirstTest = ScoreFirstTest;
            scoreSecondTest = ScoreSecondTest;
            scoreThirdTest = ScoreThirdTest;
            email = Email;
            additionalInformation = AdditionalInformation;
        }


        #endregion

        #region Методы

        // удвление из бд
        public override void Delete()
        {
            // подключение
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

            SqlCommand deleteEnrolleeCommand = null;

            // удаление контрактов
            foreach (Contract c in Contracts)
            {
                c.Delete();
            }

            // удал док
            foreach (Document d in Documents)
            {
                d.Delete();
            }

            // удал родст
            foreach (Relative r in Relatives)
            {
                r.Delete();
            }

            // удление абитуриента
            deleteEnrolleeCommand = new SqlCommand("DELETE FROM [Enrollee] WHERE [IdEnrollee]=@Id", sqlConnection);
            deleteEnrolleeCommand.Parameters.AddWithValue("Id", Id);
            deleteEnrolleeCommand.ExecuteNonQuery();

        }

        // добавить в базу
        public override void Add()
        {
            // строка подключеия к бд
            string connectionString = Connection.ConnectionString;

            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            // Объекты для для просмотра фактического состояния таблицы в БД
            //DataSet dsShowDB = new DataSet();

            // заполнение объекта dst
            adapter = new SqlDataAdapter("SELECT * FROM Enrollee", connectionString);
            adapter.Fill(dst, "Enrollee");

            // Создаем пустую рабочую строку
            DataRow row = (dst.Tables["Enrollee"]).NewRow();

            row[1] = TrainingDirection.Id;
            row[2] = LevelEducation.Id;

            if (Concession == null)
                row[3] = DBNull.Value;
            else
                row[3] = Concession.Id;

            row[4] = DateOfRegistration;
            row[5] = Firstname;
            row[6] = Lastname;
            row[7] = Patronymic;
            row[8] = Sex;
            row[9] = DateOfBirth;
            row[10] = Passport.Series;
            row[11] = Passport.Number;
            row[12] = Passport.PersonalNumber;
            row[13] = Passport.IssuedBy;
            row[14] = Passport.DateOfIssue;
            row[15] = Passport.DateExpiry;
            row[16] = Address;
            row[17] = Postcode;
            row[18] = PhoneNumber;
            row[19] = Diploma.Education;
            row[20] = PlaceOfStudy.Name;
            row[21] = PlaceOfStudy.Address;
            row[22] = PlaceOfStudy.GraduationDate;
            row[23] = Diploma.Number;
            row[24] = Diploma.Points;

            if (ScoreFirstTest == null)
                row[25] = DBNull.Value;
            else
                row[25] = ScoreFirstTest;

            if (ScoreSecondTest == null)
                row[26] = DBNull.Value;
            else
                row[26] = ScoreSecondTest;

            if (ScoreThirdTest == null)
                row[27] = DBNull.Value;
            else
                row[27] = ScoreThirdTest;

            row[28] = Email;
            row[29] = AdditionalInformation;

            // Добавляем в таблицу рабочую строку
            dst.Tables["Enrollee"].Rows.Add(row);


            new SqlCommandBuilder(adapter);

            // Внести все изменения в базу !!!
            adapter.Update(dst, "Enrollee");
        }

        // обновление данных
        public override void Update()
        {
            // подключение
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

            SqlCommand updataEnrolleeCommand = new SqlCommand("UPDATE [Enrollee] SET [IdDirectionTraining]=@IdDirectionTraining," +
                    " [IdLevelEducation]=@IdLevelEducation, " +
                    " [IdConcession]=@IdConcession," +
                    " [EnrolleeFirstname]=@EnrolleeFirstname," +
                    " [EnrolleeLastname]=@EnrolleeLastname," +
                    " [EnrolleePatronymic]=@EnrolleePatronymic," +
                    " [EnrolleeSex]=@EnrolleeSex," +
                     " [EnrolleeDateOfBirth]=@EnrolleeDateOfBirth," +
                    " [EnrolleePassportSeries]=@EnrolleePassportSeries, " +
                    " [EnrolleePassportNumber]=@EnrolleePassportNumber," +
                    " [EnrolleePassportPersonalNumber]=@EnrolleePassportPersonalNumber," +
                    " [EnrolleePassportIssuedBy]=@EnrolleePassportIssuedBy," +
                    " [EnrolleeDateOfIssue]=@EnrolleeDateOfIssue," +
                    " [EnrolleeDateExpiry]=@EnrolleeDateExpiry," +
                    " [EnrolleeAddress]=@EnrolleeAddress," +
                    " [EnrolleePostcode]=@EnrolleePostcode," +
                    " [EnrolleePhoneNumber]=@EnrolleePhoneNumber," +
                    " [EnrolleeEducation]=@EnrolleeEducation," +
                    " [EnrolleeLastPlaceOfStudy]=@EnrolleeLastPlaceOfStudy," +
                     " [EnrolleeAddressLastPlaceOfStudy]=@EnrolleeAddressLastPlaceOfStudy," +
                    " [EnrolleeGraduationDate]=@EnrolleeGraduationDate, " +
                    " [EnrolleeNumberCertificateOrDiploma]=@EnrolleeNumberCertificateOrDiploma," +
                    " [EnrolleeAverageGradeOfCertificateOrDiploma]=@EnrolleeAverageGradeOfCertificateOrDiploma," +
                    " [EnrolleeScoreOfTheFirstEntranceTest]=@EnrolleeScoreOfTheFirstEntranceTest," +
                    " [EnrolleeScoreOfTheSecondEntranceTest]=@EnrolleeScoreOfTheSecondEntranceTest," +
                    " [EnrolleeScoreOfTheThirdEntranceTest]=@EnrolleeScoreOfTheThirdEntranceTest," +
                    " [EnrolleeEmail]=@EnrolleeEmail, " +
                      " [EnrolleeAdditionalInformation]=@EnrolleeAdditionalInformation " +
                    " WHERE [IdEnrollee]=@IdEnrollee", sqlConnection);   // Запрос на изменение по Id

            updataEnrolleeCommand.Parameters.AddWithValue("IdEnrollee", Id);
            updataEnrolleeCommand.Parameters.AddWithValue("IdDirectionTraining", TrainingDirection.Id);
            updataEnrolleeCommand.Parameters.AddWithValue("IdLevelEducation", LevelEducation.Id);

            if (Concession != null)
                updataEnrolleeCommand.Parameters.AddWithValue("IdConcession", Concession.Id);
            else
                updataEnrolleeCommand.Parameters.AddWithValue("IdConcession", DBNull.Value);

            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeFirstname", Firstname);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeLastname", Lastname);

            if (Patronymic != null || Patronymic != "")
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePatronymic", Patronymic);
            else
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePatronymic", DBNull.Value);

            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeSex", Sex);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeDateOfBirth", Convert.ToDateTime(DateOfBirth));
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePassportSeries", Passport.Series);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePassportNumber", Passport.Number);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePassportPersonalNumber", Passport.PersonalNumber);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePassportIssuedBy", Passport.IssuedBy);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeDateOfIssue", Convert.ToDateTime(Passport.DateOfIssue));
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeDateExpiry", Convert.ToDateTime(Passport.DateExpiry));
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeAddress", Address);

            if (Postcode != null)
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePostcode", Postcode.ToString());
            else
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePostcode", DBNull.Value);

            if (PhoneNumber != null)
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePhoneNumber", PhoneNumber.ToString());
            else
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleePhoneNumber", DBNull.Value);


            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeEducation", Diploma.Education);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeLastPlaceOfStudy", PlaceOfStudy.Name);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeAddressLastPlaceOfStudy", PlaceOfStudy.Address);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeGraduationDate", Convert.ToDateTime(PlaceOfStudy.GraduationDate));
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeNumberCertificateOrDiploma", Diploma.Number);
            updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeAverageGradeOfCertificateOrDiploma", Convert.ToDouble(Diploma.Points));

            if (ScoreFirstTest != null)
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeScoreOfTheFirstEntranceTest", Convert.ToDouble(ScoreFirstTest));
            else
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeScoreOfTheFirstEntranceTest", DBNull.Value);

            if (ScoreSecondTest != null)
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeScoreOfTheSecondEntranceTest", Convert.ToDouble(ScoreSecondTest));
            else
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeScoreOfTheSecondEntranceTest", DBNull.Value);

            if (ScoreThirdTest != null)
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeScoreOfTheThirdEntranceTest", Convert.ToDouble(ScoreThirdTest));
            else
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeScoreOfTheThirdEntranceTest", DBNull.Value);

            if (Email != null)
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeEmail", Email.ToString());
            else
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeEmail", DBNull.Value);

            if (AdditionalInformation != null)
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeAdditionalInformation", AdditionalInformation.ToString());
            else
                updataEnrolleeCommand.Parameters.AddWithValue("EnrolleeAdditionalInformation", DBNull.Value);

            try
            {
                // обновление
                updataEnrolleeCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // счит с базы 
        public static List<Enrollee> ReadDataEnrollee()
        {
            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            // счит всех необх донных
            List<TrainingDirection> trainingDirections_ = TrainingDirection.ReadToEndListSpec();
            List<LevelEducation> levelEducations_ = LevelEducation.ReadToEndDataInList();
            List<Concession> concessions_ = Concession.ReadToEndDataInList();
            List<Contract> contracts_ = Contract.ReadToEndDataInList();
            List<Document> documents_ = Document.ReadToEndDataInList();
            List<Relative> relatives_ = Relative.ReadToEndDataInList();

            List<Enrollee> en = new List<Enrollee>();

            adapter = new SqlDataAdapter("SELECT * FROM Enrollee", Connection.ConnectionString);
            adapter.Fill(dst, "Enrollee");

            foreach (DataTable dt in dst.Tables)
            {
               // перебор всех строк таблицы
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;

                    // атрибуты объекта
                    Passport passport1 = new Passport((string)cells[10], (string)cells[11], (string)cells[12], (string)cells[13], (DateTime)cells[14], (DateTime)cells[15]);
                    PlaceOfStudy placeOfStudies1 = new PlaceOfStudy((string)cells[20], (string)cells[21], (DateTime)cells[22]);
                    Diploma diploma1 = new Diploma((string)cells[23], (double)cells[24], (string)cells[19]);

                    TrainingDirection trainingDirection1 = null;
                    LevelEducation levelEducation1 = null;
                    Concession concession1 = null;

                    // атрибуьы кажд объекта
                    List<Contract> contracts1 = new List<Contract>();
                    List<Document> documents1 = new List<Document>();
                    List<Relative> relatives1 = new List<Relative>();

                    // опр напр подготовки
                    foreach (TrainingDirection t in trainingDirections_)
                    {
                        if ((int)cells[1] == t.Id)
                            trainingDirection1 = t;
                    }

                    // опр ур образ
                    foreach (LevelEducation t in levelEducations_)
                    {
                        if ((int)cells[2] == t.Id)
                            levelEducation1 = t;
                    }

                    // опр льготы
                    foreach (Concession t in concessions_)
                    {
                        if (cells[3] != DBNull.Value)
                            if ((int)cells[3] == t.Id)
                                concession1 = t;
                    }

                    // опр документ
                    foreach (Document t in documents_)
                    {
                        if ((int)cells[0] == t.IdEnrollee)
                        {
                            documents1.Add(t);
                        }
                    }

                    // опр контрактов
                    foreach (Contract t in contracts_)
                    {
                        if ((int)cells[0] == t.IdEnrollee)
                        {
                            contracts1.Add(t);
                       }
                    }

                    // опр родственников
                    foreach (Relative t in relatives_)
                    {
                        if ((int)cells[0] == t.IdEnrollee)
                            relatives1.Add(t);
                    }

                    string patronymic1, postcode1, phoneNumber1, email1, additionalInformation1;
                    patronymic1 = postcode1 = phoneNumber1 = email1 = additionalInformation1 = null;
                    double? firstEntranceTest1, secondEntranceTest1, thirdEntranceTest1;
                    firstEntranceTest1 = secondEntranceTest1 = thirdEntranceTest1 = null;

                    if (cells[7] != DBNull.Value)
                        patronymic1 = (string)cells[7];

                    if (cells[17] != DBNull.Value)
                        postcode1 = (string)cells[17];

                    if (cells[18] != DBNull.Value)
                        phoneNumber1 = (string)cells[18];


                    if (cells[28] != DBNull.Value)
                        email1 = (string)cells[28];

                    if (cells[29] != DBNull.Value)
                        additionalInformation1 = (string)cells[29];


                    if (cells[25] != DBNull.Value)
                        firstEntranceTest1 = (double?)cells[25];

                    if (cells[26] != DBNull.Value)
                        secondEntranceTest1 = (double?)cells[26];

                    if (cells[27] != DBNull.Value)
                        thirdEntranceTest1 = (double?)cells[27];

                    en.Add(new Enrollee((int)cells[0], (string)cells[5], (string)cells[6], patronymic1, (string)cells[8], (DateTime)cells[9], passport1,
                        (string)cells[16], phoneNumber1, trainingDirection1, levelEducation1, concession1, (DateTime)cells[4], postcode1,
                        placeOfStudies1, diploma1, firstEntranceTest1, secondEntranceTest1, thirdEntranceTest1, email1, additionalInformation1, contracts1,
                        documents1, relatives1));
                }
            }
            return en;
        }

        // возв id посл абитур
        public int ReturnLastId()
        {
            string connectionString = Connection.ConnectionString;

            // Объекты для хранения и обработки данных в памяти 
            DataSet dst = new DataSet();
            SqlDataAdapter adapter;

            // знач абитуриента
            int id = 0;

            // соед с табл и выгрузка данных
            adapter = new SqlDataAdapter("SELECT * FROM Enrollee", connectionString);
            adapter.Fill(dst, "Enrollee");

            // поиск посл добавленного абит
            foreach (DataTable dt in dst.Tables)
            {
                DataRow dataRow = dt.Rows[dt.Rows.Count - 1];
                var cells = dataRow.ItemArray;

                id = (int)cells[0];
            }

            return id;
        }

        // возв id направ
        public static int RetCountTrDByIdTr(int idTr)
        {
            int countEn = 0;
            List<Enrollee> enrollees = Enrollee.ReadDataEnrollee();
            foreach (Enrollee e in enrollees)
            {
                if (e.TrainingDirection.Id == idTr)
                    countEn++;
            }
            return countEn;
        }

        // метод получен возраста чел
        public override string AgePers()
        {
            // выз баз метод
            string year = base.AgePers();

            return $"Полных лет: {year}";
        }

        // метод подс сумму баллов
        public double AmountOfPoints()
        {
            // сдавось ли 1 вс исп
            if (ScoreFirstTest == null)
                scoreFirstTest = 0;
            // сдавось ли 2 вст исп
            if (ScoreSecondTest == null)
                scoreSecondTest = 0;
            // сдавось ли 3 вст исп
            if (ScoreThirdTest == null)
                scoreThirdTest = 0;

            return Convert.ToDouble(ScoreFirstTest.Value + ScoreSecondTest.Value + ScoreThirdTest.Value + Diploma.Points);
        }

        // возв строку сумму баллов
        public string AmountOfPointsSt()
        {
            return $"Сумма балов: {AmountOfPoints()}";
        }

        // переопределение метода фио
        public override string FIO()
        {
            return "Абитуриент: " + base.FIO();
        }

        // отображение по факультетам
        public static List<Enrollee> SelectByFaculty(string str)
        {
            List<Enrollee> enrollees = Enrollee.ReadDataEnrollee();
            List<Enrollee> newEn = new List<Enrollee>();

            if (str == "Все")
                return enrollees;

            // находим абит уч под на данный факультет
            foreach (Enrollee e in enrollees)
            {
                if (e.TrainingDirection.Specialty_.Faculty.ShortName == str)
                    newEn.Add(e);
            }
            return newEn;
        }

        // отображение по спеу
        public static List<Enrollee> SelectBySpecialty(string fst, string sst)
        {
            List<Enrollee> enrollees = Enrollee.ReadDataEnrollee();
            List<Enrollee> newEn = new List<Enrollee>();

            if (fst == "Все" && sst == "Все")
                return enrollees;

            if (fst == "Все" && sst != "Все")
            {
                // находим абит уч под на данный факультет
                foreach (Enrollee e in enrollees)
                {
                    if (e.TrainingDirection.Specialty_.ShortName == sst)
                        newEn.Add(e);
                }
            }

            if (fst != "Все" && sst != "Все")
            {
                // находим абит уч под на данный факультет
                foreach (Enrollee e in enrollees)
                {
                    if (e.TrainingDirection.Specialty_.ShortName == sst &&
                        e.TrainingDirection.Specialty_.Faculty.ShortName == fst)
                    {
                        newEn.Add(e);
                    }
                }
            }
            return newEn;
        }

        // отображение по форм обуч
        public static List<Enrollee> SelectByFormStudy(string fst, string sst, string fsst)
        {
            List<Enrollee> enrollees = SelectBySpecialty(fst, sst);

            List<Enrollee> newEn = new List<Enrollee>();

            if (fsst == "Все")
                return enrollees;

            // находим абит уч под на данный факультет
            foreach (Enrollee e in enrollees)
            {
                if (e.TrainingDirection.FormStudy_.Name == fsst)
                    newEn.Add(e);
            }

            return newEn;
        }

        // поиск по id
        public static List<Enrollee> SearchById(List<Enrollee> enrollees, string st)
        {
            try
            {
                int id_ = Convert.ToInt32(st);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Укажите id целым числом");
                return enrollees;
            }

            List<Enrollee> newEn = new List<Enrollee>();

            foreach (Enrollee en in enrollees)
            {
                if (en.Id == Convert.ToInt32(st))
                    newEn.Add(en);
            }
            return newEn;
        }

        // поиск по фамилиии
        public static List<Enrollee> SearchByLastname(List<Enrollee> enrollees, string st)
        {
 
            List<Enrollee> newEn = new List<Enrollee>();

            foreach (Enrollee en in enrollees)
            {
                if (en.Lastname.ToLower().Contains(st.ToLower()))
                    newEn.Add(en);
            }

            return newEn;
        }

         // поиск по полу
        public static List<Enrollee> SearchBySex(List<Enrollee> enrollees, string st)
        {
 
            List<Enrollee> newEn = new List<Enrollee>();

            foreach (Enrollee en in enrollees)
            {
                if (en.Sex.ToLower().Contains(st.ToLower()))
                    newEn.Add(en);
            }

            return newEn;
        }

        // поиск по серии паспорта
        public static List<Enrollee> SearchByPassportSeries(List<Enrollee> enrollees, string st)
        {

            List<Enrollee> newEn = new List<Enrollee>();

            foreach (Enrollee en in enrollees)
            {
                if (en.Passport.Series.ToLower().Contains(st.ToLower()))
                    newEn.Add(en);
            }

            return newEn;
        }

        // поиск по номеру паспорта
        public static List<Enrollee> SearchByPassportNumber(List<Enrollee> enrollees, string st)
        {

            List<Enrollee> newEn = new List<Enrollee>();

            foreach (Enrollee en in enrollees)
            {
                if (en.Passport.Number.ToLower().Contains(st.ToLower()))
                    newEn.Add(en);
            }

            return newEn;
        }

        // поиск по дате рожд до
        public static List<Enrollee> SearchByDateOfBirthLow(List<Enrollee> enrollees, string st)
        {

            try
            {
                DateTime dt = Convert.ToDateTime(st);
            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return enrollees;
            }

            List<Enrollee> newEn = new List<Enrollee>();

            foreach (Enrollee en in enrollees)
            {
                if (en.DateOfBirth <=Convert.ToDateTime(st) )
                    newEn.Add(en);
            }

            return newEn;
        }

        // поиск по дате рожд после
        public static List<Enrollee> SearchByDateOfBirthTop(List<Enrollee> enrollees, string st)
        {

            try
            {
                DateTime dt = Convert.ToDateTime(st);
            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return enrollees;
            }

            List<Enrollee> newEn = new List<Enrollee>();

            foreach (Enrollee en in enrollees)
            {
                if (en.DateOfBirth >= Convert.ToDateTime(st))
                    newEn.Add(en);
            }
            return newEn;
        }

        // поиск по дате рег до
        public static List<Enrollee> SearchByDateOfRegistrationLow(List<Enrollee> enrollees, string st)
        {

            try
            {
                DateTime dt = Convert.ToDateTime(st);
            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return enrollees;
            }

            List<Enrollee> newEn = new List<Enrollee>();

            foreach (Enrollee en in enrollees)
            {
                if (en.DateOfRegistration <= Convert.ToDateTime(st))
                    newEn.Add(en);
            }
            return newEn;
        }

        // поиск по дате рег после
        public static List<Enrollee> SearchByDateOfRegistrationTop(List<Enrollee> enrollees, string st)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(st);
            }
            catch (Exception)
            {
                MessageBox.Show("Дата указана неверно");
                return enrollees;
            }
            List<Enrollee> newEn = new List<Enrollee>();

            foreach (Enrollee en in enrollees)
            {
                if (en.DateOfRegistration >= Convert.ToDateTime(st))
                    newEn.Add(en);
            }
            return newEn;
        }

        #endregion
    }
}
