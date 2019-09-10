namespace EnrolleeForms
{
    // структурная единица (университета)
    abstract class StructuralUnit
    {
        protected int id;      
        public int Id
        {
            get
            {
                return id;
            }
        }
        // полное название
        protected string fullName;
        public string FullName
        {
            get
            {
                return fullName;
            }
        }
        // сокр назв
        protected string shortName;       
        public string ShortName
        {
            get
            {
                return shortName;
            }
        }
        // конструктор
        protected StructuralUnit(int id, string fullName, string shortName)
        {
            this.id = id;
            this.fullName = fullName;
            this.shortName = shortName;
        }

        // метод выв инфор
        public abstract string Info();
    }
}
