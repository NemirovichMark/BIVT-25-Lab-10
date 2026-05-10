using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string _output; protected (string, char)[] _codes; 
        public string Output => _output; public (string, char)[] Codes => _codes;
        public Task3(string text) : base(text ?? string.Empty)
        {
            _output = string.Empty;
            _codes = Array.Empty<(string, char)>();
        }
        public override void Review()
        {
            if (string.IsNullOrEmpty(Input)) { _output = string.Empty; _codes = Array.Empty<(string, char)>(); return; }
            string[] str = new string[Input.Length];
            int[] bgn = new int[Input.Length];
            int[] ints = new int[Input.Length];
            int c = 0;
            for (int i = 0; i < str.Length - 1; i++)
                if (char.IsLetter(Input[i]) && char.IsLetter(Input[i + 1]))
                { 
                    string pair = "" + Input[i] + Input[i+1];
                    int ind = -1;
                    for (int j = 0; j < c; j++)
                        if (str[j] == pair)
                        { ind = j; break; }
                    if (ind == -1)
                    {
                        str[c] = pair; bgn[c] = i;
                        ints[c++] = 1;
                    }
                    else ints[ind]++;
                }
            int n = c < 5 ? c : 5; bool[] used = new bool[c];
            _codes= new (string, char)[n];
            for (int i = 0; i < n; i++)
            {
                int ind = -1;
                for (int j = 0; j < c; j++)
                {
                    if (used[j]) continue;
                    if (ind == -1 || ints[j] > ints[ind] ||
                        (ints[j] == ints[ind] && bgn[j] < bgn[ind]))
                        //если: пара встретилась впервые; встречается > раз, чем лидер;
                        //..==., но встречается раньше лидера               
                        ind = j;
                }
                _codes[i] = (str[ind], (char)0); used[ind]=true; // пара просмотрена
            }
            bool[] smbls = new bool[127];
            for (int i=0;i<Input.Length;i++)
                if (Input[i]>=32 && Input[i]<=126) smbls[Input[i]]= true;
            int k = 0;
            for (int i = 32; i < 127 && k < n; i++)
                if (!smbls[i])
                { _codes[k] = (_codes[k].Item1, (char)i); k++; }
            string result = Input;
            for (int i = 0; i < _codes.Length; i++)
            {
                string pair = _codes[i].Item1; char smbl=_codes[i].Item2;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                int q = 0;
                while (q < result.Length)
                {
                    if (q < result.Length - 1 && result[q] == pair[0]
                        && result[q + 1] == pair[1])
                    { sb.Append(smbl); q += 2; }
                    else { sb.Append(result[q]); q++; }
                }
                result = sb.ToString();
            }
            _output = result;
        }
        public override string ToString()
        {
            return Output ?? "";
        }
    }
}
