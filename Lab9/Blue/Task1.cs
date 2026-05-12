namespace Lab9.Blue;

public class Task1 : Blue
{
    private string[] _output;
    public string[] Output => _output.ToArray();

    public Task1(string input) : base(input)
    {
        _output = new string[0];
    }

    public override void Review()
    {
        _output = new string[0];
        var words = Input.Split(' ');
        string str = "";
        foreach (var word in words)
        {
            if (str.Length + word.Length + 1 <= 50)
            {
                str += (str.Length == 0 ? "" : " ") + word;
            }
            else
            {
                Edit.Add(ref _output, str);
                str = word;
            }
        }
        if (str.Length > 0) Edit.Add(ref _output, str);
    }

    public override string ToString()
    {
        return Edit.Join(_output, Environment.NewLine);
    }
}