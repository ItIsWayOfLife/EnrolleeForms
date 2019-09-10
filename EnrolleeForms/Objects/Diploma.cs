namespace EnrolleeForms
{
    // аттестат или диплом
    class Diploma 
    {
        // номер
        protected string number;

        public string Number
        {
            get
            {
                return number;
            }
        }
        // баллы
        protected double points;

        public double Points
        {
            get
            {
                return points;
            }
        }


        // образование
        private string education;

        public string Education
        {
            get
            {
                return education;
            }
        }

        // конструктор
        public Diploma(string number, double points, string education )         
        {
            this.number = number;
            this.points = points;
            this.education = education;
        }          
    }
}
