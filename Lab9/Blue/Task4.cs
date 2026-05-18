using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task4 : Blue
    {
        private int _output;

        public int Output
        {
            get
            {
                return _output;
            }
        }

        public Task4(string text) : base(text)
        {
            _output = 0;
        }

        public override void Review()
        {
            if (Input == null)
            {
                _output = 0;
                return;
            }

            int sum = 0;
            int index = 0;

            while (index < Input.Length)
            {
                if (char.IsDigit(Input[index]))
                {
                    int number = 0;

                    while (index < Input.Length && char.IsDigit(Input[index]))
                    {
                        number = number * 10 + (Input[index] - '0');
                        index++;
                    }

                    sum += number;
                }
                else
                {
                    index++;
                }
            }

            _output = sum;
        }

        public override string ToString()
        {
            return _output.ToString();
        }
    }
}
