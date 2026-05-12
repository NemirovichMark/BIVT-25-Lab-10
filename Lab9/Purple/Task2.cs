using System.Text;

namespace Lab9.Purple;

public class Task2: Purple
{
    private string[]? _output;
    
    public string[] Output => _output ?? default; //при null default 
    
    public Task2(string input) : base(input)
    {
    }

    public override void Review()
    {
        const int lineLength = 50;

        string[] tempResult = Array.Empty<string>();

        if (string.IsNullOrEmpty(Input))
            return;

        string[] words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries); //убрирает пустые вхождения

        int currentLength = 0; //кол-во символов в строке

        string[] wordsInLine = Array.Empty<string>();

        foreach (string word in words)
        {
            int resultLength = currentLength + word.Length;

            if (wordsInLine.Length > 0)
                resultLength++; //пробел после первого

            if (resultLength <= lineLength)
            {
                Array.Resize(ref wordsInLine, wordsInLine.Length + 1);
                wordsInLine[wordsInLine.Length - 1] = word;
                currentLength = resultLength;
            }

            else
            {
                Array.Resize(ref tempResult, tempResult.Length + 1);
                tempResult[tempResult.Length - 1] = AlignLine(wordsInLine, 50);
                wordsInLine = new string[] { word };
                currentLength = word.Length;
            }
        }

        if (wordsInLine.Length > 0)
        {
            Array.Resize(ref tempResult, tempResult.Length + 1);
            tempResult[tempResult.Length - 1] = AlignLine(wordsInLine, 50);
        }

        _output = tempResult;
    }

    private string AlignLine(string[] words, int width)
    {
        if (words.Length == 1)
            return words[0];
        
        int totalWordSymbols = words.Sum(w => w.Length);
        
        int totalSpaces = width - totalWordSymbols;
        
        int spacesBetweenWords = totalSpaces / (words.Length - 1);
        
        int extraSpaces = totalSpaces % (words.Length - 1);
        
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < words.Length; i++)
        {
            result.Append(words[i]);
            if (i != words.Length - 1)
            {
                result.Append(' ', spacesBetweenWords);
                if (extraSpaces > 0)
                {
                    result.Append(' ');
                    extraSpaces--;
                }
            }
        }
        
        return result.ToString();
    }
    
    public override string ToString() => 
        string.Join('\n', Output);

}