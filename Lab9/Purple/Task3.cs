namespace Lab9.Purple;

public class Task3 : Purple
{
    private string _output;
    private (string, char)[] _codes;

    public string Output => _output;
    public (string, char)[] Codes => _codes;

    public Task3(string text) : base(text)
    {
        _codes = [];
    }

    public override void Review()
    {
        if (Input.Length < 2)
        {
            _output = Input;
            _codes = [];
            return;
        }

        string[] pairs = new string[Input.Length];
        int[] counts = new int[Input.Length];
        int[] firstPos = new int[Input.Length];
        int n = 0;

        for (int i = 0; i < Input.Length - 1; i++)
        {
            char a = Input[i];
            char b = Input[i + 1];

            if (!char.IsLetter(a) || !char.IsLetter(b))
                continue;

            string pair = a + b.ToString();

            int idx = -1;
            for (int j = 0; j < n; j++)
            {
                if (pairs[j] == pair)
                {
                    idx = j;
                    break;
                }
            }

            if (idx >= 0)
            {
                counts[idx]++;
            }
            else
            {
                pairs[n] = pair;
                counts[n] = 1;
                firstPos[n] = i;
                n++;
            }
        }

        if (n == 0)
        {
            _output = Input;
            _codes = [];
            return;
        }

        int[] idxs = new int[n];
        for (int i = 0; i < n; i++)
            idxs[i] = i;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                int x = idxs[j];
                int y = idxs[j + 1];

                bool swap =
                    counts[x] < counts[y] ||
                    (counts[x] == counts[y] && firstPos[x] > firstPos[y]);

                if (swap)
                {
                    int temp = idxs[j];
                    idxs[j] = idxs[j + 1];
                    idxs[j + 1] = temp;
                }
            }
        }

        int take = n < 5 ? n : 5;

        bool[] used = new bool[127];
        foreach (char c in Input)
        {
            if (c >= 32 && c <= 126)
                used[c] = true;
        }

        _codes = new (string, char)[take];

        int codeIndex = 0;
        for (char code = (char)32; code <= 126 && codeIndex < take; code++)
        {
            if (used[code])
                continue;

            string pair = pairs[idxs[codeIndex]];
            _codes[codeIndex] = (pair, code);

            codeIndex++;
        }

        string result = Input;

        for (int i = 0; i < _codes.Length; i++)
        {
            result = result.Replace(_codes[i].Item1, _codes[i].Item2.ToString());
        }

        _output = result;
    }

    public override string ToString()
    {
        return _output;
    }
}