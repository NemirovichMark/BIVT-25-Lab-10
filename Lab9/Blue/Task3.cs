using System;
using System.Globalization;
using System.Security.Cryptography;

namespace Lab9.Blue
{
    public class Task3 : Blue
    {
        private (char let, double per)[] _output = new (char, double)[0];

        public Task3(string input) : base(input) { }

        public override void Review()
        {
            _output = new (char let, double per)[0];
            int count = 0;
            string[] words = Input.Split(' ');

            foreach (string word in words)
            {
                foreach (char ch in word)
                {
                    if (Char.IsDigit(ch))
                    {
                        break;
                    }
                    if (Char.IsLetter(ch))
                    {
                        count++;
                        bool b = true;
                        for (int i = 0; i < _output.Length; i++)
                        {
                            if (_output[i].let == Char.ToLower(ch))
                            {
                                _output[i].per++;
                                b = false;
                            }
                        }
                        if (b)
                        {
                            Array.Resize(ref _output, _output.Length + 1);
                            _output[^1] = (Char.ToLower(ch), 1);
                        }
                        break;
                    }
                }
            }
            for (int i = 0; i < _output.Length; i++)
            {
                _output[i].per = Math.Round(_output[i].per / count * 100.0, 4);
            }

            for (int i = 0; i < _output.Length - 1; i++)
            {
                for (int j = i + 1; j < _output.Length; j++)
                {
                    if (_output[i].per < _output[j].per || (_output[i].per == _output[j].per && _output[i].let > _output[j].let))
                    {
                        var temp = _output[i];
                        _output[i] = _output[j];
                        _output[j] = temp;
                    }
                }
            }
        }

        public (char, double)[] Output => _output;

        public override string ToString()
        {
            string[] res = new string[_output.Length];
            for (int i = 0; i < _output.Length; i++)
            {
                res[i] = $"{_output[i].let}:{_output[i].per.ToString("F4")}";
            }
            return string.Join(Environment.NewLine, res);
        }
    }
}