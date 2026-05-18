using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task1 : Blue
    {
        private string[] _output;

        public string[] Output
        {
            get
            {
                return _output;
            }
        }

        public Task1(string text) : base(text)
        {
            _output = null;
        }

        public override void Review()
        {
            string[] words = ExtractWords();

            if (words == null || words.Length == 0)
            {
                _output = new string[0];
                return;
            }

            int linesCount = 1;
            int currentLength = 0;

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                if (word == null)
                {
                    continue;
                }

                if (currentLength == 0)
                {
                    currentLength = word.Length;
                }
                else if (currentLength + 1 + word.Length <= 50)
                {
                    currentLength += 1 + word.Length;
                }
                else
                {
                    linesCount++;
                    currentLength = word.Length;
                }
            }

            _output = new string[linesCount];
            currentLength = 0;
            int lineIndex = 0;
            string currentLine = string.Empty;

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                if (word == null)
                {
                    continue;
                }

                if (currentLength == 0)
                {
                    currentLine = word;
                    currentLength = word.Length;
                }
                else if (currentLength + 1 + word.Length <= 50)
                {
                    currentLine += " " + word;
                    currentLength += 1 + word.Length;
                }
                else
                {
                    _output[lineIndex] = currentLine;
                    lineIndex++;
                    currentLine = word;
                    currentLength = word.Length;
                }
            }

            _output[lineIndex] = currentLine;
        }

        public override string ToString()
        {
            if (_output == null)
            {
                return string.Empty;
            }

            if (_output.Length == 0)
            {
                return string.Empty;
            }

            string result = string.Empty;

            for (int i = 0; i < _output.Length; i++)
            {
                if (_output[i] == null)
                {
                    continue;
                }

                if (result.Length > 0)
                {
                    result += "\n";
                }

                result += _output[i];
            }

            return result;
        }
    }
}
