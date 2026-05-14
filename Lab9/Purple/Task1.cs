using System.ComponentModel.DataAnnotations;
using System.Xml;
using Microsoft.VisualBasic;
using System.Linq;

namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        private string _output;

        public string Output => _output;

        public override string ToString() => _output;

        public Task1(string text) : base(text)
        {
            _output = default;
        }

        public override void Review()
        {
            char[] chars = {'.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', ' '};
            int n = _input.Length;
            string word = "";
            string revWord;
            bool rev = true;


            for (int i = 0; i < n; i++)
            {
                if (chars.Contains(_input[i]))//если встретился знак
                {
                    if (char.IsDigit(_input[i - 1]) && i + 1 < n && char.IsDigit(_input[i + 1]) && _input[i] == ',')
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
                            revWord = "";
                            for (int j = 0; j < word.Length; j++)
                            {
                                revWord += word[word.Length - j - 1];
                            }
                            _output += revWord + _input[i];
                            word = "";
                        }
                    }

                } 

                else if (char.IsDigit(_input[i]))//если встретилась цифра
                {
                    rev = false;
                    word += _input[i];
                }

                else//если встретилась буква
                {
                    word += _input[i];
                }
            }
        }


    }
}
