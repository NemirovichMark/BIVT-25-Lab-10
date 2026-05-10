using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char, double)[] _output;
        public (char, double)[] Output => _output;
        public Task1(string input) : base(input)
        {
            _output = new (char, double)[0];
        }
        public override void Review()
        {
            string text = Input.ToLower();
            int count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    count++;
                }
            }
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            int uniqueCount = 0;
            foreach (char letter in alphabet)
            {
                bool found = false;
                foreach (char symbol in text)
                {
                    if (symbol == letter)
                    {
                        found = true; break;
                    }
                }
                if (found)
                    uniqueCount++;
            }

            _output = new (char, double)[uniqueCount];
            int index = 0;
            for (int i = 0; i < alphabet.Length; i++)
            {
                int k = 0; 
                for (int j = 0; j < text.Length; j++)
                {
                    if (text[j] == alphabet[i]) k++;
                }
                if (k > 0)
                {
                    double result = (double)k / count;
                    _output[index] = (alphabet[i], result);
                    index++;
                }
            }
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < _output.Length; i++)
            {
                char letter = _output[i].Item1; 
                double value = _output[i].Item2;
                

                result = result + letter + ":" + value.ToString("F4");
                if (i < _output.Length - 1)
                {
                    result = result + "\n";
                }
            }
            return result;
        }
    }
}