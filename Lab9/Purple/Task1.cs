using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        private string _output;
        public string Output => _output;
        public override string ToString() => _output;
        public Task1(string input) : base(input) { }
        public override void Review()
        {
            char[] ch = { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', ' ' };
            string w = "";
            string rw = "";
            bool rev = true;

            for (int i = 0; i < Input.Length; i++)
            {
                if (ch.Contains(Input[i]))
                {
                    if (Input[i] == ',' && char.IsDigit(Input[i - 1]) && char.IsDigit(Input[i + 1]) && (i + 1) < Input.Length)
                    {
                        w += Input[i];
                    }
                    else
                    {
                        if (rev)
                        {
                            rw = "";
                            for (int j = 0; j < w.Length; j++)
                            {
                                rw += w[w.Length - j - 1];
                            }
                            _output += rw + Input[i];
                            w = "";
                        }
                        else
                        {
                            _output += w + Input[i];
                            w = "";
                            rev = true;
                        }
                    }
                }
                else if (char.IsDigit(Input[i]))
                {
                    w += Input[i];
                    rev = false;
                }
                else
                {
                    w += Input[i];
                }
            }
        }
    }
}
