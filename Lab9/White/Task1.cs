using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public class Task1 : White 
    {
        private readonly char[] _punctuationMarks =
        {
            '.', '!', '?', ',', ':', '"', ';', '–', '\'', '(', ')', '[', ']', '{', '}', '/'
        };

        private readonly char[] _sentenceEnders = { '.', '!', '?' };

        public double Output
        {
            get
            {
                if (string.IsNullOrEmpty(Input))
                    return 0;

                return CalculateAverageComplexity();
            }
        }

        public Task1(string text) : base(text) // пустой конструктор
        {
        }

        private double CalculateAverageComplexity() // находим среднее(символы / количество предложений)
        {
            string[] sentences = SplitBySentenceEnders(Input);

            if (sentences.Length == 0)
                return 0;

            double totalComplexity = 0;
            int realSentenceCount = 0;

            foreach (var sentence in sentences)
            {
                var trimmed = sentence.Trim();
                if (string.IsNullOrWhiteSpace(trimmed))
                    continue;

                int wordCount = CountWords(trimmed);
                int punctuationCount = CountPunctuation(trimmed);

                totalComplexity += wordCount + punctuationCount;
                realSentenceCount++;
            }

            return realSentenceCount == 0 ? 0 : totalComplexity / realSentenceCount;
        }

        private string[] SplitBySentenceEnders(string text)
        {
            int count = 0;
            var current = new StringBuilder(); // смотрим предложение

            for (int i = 0; i < text.Length; i++)
            {
                current.Append(text[i]); // добавляем символ
                if (Array.IndexOf(_sentenceEnders, text[i]) >= 0) // проверка на конец предл если конец + дальше идёт пробел перенос тд
                {
                    bool isLast = i == text.Length - 1;
                    bool nextIsSpace = !isLast && char.IsWhiteSpace(text[i + 1]);
                    if (isLast || nextIsSpace)
                    {
                        count++;
                        current.Clear(); // очистка для сл предл
                    }
                }
            }
            if (current.Length > 0)
                count++; // счетчик предложений

            string[] sentences = new string[count];
            current.Clear();
            int idx = 0;

            for (int i = 0; i < text.Length; i++)
            {
                current.Append(text[i]);
                if (Array.IndexOf(_sentenceEnders, text[i]) >= 0)
                {
                    bool isLast = i == text.Length - 1;
                    bool nextIsSpace = !isLast && char.IsWhiteSpace(text[i + 1]);
                    if (isLast || nextIsSpace)
                    {
                        sentences[idx++] = current.ToString();
                        current.Clear();
                    }
                }
            }
            if (current.Length > 0)
                sentences[idx] = current.ToString();

            return sentences;
        }

        private int CountWords(string text)
        {
            var cleaned = new StringBuilder();

            foreach (var ch in text)
            {
                if (Array.IndexOf(_punctuationMarks, ch) < 0)
                    cleaned.Append(ch);
            }

            var words = cleaned.ToString().Replace("-", " ").Split(
                new[] { ' ', '\t', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries
            );

            return words.Length;
        }

        private int CountPunctuation(string text)
        {
            int count = 0;

            foreach (var ch in text)
            {
                if (Array.IndexOf(_punctuationMarks, ch) >= 0)
                    count++;
            }

            return count;
        }

        public override void Review()
        {
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
