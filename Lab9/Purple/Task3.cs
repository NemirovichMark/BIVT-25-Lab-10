using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string _output;
        protected (string, char)[] _codes;
        public string Output => _output;

        public (string, char)[] Codes => _codes;

        public Task3(string input) : base(input)
        {
            _output = "";
            _codes = new (string, char)[5];
        }
        public override void Review()
        {
            string input = _input;
            char[] inputarray = input.ToCharArray();
            int c = 0;
            for (int j = 0; j < inputarray.Length - 1; j++)
            {
                if (Char.IsLetter(inputarray[j]) && Char.IsLetter(inputarray[j + 1]))
                {
                    c++;
                }
            }

            string[] sravnenie = new string[c];
            c = 0;
            for (int j = 0; j < inputarray.Length - 1; j++)
            {
                if (Char.IsLetter(inputarray[j]) && Char.IsLetter(inputarray[j + 1]))
                {
                    sravnenie[c++] = inputarray[j] + "" + inputarray[j + 1];
                }
            }
            string[] itog = sravnenie.Distinct().ToArray(); // Distinct().ToArray()- убирает повторяющиеся в массиве
            int[] mxarray = new int[itog.Length];
            for (int j = 0; j < itog.Length; j++)
            {
                int len = 0;
                for (int i = 0; i < sravnenie.Length; i++)
                {
                    if (itog[j] == sravnenie[i])
                    {
                        len++;
                    }

                }
                mxarray[j] = len;
            }
            // сортировка
            for (int i = 0; i < mxarray.Length; i++)
            {
                for (int j = 1; j < mxarray.Length; j++)
                {
                    if (mxarray[j - 1] < mxarray[j])
                    {
                        (mxarray[j - 1], mxarray[j]) = (mxarray[j], mxarray[j - 1]);
                        (itog[j - 1], itog[j]) = (itog[j], itog[j - 1]);
                    }
                }
            }
            string[] itog5 = new string[5];
            int[] mxarray5 = new int[5];
            // первые 5 которые больше всего встречаются в тексте
            for (int i = 0; i < 5; i++)
            {
                itog5[i] = itog[i];
                mxarray5[i] = mxarray[i];

            }

            c = 0;
            for (int i = 32; i < 127; i++)
            {
                int count = 0;
                for (int j = 0; j < inputarray.Length; j++)
                {
                    if ((char)i == inputarray[j])
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    c++;
                }
            }
            char[] chararray = new char[c];
            c = 0;
            for (int i = 32; i < 127; i++)
            {
                int count = 0;
                for (int j = 0; j < inputarray.Length; j++)
                {
                    if ((char)i == inputarray[j])
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    chararray[c++] = (char)i;
                }
            }
            string[] chararray5 = new string[5];
            for (int i = 0; i < 5; i++)
            {
                chararray5[i] = chararray[i] + "";
            }

            for (int i = 0; i < 5; i++)
            {
                input = input.Replace(itog[i], chararray5[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                _codes[i] = (itog[i], chararray[i]);
            }
            _output = input;
        }

        public override string ToString()
        {
            return _output;
        }

    }
}
