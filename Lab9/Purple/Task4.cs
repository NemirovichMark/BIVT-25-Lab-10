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
            _codes = codes;
        }

        public override void Review()
        {
            _output = Input;

            for (int i = 0; i < Codes.Length; i++)
            {
                int j = 0;

                while (j < _output.Length)
                {
                    if (_output[j] == Codes[i].Item2)
                    {
                        string replacement = Codes[i].Item1;

                        _output = _output.Remove(j, 1);
                        _output = _output.Insert(j, replacement);

                        j += replacement.Length;
                    }
                    else
                    {
                        j++;
                    }
                }
            }
        }

        public override string ToString()
        {
            return _output;
        }
    }
}
