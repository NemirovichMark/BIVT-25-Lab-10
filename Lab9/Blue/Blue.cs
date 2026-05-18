namespace Lab9.Blue
{
    public abstract class Blue
    {
        private string _input;
        public string Input
        {
            get
            {
                return _input;
            }
        }
        protected Blue(string text)
        {
            _input = text;
        }
        public abstract void Review();
        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }
        protected bool IsWordSymbol(char symbol)
        {
            return char.IsLetter(symbol) || symbol == '-' || symbol == '\'';
        }
        protected string[] ExtractWords()
        {
            if (_input == null)
            {
                return new string[0];
            }
            int count = 0;
            int index = 0;

            while (index < _input.Length)
            {
                while (index < _input.Length && !IsWordSymbol(_input[index]))
                {
                    index++;
                }
                if (index < _input.Length)
                {
                    count++;

                    while (index < _input.Length && IsWordSymbol(_input[index]))
                    {
                        index++;
                    }
                }
            }
            string[] words = new string[count];
            index = 0;
            int wordIndex = 0;

            while (index < _input.Length)
            {
                while (index < _input.Length && !IsWordSymbol(_input[index]))
                {
                    index++;
                }

                if (index < _input.Length)
                {
                    int start = index;

                    while (index < _input.Length && IsWordSymbol(_input[index]))
                    {
                        index++;
                    }

                    words[wordIndex] = _input.Substring(start, index - start);
                    wordIndex++;
                }
            }
            return words;
        }
    }
}
