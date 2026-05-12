using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string _output = "";
        private (string, char)[] _codes;
        public string Output => _output;
        public (string, char)[] Codes => _codes;
        public Task3(string text) : base(text) { }

        public override void Review()
        {
            string[] pairs = new string[Input.Length];
            int[] first_pos = new int[Input.Length];
            int[] counts = new int[Input.Length];
            int n = 0;

            for (int i = 0; i < Input.Length - 1; i++)
            {
                string p = Input[i].ToString() + Input[i + 1].ToString();
                if (!char.IsLetter(p[0]) || !char.IsLetter(p[1])) continue;

                int idx = -1;
                for (int j = 0; j < n; j++)
                {
                    if (pairs[j] == p)
                    {
                        idx = j;
                        break;
                    }
                }
                if (idx >= 0) counts[idx]++;
                else
                {
                    pairs[n] = p;
                    first_pos[n] = i;
                    counts[n] = 1;
                    n++;
                }
            }
            var sorted = Enumerable.Range(0, n) //создает  i
                .Select(i => new { pair = pairs[i], firstPos = first_pos[i], count = counts[i] })
                .OrderByDescending(x => x.count)
                .ThenBy(x => x.firstPos)
                .Take(5)
                .ToArray();

            bool[] used = new bool[127];
            foreach (char c in Input)
            {
                if (c >= 32 && c <= 126) used[c] = true;
            }
            char[] codes = new char[sorted.Length];
            int ci = 0;
            char codeChar = ' ';
            while (codeChar <= 126 && ci < sorted.Length)
            {
                if (!used[codeChar])
                    codes[ci++] = codeChar;
                codeChar++;
            }
            _codes = new (string, char)[sorted.Length];
            string result = Input;
            for (int i = 0; i < sorted.Length; i++)
            {
                _codes[i] = (sorted[i].pair, codes[i]);
                result = result.Replace(sorted[i].pair, codes[i].ToString());
            }

            _output = result;
        }

        public override string ToString()
        {
            return _output;
        }
    }
}



