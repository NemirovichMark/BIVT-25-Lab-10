using System.Text;
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
            foreach (char c in Input) { if (c >= 32 && c <= 126) used[c] = true; }
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
