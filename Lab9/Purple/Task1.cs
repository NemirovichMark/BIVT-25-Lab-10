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

        public Task1(string text) : base(text)
        {
            _output = "";
        }

        public override void Review()
        {
            if (_input == null)
            {
                _output = "";
                return;
            }

            char[] chars = { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', ' ' };
            int n = _input.Length;
            string word = "";
            string revWord;
            bool rev = true;

            for (int i = 0; i < n; i++)
            {
                if (chars.Contains(_input[i]))
                {
                    if (i > 0 && i + 1 < n && char.IsDigit(_input[i - 1]) && char.IsDigit(_input[i + 1]) && _input[i] == ',')
                    {
                        word += _input[i];
                    }
                    else
                    {
                        if (rev == false)
                        {
                            _output += word + _input[i];
                            word = "";
                            rev = true;
                        }
                        else
                        {
                            revWord = new string(word.Reverse().ToArray());
                            _output += revWord + _input[i];
                            word = "";
                        }
                    }
                    continue;
                }

                if (char.IsDigit(_input[i]))
                {
                    rev = false;
                    word += _input[i];
                    continue;
                }

                word += _input[i];
            }

            if (word != null && word.Length > 0)
            {
                if (rev == false)
                {
                    _output += word;
                }
                else
                {
                    _output += new string(word.Reverse().ToArray());
                }
            }
        }

        public override string ToString()
        {
            return _output ?? "";
        }
    }
}
