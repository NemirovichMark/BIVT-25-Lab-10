namespace Lab10.Blue
{
    public abstract class Blue
    {
        private string _input;
        public string Input => _input ?? string.Empty;

        protected Blue(string text)
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
