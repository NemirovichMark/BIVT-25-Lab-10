namespace Lab9.Purple
{
    public abstract class Purple
    {
        protected Purple(string input)
        {
            Input = input;
        }
        public string Input { get; private set; }
        public abstract void Review();
        public virtual void ChangeText(string text)
        {
            Input = text;
            //Review();
        }
    }
}