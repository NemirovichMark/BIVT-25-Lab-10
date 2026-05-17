using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        private string _output = "";
        private (string, char)[] _codes;
        public string Output => _output;
        public (string, char)[] Codes => _codes;
        public Task4(string input, (string, char)[] codes) : base(input)
        {
            _codes = codes;
        }
        public override void Review()
        {
            string text = Input;
            foreach (var code in _codes)
            {
                text = text.Replace(code.Item2.ToString(), code.Item1);
            }
            _output = text;
        }
        public override string ToString()
        {
            return _output;
        }
    }
}
