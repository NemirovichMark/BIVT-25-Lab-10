namespace Lab9.Blue
{
    public abstract class Blue : Object
    {
        protected string _input;
        public string Input => _input;
        protected Blue(string input)
        {
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
