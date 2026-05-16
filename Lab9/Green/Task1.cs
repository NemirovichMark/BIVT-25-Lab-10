using System;

namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char, double)[] _output;
        public (char, double)[] Output => _output;
        public Task1(string input) : base(input)
        {
            _output = Array.Empty<(char, double)>();
        }
        public override void Review()
        {
            string text = Input.ToLower();
            char[] rus_letters = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
            int[] counts = new int[rus_letters.Length];
            double totalLetters = 0;
            
            for (int i = 0; i < text.Length; i++)
            {
                char currentLetter = text[i];
                if (char.IsLetter(currentLetter))
                {
                    totalLetters++;
                    for (int j = 0; j < rus_letters.Length; j++)
                    {
                        if (currentLetter == rus_letters[j])
                        {
                            counts[j]++;
                            break;
                        }
                    }
                }
            }
            
            int differentLettersCount = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > 0)
                {
                    differentLettersCount++;
                }
            }
            
            (char, double)[] result = new (char, double)[differentLettersCount];
            
            int resultIndex = 0;
            for (int i = 0; i < rus_letters.Length; i++)
            {
                if (counts[i] > 0)
                {
                    double frequency = counts[i] / totalLetters;
                    result[resultIndex] = (rus_letters[i], frequency);
                    resultIndex++;
                }
            }
            _output = result;
        }
        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return "";
    
            string result = "";
    
            for (int i = 0; i < _output.Length; i++)
            {
                char letter = _output[i].Item1;
                double frequency = _output[i].Item2;
                string value = frequency.ToString("F4");
                value = value.Replace('.', ',');
                if (i > 0)
                {
                    result += "\n";
                }
                result += letter + ":" + value;
            }
            return result;
        }
    }
}