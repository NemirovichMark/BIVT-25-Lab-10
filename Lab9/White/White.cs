namespace Lab9.White
{
    public abstract class White
    {
        protected string _input;

        protected char[] _ends = new char[] { '.', '?', '!' }; // конец предлож

        protected char[] _punctuation = new char[] { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' }; // знаки преп
        
        public string Input => _input; // для чтения текста

        protected White(string input)
        {
            _input = input;

        }
        public abstract void Review();

        public virtual void ChangeText(string text) // для обновления текста и пересчета результата
        {
            _input = text;
            Review(); // пересчитываем _output в наследнике после смены текста
        }
        protected string[] SplitToSentenses() // разбиваем текст на массив предложений по знакам из _ends
        {
            return _input.Split(_ends, StringSplitOptions.RemoveEmptyEntries);
        }
        protected string[] SplitToWords(string text) // разбиваем на слова
        {
            var rawWords = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var words = new string[rawWords.Length];
            for (int i = 0; i < rawWords.Length; i++)
            {
                words[i] = rawWords[i].Trim(_punctuation);
            }
            var count = 0;// -1 2015 16th
            for (int i = 0; i < words.Length; i++)
            {
                if (string.IsNullOrEmpty(words[i])) // || Char.IsDigit(words[i][0])
                {
                    count++;
                }
            }
            var result = new string[words.Length - count];
            count = 0;
            for (int i = 0, j = 0; i < words.Length; i++)
            {
                if (string.IsNullOrEmpty(words[i])) // || Char.IsDigit(words[i][0])

                {

                    count++;
                }
                else
                {
                    result[j] = words[i];
                    j++;
                }
            }
            return result;
            
        }
        protected string[] SplitToWords()
        {
            return SplitToWords(_input);
        }
        protected int CountPunctuation(string text) // считает кол-во знаков пунктуации
        {
            int count = 0;
            foreach (char ch in text)
            {
                if (char.IsPunctuation(ch)) count++;
            }
            return count;
        }
        protected int CountSyllables(string word)// для подсчета слогов
        {
            string vowels = "аеёиоуыэюяaeiouy";
            int count = 0;
            foreach(char ch in word.ToLower()) // мал буквы
            {
                if(vowels.Contains(ch)) count++;
            }
            return count == 0 ? 1 : count;
        }

    }

}