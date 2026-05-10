using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab9.Blue
{
    public class Task4 : Blue
    {
        private int _output = 0;
        public int Output => _output;
        private int sum = 0;
        public Task4(string input) : base(input)
        {

        }
        public override void Review()
        {
            sum = 0;
            string input = _input;
            input = input.Replace(',', ' ');
            input = input.Replace('.', ' ');
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.None);
            string[] digits = new string[0];
            char[] allLetters = new char[]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л',
                'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш',
                'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я',
                'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л',
                'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш',
                'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я'
            };
            string[] words2 = new string[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                words2[i] = words[i].Trim(allLetters);
            }
            foreach (var word in words2)
            {

                if (!string.IsNullOrEmpty(word))
                {
                    if (char.IsDigit(word[0]))
                    {
                        Array.Resize(ref digits, digits.Length + 1);
                        digits[^1] = word;
                    }
                }
            }
;
            foreach (string dig in digits)
            {

                int digit = 0;
                for (int i = 0; i < dig.Length; i++)
                {
                    int x = 1;
                    for (int j = 0; j < dig.Length - i - 1; j++)
                    {
                        x = x * 10;
                    }
                    digit += (dig[i] - 48) * x;
                }
                sum += digit;
            }
            _output = sum;
        }
        public override string ToString()
        {
            if (_output == 0) return "0";
            string output = "";
            while (sum > 0)
            {
                int d = sum % 10;
                sum = sum / 10;
                char dig = (char)(48 + d);
                output = dig + output;
            }
            return output;
        }
    }
}