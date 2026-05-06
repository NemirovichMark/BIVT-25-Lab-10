using System;
using System.Linq;

namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char, double)[] _output;
        public (char, double)[] Output => _output;

        private string _alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public Task1(string input) : base(input)
        {
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = new (char, double)[0];
                return;
            }

            char[] uniqueLetters = Input.ToLower()
                                        .Where(c => _alph.Contains(c))
                                        .Distinct()
                                        .OrderBy(c => c)
                                        .ToArray();

            _output = new (char, double)[uniqueLetters.Length];

            double totalLettersCount = Input.Count(c => char.IsLetter(c));

            if (totalLettersCount == 0) return;

            for (int i = 0; i < uniqueLetters.Length; i++)
            {
                char currentLetter = uniqueLetters[i];
                double count = Input.Count(c => char.ToLower(c) == currentLetter);

                _output[i] = (currentLetter, count / totalLettersCount);
            }
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;

            string value = "";
            for (int i = 0; i < Output.Length - 1; i++)
            {
                value += $"{Output[i].Item1}:{(Output[i].Item2):F4}\n";
            }

            value += $"{Output[Output.Length - 1].Item1}:{(Output[Output.Length - 1].Item2):F4}";

            return value;
        }
    }
}