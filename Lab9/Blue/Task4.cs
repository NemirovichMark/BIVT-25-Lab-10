using System;

namespace Lab9.Blue
{
    public class Task4 : Blue
    {
        private int _output = 0;

        public Task4(string input) : base(input) { }

        public override void Review()
        {
            int summ = 0;
            int number = 0;
            bool a = false;

            foreach (char ch in Input)
            {
                if (ch >= '0' && ch <= '9')
                {
                    number = number * 10 + (ch - '0');
                    a = true;
                }
                else
                {
                    if (a)
                    {
                        summ += number;
                        number = 0;
                        a = false;
                    }
                }
            }
            if (a)
            {
                summ += number;
            }

            _output = summ;
        }

        public int Output => _output;

        public override string ToString()
        {
            return _output.ToString();
        }
    }
}