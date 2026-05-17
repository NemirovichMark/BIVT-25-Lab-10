using System.Text;

namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        public Task1(string text) : base(text) { }
        public override void Review()
        {
            var result = new StringBuilder();
            var word = new StringBuilder();
            bool flag = false;
            foreach (char s in _input)
            {
                if (char.IsLetterOrDigit(s) || s == '\'' || s == '-')
                {
                    word.Append(s);
                    if (char.IsDigit(s)) flag = true;
                }
                else
                {
                    if (flag) flag = false;
                    else word = new StringBuilder(string.Concat(word.ToString().Reverse()));
                    result.Append(word);
                    result.Append(s);
                    word.Clear();
                }
            }
            Output = result.ToString();
        }

        public override string ToString() { return Output; }
    }
}
