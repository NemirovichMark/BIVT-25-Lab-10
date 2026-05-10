using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab9.Blue
{
    public class Task1 : Blue
    {
        private string[] _output = Array.Empty<string>();
        public string[] Output => (string[])_output.Clone();

        public Task1(string input) : base(input)
        {
            _output = new string[0];
        }

        public override void Review()
        {
            var words = Input.Split();
            string str = "";
            int k = 0;
            foreach (var word in words)
            {
                if (str.Length + word.Length + Math.Sign(str.Length) > 50)
                {
                    Array.Resize(ref _output, _output.Length + 1);
                    _output[k] = str;
                    k++;
                    str = word;
                }
                else
                {
                    if (str.Length > 0)
                    {
                        str += " " + word;
                    }
                    else
                    {
                        str += word;
                    }
                }
            }
            if (str.Length != 0)
            {
                Array.Resize(ref _output, _output.Length + 1);
                _output[k] = str;
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _output);
        }
    }
}