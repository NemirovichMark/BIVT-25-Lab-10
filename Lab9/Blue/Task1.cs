using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task1 : Blue
    {
        public Task1(string input) : base(input) { }
        private string[] _output;
        public string[] Output => _output;
        public override void Review()
        {
            if (Input == null)
            {
                _output = null;
                return;
            }
            string[] words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string current = "";
            int count = 0;
            foreach (var i in words)
            {
                if (current.Length == 0)
                {
                    current += i;
                }
                else if (current.Length + 1 + i.Length <= 50)
                {
                    current += " " + i;
                }
                else
                {
                    count++;
                    current = i;
                }
            }
            if (current.Length > 0) count++;

            _output = new string[count];
            int ind = 0;
            current = "";
            foreach (var i in words)
            {
                if (current.Length == 0)
                {
                    current += i;
                }
                else if (current.Length + 1 + i.Length <= 50)
                {
                    current += " " + i;
                }
                else
                {
                    _output[ind++] = current;
                    current = i;
                }
            }
            if (current.Length > 0) _output[ind] = current;
        }
        public override string ToString()
        {
            if (_output == null) return string.Empty;
            return string.Join(Environment.NewLine, _output);
        }

    }
}
