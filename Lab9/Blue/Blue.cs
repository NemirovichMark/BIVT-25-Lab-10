namespace Lab9.Blue
{
    public abstract class Blue
    {
        private string _input;

        public string Input => _input;

        protected Blue(string input)
        {
            _input = input;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }

        protected static bool IsWordCharacter(char symbol)
        {
            return char.IsLetter(symbol) || symbol == '-' || symbol == '\'' || symbol == '`';
        }

        protected static bool IsWordBorderedByDigits(string text, int start, int endExclusive)
        {
            bool digitBefore = start > 0 && char.IsDigit(text[start - 1]);
            bool digitAfter = endExclusive < text.Length && char.IsDigit(text[endExclusive]);
            return digitBefore || digitAfter;
        }
    }
}
