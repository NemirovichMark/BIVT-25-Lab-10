using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public class Task2 : White
    {
        private int [,] _output;
        public int [,] Output => _output;
        public Task2(string input) : base(input) // массив должен быть null
        {
            _output = new int[0, 0];
        }
        public override void Review()
        {
            string[] words = SplitToWords();
            if (words == null || words.Length == 0)
            {
                _output = new int[0, 0];
                return;
            }
            int[] syllableCounts = new int[101];
            int maxSyllables = 0;
            foreach(var word in words)
            {
                if (!string.IsNullOrEmpty(word) && char.IsLetter(word[0]))
                {
                    int s = CountSyllables(word); // метод в White
                    if (s < 101)
                    {
                        syllableCounts[s]++;
                        if (s > maxSyllables) maxSyllables = s;
                    }
                }
            }
            int futrows = 0;
            for(int i = 1;i <= maxSyllables;i++) // считаем, сколько строк будет в матрице(кол-во слогов > 0)
            {
                if (syllableCounts[i] > 0) futrows++;
            }
            _output = new int[futrows, 2];
            int currentRow = 0;
            for(int i = 1;i<= maxSyllables;i++)
            {
                if (syllableCounts[i] > 0)
                {
                    _output[currentRow, 0] = i;
                    _output[currentRow, 1] = syllableCounts[i];
                    currentRow++;
                }
            } 
        }
        public override string ToString()
        {
            var matrix = Output;
            if (matrix == null) return string.Empty;

            var result = new System.Text.StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result.Append(matrix[i, 0]);
                result.Append(':'); 
                result.Append(matrix[i, 1]);

                // Добавляем перенос только если это НЕ последняя строка
                if (i < matrix.GetLength(0) - 1)
                {
                    result.AppendLine();
                }
            }
            return result.ToString(); 
        }
    }
}