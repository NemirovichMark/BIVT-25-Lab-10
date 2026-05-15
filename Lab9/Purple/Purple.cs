namespace Lab9.Purple
{
    public abstract class Purple
    {
        protected string _text;

        public string Input => _text;
        public string Output { get; protected set; }

        protected Purple(string Text)
        {
            _text = Text;
        }
        public abstract void Review();

        public void ChangeText(string text)
        {
            _text = text;
            Review();
        }
        public override string ToString()
        {
            return Output;
        }
    }
}