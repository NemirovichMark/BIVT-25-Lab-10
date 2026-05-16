using System;

namespace Lab9.Blue
{
    public class Task4 : Blue
    {
        private int _output = 0;

        public Task4(string input) : base(input) { }

        public override void Review()
        {
            int sum = 0;
            int number = 0;
            bool b = false;

            foreach (char ch in Input)
            {
                if (ch >= '0' && ch <= '9')
                {
                    number = number * 10 + (ch - '0');
                    b = true;
                }
                else
                {
                    if (b)
                    {
                        sum += number;
                        number = 0;
                        b = false;
                    }
                }
            }
            if (b)
            {
                sum += number;
            }

            _output = sum;
        }

        public int Output => _output;

        public override string ToString()
        {
            return _output.ToString();
        }
    }
}