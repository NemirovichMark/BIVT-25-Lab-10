using System.Globalization;

namespace Lab9.Blue
{
    public class Task3 : Blue
    {
        private (char, double)[] _output;

        public (char, double)[] Output => _output;

        public Task3(string input) : base(input)
        {
            _output = null;
        }

        public override void Review()
        {
            if (Input == null || Input.Length == 0)
            {
                _output = [];
                return;
            }

            List<char> letters = new List<char>();
            List<int> counts = new List<int>();
            int totalWords = 0;
            int index = 0;

            while (index < Input.Length)
            {
                if (IsWordCharacter(Input[index]))
                {
                    int start = index;

                    while (index < Input.Length && IsWordCharacter(Input[index]))
                    {
                        index++;
                    }

                    if (!IsWordBorderedByDigits(Input, start, index))
                    {
                        char firstLetter = FindFirstLetter(Input, start, index);

                        if (firstLetter != '\0')
                        {
                            totalWords++;
                            AddLetter(firstLetter, letters, counts);
                        }
                    }
                }
                else
                {
                    index++;
                }
            }

            if (totalWords == 0)
            {
                _output = [];
                return;
            }

            Sort(letters, counts);

            _output = new (char, double)[letters.Count];
            for (int i = 0; i < letters.Count; i++)
            {
                _output[i] = (letters[i], counts[i] * 100.0 / totalWords);
            }
        }

        public override string ToString()
        {
            if (_output == null)
            {
                return string.Empty;
            }

            string[] lines = new string[_output.Length];
            for (int i = 0; i < _output.Length; i++)
            {
                lines[i] = _output[i].Item1 + ":" + _output[i].Item2.ToString("F4", CultureInfo.InvariantCulture);
            }

            return string.Join(Environment.NewLine, lines);
        }

        private static char FindFirstLetter(string text, int start, int endExclusive)
        {
            for (int i = start; i < endExclusive; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    return char.ToLower(text[i]);
                }
            }

            return '\0';
        }

        private static void AddLetter(char letter, List<char> letters, List<int> counts)
        {
            for (int i = 0; i < letters.Count; i++)
            {
                if (letters[i] == letter)
                {
                    counts[i]++;
                    return;
                }
            }

            letters.Add(letter);
            counts.Add(1);
        }

        private static void Sort(List<char> letters, List<int> counts)
        {
            for (int i = 0; i < counts.Count - 1; i++)
            {
                for (int j = 0; j < counts.Count - 1 - i; j++)
                {
                    bool mustSwap = counts[j] < counts[j + 1] ||
                                    (counts[j] == counts[j + 1] && letters[j] > letters[j + 1]);

                    if (mustSwap)
                    {
                        (letters[j], letters[j + 1]) = (letters[j + 1], letters[j]);
                        (counts[j], counts[j + 1]) = (counts[j + 1], counts[j]);
                    }
                }
            }
        }
    }
}
