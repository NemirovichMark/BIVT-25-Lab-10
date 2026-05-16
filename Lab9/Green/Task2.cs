using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task2 : Green
    {
        private (char letter, double freq)[] _arr;

        public Task2(string text) : base(text)
        {
            _arr = new (char, double)[0];
        }

        public override void Review()
        {
            char[] symbols = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', ' ' };

            _arr = new (char letter, double freq)[0];
            int count = 0;
            string str = Input.ToLower().Trim(symbols);
            char[] text = str.ToCharArray();

            if (text != null && text.Length > 0 && char.IsLetter(text[0]))
            {
                _arr = new (char letter, double freq)[] { (text[0], 1) };
                count++;
            }


            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    count++;
                }

                if (text.Length > 0 && char.IsLetter(text[i]) && !char.IsLetter(text[i - 1]) && !char.IsDigit(text[i - 1]) && text[i - 1] != '-' && text[i - 1] != '\'' && text[i - 1] != '’')
                {
                    bool found = false;
                    int index = 0;
                    for (int j = 0; j < _arr.Length; j++)
                    {
                        if (_arr[j].letter == text[i])
                        {
                            found = true;
                            index = j;
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
                        _arr[_arr.Length - 1] = (letter: text[i], freq: 1);
                    }

                }
            }
            if (count == 0)
                return;

            for (int l = 0; l < _arr.Length; ++l)
            {
                var tuple = _arr[l];
                _arr[l] = (letter: tuple.letter, freq: tuple.freq / (double)count);
            }
            Sort();
            
        }


        private void Sort()
        {
            for (int i = 1; i < _arr.Length; i++)
            {
                int j = i;
                while (i > 0)
                {
                    bool swapped = false;

                    if (_arr[i].freq > _arr[i - 1].freq)
                    {
                        var temp = _arr[i];
                        _arr[i] = _arr[i - 1];
                        _arr[i - 1] = temp;
                        swapped = true;
                    }
                    else if (_arr[i].freq == _arr[i - 1].freq)
                    {
                        if (_arr[i].letter < _arr[i - 1].letter)
                        {
                            var temp = _arr[i];
                            _arr[i] = _arr[i - 1];
                            _arr[i - 1] = temp;
                            swapped = true;
                        }
                    }

                    if (swapped)
                        i--;
                    else
                        break;
                }
                i = j;
            }
        }
        

        public char[] Output
        {
            get 
            {
                char[] result = new char[_arr.Length];
                for (int i = 0; i < _arr.Length; i++)
                {
                    result[i] = _arr[i].letter;
                }
                return result;
            }
        }
        public override string ToString()
        {
            return string.Join(", ", Output);
        }
    }
}







