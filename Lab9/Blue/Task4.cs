using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task4 : Blue
    {
        public Task4(string input) : base(input) { }
        private int _output;
        public int Output => _output;

        public override string ToString()
        {
            return Output.ToString();
        }

        public override void Review()
        {
            if (Input == null)
            {
                _output = 0;
                return;
            }

            int sum = 0;
            int i = 0;
            while (i < Input.Length)
            {
                if (!char.IsDigit(Input[i]))
                {
                    i++;
                    continue;
                }

                int number = 0;
                while (i < Input.Length && char.IsDigit(Input[i]))
                {
                    number = number * 10 + (Input[i] - '0');
                    i++;
                }
                sum += number;
            }

            _output = sum;
        }
    }
}
