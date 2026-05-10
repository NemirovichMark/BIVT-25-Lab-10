using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        private string[] _output;
        public string[] Output => _output;
        public Task2(string text) : base(text) { }
        private string Add_space(string text)
        {
            string[] all = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int count_gap = all.Length - 1;

            if (count_gap <= 0) return text;

            int len = 0;
            foreach (var w in all)
                len += w.Length;
            int spaces = 50 - len;

            for (int i = 0; i < count_gap; i++)
            {
                all[i] += ' ';
                spaces--;
            }
            for (int i = 0; i < spaces; i++)
            {
                all[i % count_gap] += ' ';
            }
            return string.Join("", all);
        }

        public override void Review()
        {
            int cur_lenght = 0;
            string[] answer = new string[0];
            string[] all_words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder res = new StringBuilder();
            res.Append(all_words[0]);
            cur_lenght += all_words[0].Length;
            for (int i = 1; i < all_words.Length; i++)
            {
                if (cur_lenght + 1 + all_words[i].Length <= 50)
                {
                    res.Append(' ');
                    res.Append(all_words[i]);
                    cur_lenght += all_words[i].Length + 1;
                }
                else
                {
                    Array.Resize(ref answer, answer.Length + 1);
                    answer[answer.Length - 1] = Add_space(res.ToString());

                    res = new StringBuilder(all_words[i]);
                    cur_lenght = res.Length;
                }

            }
            if (res.Length != 0)
            {
                Array.Resize(ref answer, answer.Length + 1);
                answer[answer.Length - 1] = Add_space(res.ToString());
            }

            Console.WriteLine(answer[answer.Length - 1]);
            _output = answer.ToArray();
        }

        public override string ToString()
        {
            if (_output == null) return string.Empty;
            return string.Join(Environment.NewLine, _output);
        }
    }
}




