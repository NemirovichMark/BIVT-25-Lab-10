using System.Text;

namespace Lab9.Blue
{
    public class Task2 : Blue
    {
        private string _sequence;
        private string _output;

        public string Output => _output;

        public Task2(string inputText, string sequence) : base(inputText)
        {
            _sequence = sequence;
            _output = null;
        }

        public override void Review()
        {
            if (Input == null || Input.Length == 0)
            {
                _output = string.Empty;
                return;
            }

            if (_sequence == null || _sequence.Length == 0)
            {
                _output = NormalizeSpaces(Input);
                return;
            }

            string loweredSequence = _sequence.ToLower();
            StringBuilder builder = new StringBuilder();
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

                    string word = Input.Substring(start, index - start);
                    bool borderedByDigits = IsWordBorderedByDigits(Input, start, index);
                    bool shouldDelete = !borderedByDigits && word.ToLower().Contains(loweredSequence);

                    if (!shouldDelete)
                    {
                        builder.Append(word);
                    }
                }
                else
                {
                    builder.Append(Input[index]);
                    index++;
                }
            }

            _output = NormalizeSpaces(builder.ToString());
        }

        public override string ToString()
        {
            return _output ?? string.Empty;
        }

        private static string NormalizeSpaces(string text)
        {
            if (text == null || text.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            bool previousWasSpace = false;

            for (int i = 0; i < text.Length; i++)
            {
                char symbol = text[i];

                if (symbol == ' ')
                {
                    if (!previousWasSpace && builder.Length > 0)
                    {
                        builder.Append(symbol);
                        previousWasSpace = true;
                    }
                }
                else
                {
                    if (IsPunctuationWithoutSpaceBefore(symbol) && builder.Length > 0 && builder[^1] == ' ')
                    {
                        builder.Length--;
                    }

                    builder.Append(symbol);
                    previousWasSpace = false;
                }
            }

            return builder.ToString().Trim(' ');
        }

        private static bool IsPunctuationWithoutSpaceBefore(char symbol)
        {
            return symbol == '.' || symbol == ',' || symbol == '!' || symbol == '?' ||
                   symbol == ':' || symbol == ';' || symbol == ')' || symbol == ']';
        }
    }
}
