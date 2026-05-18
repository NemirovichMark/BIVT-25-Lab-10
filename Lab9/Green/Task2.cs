using System;
using System.Linq;

namespace Lab9.Green
{
    public class Task2 : Green
    {
        private char[] _output = null!;
        public char[] Output => _output;

        public Task2(string input) : base(input)
        {
        }

        public override void Review()
        {
            string text = Input;

            if (string.IsNullOrEmpty(text))
            {
                _output = new char[0];
                return;
            }

            char[] delimiters = text.Where(c =>
                (char.IsSeparator(c) || char.IsPunctuation(c)) && c != '-' && c != '\''
            ).ToArray();

            string[] words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            _output = words
                .Where(w => w.Length > 0 && char.IsLetter(w[0]))
                .Select(w => char.ToLower(w[0]))
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .Select(g => g.Key)

                .ToArray();
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;

            return string.Join(", ", _output);
        }
    }
}