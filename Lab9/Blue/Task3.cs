using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task3 : Blue
    {
        private (char, double)[] _output;
        public (char, double)[] Output => _output;

        public Task3(string input) : base(input) { }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = null;
                return;
            }

            var counts = new Dictionary<char, int>();
            int totalWords = 0;

            for (int i = 0; i < Input.Length; i++)
            {
                if (IsWordStart(Input, i))
                {
                    char firstChar = char.ToLower(Input[i]);
                    if (counts.ContainsKey(firstChar))
                        counts[firstChar]++;
                    else
                        counts[firstChar] = 1;
                    totalWords++;
                }
            }

            if (totalWords == 0)
            {
                _output = new (char, double)[0];
                return;
            }

            var list = new List<(char, double)>();
            foreach (var kv in counts)
            {
                double percent = kv.Value * 100.0 / totalWords;
                list.Add((kv.Key, percent));
            }

            _output = list.OrderByDescending(x => x.Item2).ThenBy(x => x.Item1).ToArray();
        }

        private bool IsWordChar(char c) => char.IsLetter(c) || c == '-' || c == '\'';

        private bool IsWordStart(string s, int i)
        {
            if (!IsWordChar(s[i])) return false;
            if (i == 0) return true;
            return !IsWordChar(s[i - 1]) && !char.IsDigit(s[i - 1]);
        }

        public override string ToString()
        {
            if (_output == null) return "";
            var lines = new string[_output.Length];
            for (int i = 0; i < _output.Length; i++)
            {
                lines[i] = $"{_output[i].Item1}:{_output[i].Item2:F4}";
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}
