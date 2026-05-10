namespace Lab9.Purple;

public class Task4 : Purple
{
    public string Output { get; private set; }
    public (string, char)[] Codes { get; private set;}

    public Task4(string text, (string, char)[] codes) : base(text)
    {
        Codes = codes;
        Output = string.Empty;
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input))
        {
            Output = Input ?? string.Empty;
            return;
        }

        string result = Input;

        foreach (var code in Codes)
            result = result.Replace(code.Item2.ToString(), code.Item1);

        Output = result;
    }

    public override string ToString() => Output ?? string.Empty;
}