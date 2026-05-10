namespace Lab9.Purple
{
    public abstract class Purple(string text)
    {
        public string Input { get; private set; } = text;
        public string? Output { get; protected set; }

        public abstract void Review();

        public void ChangeText(string text)
        {
            Input = text;
            Review();
        }
    }
}