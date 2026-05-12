namespace Lab9.Purple;

public class Task3 : Purple
{
    public string Output { get; private set;}
    public (string, char)[] Codes { get; private set;}

    public Task3(string text) : base(text)
    {
        Output = string.Empty;
        Codes = Array.Empty<(string, char)>();
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input)) return;

        var topPairs  = Enumerable.Range(0, Input.Length-1)
            .Where(i => char.IsLetter(Input[i]) && char.IsLetter(Input[i+1]))
            .GroupBy(i => Input.Substring(i, 2))
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Min())
            .Select(g => g.Key)
            .Take(5)
            .ToArray();

        var usedChars = Input.Distinct().ToArray();
        var avialableChars = Enumerable.Range(32, 95)
            .Select(i => (char)i)
            .Except(usedChars)
            .Take(topPairs.Length)
            .ToArray();

        Codes = topPairs.Zip(avialableChars, (pair, c) => (pair, c)).ToArray();

        string result = Input;
        foreach (var code in Codes)
            result = result.Replace(code.Item1, code.Item2.ToString());

        Output = result;
    }

    public override string ToString() => Output ?? string.Empty;
}