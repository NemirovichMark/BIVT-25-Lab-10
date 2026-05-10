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
        public string Sequence => _sequence;
        public Task2(string input, string sequence) : base(input)
        {
            _sequence = sequence;
        }
        private string _output;     
        public string Output => _output;
        public override string ToString()
        {
            if (_output == null)
                return "";
            return _output;
        }
        public override void Review()
        {
            if (Input == null || _sequence == null)
            {
                _output = Input;
                return;
            }
            string[] parts = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int length = 0;
            foreach (string part in parts)
            {
                int start = 0;
                while (start < part.Length && !IsWordChar(part[start]))
                    start++;
                int end = part.Length - 1;
                while (end >= 0 && !IsWordChar(part[end]))
                    end--;
                if (start > end)
                {
                    length++;
                    continue;
                }

                string prefix = part.Substring(0, start);
                string word = part.Substring(start, end - start + 1);
                string suffix = part.Substring(end + 1);

                if (word.Contains(_sequence, StringComparison.OrdinalIgnoreCase))
                {
                    string newPart = prefix + suffix;
                    if (newPart.Length > 0) length++;
                }
                else length++;
            }

            string[] result = new string[length];
            int ind = 0;
            foreach (string part in parts)
            {
                int start = 0;
                while (start < part.Length && !IsWordChar(part[start]))
                    start++;
                int end = part.Length - 1;
                while (end >= 0 && !IsWordChar(part[end]))
                    end--;
                if (start > end)
                {
                    result[ind++] = part;
                    continue;
                }

                string prefix = part.Substring(0, start);
                string word = part.Substring(start, end - start + 1);
                string suffix = part.Substring(end + 1);

                if (word.Contains(_sequence, StringComparison.OrdinalIgnoreCase))
                {
                    string newPart = prefix + suffix;
                    if (newPart.Length > 0) result[ind++] = newPart;
                }
                else result[ind++] = part;
            }

            _output = String.Join(" ", result);
            _output = Output.Replace(" .", ".");
            _output = Output.Replace(" ,", ",");
            _output = Output.Replace(" !", "!");
            _output = Output.Replace(" ?", "?");
            _output = Output.Replace(" :", ":");
            _output = Output.Replace(" ;", ";");
        }
        private bool IsWordChar(char c)
        {
            return char.IsLetter(c) || c == '-' || c == '\'';
        }

    }
}
