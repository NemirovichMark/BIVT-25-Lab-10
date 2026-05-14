using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char CH, double C)[] _output;
        public (char, double)[] Output => _output;

        public Task1(string input) : base(input)
        {
            _output = new (char, double)[33];
        }

        public override void Review()
        {
            _output = new (char, double)[33];
            string alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            double inputC = Input.Count(a => char.IsLetter(a));
            for (int i= 0; i<alph.Length; i++)
            {
                char c = alph[i];
                _output[i].CH = c;
                double count = Input.Where(a => char.ToLower(a) == c).Count();
                _output[i].C = count / inputC;
            }
            (char, double)[] f = new (char, double)[33];
            f=_output.Where(_output=>_output.C > 0).ToArray();
            _output = f;
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < _output.Length; i++)
            {
                result += $"{_output[i].CH}:{_output[i].C:F4}\n";

            }
            return result.TrimEnd();
        }
    }

    
}
