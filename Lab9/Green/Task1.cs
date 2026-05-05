using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char Letter, double Count)[] _output;
        public (char, double)[] Output => _output.ToArray();

        public Task1(string input) : base(input)
        {
            _output = new (char, double)[33];
        }

        public override void Review()
        {
            _output = new (char, double)[33];
            string ABC = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            double countinput = Input.Count(c => char.IsLetter(c));
            for (int i = 0; i < ABC.Length; i++)
            {
                char letter = ABC[i];
                _output[i].Letter = letter;
                double count = Input.Where(c => char.ToLower(c) == letter).Count();
                _output[i].Count = (count / countinput);
            }

            (char Letter, double Count)[] _output2 = new (char, double)[33];
            _output = _output.Where(_output => _output.Count > 0).ToArray();
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < _output.Length; i++)
            {

                res += $"{_output[i].Letter}:{_output[i].Count:F4}\n";

            }
            return res.TrimEnd();
        }
    }
}