using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab9.Green
{
    public class Task3 : Green
    {
        private string _sequence;
        private string[] _output;

        public string[] Output => _output;

        public Task3(string input, string sequence) : base(input)
        {
            _sequence = sequence;
            _output = new string[0];


        }

        private string[] Split()
        {
            char[] symbols = new char[]
            {
                ' ', '.', ',', '!', '?', ';', ':', '\n', '\r', '\t',
                '-', '—', '(', ')', '[', ']', '{', '}', '"', '\'', ' '
             };

            var words = Input.Split(symbols).Where(w => w.Length > 0).ToArray();
            return words;
        }



        public override void Review()
        {
            string[] comparison_w = new string[1000];
            int k = 0;

            string[] words = Split();
            string lowerSequence = _sequence.ToLower();

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];       
                string lowerWord = word.ToLower();   

                if (lowerWord.Contains(lowerSequence))
                {
                    bool fl = false;

                    for (int m = 0; m < k; m++)
                    {
                        if (string.Equals(comparison_w[m], word, StringComparison.OrdinalIgnoreCase))
                        {
                            fl = true;
                            break;
                        }
                    }

                    if (fl == false)
                    {
                        comparison_w[k++] = word;
                    }
                }
            }

            string[] result = new string[k];
            for (int i = 0; i < k; i++)
            {
                result[i] = comparison_w[i];
            }
            _output = result;

        }

        public override string ToString()
        {
            return _output == null || _output.Length == 0
                ? ""
                : string.Join("\r\n", _output);
        }
    }

}
