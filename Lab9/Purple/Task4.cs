using System.Linq.Expressions;
using System.Text;

namespace Lab9.Purple;

public class Task4: Purple
{
    private string? _output;
    
    private (string, char)[] _codes;
    public string Output => _output ?? default; //при null default 
    
    public (string, char)[] Codes => _codes;
    public Task4(string input, (string, char)[] codes) : base(input)
    {
        _codes = codes;
    }

    public override void Review()
    {
        _output = new string(Input);
        foreach ((string Pair, char Symbol) code in _codes)
        {
            _output = _output.Replace(code.Symbol.ToString(), code.Pair);
        }
    }

    public override string ToString() => Output;
}