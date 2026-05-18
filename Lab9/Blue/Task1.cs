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
        public string[] Output => _output;

        public Task1(string text) : base(text)
        {
            _output = new string[0]; 
        }

        public override void Review()
        {

            string[] words = Input.Split(' ');

            int rowCount = 1;
            int currentLength = 0;

            foreach (string word in words)
            {
                int wordWithSpace;

                if (currentLength == 0)
                {
                    wordWithSpace = word.Length;
                }
                else
                {
                    wordWithSpace = word.Length + 1;
                }

                if (currentLength + wordWithSpace > 50)
                {
                    rowCount++; 
                    currentLength = word.Length;
                }
                else
                {
                    currentLength += wordWithSpace;
                }
            }

            _output = new string[rowCount];

            int currentRow = 0;
            currentLength = 0;
            string currentLine = "";

            foreach (string word in words)
            {
                if (currentLine == "")
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
                    _output[currentRow] = currentLine;
                    currentRow++;
                    currentLine = word;
                    currentLength = word.Length;
                }
            }
            if (currentLine != "")
            {
                _output[currentRow] = currentLine;
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _output);
        }
    }
}
