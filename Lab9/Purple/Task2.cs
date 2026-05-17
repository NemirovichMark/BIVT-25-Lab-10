using System.Text;

namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        private string[] _output;
        public string[] Output => _output;
        public Task2(string text) : base(text) { }
        private string probely(string text)
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int c = words.Length - 1;
            if (c <= 0) return text; 
            int len = 0;
            foreach (var word in words) len += word.Length;
            int s = 50 - len;
            for (int i = 0; i < c; i++) { words[i] += ' '; s--; }
            for (int i = 0; i < s; i++) words[i % c] += ' ';
            return string.Join("", words);
        }
        public override void Review()
        {
            int cur = 0;
            string[] ans = new string[0];
            string[] words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder res = new StringBuilder();
            res.Append(words[0]);
            cur += words[0].Length;
            for (int i = 1; i < words.Length; i++)
            {
                if (cur + 1 + words[i].Length <= 50)
                {
                    res.Append(' ');
                    res.Append(words[i]);
                    cur += words[i].Length + 1;
                }
                else
                {   
                    Array.Resize(ref ans, ans.Length + 1);
                    ans[ans.Length - 1] = probely(res.ToString()); 
                    res = new StringBuilder(words[i]);
                    cur = res.Length;            
                }
            }
            if (res.Length != 0)
            {
                Array.Resize(ref ans, ans.Length + 1);
                ans[ans.Length - 1] = probely(res.ToString());
            }
            _output = ans.ToArray();
        }
        public string ToString() { return _output == null ? string.Empty : string.Join(Environment.NewLine, _output); }
    }
}
