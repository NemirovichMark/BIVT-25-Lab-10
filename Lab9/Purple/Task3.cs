using Lab9.Purple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string _output = "";
        public string Output => _output;
        private (string, char)[] _codes;
        public (string, char)[] Codes => _codes;
        public Task3(string text) : base(text) { }

        public override void Review()
        {
            string[] pairs = new string[Input.Length];
            int[] position = new int[Input.Length];
            int[] counts = new int[Input.Length];
            int n = 0;

            for (int i = 0; i < Input.Length - 1; i++)
            {
                string p = Input[i].ToString() + Input[i + 1].ToString();
                if (!char.IsLetter(p[0]) || !char.IsLetter(p[1])) continue;

                int idx = -1;
                for (int j = 0; j < n; j++)
                {
                    if (pairs[j] == p)
                    {
                        idx = j;
                        break;
                    }
                }
                if (idx >= 0) counts[idx]++;
                else
                {
                    pairs[n] = p;
                    position[n] = i;
                    counts[n] = 1;
                    n++;
                }
            }
            var sorted = Enumerable.Range(0, n).Select(i => new { pair = pairs[i], firstPos = position[i], count = counts[i] }).OrderByDescending(x => x.count).ThenBy(x => x.firstPos).Take(5).ToArray();
            bool[] used = new bool[127];
            foreach (char c in Input) { if (c >= 32 && c <= 126) used[c] = true; }       // символы от пробела до тильды (ASCII 32-126)
            char[] codes = new char[sorted.Length];
            int ci = 0;
            char cc = ' ';
            while (cc <= 126 && ci < sorted.Length)
            {
                if (!used[cc]) codes[ci++] = cc;
                cc++;
            }
            _codes = new (string, char)[sorted.Length];
            string result = Input;
            for (int i = 0; i < sorted.Length; i++)
            {
                _codes[i] = (sorted[i].pair, codes[i]);
                result = result.Replace(sorted[i].pair, codes[i].ToString());
            }
            _output = result;
        }
        public override string ToString() { return _output; }
    }
}


//    public class Task3 : Purple
//    {
//        private string _output;
//        private (string, char)[] _codes;
//        public string Output => _output;
//        public (string, char)[] Codes => _codes;

//        public Task3(string input) : base(input) { }

//        public override void Review()
//        {
//            string text = Input;

//            var bigramStats = new Dictionary<string, (int count, int firstPos)>();

//            for (int i = 0; i < text.Length - 1; i++)
//            {
//                if (!char.IsLetter(text[i]) || !char.IsLetter(text[i + 1]))
//                    continue;

//                string bigram = text.Substring(i, 2);

//                if (!bigramStats.ContainsKey(bigram))
//                {
//                    bigramStats[bigram] = (count: 0, firstPos: i);
//                }

//                bigramStats[bigram] = (bigramStats[bigram].count + 1, bigramStats[bigram].firstPos);
//            }

//            List<(string bigram, int count, int firstPos)> bigramList = new List<(string, int, int)>();
//            foreach (var kvp in bigramStats)
//            {
//                bigramList.Add((kvp.Key, kvp.Value.count, kvp.Value.firstPos));
//            }

//            bigramList = bigramList.OrderByDescending(b => b.count).ThenBy(b => b.firstPos).ToList();

//            int topCount = Math.Min(5, bigramList.Count);
//            var topBigrams = bigramList.GetRange(0, topCount);

//            List<char> availableChars = new List<char>();
//            for (char c = ' '; c <= '~'; c++)  // символы от пробела до тильды (ASCII 32-126)
//            {
//                if (!text.Contains(c))
//                {
//                    availableChars.Add(c);
//                    if (availableChars.Count == topCount)
//                        break;
//                }
//            }

//            _codes = new (string, char)[topCount];
//            for (int i = 0; i < topCount; i++)
//            {
//                _codes[i] = (topBigrams[i].bigram, availableChars[i]);
//            }

//            string result = text;
//            foreach (var (bigram, code) in _codes)
//            {
//                result = result.Replace(bigram, code.ToString());
//            }
//            _output = result;
//        }

//        public override string ToString()
//        {
//            return _output;
//        }
//    }
//}




