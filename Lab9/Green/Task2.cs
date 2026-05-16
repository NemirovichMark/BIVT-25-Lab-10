namespace Lab9.Green;

public class Task2 : Green
    {
        public char[] Output { get; private set; }
        
        public Task2(string input) : base(input)
        {
            Output = new char[0];
        }
        
        public override void Review()
        {
            string[] words = Input.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', '!', '?', ';', ':', '"', '(', ')', '[', ']', '{', '}', '/' }, StringSplitOptions.RemoveEmptyEntries);
            
            char[] tempLetters = new char[33];
            int[] tempCounts = new int[33];
            int uniqueCount = 0;
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word.Length > 0)
                {
                    char firstChar = char.ToLower(word[0]);
                    if (char.IsLetter(firstChar))
                    {
                        int foundIndex = -1;
                        for (int f = 0; f < uniqueCount; f++)
                        {
                            if (tempLetters[f] == firstChar)
                            {
                                foundIndex = f;
                                break;
                            }
                        }
                        
                        if (foundIndex != -1)
                        {
                            tempCounts[foundIndex]++;
                        }
                        else
                        {
                            tempLetters[uniqueCount] = firstChar;
                            tempCounts[uniqueCount] = 1;
                            uniqueCount++;
                        }
                    }
                }
            }
            
            for (int i = 0; i < uniqueCount - 1; i++)
            {
                for (int j = 0; j < uniqueCount - i - 1; j++)
                {
                    if (tempCounts[j] < tempCounts[j + 1])
                    {
                        char tempChar = tempLetters[j];
                        tempLetters[j] = tempLetters[j + 1];
                        tempLetters[j + 1] = tempChar;
                        
                        int tempCount = tempCounts[j];
                        tempCounts[j] = tempCounts[j + 1];
                        tempCounts[j + 1] = tempCount;
                    }
                    else if (tempCounts[j] == tempCounts[j + 1])
                    {
                        if (tempLetters[j] > tempLetters[j + 1])
                        {
                            char tempChar = tempLetters[j];
                            tempLetters[j] = tempLetters[j + 1];
                            tempLetters[j + 1] = tempChar;
                        }
                    }
                }
            }
            
            Output = new char[uniqueCount];
            for (int i = 0; i < uniqueCount; i++)
            {
                Output[i] = tempLetters[i];
            }
        }
        public override string ToString()
        {
            if (Output.Length == 0)
            {
                return "";
            }
            
            string result = Output[0].ToString();
            
            for (int i = 1; i < Output.Length; i++)
            {
                result += ", " + Output[i];
            }
            return result;
        }
    }