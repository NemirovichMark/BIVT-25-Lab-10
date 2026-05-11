
namespace Lab9.Purple;

public class Task3 : Purple
{
    private string _output;
    private (string, char)[] _codes;

    public string Output => _output;
    public (string, char)[] Codes => _codes;

    
    public Task3(string input) : base(input)
    {
        _output = default(string);
        _codes = new (string, char)[0];
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input)) return;
        
        
        // 5 символов, которых нет в input
        char[] fiveChars = new char[5];
        int countChars = 0;

        for (int i = 32; i <= 126; i++)
        {
            char symbol = (char)i;

            bool CharInInput = false;
            for (int j = 0; j < Input.Length; j++)
            {
                if (Input[j] == symbol)
                {
                    CharInInput = true;
                    break;
                }
            }

            if (!CharInInput)
            {
                fiveChars[countChars] = symbol;
                countChars++;
                if (countChars == 5) break;
            }
        }
        
        string[] pairs = new string[Input.Length];
        int[] counts = new int[Input.Length];
        int[] firstIndex = new int[Input.Length];
                                                                                                                                                                                                                                    int uniquePairsCount = 0;

        for (int i = 0; i < Input.Length - 1; i++)
        {
            if (char.IsLetter(Input[i]) && char.IsLetter(Input[i + 1]))
            {
                string pair = Input[i].ToString() + Input[i + 1].ToString();

                bool found = false;
                for (int j = 0; j < uniquePairsCount; j++)
                {
                    if (pairs[j] == pair)
                    {
                        counts[j]++;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    pairs[uniquePairsCount] = pair;
                    counts[uniquePairsCount] = 1;
                    firstIndex[uniquePairsCount] = i;
                    uniquePairsCount++;
                }
            }
        }

        int topN = 5;
        if (uniquePairsCount < 5) topN = uniquePairsCount;
        
        _codes = new (string, char)[topN];
        string currentText = Input;

        for (int t = 0; t < topN; t++)
        {
            int maxCount = -1;
            int bestFirstIndex = int.MaxValue;
            int bestIndexinArray = -1;

            for (int i = 0; i < uniquePairsCount; i++)
            {
                if (counts[i] > maxCount || (counts[i] == maxCount && firstIndex[i] < bestFirstIndex))
                {
                    maxCount = counts[i];
                    bestFirstIndex = firstIndex[i];
                    bestIndexinArray = i;
                }
            }

            if (bestIndexinArray != -1)
            {
                string bestPair = pairs[bestIndexinArray];
                char codeChar = fiveChars[t];

                _codes[t] = (bestPair, codeChar);
                currentText = currentText.Replace(bestPair, codeChar.ToString());
                counts[bestIndexinArray] = -1;
                
            }
        }

        _output = currentText;
    }

    public override string ToString()
    {
        if (_output == null) return "";

        return _output;
    }
}
