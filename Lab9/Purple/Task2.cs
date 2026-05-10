using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        private string[] _output;
        public string[] Output => _output;
        public Task2(string text) : base(text)
        {
            _output = default;
            Review();
        }

        public override void Review()
        {
            string pattern = @"\s+";
            string[] result = Regex.Split(_input, pattern);
            string[] ans = new string[0];
            int c = 0;
            while (c < result.Length)
            {
                Array.Resize(ref ans, ans.Length + 1);
                ans[^1] = result[c];
                int len = result[c].Length;
                int j = c + 1;

                while (j < result.Length && len + 1 + result[j].Length <= 50)
                {
                    ans[^1] += " " + result[j];
                    len += 1 + result[j].Length;
                    j++;
                }

                c = j;
            }
            Console.WriteLine(string.Join("\n", ans));
            string pat = @"\s";
            int r = 0;
            foreach (string s in ans)
            {
                if (r > ans.Length - 1) break;
                int kol_dob_symbol = 50 - ans[r].Length;
                int kol_prob = ans[r].Count(m => m == ' ');
                if (kol_prob == 0)
                {
                    r++;
                    continue;
                }
                ans[r] = Regex.Replace(s, pattern, m => m.Value + new string(' ', kol_dob_symbol / kol_prob));
                int ost = kol_dob_symbol%kol_prob;
                int t = 0;
                int last = -1;
                string pu = ans[r];
                while (ost > 0)
                {
                    if (t >= pu.Length)
                    {
                        t = 0;
                    }
                        
                    if (pu[t] == ' ' && pu[t+1] != ' ')
                    {
                        pu = pu.Insert(t+1, " ");
                        ost--;
                        t +=2;
                    }
                    t++;
                }
                ans[r] = pu;
                r++;

            }
            _output = ans;
        

        }
        public override string ToString()
        {
            if (_output == null)
            {
                return "";
            }
            string ans = _output[0];
            return string.Join(Environment.NewLine, _output);
        }
    }
}
