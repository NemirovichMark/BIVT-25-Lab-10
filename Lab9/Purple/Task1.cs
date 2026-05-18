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
            char[] punc = { '.', '!', '?', ',', ';', ':', '\"', '\'', '-', '(', ')', '[', ']', '{', '}', '/' };

            string result = "";
            string currentWord = "";

            bool hasDigit = false;
            for (int i = 0; i < _input.Length; i++)
            {
                char c = _input[i];
                bool isSep = char.IsWhiteSpace(c);
                if (!isSep)
                {
                    foreach (char p in punc)
                    {
                        if (c == p && c != '-' && c != '\'')
                        {
                            isSep = true;
                            break;
                        }
                    }
                }

                if (!isSep)
                {
                    currentWord += c;
                    if (c >= '0' && c <= '9') hasDigit = true;
                }
                else
                { 
                    if (currentWord != "")
                    {
                        if (hasDigit)
                        {
                            result += currentWord;
                        }
                        else
                        {
                            for (int j = currentWord.Length - 1; j >= 0; j--)
                                result += currentWord[j];
                        }
                        currentWord = ""; 
                        hasDigit = false;
                    }
                    result += c; 
                }
            }

            if (currentWord != "")
            {
                if (hasDigit) result += currentWord;
                else
                {
                    for (int j = currentWord.Length - 1; j >= 0; j--)
                        result += currentWord[j];
                }
            }

            _output = result;
        }

        public override string ToString()
        {
            return _output;
        }
    }
}
