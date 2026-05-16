namespace Lab9.Green
{
    public abstract class Green
    {
        private string _input;

        public string Input => _input;

        protected Green(string text)
        {
            if (text != null)
                _input = text;
            else
                _input = "";
        }


        public virtual void ChangeText(string text)
        {
            if (text != null)
                _input = text;
            else
                _input = "";
            Review();
        }

        public abstract void Review();
    }
}
