using System;


namespace EnrolleeForms
{
    // прошл место учебы
    class PlaceOfStudy
    {
        // название
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        // адрес
        private string address;

        public string Address
        {
            get
            {
                return address;
            }
        }

        // дата окончания
        private DateTime graduationDate;

        public DateTime GraduationDate
        {
            get
            {
                return graduationDate;
            }
        }

        // конструктор
        public PlaceOfStudy(string name, string address, DateTime graduationDate)
        {
            this.name = name;
            this.address = address;
            this.graduationDate = graduationDate;
        }
    }
}
