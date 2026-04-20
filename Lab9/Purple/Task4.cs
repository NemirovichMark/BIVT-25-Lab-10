using System.Text;

namespace Lab9.Purple;

public class Task4 : Purple
{
    private (string,char)[] _codes;
    public Task4(string input, (string,char)[] codes) 
        : base(input) => _codes = codes;
    public string Output {get; private set;}
    public override string ToString() => Output;
    public override void Review()
    {
        StringBuilder decoded = new StringBuilder(Input);
        foreach (var code in _codes)
        {
            decoded.Replace(code.Item2.ToString(),code.Item1);
        }
        Output = decoded.ToString();
    }
}