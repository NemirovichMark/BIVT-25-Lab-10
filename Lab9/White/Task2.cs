using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public class Task2 : White
    {
        private int[,] _output; // количество слогов, частота встречи таких слов

        public Task2(string text) : base(text) { }

        public override void Review()
        {
            _output = syllablesMatrix(Input);
        }

        public int[,] Output => _output ?? new int[0, 2];

        public static int[,] syllablesMatrix(string text)
        {
            var currentWord = new System.Text.StringBuilder();
            bool wordStartedAfterDigit = false;  // слово сразу после цифры не считаем
            bool lastWasDigit = false;

            int wordCount = 0;
            foreach (char c in text)                              // считаем нормальные слова и создаем массив нужного размера
            {
                if (char.IsLetter(c) || c == '-' || c == '\'')
                {
                    if (currentWord.Length == 0)
                        wordStartedAfterDigit = lastWasDigit;
                    currentWord.Append(c);
                    lastWasDigit = false;
                }
                else
                {
                    if (currentWord.Length > 0)
                    {
                        if (!wordStartedAfterDigit)
                            wordCount++;
                        currentWord.Clear();
                    }
                    lastWasDigit = char.IsDigit(c);
                }
            }
            if (currentWord.Length > 0 && !wordStartedAfterDigit) 
                wordCount++;

            string[] words = new string[wordCount];
            currentWord.Clear();
            wordStartedAfterDigit = false;
            lastWasDigit = false;
            int wi = 0;

            foreach (char c in text)                                 // заполняем массив словами
            {
                if (char.IsLetter(c) || c == '-' || c == '\'')
                {
                    if (currentWord.Length == 0)
                        wordStartedAfterDigit = lastWasDigit;
                    currentWord.Append(c);
                    lastWasDigit = false;
                }
                else
                {
                    if (currentWord.Length > 0)
                    {
                        if (!wordStartedAfterDigit)
                            words[wi++] = currentWord.ToString();
                        currentWord.Clear();
                    }
                    lastWasDigit = char.IsDigit(c);
                }
            }
            if (currentWord.Length > 0 && !wordStartedAfterDigit)
                words[wi] = currentWord.ToString();

            char[] vowels = { 'а','е','ё','и','о','у','ы','э','ю','я',
            'А','Е','Ё','И','О','У','Ы','Э','Ю','Я',
            'a','e','i','o','u','y','A','E','I','O','U','Y' };

            int[] syllablesPerWord = new int[words.Length];
            int maxSyllables = 0;

            for (int i = 0; i < words.Length; i++)
            {
                int syllables = 0;

                foreach (char c in words[i])
                {
                    if (Array.IndexOf(vowels, c) >= 0)
                        syllables++;
                }

                if (syllables == 0)
                    syllables = 1;

                syllablesPerWord[i] = syllables;

                if (syllables > maxSyllables)
                    maxSyllables = syllables; 
            }

            // нашли максимальное кол во символов и выделяем под них матрицу(временная)
            int[,] matrix = new int[maxSyllables, 2]; // номер слога, частота

            for (int i = 0; i < words.Length; i++)
            {
                int s = syllablesPerWord[i];
                if (s > 0)
                {
                    matrix[s - 1, 0] = s;
                    matrix[s - 1, 1]++;
                }
            }

            int filledCount = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, 1] > 0)
                    filledCount++;
            }

            int[,] result = new int[filledCount, 2]; // конечная матрица еопируем только заполненные стр
            int resultIndex = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, 1] > 0)
                {
                    result[resultIndex, 0] = matrix[i, 0];
                    result[resultIndex, 1] = matrix[i, 1];
                    resultIndex++;
                }
            }

            return result;
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
                result.AppendLine();
            }

            return result.ToString().TrimEnd();
        }
    }
}
