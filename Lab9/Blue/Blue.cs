namespace Lab9.Blue
{
    public abstract class Blue
    {
        public string Input { get; protected set; }
        protected Blue(string input) => Input = input;
        public abstract void Review();
        public virtual void ChangeText(string text)
        {
            Input = text;
            Review();
        }
    }
}