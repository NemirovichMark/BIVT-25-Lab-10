using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char Char, double Count)[] _output;
        public (char, double)[] Output => _output.ToArray();

        public Task1(string input) : base(input)
        {
            _output = new (char, double)[33];
        }

        public override void Review()
        {
            _output = new (char, double)[33];
            string alfavit = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            double countinput = Input.Count(c => char.IsLetter(c));
            for (int i = 0; i < alfavit.Length; i++)
            {
                char chacha = alfavit[i];
                _output[i].Char = chacha;
                double count = Input.Where(c => char.ToLower(c) == chacha).Count();
                _output[i].Count = (count / countinput);
            }

            (char Char, double Count)[] _output2 = new (char, double)[33];
            _output2 = _output.Where(_output => _output.Count > 0).ToArray();
            _output = _output2;
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < _output.Length; i++)
            {
                res += $"{_output[i].Char}:{_output[i].Count:F4}\n";
                
            }
            return res.TrimEnd();
        }
    }
}