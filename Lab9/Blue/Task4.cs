using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task4 : Blue
    {
        private int _output;

        public int Output => _output;

        public Task4(string input) : base(input)
        {
            _output = 0;
        }
        public override void Review()
        {
            string[] words = Input.Split(' ', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/');
            int sum = 0;

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = 0;  j < words[i].Length; j++)
                {
                    if (char.IsDigit(words[i][j]))
                    {
                        int num = 0;
                        for (; j < words[i].Length && char.IsDigit(words[i][j]); j++)
                        {
                            num = num * 10 + (words[i][j] - '0');
                        }
                        sum += num;
                    }
                }
            }
            _output = sum;
        }
        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
