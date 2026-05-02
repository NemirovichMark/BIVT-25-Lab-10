namespace Lab9.Purple;

public class Task4:Purple
{
    private string _output;
    private (string, char)[] _codes;
    public string Output => _output;

    public (string, char)[] Codes
    {
        get
        {
            if (_codes == null) return null;
            (string, char)[] codes = new (string, char)[_codes.Length];
            for (int i = 0; i < codes.Length; i++)
            {
                codes[i] = _codes[i];
            }

            return codes;
        }
    }
    

    public Task4(string text, (string, char)[] codes) : base(text)
    {
        _output = null;
        _codes = codes;
    }
    public Task4():base("alo"){}

    public override void Review()
    {
        if(string.IsNullOrEmpty(Input) || string.IsNullOrWhiteSpace(Input)) return;
        string[] strings = Input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < strings.Length; i++)
        {
            for (int j = 0; j < _codes.Length; j++)
            {
                if (strings[i].Contains(_codes[j].Item2))
                {
                    strings[i] = strings[i].Replace(_codes[j].Item2 + "", _codes[j].Item1);
                }
            }
        }

        _output = String.Join(" ", strings);
    }

    public override string ToString()
    {
        return _output;
    }
}