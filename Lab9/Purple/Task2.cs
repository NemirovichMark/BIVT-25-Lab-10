using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        private string[] _output;

        public string[] Output => _output;

        public Task2(string input) : base(input) { }

        

        public override void Review()
        {
            string[] words = Input.Split();

            string[] lines = [];

            int wordIdx = 0;
            while (wordIdx < words.Length)
            {
                int lineLen = words[wordIdx].Length;
                int count = 1;

                while (wordIdx + count < words.Length && lineLen + 1 + words[wordIdx + count].Length <= 50)
                {
                    lineLen += 1 + words[wordIdx + count].Length;
                    count++;
                }

                string[] lineWords = new string[count];
                for (int k = 0; k < count; k++)
                    lineWords[k] = words[wordIdx + k];

                string line = lineWords.Length > 1 ? AddSpaces(lineWords, 50) : lineWords[0];

                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = line;

                wordIdx += count;
            }

            _output = lines;
        }

        private string AddSpaces(string[] words, int width)
        {
            int totalWordLen = words.Sum(w => w.Length);

            int totalSpaces = width - totalWordLen;

            int gaps = words.Length - 1;

            int baseSpaces = totalSpaces / gaps;

            int extra = totalSpaces % gaps;

            var sb = new StringBuilder();
            sb.Append(words[0]);

            for (int i = 1; i < words.Length; i++)
            {
                int spaces = baseSpaces + (i - 1 < extra ? 1 : 0);
                sb.Append(new string(' ', spaces));
                sb.Append(words[i]);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            if (_output == null) return null;
            return string.Join("\n", _output);
        }
    }
}
