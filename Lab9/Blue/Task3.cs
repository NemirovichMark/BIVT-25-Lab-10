namespace Lab9.Blue
{
    public class Task3 : Blue
    {
        public Task3(string input) : base(input) {}
        public (char, double)[] Output { get; private set; }

        public override string ToString()
        {
            if (Output == null) return "";
            string[] lines = new string[Output.Length];

            for (int i = 0; i < Output.Length; i++)
            {
                lines[i] = $"{Output[i].Item1}:{Output[i].Item2:F4}";
            }

            return string.Join(Environment.NewLine, lines);
        }

        public override void Review()
        {
            if (Input == null)
            {
                Output = null;
                return;
            }
            int wordCount = 0;
            for (int i = 0; i < Input.Length; i++)
            {
                if (IsWordStart(Input, i)) wordCount++;
                
            }
            char[] firstLetters = new char[wordCount];
            int index = 0;
            for (int i = 0; i < Input.Length; i++)
            {
                if (IsWordStart(Input, i))
                {
                    char c = char.ToLower(Input[i]);
                    firstLetters[index++] = c;
                }
            }

            char[] uniqueLetters = firstLetters.Distinct().ToArray();

            int[] counts = new int[uniqueLetters.Length];
            for (int i = 0; i < uniqueLetters.Length; i++)
            {
                for (int j = 0; j < firstLetters.Length; j++)
                {
                    if (uniqueLetters[i] == firstLetters[j])
                        counts[i]++;
                }
            }
            
            Output = new (char, double)[uniqueLetters.Length];
            for (int i = 0; i < uniqueLetters.Length; i++)
            {
                double percent = counts[i] * 100.0 / firstLetters.Length;
                Output[i] = (uniqueLetters[i], percent);
            }

            Output = Output
                .OrderByDescending(x => x.Item2)
                .ThenBy(x => x.Item1)
                .ToArray();
        }

        private bool IsWordChar(char c)
        {
            return char.IsLetter(c) || c == '-'  || c == '\'';
        }

        private bool IsWordStart(string text, int index)
        {
            if (!IsWordChar(text[index])) return false;
            if (index == 0) return true;
            if (char.IsDigit(text[index - 1])) return false;
            return !IsWordChar(text[index - 1]);
        }
    }
}