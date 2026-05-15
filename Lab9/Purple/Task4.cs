using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        private (string, char)[] _codes;

        public (string, char)[] Codes;
        public Task4(string text, (string, char)[] codes) : base(text)
        {
            _codes = codes;
        }
        public override void Review()
        {
            string result = _text;
            for (int i = 0; i < _codes.Length; i++)
                result = result.Replace(_codes[i].Item2.ToString(), _codes[i].Item1);
            Output = result;
        }
    }
}