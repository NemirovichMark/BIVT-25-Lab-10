namespace Lab9.Purple;

public class Task4 : Purple
{
    private string _output;
    private (string, char)[] _codes;

    public string Output => _output;
    public (string, char)[] Codes => _codes;

    
    public Task4(string input, (string, char)[] codes) : base(input)
    {
        _output = default(string);
        _codes = codes;
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input)) return;

        string currentText = Input;

        for (int i = 0; i < _codes.Length; i++)
        {
            string pair = _codes[i].Item1;
            char code = _codes[i].Item2;

            if (!string.IsNullOrEmpty(pair) && (int)code > 0)
            {
                currentText = currentText.Replace(code.ToString(), pair);
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
