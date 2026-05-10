using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task1 : Blue
    {
        private string[] _output;

        public string[] Output => _output.ToArray();
        public Task1(string input) : base(input)
        {
            _output = new string[0];
        }

        public override void Review()
        {
            _output = new string[0];
            var t = String.Split(_input);
            var s = "";
            foreach (var i in t)
            {
                if(s.Length + i.Length + 1 <= 50)
                {
                    s += (s.Length == 0?"":" ") + i;
                }
                else
                {
                    String.Add(ref _output, s);
                    s = i;
                }
            }
            String.Add(ref _output, s);
        }
        public override string ToString()
        {
            return String.Join(_output, Environment.NewLine);
        }
    }
}
