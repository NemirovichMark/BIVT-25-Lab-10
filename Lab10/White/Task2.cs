using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public class Task2 : White
    {
        private int[,] _output = new int[0, 0];
        public int[,] Output => _output;

        public Task2(string input) : base(input)
        {
            Review();
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = new int[0, 0];
                return;
            }

            // Извлекаем все слова
            string[] words = ExtractWords(Input);

            // Временный массив для хранения (слог, количество)
            int[] syllablesArray = new int[100]; // максимум 100 разных количеств слогов
            int[] countArray = new int[100];
            int uniqueCount = 0;

            // Подсчитываем слоги для каждого слова
            for (int i = 0; i < words.Length; i++)
            {
                if (string.IsNullOrEmpty(words[i])) continue;

                int syllables = CountSyllables(words[i]);

                // Ищем, есть ли уже такое количество слогов
                bool found = false;
                for (int j = 0; j < uniqueCount; j++)
                {
                    if (syllablesArray[j] == syllables)
                    {
                        countArray[j]++;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    syllablesArray[uniqueCount] = syllables;
                    countArray[uniqueCount] = 1;
                    uniqueCount++;
                }
            }

            // Сортируем пузырьком по возрастанию количества слогов
            for (int i = 0; i < uniqueCount - 1; i++)
            {
                for (int j = 0; j < uniqueCount - i - 1; j++)
                {
                    if (syllablesArray[j] > syllablesArray[j + 1])
                    {
                        // Меняем слоги
                        int tempSyllables = syllablesArray[j];
                        syllablesArray[j] = syllablesArray[j + 1];
                        syllablesArray[j + 1] = tempSyllables;

                        // Меняем количества
                        int tempCount = countArray[j];
                        countArray[j] = countArray[j + 1];
                        countArray[j + 1] = tempCount;
                    }
                }
            }

            // Создаём матрицу
            _output = new int[uniqueCount, 2];
            for (int i = 0; i < uniqueCount; i++)
            {
                _output[i, 0] = syllablesArray[i];
                _output[i, 1] = countArray[i];
            }
        }

        private string[] ExtractWords(string text)
        {
            // Сначала разбиваем по пробелам и другим разделителям
            string[] temp = new string[text.Length];
            int tempIndex = 0;
            string currentWord = "";

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c) || c == '-' || c == '\'')
                {
                    currentWord += c;
                }
                else
                {
                    if (currentWord.Length > 0)
                    {
                        temp[tempIndex] = currentWord;
                        tempIndex++;
                        currentWord = "";
                    }
                }
            }

            if (currentWord.Length > 0)
            {
                temp[tempIndex] = currentWord;
                tempIndex++;
            }

            // Копируем в массив нужного размера
            string[] result = new string[tempIndex];
            for (int i = 0; i < tempIndex; i++)
            {
                result[i] = temp[i];
            }

            return result;
        }

        private int CountSyllables(string word)
        {
            // Гласные буквы (Ё считается, Y считается)
            char[] vowels = {
            'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я',
            'a', 'e', 'i', 'o', 'u', 'y'
        };

            string lowerWord = word.ToLower();
            int count = 0;

            for (int i = 0; i < lowerWord.Length; i++)
            {
                for (int j = 0; j < vowels.Length; j++)
                {
                    if (lowerWord[i] == vowels[j])
                    {
                        count++;
                        break;
                    }
                }
            }

            // Слова без гласных считаем односложными
            if (count == 0) return 1;
            return count;
        }

        public override string ToString()
        {
            if (_output.GetLength(0) == 0) return "";

            string result = "";
            for (int i = 0; i < _output.GetLength(0); i++)
            {
                if (i > 0) result += "\n";
                result += _output[i, 0] + ":" + _output[i, 1];
            }
            return result;
        }
    }
}
