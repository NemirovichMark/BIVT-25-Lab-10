using System.Text;

namespace Lab9.Purple;

public class Task2 : Purple
{
    private string[] _output;

    public string[] Output => _output;

    public Task2(string text) : base(text)
    {
        _output = [];
    }

    private string AlignLine(string line)
    {
        string[] words = line.Split(' ');

        int spaces = words.Length - 1;
        if (spaces <= 0)
            return line;

        int wordsLength = 0;
        foreach (string w in words)
            wordsLength += w.Length;

        int totalSpaces = 50 - wordsLength;

        for (int i = 0; i < spaces; i++)
            words[i] += ' ';

        totalSpaces -= spaces;

        for (int i = 0; i < totalSpaces; i++)
            words[i % spaces] += ' ';

        return string.Join("", words);
    }

    public override void Review()
    {
        string[] words = Input.Split(' ');

        if (words.Length == 0)
        {
            _output = [];
            return;
        }

        string[] result = [];
        StringBuilder line = new StringBuilder(words[0]);
        int currentLength = words[0].Length;

        for (int i = 1; i < words.Length; i++)
        {
            string word = words[i];

            if (currentLength + 1 + word.Length <= 50)
            {
                line.Append(' ');
                line.Append(word);
                currentLength += 1 + word.Length;
            }
            else
            {
                Array.Resize(ref result, result.Length + 1);
                result[^1] = AlignLine(line.ToString());

                line.Clear();
                line.Append(word);
                currentLength = word.Length;
            }
        }

        if (line.Length > 0)
        {
            Array.Resize(ref result, result.Length + 1);
            result[^1] = AlignLine(line.ToString());
        }

        _output = result;
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, _output);
    }
}