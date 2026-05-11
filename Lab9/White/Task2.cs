using System;
using System.Text;

namespace Lab9.White;

public class Task2 : White
{
    private readonly char[] _vowels =
        {
            'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я',
            'А', 'Е', 'Ё', 'И', 'О', 'У', 'Ы', 'Э', 'Ю', 'Я',
            'a', 'e', 'i', 'o', 'u', 'y',
            'A', 'E', 'I', 'O', 'U', 'Y'
        };
 
        private int[,] _output;
 
        public int[,] Output => _output;
 
        public Task2(string text) : base(text)
        {
            _output = new int[0, 2];
            Review();
        }
 
        public override void Review()
        {
            _output = new int[0, 2];
 
            if (string.IsNullOrEmpty(Input))
                return;
 
            string[] words = ExtractWords(Input);
            if (words.Length == 0)
                return;
 
            int maxSyllables = 0;
            int[] syllableCounts = new int[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                syllableCounts[i] = CountSyllables(words[i]);
                if (syllableCounts[i] > maxSyllables)
                    maxSyllables = syllableCounts[i];
            }
 
            int[] freq = new int[maxSyllables + 1];
            for (int i = 0; i < syllableCounts.Length; i++)
                freq[syllableCounts[i]]++;
 
            int rowCount = 0;
            for (int s = 1; s <= maxSyllables; s++)
                if (freq[s] > 0)
                    rowCount++;
 
            _output = new int[rowCount, 2];
            int row = 0;
            for (int s = 1; s <= maxSyllables; s++)
            {
                if (freq[s] > 0)
                {
                    _output[row, 0] = s;
                    _output[row, 1] = freq[s];
                    row++;
                }
            }
        }
 
        private string[] ExtractWords(string text)
        {
            int count = 0;
            int i = 0;
            while (i < text.Length)
            {
                if (char.IsLetter(text[i]))
                {
                    bool precededByDigit = i > 0 && char.IsDigit(text[i - 1]);
                    int j = i;
                    while (j < text.Length && (char.IsLetter(text[j]) || text[j] == '-' || text[j] == '`' || text[j] == '\''))
                        j++;
                    if (!precededByDigit)
                        count++;
                    i = j;
                }
                else
                    i++;
            }

            string[] words = new string[count];
            int idx = 0;
            i = 0;
            var sb = new StringBuilder();
            while (i < text.Length)
            {
                if (char.IsLetter(text[i]))
                {
                    bool precededByDigit = i > 0 && char.IsDigit(text[i - 1]);
                    sb.Clear();
                    int j = i;
                    while (j < text.Length && (char.IsLetter(text[j]) || text[j] == '-' || text[j] == '`' || text[j] == '\''))
                    {
                        sb.Append(text[j]);
                        j++;
                    }
                    if (!precededByDigit)
                    {
                        string word = sb.ToString().TrimEnd('-', '`', '\'');
                        if (word.Length > 0)
                            words[idx++] = word;
                    }
                    i = j;
                }
                else
                    i++;
            }
            return words;
        }
 
        private int CountSyllables(string word)
        {
            int count = 0;
            foreach (char c in word)
                if (Array.IndexOf(_vowels, c) >= 0)
                    count++;
            return count == 0 ? 1 : count;
        }
 
        public override string ToString()
        {
            if (_output == null || _output.GetLength(0) == 0)
                return string.Empty;
 
            var sb = new StringBuilder();
            for (int i = 0; i < _output.GetLength(0); i++)
            {
                if (i > 0) 
                    sb.Append('\n');
                sb.Append(_output[i, 0]);
                sb.Append(':');
                sb.Append(_output[i, 1]);
            }
            return sb.ToString();
        }
}