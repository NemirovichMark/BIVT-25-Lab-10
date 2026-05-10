namespace Lab9.Blue
{
    public abstract class Blue
    {
        private string input;

        public string Input => input;

        protected Blue(string text)
        {
            input = text;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            input = text;
            Review(); 
        }
    }
}