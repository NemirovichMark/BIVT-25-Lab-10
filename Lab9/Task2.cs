namespace Lab9.White
{
    public class Task2 : White
    {
        private int[,] _output; 

        public Task2(string text) : base(text)
        {
        }

        public override void Review()
        {
            _output = syllablesMatrix(Input);
        }

        public int[,] Output => _output ?? new int[0, 2];//если null то вернуть матрицу 2 столбца без строк

        public static int[,] syllablesMatrix(string text)//принимаем текст возвращаем массив 
        {
            var currentWord = new System.Text.StringBuilder();//позволяет менять содержимое без создания новых объектов
            bool wordStartedAfterDigit = false; // слово сразу после цифры не считаем (например 123абв)
            bool lastWasDigit = false;

            int wordCount = 0;
            foreach (char c in text) // считаем нормальные слова и создаем массив нужного размера
            {
                if (char.IsLetter(c) || c == '-' || c == '\'')//Если буква, дефис или | — часть слова
                {
                    if (currentWord.Length == 0)//если слово сразу идет после цифры не берем если почле буквы или разделителя то считаем
                        wordStartedAfterDigit = lastWasDigit;
                    currentWord.Append(c);//добавляем символ и сбрасываем флаг 
                    lastWasDigit = false;
                }
                else//если не буква, не дефис и тд (те пробел точка или запятая)
                {
                    if (currentWord.Length > 0)//
                    {
                        if (!wordStartedAfterDigit)//если слово не сразу после цифры то считаем его 
                            wordCount++;
                        currentWord.Clear();
                    }

                    lastWasDigit = char.IsDigit(c);//запоминаем был ли разделитель цифрой для след слова
                }
            }

            if (currentWord.Length > 0 && !wordStartedAfterDigit)//если текст закончился на слове то его тоже считаем 
                wordCount++;

            string[] words = new string[wordCount];//собираем слова в массив
            currentWord.Clear();//сбрасываем все 
            wordStartedAfterDigit = false;
            lastWasDigit = false;
            int wi = 0;

            foreach (char c in text) // аналогично с первом только записываем сами слова и i++
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

            char[] vowels =//гласные
            {
                'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я',
                'А', 'Е', 'Ё', 'И', 'О', 'У', 'Ы', 'Э', 'Ю', 'Я',
                'a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y'
            };

            int[] syllablesPerWord = new int[words.Length];//считаем гласные в каждом слове => кол-во слогов
            int maxSyllables = 0;

            for (int i = 0; i < words.Length; i++)//если 0 гласных то 1 слог
            {
                int syllables = 0;

                foreach (char c in words[i])
                {
                    if (Array.IndexOf(vowels, c) >= 0)//ищем в массиве, если нашел то 
                        syllables++;
                }

                if (syllables == 0)
                    syllables = 1;

                syllablesPerWord[i] = syllables;

                if (syllables > maxSyllables)//максимум
                    maxSyllables = syllables;
            }

            // нашли максимальное кол во символов и выделяем под них матрицу(временная)
            int[,] matrix = new int[maxSyllables, 2]; // номер слога, частота

            for (int i = 0; i < words.Length; i++)//заполняем табличку
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

            int[,] result = new int[filledCount, 2]; // конечная матрица удаляем ненужные строки 
            int resultIndex = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)//Копируем только заполненные строки в итоговую матрицу и возвращаем её
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
            if (matrix == null) return string.Empty;//если нет то пустая строка

            var result = new System.Text.StringBuilder();//сборка строки

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result.Append(matrix[i, 0]);
                result.Append(':');
                result.Append(matrix[i, 1]);
                result.AppendLine();//перевод строки 
            }

            return result.ToString().TrimEnd();//цдаляем последний перевод строки 
        }
    }
}
