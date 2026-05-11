using System;
using System.Text;

namespace Lab9.White
{
    public class Task1 : White
    {
        private readonly char[] _punctuationMarks =
        {
            '.', '!', '?', ',', ':', '"', ';',  '\'', '(', ')', '[', ']', '{', '}', '/'
        };
        private readonly char[] _sentenceEnders = { '.', '!', '?' };

        private double _output;

        public double Output => _output;

        public Task1(string text) : base(text)
        {
            _output = 0.0;
            Review();
        }

        public override void Review()
        {
            _output = 0.0;

            if (string.IsNullOrEmpty(Input))
                return;

            string[] sentences = SplitBySentenceEnders(Input);
            if (sentences.Length == 0)
                return;

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

            if (realSentenceCount > 0)
                _output = totalComplexity / realSentenceCount;
        }

        private string[] SplitBySentenceEnders(string text)
        {
            int count = 0;
            var current = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                current.Append(text[i]);
                if (Array.IndexOf(_sentenceEnders, text[i]) >= 0)
                {
                    bool isLast = i == text.Length - 1;
                    bool nextIsSpace = !isLast && char.IsWhiteSpace(text[i + 1]);
                    if (isLast || nextIsSpace)
                    {
                        count++;
                        current.Clear();
                    }
                }
            }
            if (current.Length > 0)
                count++;

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

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}