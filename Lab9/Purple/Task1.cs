using System.Linq;
using Newtonsoft.Json;

namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        private string _output = null;

        public string Output => _output;

        public Task1(string input) : base(input) { }

        public override void Review()
        {
            string[] words = Input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                char[] chars = words[i].ToCharArray();

                if (chars.Any(char.IsDigit))
                    continue;

                int[] wordCharIdx = chars
                    .Select((c, j) => (c, j))
                    .Where(x => char.IsLetter(x.c) || x.c == '-' || x.c == '`' || x.c == '\'')
                    .Select(x => x.j)
                    .ToArray();

                char[] wordChars = wordCharIdx.Select(j => chars[j]).ToArray();

                Array.Reverse(wordChars);

                for (int k = 0; k < wordCharIdx.Length; k++)
                    chars[wordCharIdx[k]] = wordChars[k];

                words[i] = new string(chars);
            }

            _output = string.Join(" ", words);
        }

        public override string ToString() => _output;
    }
}
