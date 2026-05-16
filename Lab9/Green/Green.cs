namespace Lab9.Green
{
    public abstract class Green
    {
        private string _input;
        public string Input => _input;
        protected static char[] _punc;
        protected Green(string input)
        {
            _input = input;
        }
        static Green()
        {
            _punc = new char[] { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' };
        }
        public abstract void Review();
        public virtual void ChangeText(string input)
        {
            _input = input;
            Review();
        }
    }
}