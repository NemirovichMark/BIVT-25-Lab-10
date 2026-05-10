using System;

namespace Lab9.Blue
{
    public class Task2 : Blue
    {
        private string _ord;
        public string Ord => _ord;
        private string _output = "";
        public string Output => _output;

        public Task2(string input, string ord) : base(input)
        {
            _ord = ord;
        }

        public override void Review()
        {
            string[] words = Input.Split(' ');
            string[] result = new string[0];
            char[] spacePunct = { '–', '(', '[', '{', '"' };

            foreach (string word in words)
            {
                string piece = "";

                if (word.Contains(_ord))
                {
                    foreach (char c in word)
                    {
                        if (char.IsPunctuation(c))
                        {
                            piece = piece + c;
                        }
                    }
                }
                else
                {
                    piece = word;
                }

                if (string.IsNullOrEmpty(piece)) continue;

                if (result.Length > 0 && result[^1] != " ")
                {
                    bool needsSpace = !char.IsPunctuation(piece[0]);

                    foreach (char sp in spacePunct)
                    {
                        if (piece[0] == sp)
                        {
                            needsSpace = true;
                            break;
                        }
                    }

                    if (needsSpace)
                    {
                        Array.Resize(ref result, result.Length + 1);
                        result[result.Length - 1] = " ";
                    }
                }

                Array.Resize(ref result, result.Length + 1);
                result[result.Length - 1] = piece;
            }

            _output = String.Join("", result).Trim();
        }

        public override string ToString() => _output;
    }
}
