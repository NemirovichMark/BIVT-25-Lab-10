using System.Text;

namespace Lab9.Purple;

public class Task2 : Purple
{
    public string[] Output { get; private set; }

    public Task2(string text) : base(text)
    {
        Output = Array.Empty<string>();
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input)) return;

        var words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] tempLines = new string[words.Length];
        
        int lineCount = 0, start = 0, len = 0;

        for (int i = 0; i < words.Length; i++)
        {
            if (len == 0)
                len = words[i].Length;
            else if (len + 1 +words[i].Length <= 50)
                len += 1 + words[i].Length;
            else
            {
                tempLines[lineCount++] = JustifyLine(words, start, i-1, 50);
                start = i;
                len = words[i].Length;
            }
        }

        if (len > 0)
            tempLines[lineCount++] = JustifyLine(words, start, words.Length - 1, 50);
        
        Output = tempLines.Take(lineCount).ToArray();
    }

    private static string JustifyLine(string[] words, int start, int end, int targetWidth)
    {
        if (start == end) return words[start];

        var lineWords = words.Skip(start).Take(end-start+1).ToArray();
        int lettersCount = lineWords.Sum(w => w.Length);

        int totalSpacesNeeded = targetWidth - lettersCount;
        int gapsCount = lineWords.Length-1;
        int spacesPerGap = totalSpacesNeeded / gapsCount;
        int extraSpaces = totalSpacesNeeded % gapsCount;

        string result = string.Empty;
        for (int i = 0; i < gapsCount; i++)
            result += lineWords[i] + new string(' ', spacesPerGap + (i < extraSpaces ? 1 : 0));
        result += lineWords.Last();

        return result;
    }

    public override string ToString() => Output != null ? string.Join(Environment.NewLine, Output) : string.Empty;
}