namespace Lab9.Purple;

public class Task3:Purple
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

    public Task3(string text) : base(text)
    {
        _output = null;
        _codes = null;
    }
  
    public override void Review()
    {
        if (string.IsNullOrEmpty(Input) || string.IsNullOrWhiteSpace(Input)) return;
        char[] Chars = Input.ToCharArray();
        string[] strings = Input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        string[] codes = new string[0];
        for (int i = 0; i + 1 < Chars.Length; i++)
        {
            if (char.IsWhiteSpace(Chars[i]) || char.IsWhiteSpace(Chars[i + 1]) || char.IsPunctuation(Chars[i]) ||
                char.IsPunctuation(Chars[i + 1])) continue;
            string tmp = new string(new char[] { Chars[i], Chars[i + 1] });
            bool meet = false;
            for (int j = 0; j < codes.Length; j++)
            {
                if (codes[j] == tmp)
                {
                    meet = true;
                    break;
                }
            }

            if (!meet)
            {
                Array.Resize(ref codes,codes.Length+1);
                codes[codes.Length - 1] = tmp;
            }
        }

        int[] counts = new int[codes.Length];
        for (int i = 0; i + 1 < Chars.Length; i++)
        {
            string tmp = new string(new char[] { Chars[i], Chars[i + 1] });
            for (int j = 0; j < codes.Length; j++)
            {
                if (codes[j] == tmp)
                {
                    counts[j]++;
                }
            }
        }

        int i1 = 0;
        while (i1 < counts.Length)
        {
            if (i1 == 0 || counts[i1] < counts[i1 - 1])
            {
                i1++;
            }
            else if (counts[i1] == counts[i1 - 1])
            {
                int idx1 = -1, idx2 = -1;
                for (int i = 0; i + 1 < Chars.Length; i++)
                {
                    string tmp = new string(new char[] { Chars[i], Chars[i + 1] });
                    if (tmp == codes[i1])
                    {
                        idx1 = i;
                        break;
                    }
                }

                for (int i = 0; i < Chars.Length; i++)
                {
                    string tmp = new string(new char[] { Chars[i], Chars[i + 1] });
                    if (tmp == codes[i1 - 1])
                    {
                        idx2 = i;
                        break;
                    }
                }

                if (idx2 <= idx1)
                {
                    i1++;
                }
                else
                {
                    int tmp = counts[i1];
                    counts[i1] = counts[i1 - 1];
                    counts[i1 - 1] = tmp;
                    string tmp1 = codes[i1];
                    codes[i1] = codes[i1 - 1];
                    codes[i1 - 1] = tmp1;
                    i1--;
                }
            }
            else
            {
                int tmp = counts[i1];
                counts[i1] = counts[i1 - 1];
                counts[i1 - 1] = tmp;
                string tmp1 = codes[i1];
                codes[i1] = codes[i1 - 1];
                codes[i1 - 1] = tmp1;
                i1--;
            }
        }

        i1 = 0;
        char[] replace = new char[5];
        for (int i = 32; i <= 126; i++)
        {
            bool meet = false;
            for (int j = 0; j < Chars.Length; j++)
            {
                if ((char)i == Chars[j])
                {
                    meet = true;
                    break;
                }
            }

            if (!meet)
            {
                if (i1 < replace.Length)
                {
                    replace[i1] = (char)i;
                    i1++;
                }
            }
        }

        (string, char)[] ans = new (string, char)[5];
        for (int i = 0; i < ans.Length; i++)
        {
            ans[i].Item1 = codes[i];
            ans[i].Item2 = replace[i];
        }
        
        for (int i = 0; i < strings.Length; i++)
        {
            for (int j = 0; j < ans.Length; j++)
            {
                if (strings[i].Contains(ans[j].Item1))
                {
                    strings[i] = strings[i].Replace(ans[j].Item1, ans[j].Item2 + "");
                }
            }
        }

        _codes = ans;
        _output = string.Join(" ", strings);
    }

    public override string ToString()
    {
        return _output;
    }
}