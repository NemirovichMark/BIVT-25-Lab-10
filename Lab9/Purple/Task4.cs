using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        private string _output;
        private (string, char)[] _codes;
        public string Output => _output;
        public (string, char)[] Codes => _codes;
        public Task4(string input, (string, char)[] table) : base(input)
        {
            _output = default;
            _codes = table;
            Review();
        }

        public override void Review()
        {
            _output = _input;
            foreach (var code in _codes)
            {
                _output = _output.Replace(code.Item2.ToString(), code.Item1);
            }

        }
        public override string ToString() => _output;
    }
}
