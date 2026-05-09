using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        private string[] _output;

        public string[] Output => _output;
        public Task2(string input) : base(input)
        {
            _output = new string[0];
        }

        public override void Review()
        {
            if (string.IsNullOrWhiteSpace(Input))
            {
                _output = new string[0];
                return;
            }

            int width = 50;

            string[] words = Input.Split(' ');

            string[] tempLines = new string[words.Length];
            int lineCount = 0;

            int i = 0;

            while (i < words.Length)
            {
                string[] lineWords = new string[words.Length];
                int wordCount = 0;
                int currentLength = 0;

                while (i < words.Length)
                {
                    string word = words[i].Trim();
                    int newLength;

                    if (wordCount == 0)
                    {
                        newLength = word.Length;
                    }
                    else
                    {
                        newLength = currentLength + 1 + word.Length;
                    }

                    if (newLength <= width)
                    {
                        lineWords[wordCount] = word;
                        wordCount++;
                        currentLength = newLength;
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }

                string line = "";
                if (wordCount == 1)
                {
                    line = lineWords[0];
                }
                else
                {
                    int lettersCount = 0;
                    for (int j = 0; j < wordCount; j++)
                    {
                        lettersCount = lettersCount + lineWords[j].Length;
                    }

                    int spacesCount = width - lettersCount;
                    int gaps = wordCount - 1;

                    int baseSpace = spacesCount / gaps;
                    int extraSpaces = spacesCount % gaps;

                    StringBuilder sb = new StringBuilder();

                    for (int j = 0; j < wordCount; j++)
                    {
                        sb.Append(lineWords[j]);

                        if (j < gaps)
                        {
                            int spaces = baseSpace;

                            if (j < extraSpaces)
                            {
                                spaces++;
                            }

                            sb.Append(' ', spaces);
                        }
                    }

                    line = sb.ToString();
                }

                tempLines[lineCount] = line;
                lineCount++;
            }

            _output = new string[lineCount];
            for (int j = 0; j < lineCount; j++)
            {
                _output[j] = tempLines[j];
            }
        }
        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return "";

            string result = _output[0];

            for (int i = 1; i < _output.Length; i++)
            {
                result += Environment.NewLine + _output[i];
            }

            return result;
        }

    }
}
