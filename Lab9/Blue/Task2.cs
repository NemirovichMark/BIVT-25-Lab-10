using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task2 : Blue
    {
        private string _output = "";
        public string Output => _output;
        private string _combo;
        public Task2(string input, string combo) : base(input)
        {
            _combo = combo;
        }
        public override void Review()
        {

            string[] words = _input.Split(new char[] { ' ' }, StringSplitOptions.None);

            foreach (string word in words)
            {
                if (!word.Contains(_combo))
                {
                    if (_output.Length == 0)
                    {
                        _output = word;
                    }
                    else _output += ' ' + word;
                }
                else
                {

                    char[] punctuationMarks = { '.', ',', '!', '?', ';', ':' };
                    if (word.Contains('\"'))
                    {
                        if (word.Any(c => punctuationMarks.Contains(c)))
                        {
                            _output += " \"\"" + word[^1];
                        }
                        else if (_output.Length == 0)
                            _output = "\"\"";
                        else
                            _output += " \"\"";
                    }
                    else if (word.Any(c => punctuationMarks.Contains(c)))
                    {
                        _output += word[^1];
                    }

                }
            }
        }
        public override string ToString()
        {
            return _output;
        }
    }
}