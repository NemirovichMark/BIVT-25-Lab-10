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

        public (char, double)[] Output
        {
            get
            {
                return _output;
            }
        }

        public Task3(string text) : base(text)
        {
            _output = null;
        }

        public override void Review()
        {
            string[] words = ExtractWords();

            if (words == null || words.Length == 0)
            {
                _output = new (char, double)[0];
                return;
            }

            char[] letters = new char[words.Length];
            int[] counts = new int[words.Length];
            int uniqueCount = 0;
            int totalWords = 0;

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                if (word == null || word.Length == 0)
                {
                    continue;
                }

                char first = word[0];

                if (!char.IsLetter(first))
                {
                    continue;
                }

                first = char.ToLower(first);
                totalWords++;

                int position = -1;

                for (int j = 0; j < uniqueCount; j++)
                {
                    if (letters[j] == first)
                    {
                        position = j;
                        break;
                    }
                }

                if (position == -1)
                {
                    letters[uniqueCount] = first;
                    counts[uniqueCount] = 1;
                    uniqueCount++;
                }
                else
                {
                    counts[position]++;
                }
            }

            _output = new (char, double)[uniqueCount];

            for (int i = 0; i < uniqueCount; i++)
            {
                double percent = 0.0;

                if (totalWords > 0)
                {
                    percent = counts[i] * 100.0 / totalWords;
                }

                _output[i] = (letters[i], percent);
            }

            for (int i = 0; i < _output.Length - 1; i++)
            {
                for (int j = i + 1; j < _output.Length; j++)
                {
                    bool needSwap = false;

                    if (_output[i].Item2 < _output[j].Item2)
                    {
                        needSwap = true;
                    }
                    else if (_output[i].Item2 == _output[j].Item2 && _output[i].Item1 > _output[j].Item1)
                    {
                        needSwap = true;
                    }

                    if (needSwap)
                    {
                        (char, double) temp = _output[i];
                        _output[i] = _output[j];
                        _output[j] = temp;
                    }
                }
            }
        }

        public override string ToString()
        {
            if (_output == null)
            {
                return string.Empty;
            }

            if (_output.Length == 0)
            {
                return string.Empty;
            }

            string result = string.Empty;

            for (int i = 0; i < _output.Length; i++)
            {
                if (i > 0)
                {
                    result += "\n";
                }

                result += _output[i].Item1 + ":" + _output[i].Item2.ToString("F4");
            }

            return result;
        }
    }
}
