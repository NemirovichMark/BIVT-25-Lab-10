namespace Lab9.Green;
public class Task2 : Green
{
    private char[] _output;
    private (char Char, double Count)[] _counter;
    public char[] Output => _output.ToArray();

    public Task2(string input) : base(input)
    {
        _output = new char[60];
        _counter = new (char Char, double Count)[60];
    }
    public override void Review()
    {
        string alfavit = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz";
        double countinput = Input.Count(c => char.IsLetter(c));

        for (int i = 0; i < alfavit.Length; i++)
        {
            char chacha = alfavit[i];
            _counter[i].Char = chacha;
        }

        for (int i = 0; i < Input.Length; i++)
        {
            if (char.IsLetter(Input[i]))
            {
                if (i == 0)
                {
                    int index = Array.FindIndex(_counter, item => item.Char == char.ToLower(Input[i]));
                    _counter[index].Count++;
                }

                else
                {
                    if (char.IsLetter(Input[i - 1]) == false)
                    {
                        if (Input[i - 1] != '-' && Input[i - 1] != '\'' && Input[i - 1] != '`' && char.IsNumber(Input[i - 1]) ==  false) // \'
                        {
                            int index = Array.FindIndex(_counter, item => item.Char == char.ToLower(Input[i]));
                            _counter[index].Count += 1;
                        }
                    }
                }
            }
        }

        _output = _counter
            .Where(item => item.Count > 0)
            .OrderByDescending(item => item.Count)
            .ThenBy(item => item.Char)
            .Select(item => item.Char)
            .ToArray();

    }

    public override string ToString()
    {
        string res = "";
        for (int i = 0; i < _output.Length; i++)
        {
            res += _output[i] + ", " ;

        }
        
        return res.TrimEnd()[..^1];
    }
}