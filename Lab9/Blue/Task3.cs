namespace Lab9.Blue
{
    public class Task3 : Blue
    {
        public Task3(string input) : base(input)
        {
        }
        private (char, double)[] output;
        public (char, double)[] Output
        {
            get
            {
                return output;
            }
            private set
            {
                output = value;
            }
        }
        private bool IsWordChar(char c)
        {
            return char.IsLetter(c) || c == '-'  || c == '\'';
        }

        private bool IsWordStart(string text, int index)
        {
            if (!IsWordChar(text[index]))
            {
                return false;
            }

            if (index == 0)
            {
                return true;
            }

            if (char.IsDigit(text[index - 1]))
            {
                return false;
            }
            
            return !IsWordChar(text[index - 1]);
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

            for (int i = 0; i < Output.Length - 1; i++)
            {
                for (int j = 0; j < Output.Length - 1 - i; j++)
                {
                    bool needSwap = false;
                    if (Output[j].Item2 < Output[j + 1].Item2)
                    {
                        needSwap = true;
                    }
                    else if (Output[j].Item2 == Output[j + 1].Item2)
                    {
                        if (Output[j].Item1 > Output[j + 1].Item1)
                        {
                            needSwap = true;
                        }
                    }

                    if (needSwap)
                    {
                        (Output[j], Output[j + 1]) = (Output[j + 1], Output[j]);
                    }
                }
            }
        }
        
        public override string ToString()
        {
            if (Output == null || Output.Length == 0)
            {
                return "";
            }

            string result = "";
            for (int i = 0; i < Output.Length; i++)
            {
                result += Output[i].Item1;
                result += ":";
                result += Output[i].Item2.ToString("F4");
                if (i < Output.Length - 1)
                {
                    result += "\n";
                }
            }
            return result;
        }
    }
}
