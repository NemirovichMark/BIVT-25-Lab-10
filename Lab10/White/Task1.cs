using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public class Task1 : White
    {
        private double _output = 0.0;
        public double Output => _output;

        public Task1(string input) : base(input)
        {
            Review();
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = 0.0;
                return;
            }

            // Знаки конца предложения
            char[] sentenceEndings = { '.', '!', '?' };

            // Разбиваем на предложения
            string[] sentences = Input.Split(sentenceEndings, StringSplitOptions.RemoveEmptyEntries);

            if (sentences.Length == 0)
            {
                _output = 0.0;
                return;
            }

            double totalComplexity = 0.0;

            for (int i = 0; i < sentences.Length; i++)
            {
                totalComplexity += GetSentenceComplexity(sentences[i]);
            }

            _output = totalComplexity / sentences.Length;
        }

        private int GetSentenceComplexity(string sentence)
        {
            int wordCount = CountWords(sentence);
            int punctuationCount = CountPunctuation(sentence);
            return wordCount + punctuationCount;
        }

        private int CountWords(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;

            int count = 0;
            bool inWord = false;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                // Буквы, дефис и апостроф считаются частью слова
                if (char.IsLetter(c) || c == '-' || c == '\'')
                {
                    if (!inWord)
                    {
                        inWord = true;
                        count++;
                    }
                }
                else
                {
                    inWord = false;
                }
            }
            return count;
        }

        private int CountPunctuation(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;

            // Знаки препинания по условию
            char[] punctuationMarks = { '.', '!', '?', ',', ':', '"', ';', '–', '(', ')', '[', ']', '{', '}', '/' };
            int count = 0;

            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < punctuationMarks.Length; j++)
                {
                    if (text[i] == punctuationMarks[j])
                    {
                        count++;
                        break;
                    }
                }
            }
            return count;
        }

        public override string ToString()
        {
            // Ограничиваем 4 знаками после запятой
            return _output.ToString("F4");
        }
    }
}
