namespace Lab9.Purple;

public class Task4 : Purple
{
    private string _output;
    private (string, char)[] _codes;

    public string Output => _output;
    public (string, char)[] Codes => _codes;
    public Task4(string str, (string, char)[] codes) : base(str)
    {
        _output = str; 
        _codes = codes;
    }

    public override void Review()
    {
        if (_codes == null || _codes.Length == 0)
        {
            _output = Input;
            return;
        }

        string result = Input;

        for (int i = 0; i < _codes.Length; i++)
        {
            string originalPair = _codes[i].Item1;
            char codeChar = _codes[i].Item2;
            
            result = result.Replace(codeChar.ToString(), originalPair);
        }

        _output = result;
    }

    public override string ToString()
    {
        return _output ?? string.Empty;
    }
}