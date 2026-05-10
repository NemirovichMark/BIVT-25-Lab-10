using System.Text;
using System.Linq;

namespace Lab9.Purple
{
    public class Task1(string text) : Purple(text)
    {
        static readonly char[] extra_chars = ['\'', '-'];

        public override void Review()
        {
            var ret = new StringBuilder();
            var word = new StringBuilder();

            var word_f = false;

            foreach (var c in this.Input)
            {
                if (IsWordChar(c))
                {
                    word.Append(c);

                    if (char.IsDigit(c)) { word_f = true; }
                }
                else
                {
                    if (word_f) { word_f = false; }
                    else { word = new StringBuilder(string.Concat(word.ToString().Reverse())); }

                    ret
                        .Append(word)
                        .Append(c);

                    word.Clear();
                }
            }
            
            this.Output = ret.ToString();
        }

        static bool IsWordChar(char c) => extra_chars.Contains(c) || char.IsLetterOrDigit(c);

        public override string ToString() { return this.Output!; }
    }
}