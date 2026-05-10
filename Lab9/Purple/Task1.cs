using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        private string _output = string.Empty;
        public string Output => _output;

        public Task1(string text) : base(text) { }

        private String Reverse(String text)
        {
            StringBuilder res = new StringBuilder();

            for (int i = text.Length - 1; i >= 0; i--)
            {
                var c = text[i];
                res.Append(c);

            }
            return res.ToString();
        }
        public override void Review()
        {
            StringBuilder word = new StringBuilder(); ;
            StringBuilder res = new StringBuilder();
            bool isdig = false;

            foreach (char c in Input)
            {
                //    воздушно-топливной смеси
                //    йонвилпот-оншудзов исемс
                if (char.IsLetterOrDigit(c) || c == '-' || c == '\'')
                {
                    word.Append(c);

                    if (char.IsDigit(c))
                    {
                        isdig = true;
                    }
                }
                else
                {
                    if (word.Length > 0)
                    {
                        string rev_all = Reverse(word.ToString());
                        Console.WriteLine(rev_all);

                        if (isdig is true) res.Append(word.ToString());
                        else res.Append(rev_all.ToString());


                    }
                    word.Clear();
                    res.Append(c);
                    isdig = false;

                }
            }
            if (word.Length > 0)
            {
                string rev_all = Reverse(word.ToString());
                if (isdig is true) res.Append(word.ToString());
                else res.Append(rev_all.ToString());
            }
            _output = res.ToString();
        }

        public override string ToString()
        {
            return _output;
        }
    }

}
