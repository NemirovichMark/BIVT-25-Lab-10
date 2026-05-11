using System;
using System.Text;

namespace Lab9.White
{
    public class Task4 : White
    {
        private int _output;

        public int Output => _output;

        public Task4(string text) : base(text)
        {
            _output = 0;
            Review();
        }

        public override void Review()
        {
            _output = 0;

            if (string.IsNullOrEmpty(Input))
                return;

            foreach (char c in Input)
            {
                if (c >= '0' && c <= '9')
                    _output += c - '0';
            }
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}