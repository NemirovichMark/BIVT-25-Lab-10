namespace Lab9.Purple
{
    public abstract class Purple
    {
        private string _input;
        public string Input => _input;

        protected char[] Punc => new char[]
            { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' };

        protected Purple(string text)
        {
            _input = text;
        }
        protected Purple(){}

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }
    }
}