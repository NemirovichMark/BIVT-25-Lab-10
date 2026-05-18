namespace Lab9.Purple
{
    public abstract class Purple
    {
        private string _str;

        public string Input => _str;
        
        protected Purple(string str)
        {
            _str = str;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _str = text;
            Review();
        }
    }
}