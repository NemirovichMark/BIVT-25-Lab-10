using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string _output;
        private (string, char)[] _codes;
        private char[] use_symbol = new char[0];
        public string Output => _output;
        public (string, char)[] Codes => _codes;
        public Task3(string text) : base(text)
        {

            _output = default(string);
            Review();
        }
        public void Svobodni_sym()
        {
            bool[] ascii_ = new bool[127];
            for (int i = 0; i < _input.Length; i++)
            {
                int code = (int)_input[i];

                if (code >= 32 && code <= 126)
                    ascii_[code] = true;
            }
            int k = 0;
            for (int i = 32; i < ascii_.Length; i++)
                if (ascii_[i] == false)
                {
                    Array.Resize(ref use_symbol, use_symbol.Length + 1);
                    use_symbol[k] = (char)i;
                    k++;
                }
        }
        public override void Review()
        {
            _codes = new (string, char)[5];
            string[] dve_buk = new string[0];
            for (int i = 0; i < _input.Length - 1; i++)
            {
                if (char.IsLetter(_input[i]) && char.IsLetter(_input[i + 1]))
                {
                    Array.Resize(ref dve_buk, dve_buk.Length + 1);
                    dve_buk[^1] = _input.Substring(i, 2);
                }
            }
            var sort = dve_buk
                .Select((b, index) => new { buk = b, Index = index })
    .GroupBy(x => x.buk)
    .OrderByDescending(g => g.Count())
    .ThenBy(g => g.Min(x => x.Index))
    .Select(g => g.Key)
    .Take(5).ToArray();

            Svobodni_sym();


            int k = 0;
            for (int i = 0; i < sort.Length; i++)
            {
                _codes[i] = (sort[i], use_symbol[k]);
                k++;
            }
            _output = _input;
            foreach (var code in _codes)
            {
                _output = _output.Replace(code.Item1, code.Item2.ToString());
            }



        }
        public override string ToString() => _output;
    }
}
