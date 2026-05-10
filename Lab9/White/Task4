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
        public int Output => _output;
        public Task4(string input) : base(input)
        {
            _output = 0;
        }
        public override void Review()
        {
            // Если текст null, сумма должна быть 0
            if (string.IsNullOrEmpty(_input))
            {
                _output = 0;
                return;
            }

            int sum = 0;
            // проходим по всему тексту _input
            for (int i = 0; i < _input.Length; i++)
            {
                char ch = _input[i];
                
                if (ch >= '0' && ch <= '9')
                {
                    // Превращаем символ в число через ASCII-код
                    sum += (int)(ch - '0');
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
