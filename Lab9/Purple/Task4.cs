using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        private string _output;
        protected (string, char)[] _codes;
        public string Output => _output;
        public (string, char)[] Codes => _codes;

        public Task4(string input, (string, char)[] codes) : base(input)
        {
            _output = "";
            _codes = codes;
        }
        public override void Review()
        {
            string input = _input;
            for (int i = 0; i < _codes.Length; i++)
            {
                input = input.Replace(_codes[i].Item2 + "", _codes[i].Item1);
            }
            _output = input;
        }
        public override string ToString()
        {
            return _output;
        }
    }
}

