using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task2 : Green
    {
        private char[] _output;
        public char[] Output => _output;

        public Task2(string input) : base(input)
        {
            _output = new char[0];
        }

        public override void Review()
        {
            string text = Input.ToLower();

            char[] znaki = { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', ' ' };
            string[] netyznakow = text.Split(znaki);

            string[] words = new string[netyznakow.Length];
            int wordsK = 0; 
            for (int i = 0; i < netyznakow.Length; i++)
            {
                string a = netyznakow[i];
                bool isWord = true;
                for (int j = 0; j < a.Length; j++)
                {
                    if (char.IsDigit(a[j]))
                    {
                        isWord = false;
                        break;
                    }
                }
                if (isWord)
                {
                    words[wordsK] = a;
                    wordsK++;
                }
            }

            string uniqueLetters = "";
            for (int i = 0; i < wordsK; i++)
            {
                string word = words[i];
                char first = ' ';

                for (int m = 0; m < word.Length; m++)
                {
                    if (char.IsLetter(word[m]))
                    {
                        first = word[m];
                        break;
                    }
                }

                if (first != ' ') 
                {
                    bool add = false;
                    for (int n = 0; n < uniqueLetters.Length; n++)
                    {
                        if (uniqueLetters[n] == first)
                            add = true;
                    }
                    if (!add)
                        uniqueLetters += first;
                }
            }

            _output = uniqueLetters.ToCharArray();
            int[] counts = new int[_output.Length];

            for (int i = 0; i < _output.Length; i++)
            {
                for (int j = 0; j < wordsK; j++)
                {
                    char first = ' ';
                    for (int k = 0; k < words[j].Length; k++)
                    {
                        if (char.IsLetter(words[j][k]))
                        {
                            first = words[j][k];
                            break;
                        }
                    }
                    if (first == _output[i])
                        counts[i]++;
                }
            }

            string alphabet = "abcdefghijklmnopqrstuvwxyzабвгдеёжзийклмнопрстуфхцчшщъыьэюя";

            for (int i = 0; i < _output.Length - 1; i++)
            {
                for (int j = 0; j < _output.Length - i - 1; j++)
                {
                    if (counts[j] < counts[j + 1] || (counts[j] == counts[j + 1] && alphabet.IndexOf(_output[j]) > alphabet.IndexOf(_output[j + 1])))
                    {
                        (counts[j], counts[j + 1]) = (counts[j + 1], counts[j]); 
                        (_output[j], _output[j + 1]) = (_output[j + 1], _output[j]); 
                    }
                }
            }
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < _output.Length; i++)
            {
                result += _output[i];
                if (i < _output.Length - 1)
                    result += ", ";
            }
            return result;
        }

    }
}
