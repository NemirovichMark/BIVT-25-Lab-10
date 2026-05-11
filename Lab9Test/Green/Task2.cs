using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task2 : Green
    {
        private char[] _output;

        public char[] Output 
        { get
            {
                char[] copy =  new char[_output.Length];
                for (int i = 0; i < _output.Length; i++)
                {
                    copy[i] = _output[i];
                }
                return copy; 
            } 
        }

        public Task2(string input) : base(input)
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
            string inputLower = Input.ToLower();
            char[] punc = { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' };
            string[] words = inputLower.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(word => word.Trim(punc))
                                       .Where(word => word.Length > 0)
                                       .ToArray();
            char[] firstLetters = new char[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                firstLetters[i] = words[i][0];
            }

            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz";
            int[] count = new int[alphabet.Length];       // считаем буквы в соответствующим им индексам (для а ++1 в 0 индексе, для б ++1 в 1 и т.д.)
            double totalCount = 0;

            foreach (char c in firstLetters)
            {
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

            (char, double)[] frequency = new (char, double)[uniqueCount];

            int index = 0;
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (count[i] > 0)
                {
                    frequency[index] = (alphabet[i], count[i]);
                    index++;
                }
            }

            _output = frequency.OrderByDescending(item => item.Item2)
                               .ThenBy(item =>  item.Item1)
                               .Select(item => item.Item1)
                               .ToArray();
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return string.Empty;

            return string.Join(", ", _output);
        }
    }
}
