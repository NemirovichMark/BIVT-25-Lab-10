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

        public string[] Output
        {
            get
            {
                if (_output == null)
                    return null;

                string[] copy = new string[_output.Length];
                for (int i = 0; i < _output.Length; i++)
                {
                    copy[i] = _output[i];
                }
                return copy;
            }
        }

        public Task4(string input) : base(input)
        {
            _output = null;
        }
        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = null;
                return;
            }

            string[] surnames = Input.Split(',')
                                     .Select(s => s.Trim())
                                     .Where(s => s.Length > 0)
                                     .ToArray();

            for (int i = 0; i < surnames.Length - 1; i++)
            {
                for (int j = 0; j < surnames.Length - i - 1; j++)
                {
                    if (Compare(surnames[j], surnames[j + 1]) > 0)
                    {
                        (surnames[j], surnames[j + 1]) = (surnames[j + 1], surnames[j]);
                    }
                }
            }

            _output = surnames;
        }

        private int Compare(string a, string b)
        {
            a = a.ToLower(); b = b.ToLower();
            int minLength;
            if (a.Length < b.Length)
            {
                minLength = a.Length;
            }
            else
            {
                minLength = b.Length;
            }

            for (int i = 0; i < minLength; i++)
            {
                if (a[i] < b[i])        // сравниваем char по числовому коду
                    return -1;
                if (a[i] > b[i])
                    return 1;
            }

            // если все буквы совпали то короткий идет первым
            if (a.Length < b.Length)
                return -1;
            if (a.Length > b.Length)
                return 1;

            return 0;
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return string.Empty;

            return string.Join(Environment.NewLine, _output);
        }
    }
}
