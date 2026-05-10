namespace Lab9.Purple
{
    public abstract class Purple
    {
        public string Input { get; private set; }

        protected Purple(string text)
        {
            Input = text;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            Input = text;
            Review();
        }
    }
}