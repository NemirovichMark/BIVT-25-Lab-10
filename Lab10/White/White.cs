namespace Lab9.White
{
    public abstract class White
    {
        private string _input;
        public string Input => _input;

        protected White(string input)
        {
            _input = input;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }
        public virtual string GetDataType()
        {
            return GetType().Name;
        }
    }
}