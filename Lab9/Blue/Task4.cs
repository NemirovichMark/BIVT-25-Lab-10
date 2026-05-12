namespace Lab9.Blue;

public class Task4 : Blue
{
    private int _output;
    public int Output => _output;

    public Task4(string input) : base(input)
    {
        _output = 0;
    }

    public override void Review()
    {
        var res = 0;
        var words =
            Input.Split(new char[] { ' ', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' },
                StringSplitOptions.RemoveEmptyEntries);
        foreach (var i in words)
        {
            res += Edit.Numbers(i);
        }
        _output = res;
    }

    public override string ToString()
    {
        return $"{_output}";
    }
}