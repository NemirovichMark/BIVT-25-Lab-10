using System.Text;

namespace Lab9.White
{
    public class Task3 : White
    {
        private string[,] _codes;
        private string _output;

        public Task3(string text, string[,] codes) : base(text)
        {
            _codes = codes;
        }

        public string Output
        {
            get
            {
                if (string.IsNullOrEmpty(_output))//если пустой или нуль 
                    return Input;
                return _output;
            }
        }

        public override void Review()
        {
            _output = ReplaceWordsWithCodes(Input);
        }

        private string ReplaceWordsWithCodes(string text)//заменяет слова в тексте на код 
        {
            var result = new StringBuilder();// буфер для строки (можно убирать добавлят и тд)
            var currentWord = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];//1 символ

                if (char.IsLetter(ch) || ch == '\'' || ch == '-')//если слово апостров или - то добавляем в слову 
                {
                    currentWord.Append(ch);
                }
                else
                {
                    if (currentWord.Length > 0)// если ест собранное слово
                    {
                        string word = currentWord.ToString();//преобразуем в слово
                        string code = FindCode(word);//ищем код
                        result.Append(code);//добавляем
                        currentWord.Clear();//чистим 
                    }
                    result.Append(ch);//если не буква апос или - то добавляем 
                }
            }

            if (currentWord.Length > 0)//если есть собранное слово то превращаем в строку из буфера и ищем код потом добавляем в резултат
            {
                string word = currentWord.ToString();
                string code = FindCode(word);
                result.Append(code);
            }

            return result.ToString();//возвр резултат строку
        }

        private string FindCode(string word)//ищем код
        {
            for (int i = 0; i < _codes.GetLength(0); i++)//по всем строкам 2 мерного массива 
            {
                if (string.Equals(_codes[i, 0], word, StringComparison.Ordinal))//слово из словаря,слово которое нужно найти, тип сравнения (побайтовое не важен регистр)
                {
                    return _codes[i, 1];
                }
            }
            return word;
        }

        public override string ToString()
        {
            return Output;
        }
    }
}
