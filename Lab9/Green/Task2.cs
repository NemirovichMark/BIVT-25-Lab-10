using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task2 : Green
    {
        private char[] _output;
        private (char CH, int C)[] _count;
        public char[] Output => _output.ToArray();
        public Task2(string input) : base(input)
        {
            _output = new char[59];
            _count = new (char, int)[59];

        }

        public override void Review()
        {
            string alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz";
            double countinput = Input.Count(c => char.IsLetter(c));

            for (int i = 0; i < alph.Length; i++)
            {
                char a = alph[i];
                _count[i].CH = a;
            }

            for (int i = 0; i < Input.Length; i++)
            {
                if (char.IsLetter(Input[i]))
                {
                    if (i == 0)
                    {
                        int j = Array.FindIndex(_count, item => item.CH == char.ToLower(Input[i]));
                        _count[j].C++;
                    }

                    else
                    {
                        if (char.IsLetter(Input[i - 1]) == false)
                        {
                            if (Input[i - 1] != '-' && Input[i - 1] != '\'' && Input[i - 1] != '`' && char.IsNumber(Input[i - 1]) == false) 
                            {
                                int j = Array.FindIndex(_count, item => item.CH == char.ToLower(Input[i]));
                                _count[j].C += 1;
                            }
                        }
                    }
                }
            }

            _output = _count
                .Where(item => item.C > 0)
                .OrderByDescending(item => item.C)
                .ThenBy(item => item.CH)
                .Select(item => item.CH)
                .ToArray();
        }
        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < _output.Length; i++)
            {
                res += _output[i] + ", ";

            }

            return res.TrimEnd()[..^1];
        }
    }
}
