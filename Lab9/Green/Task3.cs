using System;
using System.Linq;

namespace Lab9.Green
{
    public class Task3 : Green
    {
        private string _pattern;
        private string[] _arr;

        public string[] Output => _arr.ToArray();

        public Task3(string text, string pattern) : base(text)
        {
            if (pattern != null)
                _pattern = pattern;
            else
                _pattern = "";

            _arr = new string[0];
        }

        public override void Review()
        {
            _arr = new string[0];

            string text = Input;

            string pattern = _pattern;

            string current = "";
            foreach (char c in text)
            {
                if (char.IsLetter(c) || c == '-' || c == '\'' || c == '’')
                {
                    current += c;
                }
                else
                {
                    CheckWord(current, pattern);
                    current = "";
                }
            }

            CheckWord(current, pattern);
        }

        private void CheckWord(string word, string pattern)
        {
            if (word == null || word.Length == 0)
                return;

            if (!word.ToLower().Contains(pattern.ToLower()))
                return;

            for (int i = 0; i < _arr.Length; i++)
            {
                if (_arr[i].ToLower() == word.ToLower())
                    return;
            }

            Array.Resize(ref _arr, _arr.Length + 1);
            _arr[_arr.Length - 1] = word;
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < _arr.Length; i++)
            {
                if (i > 0)
                    result += "\r\n";

                result += _arr[i];
            }

            return result;
        }
    }
}
