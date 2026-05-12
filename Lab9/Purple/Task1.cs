using System.Text;

namespace Lab9.Purple;

public class Task1 : Purple
{
    public string Output { get; private set; }

    public Task1(string text) : base(text)
    {
        Output = string.Empty;
    }

    public override void Review()
    {
        Output = string.Join(" ", Input.Split().Select(Reverse));
    }

    private static string Reverse(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;

        if (text.Any(char.IsDigit)) return text;

        int start = 0;
        int end = text.Length-1;

        while (start <= end && !char.IsLetter(text[start])) start++;
        while (end >= start && !char.IsLetter(text[end])) end--;

        if (start > end) return text;

        var sb = new StringBuilder(text.Length);
        sb.Append(text, 0, start);
        sb.Append(new string(text[start..(end+1)]
            .Reverse()
            .ToArray()));
        sb.Append(text, end+1, text.Length-end-1);
        return sb.ToString();
    }

    public override string ToString() => Output ?? string.Empty;
}
