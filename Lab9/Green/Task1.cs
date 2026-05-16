using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char letter, double freq)[] _arr;
        private int count;


        public (char, double)[] Output => _arr.ToArray();

        public Task1(string intext) : base(intext)
        {
            _arr = new (char, double)[0];
        }

        public override void Review()
        {
            _arr = new (char letter, double freq)[0];
            count = 0;
            string str = Input.ToLower();
            foreach (char s in str)
            {
                if (char.IsLetter(s))
                    count++;

                if (s >= 'а' && s <= 'я' || s == 'ё')
                {
                    bool found = false;
                    int index = 0;
                    for (int i = 0; i < _arr.Length; i++)
                    {
                        if (_arr[i].letter == s)
                        {
                            found = true;
                            index = i;
                            break;
                        }
                    }
                    if (found)
                    {
                        var t = _arr[index];
                        _arr[index] = (t.letter, t.freq + 1);
                    }

                    else
                    {
                        Array.Resize(ref _arr, _arr.Length + 1);
                        _arr[_arr.Length - 1] = (letter: s, freq: 1);
                    }

                }
                
            }
            if (count == 0)
                return;

            for (int i = 0; i < _arr.Length; ++i)
            {
                var tuple = _arr[i];
                _arr[i] = (letter: tuple.letter, freq: tuple.freq / (double)count);
            }
            Sort();
        } 



        protected void Sort()
        {
            int i = 1;
            while (i < _arr.Length)
            {
                if (i == 0 || _arr[i - 1].letter <= _arr[i].letter)
                {
                    i++;
                }
                else
                {
                    var temp = _arr[i];
                    _arr[i] = _arr[i - 1];
                    _arr[i - 1] = temp;
                    i--;
                }
            }
        }


        public override string ToString()
        {
            string ans="";
            int i = 0;
            foreach(var it in Output)
            { 
                ans += it.Item1 + ":" + it.Item2.ToString("F4");
                if (i != Output.Length - 1)
                    ans += "\n";
                i++;
            }
            return ans;
        }

    }
}
