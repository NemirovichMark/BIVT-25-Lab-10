namespace Lab9.Purple;

public class Task2 : Purple
{
    private string[] _output;

    public string[] Output => _output.ToArray();
    
    public Task2(string input) : base(input)
    {
        _output = default(string[]);
    }
    
    public override void Review()
    {
        if (string.IsNullOrEmpty(Input)) return;
        
        string[] words = Input.Split(' ');
        string[] lines = new string[0];


        string currentLine = "";
        foreach (string word in words)
        {
            if (currentLine == "") currentLine = word;
            
            else if (currentLine.Length + 1 + word.Length <= 50) currentLine += ' ' + word;

            else
            {
                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = AddSpaces(currentLine);
                currentLine = word;
            }
        }
        
        if (currentLine != "")
        {
            Array.Resize(ref lines, lines.Length + 1);
            lines[lines.Length - 1] = AddSpaces(currentLine);
        }

        _output = lines;
    }
    
    private string AddSpaces(string line)
    {
        string[] words = line.Split(' ');

        if (words.Length == 1) return words[0];

        int letters = 0;
        foreach (string word in words) letters += word.Length;

        int space = 50 - letters;
        int gaps = words.Length - 1;

        int needSpace = space / gaps;
        int extra = space % gaps;

        string result = "";
        for (int i = 0; i < words.Length; i++)
        {
            result += words[i];

            if (i < gaps)
            {
                int countSpace = needSpace;
                if (i < extra) countSpace++;
                result += new string(' ', countSpace);
            }
        }

        return result;
    }

    public override string ToString()
    {
        if (_output == null) return "";

        return string.Join('\n', _output);
    }
}
