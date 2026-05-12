using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        private string _output; protected (string, char)[] _codes;
        public string Output => _output; public (string, char)[] Codes => _codes;
        public Task4(string text, (string, char)[] codes) : base(text??string.Empty)
        {
            _output = string.Empty; _codes = codes ?? Array.Empty<(string, char)>(); ;
        }
        public override void Review()
        {
            if (string.IsNullOrEmpty(_input))
            {_output = string.Empty; return; }
            if (_codes == null || _codes.Length == 0)
            {_output = _input; return; }
            string str = _input;
            for (int i = 0; i < _codes.Length; i++)
                str = str.Replace(_codes[i].Item2 + "", _codes[i].Item1);
            _output = str;

        }
        public override string ToString()
        {
            return _output;
        }
    }
}
