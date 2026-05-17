namespace Lab9.Purple
{
    public abstract class Purple
    {
        protected string _input;
        public string Input => _input;
        public string Output { get; protected set; }
        protected Purple(string text) { _input = text; }
        public abstract void Review();
        public void ChangeText(string text)
        {
            _input = text;
            Review();
        }
    }
}
