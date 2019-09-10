using System;

namespace EnrolleeForms
{
    // паспорт 
    class Passport
    {
        // серия
        string series;

        public string Series
        {
            get
            {
                return series;
            }
        }
        // номер
        string number;

        public string Number
        {
            get
            {
                return number;
            }
        }
        // личный номер
        string personalNumber;

        public string PersonalNumber
        {
            get
            {
                return personalNumber;
            }
        }
        // кем выдан
        string issuedBy;

        public string IssuedBy
        {
            get
            {
                return issuedBy;
            }
        }

        // когда выдан
       private  DateTime dateOfIssue;

        public DateTime DateOfIssue
        {
            get
            {
                return dateOfIssue;
            }
        }
        // годен до
        DateTime dateExpiry;

        public DateTime DateExpiry
        {
            get
            {
                return dateExpiry;
            }
        }

        // конструктор
        public Passport(string series, string number, string personalNumber, string issuedBy, DateTime dateOfIssue, DateTime dateExpiry)
        {
            this.series = series;
            this.number = number;
            this.personalNumber = personalNumber;
            this.issuedBy = issuedBy;
            this.dateOfIssue = dateOfIssue;
            this.dateExpiry = dateExpiry;
        }      
    }
}
