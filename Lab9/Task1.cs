using System.Text;

namespace Lab9.White
{
    public class Task1 : White
    {
        

        public double Output
        {
            get
            {
                
                if (string.IsNullOrEmpty(Input))
                    return 0;
                
                return Calculate();
            }
            
        }
        private readonly char[] _punctuationMarks = {'.', '!', '?', ',', ':', '"', ';', '–', '\'', '(', ')', '[', ']', '{', '}', '/' }; //массив из одинночных символов
        private readonly char[] _sentenceEnders = { '.', '!', '?' };

        public Task1(string text) : base(text)
        {
        }
        private double Calculate()//ср знач
        {
            string[] sentences = SplitBySentenceEnders(Input);

            if (sentences.Length == 0)
                return 0;
            double totalComplexity = 0; //суммарная сложность всех предложений
            int realSentenceCount = 0; //счетчик реальных (не пустых) предложений

            foreach (var sentence in sentences) //перебираем каждое предложение 
            {
                var trimmed = sentence.Trim(); //убираем пробелы и пустые символы в начале и конце
                if (string.IsNullOrWhiteSpace(trimmed)) //если после обрезки пусто то пропускаем (не считаем его)
                    continue;

                int wordCount = CountWords(trimmed); //кол-во слов в предл
                int punctuationCount = CountPunctuation(trimmed); //кол-во знаков препинания

                totalComplexity += wordCount + punctuationCount; //прибавляем к общей сложности
                realSentenceCount++; //счетчик реальных предл
            }
            if (realSentenceCount == 0)
                return 0;
            else
                return totalComplexity / realSentenceCount;
        }

        private string[] SplitBySentenceEnders(string text)
        {
            int count = 0;//счетчик
            var current = new StringBuilder(); //позволяет менять содержимое без создания новых объектов

            for (int i = 0; i < text.Length; i++)//цикл по каждому символу и добавляем в накопитель
            {
                current.Append(text[i]);
                if (Array.IndexOf(_sentenceEnders, text[i]) >= 0)//метод который ищет элемент в массиве и возвр его индекс (1 пар это массив, 2 пар знач, то есть берем все больше 0)
                {//если тек сим это знак кон предл
                    bool isLast = i == text.Length - 1;//последние ли символ в тексте 
                    bool nextIsSpace = !isLast && char.IsWhiteSpace(text[i + 1]);//если последний то проверка идет ли после него пробел или перенос строки 
                    if (isLast || nextIsSpace)//если знак в конце текста или после него пробел то увел счетчик
                    {
                        count++;
                        current.Clear();//очищаем для след предл 
                    }
                }
            }
            if (current.Length > 0)//если после цикла остались символы без знака то добавляем 1 предл
                count++; // счетчик предложений

            string[] sentences = new string[count];//массив 
            current.Clear();
            int idx = 0;
            
            for (int i = 0; i < text.Length; i++)//2 проход по тексту, 1 мы считаем кол-во предл, для создания массива нужного размера а 2 нужен для заполнения массива строками 
            {
                current.Append(text[i]);
                if (Array.IndexOf(_sentenceEnders, text[i]) >= 0)
                {
                    bool isLast = i == text.Length - 1;
                    bool nextIsSpace = !isLast && char.IsWhiteSpace(text[i + 1]);
                    if (isLast || nextIsSpace)
                    {
                        sentences[idx++] = current.ToString();//сохраняем накоп предл в массив и двигаем индекс
                        current.Clear();
                    }
                }
            }
            if (current.Length > 0)
                sentences[idx] = current.ToString();//превращаем остатки после цикла в строку посл предл

            return sentences;//возвр массив предлож
        }
        private int CountWords(string text)
        {
            var cleaned = new StringBuilder();//текст без знаков препинания

            foreach (var ch in text)//если символ не знак припоминания то добавляем в очищенный текст
            {
                if (Array.IndexOf(_punctuationMarks, ch) < 0)
                    cleaned.Append(ch);
            }

            var words = cleaned.ToString().Replace("-", " ").Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            //заменяем запятные на пробелы, разбиваем по разделителям, удаляет пустые элементы (несколько пробелов подряд)
            return words.Length;//возвращаем кол-во слов
        }
        private int CountPunctuation(string text)//считаем кол-во знаков препинания 
        {
            int count = 0;//счетчик

            foreach (var ch in text)//перебираем 
            {
                if (Array.IndexOf(_punctuationMarks, ch) >= 0)//если символ найден в массиве знаков препинания 
                    count++;//увеличиваем
            }

            return count;//возвращаем кол-во знаков препинания 
        }


        public override void Review() //метод
        {
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}

