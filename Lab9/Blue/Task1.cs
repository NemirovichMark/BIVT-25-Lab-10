using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task1 : Blue
    {
        private string[] _output;

        public string[] Output
        {
            get
            {
                string[] copy = new string[_output.Length];
                for (int i = 0; i < copy.Length; i++)
                {
                    copy[i] = _output[i];
                }
                return copy;
            }
        }

        public Task1(string input) : base(input)
        {
            _output = new string[0];
        }
        public override void Review()
        {
            string[] words = Input.Split(' ');
            string res = "";
            string line = "";
            for (int i =0; i < words.Length; i++)
            {
                if ((line + words[i]).Length > 50)
                {
                    res += line.Trim() + Environment.NewLine;
                    line = "";
                }
                line += words[i] + " ";
            }
            if (line.Length > 0)
            {
                res += line.Trim();
            }
            _output = res.Split(Environment.NewLine);
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, _output);
        }
    }
}
