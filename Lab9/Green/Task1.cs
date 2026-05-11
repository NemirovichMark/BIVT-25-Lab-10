using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char, double)[] _output;

        public (char, double)[] Output
        {
            get
            {
                (char, double)[] copy = new (char, double)[_output.Length];
                for (int i = 0; i < _output.Length; i++)
                {
                    copy[i] = _output[i];
                }
                return copy;
            }
        }

        public Task1(string input) : base(input)
        {
            _output = null;
        }

        public override void Review()
        {
            if (Input == null)
            {
                _output = null;
                return;
            }

            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            double[] count = new double[alphabet.Length];       // считаем буквы в соответствующим им индексам (для а ++1 в 0 индексе, для б ++1 в 1 и т.д.)
            double totalCount = 0;

            foreach (char c in Input.ToLower())
            {
                if (char.IsLetter(c))
                totalCount++;
                if (alphabet.Contains(c))
                {
                    count[alphabet.IndexOf(c)]++;
                }
            }

            int uniqueCount = 0;
            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] > 0)
                    uniqueCount++;
            }

            _output = new (char, double)[uniqueCount];

            int index = 0;
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (count[i] > 0)
                {
                    _output[index] = (alphabet[i], count[i] / totalCount);
                    index++;
                }
            }
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return string.Empty;

            return string.Join("\n", _output.Select(item => $"{item.Item1}:{item.Item2:F4}"));
        }
    }
}