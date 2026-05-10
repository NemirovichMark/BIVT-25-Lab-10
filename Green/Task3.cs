using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task3: Green
    {
        private string _stroka;
        private string[] _output;
        public string[] Output => _output;

        public Task3(string input, string stroka) : base(input)
        {
            _stroka = stroka;
            _output = new string[0];
        }

        public override void Review()
        {
            char[] znaki = { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', ' ' };
            string[] netyznakow = Input.Split(znaki);

            string[] temp = new string[netyznakow.Length];
            int count = 0;

            for (int i = 0; i < netyznakow.Length; i++)
            {
                string word = netyznakow[i];
                // Проверяем, что _stroka не null, прежде чем вызывать Contains
                if (_stroka != null && word.ToLower().Contains(_stroka.ToLower()))
                {
                    bool flag = false;
                    for (int j = 0; j < count; j++) // есть ли это слово уже
                    {
                        if (temp[j].ToLower() == word.ToLower())
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        temp[count] = word;
                        count++;
                    }
                }
            }

            _output = new string[count];
            for (int i = 0; i < count; i++)
            {
                _output[i] = temp[i];
            }
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return "";
            string result = "";
            for (int i = 0; i < _output.Length; i++)
            {
                result += _output[i];
                if (i < _output.Length - 1)
                {
                    result += Environment.NewLine;
                }
            }
            return result;
        }
    }
}

