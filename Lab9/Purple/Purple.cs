namespace Lab9.Purple
{
    public abstract class Purple
    {
        protected static char[] _prep;
        protected string _input;
        public string Input => _input;

        static Purple()
        {
            _prep = new char[] { '.', '!', '?', ',', ':', '\"', ';', '-', '(', ')', '[', ']', '{', '}', '/' };
        }
        protected Purple(string input)
        {
            if (input == null)
            {
                _input = "";
                return;
            }
            _input = input;

        }
        public abstract void Review();


        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }


    }
}