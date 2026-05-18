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
        private (string, char)[] _codes;
        public string Output => _output;
        public (string, char)[] Codes => _codes;
        public Task4(string input, (string, char)[] codes) : base(input)
        {
            _output = "";
            if (codes != null && codes.Length != 0)
            {
                _codes = new (string, char)[codes.Length];
                for (int i = 0; i < codes.Length; i++) _codes[i] = codes[i];
            }
        }

        public override void Review()
        {
            string result = Input;

            for (int i = 0; i < _codes.Length; i++)
            {
                string pair = _codes[i].Item1;
                char code = _codes[i].Item2;

                result = result.Replace(code.ToString(), pair);
            }

            _output = result;
        }
        public override string ToString()
        {
            return _output;
        }
    }
}
