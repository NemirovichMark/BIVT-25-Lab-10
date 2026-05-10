using System;
using System.Text;

namespace Lab9.White
{
    public class Task3 : White
    {
        private string[,] _codes;
        private string _output;

        public Task3(string text, string[,] codes) : base(text)
        {
            _codes = codes;
        }

        public string Output
        {
            get
            {
                if (string.IsNullOrEmpty(_output))
                    return Input;
                return _output;
            }
        }

        public override void Review()
        {
            _output = ReplaceWordsWithCodes(Input);
        }

        private string ReplaceWordsWithCodes(string text)
        {
            var result = new StringBuilder();
            var currentWord = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];

                if (char.IsLetter(ch) || ch == '\'' || ch == '-')
                {
                    currentWord.Append(ch);
                }
                else
                {
                    if (currentWord.Length > 0)
                    {
                        string word = currentWord.ToString();
                        string code = FindCode(word);
                        result.Append(code);
                        currentWord.Clear();
                    }
                    result.Append(ch);
                }
            }

            if (currentWord.Length > 0)
            {
                string word = currentWord.ToString();
                string code = FindCode(word);
                result.Append(code);
            }

            return result.ToString();
        }

        private string FindCode(string word)
        {
            for (int i = 0; i < _codes.GetLength(0); i++)
            {
                if (string.Equals(_codes[i, 0], word, StringComparison.Ordinal))
                {
                    return _codes[i, 1];
                }
            }
            return word;
        }

        public override string ToString()
        {
            return Output;
        }
    }
}
