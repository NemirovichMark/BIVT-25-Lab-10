using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public class Task4 : White
    {
        private int _output;

        public Task4(string text) : base(text)
        {
        }

        public int Output
        {
            get
            {
                return _output;
            }
        }

        public override void Review()
        {
            _output = CalculateSumOfDigits(Input);
        }

        private int CalculateSumOfDigits(string text)
        {
            int sum = 0;

            foreach (char i in text)
            {
                if (char.IsDigit(i))
                {
                    sum += i - '0';
                }
            }

            return sum;
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
