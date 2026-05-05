using System;

namespace Lab9.Green
{
    public class Task4 : Green
    {
        private string[] _output = new string[100];
        public string[] Output => _output;

        public Task4(string text) : base(text)
        {

        }

        public override void Review()
        {
            char[] delimiters = new char[]
            {
                ' ', '.', ',', '!', '?', ';', ':', '\n', '\r', '\t',
                '-', '—', '(', ')', '[', ']', '{', '}', '"', '\''
            };

            string[] words = Input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = 0; j < words.Length - i - 1; j++)
                {
                    string current = words[j];
                    string next = words[j + 1];

                    bool needSwap = false;

                    int minLength = Math.Min(current.Length, next.Length);

                    for (int k = 0; k < minLength; k++)
                    {
                        char currentLower = char.ToLower(current[k]);
                        char nextLower = char.ToLower(next[k]);

                        if (currentLower > nextLower)
                        {
                            needSwap = true;
                            break;
                        }
                        else if (currentLower < nextLower)
                        {
                            break;
                        }
                    }

                    if (needSwap)
                    {
                        string temp = words[j];
                        words[j] = words[j + 1];
                        words[j + 1] = temp;
                    }
                }
            }

            _output = words;
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return "";

            string result = _output[0];

            for (int i = 1; i < _output.Length; i++)
            {
                result += "\r\n" + _output[i];
            }

            return result;
        }
    }
}