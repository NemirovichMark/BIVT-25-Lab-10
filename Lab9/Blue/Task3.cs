using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task3 : Blue
    {
        private (char,double)[] _output= new (char, double)[0];
        public (char, double)[] Output => _output;
        private char[] _c;

        public Task3(string input) : base(input)
        {

        }
        public override void Review()
        {
            string[] words = _input.Split(new char[] { ' ' }, StringSplitOptions.None);
            char[] c = new char[0];
            char[] punctuation = new char[] { '.', ',', '!', '?', ';', ':', '"', '\'', '(', ')', '[', ']', '{', '}', '-', '—', '…', '«', '»' };
            string[] words2 = new string[words.Length];
            
            for (int i = 0; i < words.Length; i++)
            {
                words2[i] = words[i].Trim(punctuation);
            }
            foreach(var word in words2)
            {
                if (word.Length > 0 && char.IsLetter(word[0]))
                {
                Array.Resize(ref c, c.Length + 1);
                c[^1] = char.ToLower(word[0]);
                }
                else if (word.Length > 1 && char.IsLetter(word[1]) && !char.IsLetter(word[0]))
                {
                    Array.Resize(ref c, c.Length + 1);
                    c[^1] = char.ToLower(word[1]);
                }


            }
            char[] uniqch = c.Distinct().ToArray();
            _output = new (char, double)[uniqch.Length];
            double countWords = 0;
            foreach (var word in words2)
            {
                if (word.Length > 0 && char.IsLetter(word[0]))
                {

                    countWords++;

                }
            }

            for (int i = 0; i < uniqch.Length; i++)
            {
                double count = 0;
                
                
                foreach (var word in words2)
                {
                    if (word.Length > 0 && char.IsLetter(word[0]))
                    {
                        if (uniqch[i] == char.ToLower(word[0]))
                        {
                            count++;
                        }
                    }
                    else if (word.Length > 1 && char.IsLetter(word[1]) && !char.IsLetter(word[0]))
                    {
                        if (uniqch[i] == char.ToLower(word[1]))
                        {
                            count++;
                        }
                    }

                }
                _output[i] = (uniqch[i], (count / countWords) *100);
            }

            for (int i = 0; i < _output.Length - 1; i++) 
            {

                for (int j = 0; j < _output.Length - i - 1; j++)
                    if (_output[j].Item2 < _output[j + 1].Item2) 
                        (_output[j], _output[j + 1]) = (_output[j + 1], _output[j]); 
            }

            for (int i = 0; i < _output.Length - 1; i++)
            {

                for (int j = 0; j < _output.Length - i - 1; j++)
                    if(Math.Abs(_output[j].Item2 - _output[j + 1].Item2) < 0.0001)
                    {
                        if (_output[j].Item1 > _output[j + 1].Item1) 
                            (_output[j], _output[j + 1]) = (_output[j + 1], _output[j]);
                    }
            }
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine,_output.Select(tuple => $"{tuple.Item1}:{tuple.Item2:F4}"));
        }
    }
}
