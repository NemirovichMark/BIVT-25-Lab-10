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
        public Task2(string text) : base(text??string.Empty)
        {
            _output = Array.Empty<string>();
        }
        public override void Review()
        {
            if (string.IsNullOrEmpty(Input)) { _output = Array.Empty<string>(); return; }
            string[] str = Input.Split(' ');
            string[] words = new string[str.Length];
            int k = 0;
            for (int i = 0; i < str.Length; i++)
                if (str[i]!="")
                    words[k++] = str[i];
            string[] strings= new string[words.Length]; int stc = 0;
            string[] a= new string[words.Length]; int c = 0;int l = 0;
            //strings - готовые строки(результат), stc - сколько сформировано strings,
            //a - текущая строка, c - сколько слов в a, l -  сколько букв в a (без пробелов).
            for (int i = 0; i < k; i++)
            {
                string word = words[i];
                if (c == 0)
                { a[c++] = word; l += word.Length; }
                else
                {
                    int len = l + word.Length + c; // сколько станет букв + пробелы
                    if (len <= 50)
                    { a[c++] = word; l += word.Length; }
                    else
                    {   strings[stc++] = L(a, c, l);
                        a=new string[words.Length];
                        a[0] = word; c = 1; l = word.Length;
                    }
                }
            }
            if (c>0) strings[stc++] = L(a, c, l); //если осталась незавершенная строка
            _output = new string[stc];
            for (int i=0;i<stc;i++) _output[i] = strings[i]; // итоговый массив
        }
        private string L(string[] a, int c, int l)
        {
            if (c == 1) return a[0]; // возвращаем без пробелов
            StringBuilder res = new StringBuilder();
            int b = (50 - l) / (c - 1); // минимум пробелов в 1 промежутке
            int q = (50 - l) % (c - 1); // количествло пробелов с b++
            for (int i = 0; i < c; i++)
            {
                res.Append(a[i]);
                if (i < c - 1)
                { 
                    int b1 = b;
                    if (q > 0)
                    { b1++; q--; }
                    res.Append(' ', b1);
                }
            }
            return res.ToString();
        }
        public override string ToString()
        {
            if (Output == null || Output.Length == 0) return string.Empty;
            return string.Join(Environment.NewLine, Output);
        }
    }
}
