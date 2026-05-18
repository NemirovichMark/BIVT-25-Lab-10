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
        private (string, char)[] _table;

        public string Output => _output;

        public Task4(string input, (string, char)[] table) : base(input)
        {
            _output = default;
            _table = table ?? new (string, char)[0];
        }

        public override string ToString()
        {
            return _output ?? "";
        }

        public override void Review()
        {
            if (_input == null)
            {
                _output = "";
                return;
            }

            if (_table == null || _table.Length == 0)
            {
                _output = _input;
                return;
            }

            var codes = _table.ToDictionary(item => item.Item2, item => item.Item1);
            _output = string.Concat(_input.Select(ch => codes.TryGetValue(ch, out var pair) ? pair : ch.ToString()));
        }
    }
}
