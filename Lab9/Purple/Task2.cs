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
        
        public Task2(string text) : base(text) { }
        
        private string AddSpace(string text)
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int gapCount = words.Length - 1;

            if (gapCount <= 0) 
                return text;

            int totalLetters = 0;
            foreach (string word in words)
                totalLetters += word.Length;
            
            int totalSpaces = 50 - totalLetters;

            for (int i = 0; i < gapCount; i++)
            {
                words[i] += ' ';
                totalSpaces--;
            }
            
            for (int i = 0; i < totalSpaces; i++)
            {
                words[i % gapCount] += ' ';
            }
            
            return string.Join("", words);
        }

        public override void Review()
        {
            if (string.IsNullOrWhiteSpace(Input))
            {
                _output = new string[0];
                return;
            }
            
            int currentLength = 0;
            string[] result = new string[0];
            string[] allWords = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            StringBuilder lineBuilder = new StringBuilder();
            lineBuilder.Append(allWords[0]);
            currentLength += allWords[0].Length;
            
            for (int i = 1; i < allWords.Length; i++)
            {
                if (currentLength + 1 + allWords[i].Length <= 50)
                {
                    lineBuilder.Append(' ');
                    lineBuilder.Append(allWords[i]);
                    currentLength += allWords[i].Length + 1;
                }
                else
                {
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = AddSpace(lineBuilder.ToString());

                    lineBuilder = new StringBuilder(allWords[i]);
                    currentLength = lineBuilder.Length;
                }
            }
            
            if (lineBuilder.Length != 0)
            {
                Array.Resize(ref result, result.Length + 1);
                result[result.Length - 1] = AddSpace(lineBuilder.ToString());
            }

            _output = result.ToArray();
        }

        public override string ToString()
        {
            return _output == null ? string.Empty : string.Join(Environment.NewLine, _output);
        }
    }
}
