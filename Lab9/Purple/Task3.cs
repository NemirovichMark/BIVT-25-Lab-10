using System.Text;
namespace Lab9.Purple
{
    public class Task3(string text) : Purple(text)
    {
        public new string Output { get; private set; } = "";
        public (string, char)[] Codes { get; private set; }

        public override void Review()
        {
            var pairs = new string[Input.Length];
            var position = new int[Input.Length];
            var counts = new int[Input.Length];
            var n = 0;

            for (var i = 0; i < Input.Length - 1; i++)
            {
                var p = Input[i].ToString() + Input[i + 1].ToString();
                if (!(char.IsLetter(p[0]) && char.IsLetter(p[1]))) { continue; }

                int index = -1;
                for (int j = 0; j < n; j++)
                {
                    if (pairs[j] == p)
                    {
                        index = j;
                        break;
                    }
                }
                if (index >= 0) counts[index]++;
                else
                {
                    pairs[n] = p;
                    position[n] = i;
                    counts[n] = 1;
                    n++;
                }
            }

            var sorted = Enumerable
                .Range(0, n)
                .Select(i => new { pair = pairs[i], begin = position[i], count = counts[i] })
                .OrderByDescending(x => x.count)
                .ThenBy(x => x.begin)
                .Take(5)
                .ToArray();

            bool[] used = new bool[char.MaxValue];

            foreach (char c in Input) { used[(uint)c] = c >= 32 && c <= 126; }

            char[] codes = new char[sorted.Length];

            int char_index = 0;

            char char_v = ' ';

            while (char_v <= 126 && char_index < sorted.Length)
            {
                if (!used[char_v]) codes[char_index++] = char_v;
                char_v++;
            }

            this.Codes = new (string, char)[sorted.Length];

            string ret = Input;

            for (int i = 0; i < sorted.Length; i++)
            {
                this.Codes[i] = (sorted[i].pair, codes[i]);
                ret = ret.Replace(sorted[i].pair, codes[i].ToString());
            }

            this.Output = ret;
        }
        public override string ToString() { return this.Output; }
    }
}