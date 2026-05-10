using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task4 : Blue
    {
        private int _output;
        public int Output => _output;

        public Task4(string input):base(input)
        {
            _output = 0;
        }

        public override void Review()
        {
            var ans = 0;
            var t = String.SplitAll(_input)
                .Where(x => String.OnlyNumbers(x) > 0)
                .Select(x => String.OnlyNumbers(x))
                .ToArray();
            foreach(var x in t) Console.WriteLine(x);
            foreach (var x in t) ans += x;
            _output = ans;
        }
        public override string ToString()
        {
            return $"{Output}";
        }
    }
}
