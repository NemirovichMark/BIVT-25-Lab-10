using Lab9.Purple;
using System.Runtime.CompilerServices;


namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        public Task1(string text) : base(text)
        {
        }
        public override void Review()
        {
            string result = "";
            string word = "";

            foreach(char el in _text)
            {
                if (char.IsLetterOrDigit(el) || el == '-' || el == '\'')
                {
                    word += el;
                }
                else
                {
                    result += CheckedWord(word) + el;
                    word = "";
                }
            }
            Output = result;
        }
        private string CheckedWord(string word)
        {
            if (word.Any(char.IsDigit))
                return word;

            string result = "";
            foreach (char el in word)
                result = el + result;
            return result;
        }
    }
}
