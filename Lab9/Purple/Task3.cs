using System;
using System.Collections.Generic;
using System.Linq;
using Lab9.Purple;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        public (string, char)[] Codes { get; private set; } = new (string, char)[0];

        // constructor
        public Task3(string text) : base(text) { }

        // methods
        public override void Review()
        {
            string text = Input ?? "";

            Dictionary<string, int> freq = new Dictionary<string, int>();
            Dictionary<string, int> firstPos = new Dictionary<string, int>();

            for (int i = 0; i < text.Length - 1; i++)
            {
                char a = text[i];
                char b = text[i + 1];

                if (char.IsLetter(a) && char.IsLetter(b))
                {
                    string pair = new string(new[] { a, b });

                    if (!freq.ContainsKey(pair))
                    {
                        freq[pair] = 0;
                        firstPos[pair] = i;
                    }
                    freq[pair]++;
                }
            }

            var topPairs = freq
                .OrderByDescending(x => x.Value)
                .ThenBy(x => firstPos[x.Key])
                .Take(5)
                .Select(x => x.Key)
                .ToArray();

            List<char> freeCodes = new List<char>();
            HashSet<char> usedInText = new HashSet<char>(text);

            for (int c = 32; c <= 126 && freeCodes.Count < topPairs.Length; c++)
            {
                char ch = (char)c;
                if (!usedInText.Contains(ch))
                    freeCodes.Add(ch);
            }
            int countToEncode = Math.Min(topPairs.Length, freeCodes.Count);

            Codes = new (string, char)[countToEncode];
            for (int i = 0; i < countToEncode; i++)
            {
                Codes[i] = (topPairs[i], freeCodes[i]);
            }
            Dictionary<string, char> map = new Dictionary<string, char>();
            for (int i = 0; i < Codes.Length; i++)
                map[Codes[i].Item1] = Codes[i].Item2;

            string result = text;
            for (int i = 0; i < Codes.Length; i++)
            {
                result = result.Replace(Codes[i].Item1, Codes[i].Item2.ToString());
            }

            Output = result;
        }

        public override string ToString()
        {
            return Output ?? "";
        }
    }
}