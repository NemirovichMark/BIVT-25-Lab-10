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
            string[] words = _input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int lineCount = 0, currentLength = 0, wordCount = 0;
            for (int i = 0; i < words.Length; i++)
            {
                int space = 0;
                if (wordCount != 0) space = 1;
                
                if (currentLength + space + words[i].Length <= 50)
                {
                    currentLength += space + words[i].Length;
                    wordCount++;
                }
                else
                {
                    lineCount++;
                    currentLength = words[i].Length;
                    wordCount = 1;
                }
            }

            if (wordCount > 0) lineCount++;
            _output = new string[lineCount];

            int currentWordIndex = 0;
            for (int i = 0; i < lineCount; i++)
            {
                int start = currentWordIndex;
                int countInLine = 0;
                int lettersOnlyLen = 0;

                while (currentWordIndex < words.Length)
                {
                    if (lettersOnlyLen + countInLine + words[currentWordIndex].Length <= 50)
                    {
                        lettersOnlyLen += words[currentWordIndex].Length;
                        countInLine++;
                        currentWordIndex++;
                    }
                    else break;
                }

                if (countInLine == 1)
                {
                    _output[i] = words[start];
                }
                else
                {
                    int totalSpaces = 50 - lettersOnlyLen;
                    int gaps = countInLine - 1;
                    int baseSpaces = totalSpaces / gaps;
                    int extraSpaces = totalSpaces % gaps;

                    string line = "";
                    for (int j = 0; j < countInLine; j++)
                    {
                        line += words[start + j];
                        if (j < gaps)
                        {
                            int spacesCount = baseSpaces;
                            if (j < extraSpaces)
                                spacesCount += 1;

                            for (int k = 0; k < spacesCount; k++) line += " ";
                        }
                    }
                    _output[i] = line; 
                }
            }
        }
        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return "";

            string output = "";
            for (int i = 0; i < _output.Length; i++)
            {
                output += _output[i];
                if (i < _output.Length - 1) output += Environment.NewLine;
            }
            return output;
        }
    }
}
