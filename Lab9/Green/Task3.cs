namespace Lab9.Green;

public class Task3 : Green
    {
        private string _substring;
        
        public string[] Output { get; private set; }
        
        public Task3(string input, string substring) : base(input)
        {
            _substring = substring ?? "";
            Output = new string[0];
        }
        
        public override void Review()
        {
            if (string.IsNullOrEmpty(_substring))
                return;
            string[] foundWords = new string[1000];
            string[] uniqueCheck = new string[1000];
            int foundCount = 0;
            int uniqueCount = 0;
            string currentWord = "";
            
            for (int i = 0; i < Input.Length; i++)
            {
                char ch = Input[i];
                if (char.IsLetter(ch) || ch == '-' || ch == '\'')
                {
                    currentWord += ch;
                }
                else
                {
                    if (currentWord.Length > 0)
                    {
                        bool contains = currentWord.ToLower().Contains(_substring.ToLower());
                        if (contains)
                        {
                            string wordLower = currentWord.ToLower();
                            bool alreadyAdded = false;
                            for (int u = 0; u < uniqueCount; u++)
                            {
                                if (uniqueCheck[u] == wordLower)
                                {
                                    alreadyAdded = true;
                                    break;
                                }
                            }
                            if (!alreadyAdded)
                            {
                                foundWords[foundCount] = currentWord;
                                uniqueCheck[uniqueCount] = wordLower;
                                foundCount++;
                                uniqueCount++;
                            }
                        }
                        currentWord = "";
                    }
                }
            }
            
            if (currentWord.Length > 0)
            {
                bool contains = currentWord.ToLower().Contains(_substring.ToLower());
                if (contains)
                {
                    string wordLower = currentWord.ToLower();
                    bool alreadyAdded = false;
                    for (int u = 0; u < uniqueCount; u++)
                    {
                        if (uniqueCheck[u] == wordLower)
                        {
                            alreadyAdded = true;
                            break;
                        }
                    }
                    if (!alreadyAdded)
                    {
                        foundWords[foundCount] = currentWord;
                        uniqueCheck[uniqueCount] = wordLower;
                        foundCount++;
                        uniqueCount++;
                    }
                }
            }
            Output = new string[foundCount];
            for (int i = 0; i < foundCount; i++)
            {
                Output[i] = foundWords[i];
            }
        }
        public override string ToString()
        {
            if (Output.Length == 0)
            {
                return "";
            }
            
            string result = Output[0];
            
            for (int i = 1; i < Output.Length; i++)
            {
                result += Environment.NewLine + Output[i];
            }
            
            return result;
        }
    }