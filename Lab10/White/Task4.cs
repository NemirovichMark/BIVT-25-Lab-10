using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public class Task4 : White
    {
        private int _output = 0;
        public int Output => _output;

        public Task4(string input) : base(input)
        {
            Review();
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = 0;
                return;
            }

            int sum = 0;

            for (int i = 0; i < Input.Length; i++)
            {
                char c = Input[i];
                if (c >= '0' && c <= '9')
                {
                    // Превращаем символ цифры в число 
                    int digit = c - '0';
                    sum += digit;
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
