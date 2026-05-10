using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task2 : Blue
    {
        private string _sequence;
        private string _output;

        public string Sequence => _sequence;
        public string Output => _output;

        public Task2(string input, string sequence) : base(input)
        {
            _sequence = sequence;
            _output = "";
        }
        public override void Review()
        {
            string result = "";
            string[] words = Input.Split(' ');
            bool added = false; //есть ли в слове с подстрокой знаки
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains(_sequence))
                {
                    for (int j = 0; j < words[i].Length; j++)
                    {
                        if (Char.IsPunctuation(words[i][j])) //проверяем наличие знаков пунктуации
                        {
                            if (!Char.IsPunctuation(result[^2])) // до знака слово - добавляем знак
                            {
                                result = result.TrimEnd(); 
                                result += words[i][j];
                                added = true;
                            }
                            else
                            {
                                result += words[i][j]; //если знак, то оставляем пробел
                                added = true;
                            }
                        }
                    }
                    if (added)
                    {
                        result += " ";
                        added = false;
                    }
                }
                else 
                {
                    result += words[i];
                    if (i != words.Length - 1) result += " ";
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
