namespace Lab9.White
{
    public abstract class White
    {
        public string Input { get; private set; }

        protected White(string input)
        {
            Input = input;
        }
    
        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            Input = text;
            Review();
        }
    }
}
