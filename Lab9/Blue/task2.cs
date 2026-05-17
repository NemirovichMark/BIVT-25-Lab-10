using Lab9.Blue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task2 : Blue
    {
        private string _sequence;
        private string _output;

        public string Output => _output;
        public string Sequence => _sequence;

        public Task2(string inputText, string sequence) : base(inputText)
        {
            _sequence = sequence;
            Review();
        }

        public override void Review()
        {

            string[] words = Input.Split(' ');
            string[] processedwords = new string[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                int firstl = -1;
                int lastL =  -1;

                for (int j = 0; j < word.Length; j++)
                {
                    if (char.IsLetter(word[j]))
                    {
                        if (firstl == -1) firstl = j;
                        lastL = j;
                    }
                }

                if (firstl == -1)
                {
                    processedwords[i] = word;
                    continue;
                }

                string pre = word.Substring(0, firstl);
                string wordPart = word.Substring(firstl, lastL - firstl + 1);
                string suf = word.Substring(lastL + 1);

                if (wordPart.Contains(_sequence))
                {
                    processedwords[i] = pre + suf;
                }
                else
                {
                    processedwords[i] = word;
                }
            }

            string result = string.Join(" ", processedwords);

            string[] marks = { ".", ",", "!", "?", ":", ";" };
            foreach (var mark in marks)
            {
                result = result.Replace(" " + mark, mark);
            }

            while (result.Contains("  "))
            {
                result = result.Replace("  ", " ");
            }

            _output = result.Trim();
        }

        public override string ToString()
        {
            return _output;
        }
    }
}
