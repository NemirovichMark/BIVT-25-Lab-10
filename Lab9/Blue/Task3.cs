using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task3 : Blue
    {
        private (char, double)[] _output;

        public (char, double)[] Output => _output;

        public Task3(string input) : base(input)
        {
            _output = new (char, double)[0];
        }
        private int CountWords(char letter, string[] words)
        {
            int count = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0 && Char.IsLetter(words[i][0]))
                {
                    char first = Char.ToLower(words[i][0]);
                    if (first == letter)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public override void Review()
        {
            string[] words = Input.Split(' ', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/');
            (char letter, double percent)[] arr = new (char letter, double percent)[words.Length];
            int index = 0; 
            int totalWords = 0; 
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0 && Char.IsLetter(words[i][0]))
                {
                    totalWords++;
                }
            }
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0 || !Char.IsLetter(words[i][0]))
                    continue;
                char first = Char.ToLower(words[i][0]);
                bool iscount = false;
                for (int j = 0; j < index; j++)
                {
                    if (arr[j].letter == first)
                    {
                        iscount = true;
                        break;
                    }
                }
                if (!iscount)
                {
                    int count = CountWords(first, words);
                    double percent = (double)count * 100 / totalWords;

                    arr[index] = (first, percent);
                    index++;
                }
            }
            (char letter, double percent)[] result = new (char letter, double percent)[index];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = arr[i];
            }

            for (int i = 0; i < result.Length - 1; i++)
            {
                for (int j = 0; j < result.Length - 1 - i; j++)
                {
                    if (result[j].percent < result[j + 1].percent || result[j].percent == result[j + 1].percent && result[j].letter > result[j + 1].letter)
                    {
                        (result[j], result[j + 1]) = (result[j + 1], result[j]);
                    }
                }
            }
            _output = result;
        }
        public override string ToString()
        {
            string result = "";
            var arr = _output;
            for (int i = 0; i < arr.Length; i++)
            {
                result += arr[i].Item1 + ":" + arr[i].Item2.ToString("F4");
                if (i != arr.Length - 1)
                {
                    result += Environment.NewLine;
                }
            }
            return result;
        }
    }
}