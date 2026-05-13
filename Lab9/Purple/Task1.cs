using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        private string _output;

        public string Output => _output;


        public Task1(string input) : base(input) 
        {
            _output = input;
        }
        public override void Review()
        {
            string _input1 = _input;
            string[] znaki = new string[] { ".", "!", "?", ",", ":", "\"", ";", "[', ']", "–", "{', '}", "/", "(" , ")"};
            for (int i = 0; i < znaki.Length; i++)
            {
                _input1 = _input1.Replace(znaki[i], " " + znaki[i] + " ");
            }

            string[] inputarray = _input1.Split(' ');
            string[] outputarray = new string[inputarray.Length];

            for (int i = 0; i < inputarray.Length; i++)
            {
                char[] slovo = inputarray[i].ToCharArray(); // преобразовываем каждое слово в массив char

                bool f = true;
                foreach (char a in slovo)
                {
                    if (char.IsDigit(a) )
                    {
                        f = false; break;
                    }
                }
                if (f)
                {
                    Array.Reverse(slovo);
                }
                string itogslovo = string.Join("", slovo);
                outputarray[i] = itogslovo;
            }
            string output = string.Join(" ", outputarray);

            for (int i = 0; i < znaki.Length; i++)
            {
                output = output.Replace(" " + znaki[i] + " ", znaki[i]);
            }

            _output = output;



        }
        //public override string ToString() => Output ?? string.Empty;
        public override string ToString()
        {
            return _output;
        }
    }
}
