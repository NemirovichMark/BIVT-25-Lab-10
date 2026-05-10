using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task1 : Blue
    {
        private string[] output;
        public string[] Output
        {
            get
            {
                if (output == null)
                    return new string[0];
                return output;
            }
        }
        public Task1(string text) : base(text)
        {
            output = new string[0];
        }

        public override void Review()
        {
            if (Input == null)
            {
                output = new string[0];
                return;
            }

            
            string[] words = Input.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            string[] tempLines = new string[words.Length];
            int count = 0;
            string currentLine = "";

            foreach (string word in words)
            {
                if (currentLine == "")
                {
                    currentLine = word;
                }
                else if (currentLine.Length + 1 + word.Length <= 50)
                {
                    currentLine += " " + word;
                }
                else
                {
                    tempLines[count++] = currentLine;
                    currentLine = word;
                }
            }

            if (currentLine != "")
                tempLines[count++] = currentLine;

            
            output = new string[count];
            for (int i = 0; i < count; i++)
                output[i] = tempLines[i];
        }

        public override string ToString()
        {
            if (output == null || output.Length == 0) return "";

            string result = output[0];
            for (int i = 1; i < output.Length; i++)
            {
                
                result += "\r\n" + output[i];
            }
            return result; 
        }

    }
}

