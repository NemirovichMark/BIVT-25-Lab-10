namespace Lab9.White
{
    public abstract class White
    {
        private string _input;

        public string Input => _input;

        protected White(string text)
        {
            _input = text ?? string.Empty;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text ?? string.Empty;
            Review();
        }
    }
}