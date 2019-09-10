using System;

namespace EnrolleeForms
{
    // человек
    abstract  class Person
    {
    // id
        protected int id;

        public int Id
        {
            get
            {
                return id;
            }
        }

        // имя
        protected string firstname;

        public string Firstname
        {
            get
            {
                return firstname;
            }
        }
        // фамилия
        protected string lastname;

        public string Lastname
        {
            get
            {
                return lastname;
            }
        }

        // отчетсво
        protected string patronymic;

        public string Patronymic
        {
            get
            {
                return patronymic;
            }
        }

        // пол
        protected string sex;

        public string Sex
        {
            get
            {
                return sex;
            }
        }

        // дата рождения
        protected DateTime dateOfBirth;

        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
        }

        // паспорт
        protected Passport passport;

        public Passport Passport
        {
            get
            {
                return passport;
            }
        }

        // адрес
        protected string address;

        public string Address
        {
            get
            {
                return address;
            }
        }

        // номер тел
        protected string phoneNumber;

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
        }

        // конструктор для счит
        public Person(int id, string firstname, string lastname, string patronymic, string sex,
            DateTime dateOfBirth, Passport passport, string address, string phoneNumber)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.patronymic = patronymic;
            this.passport = passport;
            this.sex = sex;
            this.dateOfBirth = dateOfBirth;
            this.passport = passport;
            this.address = address;
            this.phoneNumber = phoneNumber;
        }

        // конструктор для созд нов объекто и записи в бд
        public Person( string firstname, string lastname, string patronymic, string sex,
            DateTime dateOfBirth, Passport passport, string address, string phoneNumber)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.patronymic = patronymic;
            this.passport = passport;
            this.sex = sex;
            this.dateOfBirth = dateOfBirth;
            this.passport = passport;
            this.address = address;
            this.phoneNumber = phoneNumber;
        }

        public abstract void Delete();

        public abstract void Update();

        public abstract void Add();

        // метод получен возраста чел
        public virtual string AgePers()
        {

            DateTime dateNow = DateTime.Now;
            int year = dateNow.Year - DateOfBirth.Year;
            if (dateNow.Month < DateOfBirth.Month ||
                (dateNow.Month == DateOfBirth.Month && dateNow.Day < DateOfBirth.Day)) year--;

            return Convert.ToString(year);
        }

        // метод возвращает фамилию и инициалы
        public virtual string FIO()
        {
            string lastname_ = " ";
            string name_ = " ";
            string patr_ = " ";

            if (Lastname != null && Lastname.Length>=1)
                lastname_ = Lastname;
            if (Firstname != null && Firstname.Length>=1)
                name_ = Firstname;

            if (Patronymic != null && Patronymic.Length>=1)
                patr_ = Patronymic;

            return lastname_ + " " + name_[0] + ". " + patr_[0] + "."; 
        }
    }
}
