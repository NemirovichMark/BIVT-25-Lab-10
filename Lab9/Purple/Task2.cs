using System.Text;

namespace Lab9.Purple
{
    public class Task2(string text) : Purple(text)
    {
        public new string[]? Output { get; private set; }

        static string ProcessWhitespaces(string text)
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);


            var c = words.Length - 1;

            if (c <= 0) return text;

            var len = 0;

            foreach (var word in words) len += word.Length;

            var s = 50 - len;

            for (var i = 0; i < c; i += 1) { words[i] += ' '; s -= 1; }
            for (var i = 0; i < s; i += 1) words[i % c] += ' ';

            return string.Join("", words);
        }
        public override void Review()
        {
            var c = 0;
            string[] ans = [];
            var words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var res = new StringBuilder();

            res.Append(words[0]);
            c += words[0].Length;

            for (int i = 1; i < words.Length; i += 1)
            {
                if (c + words[i].Length < 50)
                {
                    res.Append(' ');
                    res.Append(words[i]);
                    c += words[i].Length + 1;
                }
                else
                {
                    Array.Resize(ref ans, ans.Length + 1);
                    ans[^1] = ProcessWhitespaces(res.ToString());
                    res = new StringBuilder(words[i]);
                    c = res.Length;
                }
            }

            if (res.Length != 0)
            {
                Array.Resize(ref ans, ans.Length + 1);
                ans[^1] = ProcessWhitespaces(res.ToString());
            }

            Output = [.. ans];
        }
        public override string ToString() => Output == null ? string.Empty : string.Join(Environment.NewLine, Output);
    }
}