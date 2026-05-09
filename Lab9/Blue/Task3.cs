namespace Lab9.Blue;

public class Task3 : Blue
{
    private (char, double)[] _output;
    public (char, double)[] Output => _output.ToArray();

    public Task3(string input) : base(input)
    {
        _output = new (char, double)[0];
    }

    public override void Review()
    {
        var words = Input.Split(
            new char[] { ' ', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' },
            StringSplitOptions.RemoveEmptyEntries).Where(x => Edit.Letters(x).Length > 0);
        var length = words.Count();
            var res = words.Select(x => Edit.Letters(x)[0]).GroupBy(x => x);
        foreach (var group in res)
        {
            Array.Resize(ref _output, _output.Length + 1);
            _output[^1] = (group.Key, (double)group.Count() / length * 100.0);
        }
        _output = _output.OrderByDescending(x => x.Item2)
            .ThenBy(x => x.Item1)
            .ToArray();
        return;
    }

    public override string ToString()
    {
        var res = "";
        for (int i = 0; i < _output.Length; i++)
        {
            res += $"{_output[i].Item1}:{_output[i].Item2:F4}";
            if (i != _output.Length - 1) res += Environment.NewLine;
        }

        return res;
    }
}