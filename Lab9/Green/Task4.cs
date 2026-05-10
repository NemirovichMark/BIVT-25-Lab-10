using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task4 : Green
    {
        private string[] _output;
        public string[] Output => _output;

        public Task4(string input) : base(input)
        {
            _output = new string[0];
        }

        public override void Review()
        {
            string[] names = Input.Split(',');
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Trim(' ');
            }

            for (int i = 0; i < names.Length - 1; i++)
            {
                for (int j = 0; j < names.Length - i - 1; j++)
                {
                    string s1 = names[j].ToLower();
                    string s2 = names[j + 1].ToLower();
                    bool flag = false;

                    int min = s1.Length;
                    if (s2.Length < min)
                        min = s2.Length;

                    for (int k = 0; k < min; k++)
                    {
                        if (s1[k] > s2[k]) 
                        {
                            flag = true;
                            break;
                        }
                        if (s1[k] < s2[k])
                        {
                            flag = false;
                            break;
                        }
                        if (s1[k] == s2[k] && s1.Length > s2.Length)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        (names[j], names[j + 1]) = (names[j + 1], names[j]);
                    }
                }
            }
            _output = names;
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return "";

            string result = "";
            for (int i = 0; i < _output.Length; i++)
            {
                result += _output[i];
                if (i < _output.Length - 1)
                {
                    result += Environment.NewLine;
                }
            }
            return result;
        }
    }
}
