namespace Lab9.Green;

public class Task2 : Green
{
    private char[] _output;
    private (char Letter, double Count)[] _counter;
    public char[] Output => _output.ToArray();

    public Task2(string input) : base(input)
    {
        _output = new char[60];
        _counter = new (char Letter, double Count)[60];
    }
    public override void Review()
    {
        string ABC = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz";

        for (int i = 0; i < ABC.Length; i++)
        {
            _counter[i].Letter = ABC[i];
        }

        for (int i = 0; i < Input.Length; i++)
        {
            if (char.IsLetter(Input[i]))
            {
                if (i == 0)
                {
                    int index = Array.FindIndex(_counter, item => item.Letter == char.ToLower(Input[i]));
                    _counter[index].Count++;
                }

                else
                {
                    if (char.IsLetter(Input[i - 1]) == false)
                    {
                        if (Input[i - 1] != '-' && Input[i - 1] != '\'' && Input[i - 1] != '`' && char.IsNumber(Input[i - 1]) == false)
                        {
                            int index = Array.FindIndex(_counter, item => item.Letter == char.ToLower(Input[i]));
                            _counter[index].Count += 1;
                        }
                    }
                }
            }
        }

        _output = _counter
            .Where(item => item.Count > 0)
            .OrderByDescending(item => item.Count)
            .ThenBy(item => item.Letter)
            .Select(item => item.Letter)
            .ToArray();

    }

    public override string ToString()
    {
        string res = "";
        for (int i = 0; i < _output.Length; i++)
        {
            res += _output[i] + ", ";

        }

        return res.TrimEnd()[..^1];
    }
}