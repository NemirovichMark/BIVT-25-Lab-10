namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string _output;
        private (string, char)[] _codes;

        public string Output => _output;
        public (string, char)[] Codes => _codes;

        public Task3(string text) : base(text)
        {
            _output = default;
            _codes = default;
        }

        private string[] SplitText()
        {
            return _input.Split(' ', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
        }

        private (string, char)[] CreatePairStringChar(string[] bestStrings, int actualCount)
        {
            (string, char)[] bestPairs = new (string, char)[actualCount];
            
            // Находим все символы, которые уже есть в тексте
            bool[] usedChars = new bool[127];
            for (int i = 0; i < _input.Length; i++)
            {
                int code = (int)_input[i];
                if (code >= 32 && code <= 126)
                    usedChars[code] = true;
            }
            
            // Назначаем коды
            int codeIndex = 32;
            for (int i = 0; i < actualCount; i++)
            {
                while (codeIndex < 127 && usedChars[codeIndex])
                    codeIndex++;
                
                if (codeIndex < 127)
                {
                    bestPairs[i] = (bestStrings[i], (char)codeIndex);
                    usedChars[codeIndex] = true;
                    codeIndex++;
                }
            }
            
            return bestPairs;
        }

        private string[] FindTheBestPairs()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            Dictionary<string, int> firstOccurrence = new Dictionary<string, int>();
            string[] words = SplitText();

            // Собираем все пары ТОЛЬКО из слов
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word.Length < 2) continue;
                
                for (int j = 1; j < word.Length; j++)
                {
                    string pair = word[j - 1].ToString() + word[j].ToString();
                    
                    if (dict.ContainsKey(pair))
                    {
                        dict[pair]++;
                    }
                    else
                    {
                        dict.Add(pair, 1);
                        // Запоминаем первое вхождение
                        firstOccurrence.Add(pair, GetFirstOccurrence(pair));
                    }
                }
            }

            // Переносим всё в массив для сортировки
            var entries = new (string Pair, int Count, int FirstIndex)[dict.Count];
            int idx = 0;
            foreach (var kvp in dict)
            {
                entries[idx] = (kvp.Key, kvp.Value, firstOccurrence[kvp.Key]);
                idx++;
            }
            
            // Сортируем пузырьком (по убыванию Count, при равенстве - по FirstIndex)
            for (int i = 0; i < entries.Length - 1; i++)
            {
                for (int j = 0; j < entries.Length - i - 1; j++)
                {
                    bool shouldSwap = false;
                    
                    if (entries[j].Count < entries[j + 1].Count)
                        shouldSwap = true;
                    else if (entries[j].Count == entries[j + 1].Count && 
                             entries[j].FirstIndex > entries[j + 1].FirstIndex)
                        shouldSwap = true;
                    
                    if (shouldSwap)
                    {
                        var temp = entries[j];
                        entries[j] = entries[j + 1];
                        entries[j + 1] = temp;
                    }
                }
            }
            
            // Берём топ-5 (или меньше)
            int topCount = entries.Length < 5 ? entries.Length : 5;
            string[] theBestPairs = new string[topCount];
            for (int i = 0; i < topCount; i++)
            {
                theBestPairs[i] = entries[i].Pair;
            }
            
            return theBestPairs;
        }
        
        private int GetFirstOccurrence(string pair)
        {
            for (int i = 0; i < _input.Length - 1; i++)
            {
                if (_input[i].ToString() + _input[i + 1].ToString() == pair)
                    return i;
            }
            return int.MaxValue;
        }

        public override void Review()
        {
            string[] bestStrings = FindTheBestPairs();
            
            // Считаем реальное количество пар
            int actualCount = bestStrings.Length;
            
            if (actualCount == 0)
            {
                _output = _input;
                _codes = new (string, char)[0];
                return;
            }
            
            _codes = CreatePairStringChar(bestStrings, actualCount);
            _output = _input;
            
            // Заменяем пары на коды
            for (int i = 0; i < _codes.Length; i++)
            {
                var (pair, code) = _codes[i];
                _output = _output.Replace(pair, code.ToString());
            }
        }

        public override string ToString()
        {
            return _output ?? "";
        }
    }
}
