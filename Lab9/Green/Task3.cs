using System;
using System.Linq;

namespace Lab9.Green
{
    public class Task3 : Green
    {
        private string _pattern;
        private string[] _output;

        public string[] Output => _output;

        public Task3(string input, string pattern) : base(input)
        {
            _pattern = pattern;
            _output = Array.Empty<string>();
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input) || string.IsNullOrEmpty(_pattern))
            {
                _output = Array.Empty<string>();
                return;
            }

            char[] separators = Input
                .Where(c => char.IsPunctuation(c) || char.IsWhiteSpace(c))
                .Distinct()
                .ToArray();

            string[] words = Input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string patternLower = _pattern.ToLower();

            _output = words
                .Where(word => word.ToLower().Contains(patternLower))
                .GroupBy(word => word.ToLower())
                .Select(group => group.First())
                .ToArray();
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return string.Empty;

            return string.Join(Environment.NewLine, _output);
        }
    }
}