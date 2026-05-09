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
        public Task1(string input) : base(input)
        {
            _output = "";
        }
        public string Output => _output;

        internal StringBuilder Reverse(StringBuilder word)
        {
            StringBuilder result = new StringBuilder();
            for (int i = word.Length - 1; i>=0; i--)
            {
                if (word[i] == ')') result.Append('(');
                else if (word[i] == '(') result.Append(')'); 
                else result.Append(word[i]);
            }
            return result;
        }
        public override void Review()
        {
            StringBuilder result = new StringBuilder();

            string[] temp = Input.Split(" ");
            for (int i = 0; i < temp.Length; i++)
            {
                StringBuilder word = new StringBuilder();
                word.Append(temp[i]);
                if (Char.IsDigit(word[0]))
                {
                    for (int j = 0; j<word.Length; j++)
                    {
                        if (Char.IsPunctuation(word[j]))
                        {
                            word.Remove(j, 1);
                            break;
                        }
                    }
                    result.Append(word);
                    result.Append(' ');
                }
                else
                {
                    for (int j = word.Length - 1; j >= 0; j--)
                    {
                        if (!Char.IsPunctuation(word[j]) || (Char.IsPunctuation(word[j]) && word[j] == '-') || (Char.IsPunctuation(word[j]) && word[j].ToString() == "'"))
                        {
                            result.Append(word[j]);
                        }
                    }
                    result.Append(' ');
                }
            }

            result.Remove(result.Length - 1, 1);
            for (int i = 0; i< Input.Length; i++)
            {
                if (Char.IsPunctuation(Input[i]) && Input[i] != '-' && Input[i].ToString() != "'")
                {
                    result.Insert(i, Input[i]);
                }
            }
            _output = result.ToString();
        }

        public override string ToString()
        {
            return _output;
        }

    }
}
