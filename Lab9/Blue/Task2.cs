using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task2 : Blue
    {
        private string _substr;
        private string _output;

        public string Output => _output;
        public string Substr => _substr;

        public Task2(string input, string substr) : base(input)
        {
            _substr = substr;
            _output = "";
        }

        public override void Review()
        {
            var t1 = String.Split(_input);
            var ans = new string[1];
            for (int i = 0; i < t1.Length; i++)
            {
                if (String.Find(t1[i], _substr))
                {
                    if (String.OnlyPunct(t1[i]) == "\"\"," || String.OnlyPunct(t1[i]) == "\"\".")
                    {
                        String.Add(ref ans, String.OnlyPunct(t1[i]));
                        continue;
                    }
                    ans[ans.Length - 1] += String.OnlyPunct(t1[i]);
                    continue;
                }
                String.Add(ref ans, t1[i]);
            }
            _output = String.Join(ans, ' ', true);
        }
        public override string ToString()
        {
            return _output;
        }
    }
}
