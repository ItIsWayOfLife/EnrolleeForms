namespace EnrolleeForms
{
    // работа
    class Work
    {
        // место работы
        private string placeOfWork;

        public string PlaceOfWork
        {
            get
            {
                return placeOfWork;
            }
        }

        // должность
        private string post;

        public string Post
        {
            get
            {
                return post;
            }
        }

        // конструктор
        public Work(string placeOfWork, string post)
        {
            this.placeOfWork = placeOfWork;
            this.post = post;
        }
    }
}
