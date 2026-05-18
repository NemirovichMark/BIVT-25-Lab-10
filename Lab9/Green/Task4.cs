using System;
using System.Linq;

namespace Lab9.Green
{
    public class Task4 : Green
    {
        private string[] _output;

        public string[] Output => _output;

        public Task4(string input) : base(input)
        {
            _output = Array.Empty<string>();
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = Array.Empty<string>();
                return;
            }

            string[] names = Input.Split(',');
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Trim();
            }

            for (int i = 0; i < names.Length - 1; i++)
            {
                for (int j = 0; j < names.Length - i - 1; j++)
                {
                    if (CompareStrings(names[j], names[j + 1]) > 0)
                    {
                        string temp = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = temp;
                    }
                }
            }

            _output = names;
        }



        private int CompareStrings(string s1, string s2)
        {
            int minLen = Math.Min(s1.Length, s2.Length);
            for (int i = 0; i < minLen; i++)
            {
                if (s1[i] != s2[i])
                {
                    return s1[i] - s2[i];
                }
            }
            return s1.Length - s2.Length;
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return string.Empty;

            return string.Join(Environment.NewLine, _output);
        }
    }
}