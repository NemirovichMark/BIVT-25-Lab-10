namespace Lab9.Purple;

public class Task4 : Purple
{
    private string _output;
    private (string, char)[] _codes;
    public string Output => _output;
    public (string, char)[] Codes => _codes;

    public Task4(string input, (string, char)[] codes) : base(input)
    {
        _codes = codes;
    }

    public override void Review()
    {
        string text = Input;
        for (int i = 0; i < _codes.Length; i++)
        {
            text = text.Replace(_codes[i].Item2 + "", _codes[i].Item1);
        }
        _output = text;
    }
    public override string ToString()
    {
        return _output;
    }
}
