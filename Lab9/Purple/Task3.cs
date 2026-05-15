
using System.Timers;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        public (string, char)[] Codes;
        public Task3(string text) : base(text)
        {
            
        }
        public override void Review()
        {
            string[] repeatable = FindRepeatable(_text, 5);
            char[] unused = FindUnusedSings(_text)[0..5];
            Codes = new (string, char)[5];
            string result = _text;
            for(int i = 0; i < Codes.Length; i++)
            {
                Codes[i] = (repeatable[i], unused[i]);
            }
            foreach(var el in Codes)
            {
                result = result.Replace(el.Item1, el.Item2.ToString());
            }
            Output = result;
        }
        private char[] FindUnusedSings(string text)
        {
            Dictionary<char, int> asciiDict = Enumerable.Range(32, 95).ToDictionary(i => (char)i, i => i);
            foreach(char el in text)
                if(asciiDict.ContainsKey(el))
                    asciiDict[el]++;
            char[] unusedChars = asciiDict.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
            return unusedChars;
        }
        private string[] FindRepeatable(string text, int n)
        {
            string[] result = new string[n];
            //Знаками препинания считать: '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/'.
            char[] signs = { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' };
            string format = text;
            foreach (char el in signs)
                format = format.Replace(el.ToString(), "");
            string[] words = format.Split(' ');

            Dictionary<string, int> counts = new Dictionary<string, int>();

            foreach(string el in words)
            {
                for(int i = 0; i < el.Length - 1; i++)
                {
                    string key = el[i..(i + 2)];
                    if (counts.ContainsKey(key))
                        counts[key]++;
                    else
                        counts[key] = 1;
                }
            }
            string[] topN = counts.OrderByDescending(x => x.Value).Take(n).Select(x => x.Key).ToArray();

            return topN;
        }
    }
}
