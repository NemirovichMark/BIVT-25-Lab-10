using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task2 : Blue
    {
        private string _sequence;
        private string _output;

        public string Output
        {
            get
            {
                return _output;
            }
        }

        public string Sequence => _sequence;

        public Task2(string text, string sequence) : base(text)
        {
            _sequence = sequence;
            _output = null;
        }

        public override void Review()
        {
            if (Input == null)
            {
                _output = null;
                return;
            }

            string source = Input;
            char[] buffer = new char[source.Length];
            int bufferLength = 0;
            int index = 0;

            while (index < source.Length)
            {
                if (IsWordSymbol(source[index]))
                {
                    int start = index;

                    while (index < source.Length && IsWordSymbol(source[index]))
                    {
                        index++;
                    }

                    string word = source.Substring(start, index - start);
                    bool removeWord = false;

                    if (_sequence != null && word != null)
                    {
                        removeWord = word.Contains(_sequence);
                    }

                    if (!removeWord)
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            buffer[bufferLength] = word[i];
                            bufferLength++;
                        }
                    }
                }
                else
                {
                    buffer[bufferLength] = source[index];
                    bufferLength++;
                    index++;
                }
            }

            string intermediate = new string(buffer, 0, bufferLength);
            _output = NormalizeSpaces(intermediate);
        }

        private string NormalizeSpaces(string text)
        {
            if (text == null)
            {
                return null;
            }

            char[] buffer = new char[text.Length];
            int length = 0;
            bool previousIsSpace = true;

            for (int i = 0; i < text.Length; i++)
            {
                char symbol = text[i];
                bool currentIsSpace = symbol == ' ' || symbol == '\n' || symbol == '\r' || symbol == '\t';

                if (currentIsSpace)
                {
                    if (!previousIsSpace)
                    {
                        buffer[length] = ' ';
                        length++;
                    }

                    previousIsSpace = true;
                }
                else
                {
                    buffer[length] = symbol;
                    length++;
                    previousIsSpace = false;
                }
            }

            if (length > 0 && buffer[length - 1] == ' ')
            {
                length--;
            }

            return new string(buffer, 0, length);
        }

        public override string ToString()
        {
            return _output ?? string.Empty;
        }
    }
}
