using System.Linq;
using Newtonsoft.Json;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string _output = null;

        private (string, char)[] _codes;

        public string Output => _output;

        public (string, char)[] Codes => _codes;

        public Task3(string input) : base(input)
        {
            _codes = [];
        }

        public override void Review()
        {
            string text = Input;

            var countByPair = new Dictionary<string, int>();
            var firstPosByPair = new Dictionary<string, int>();

            for (int i = 0; i < text.Length - 1; i++)
            {
                if (!char.IsLetter(text[i]) || !char.IsLetter(text[i + 1]))
                    continue;

                string pair = text.Substring(i, 2);
                if (!countByPair.ContainsKey(pair))
                {
                    countByPair[pair] = 0;
                    firstPosByPair[pair] = i;
                }

                countByPair[pair] = countByPair[pair] + 1;
            }

            var allPairs = new (string pair, int count, int firstPos)[countByPair.Count];
            int idx = 0;
            foreach (var kv in countByPair)
            {
                allPairs[idx] = (kv.Key, kv.Value, firstPosByPair[kv.Key]);
                idx++;
            }

            Array.Sort(allPairs, (a, b) =>
            {
                int byCount = b.count.CompareTo(a.count);
                if (byCount != 0)
                    return byCount;
                return a.firstPos.CompareTo(b.firstPos);
            });

            int take = allPairs.Length < 5 ? allPairs.Length : 5;
            var bigrams = new (string pair, int count, int firstPos)[take];
            for (int j = 0; j < take; j++)
                bigrams[j] = allPairs[j];

            char[] unusedChars = Enumerable.Range(32, 126)
                .Select(i => (char)i)
                .Where(c => !text.Contains(c))
                .Take(bigrams.Length)
                .ToArray();

            _codes = new (string, char)[bigrams.Length];
            for (int i = 0; i < bigrams.Length; i++)
                _codes[i] = (bigrams[i].pair, unusedChars[i]);

            string result = text;
            foreach (var (pair, code) in _codes)
                result = result.Replace(pair, code.ToString());

            _output = result;
        }

        public override string ToString() => _output;
    }
}
