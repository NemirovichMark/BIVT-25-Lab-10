namespace Lab9.Purple;

public class Task2:Purple
{
    private string[] _output;

    public string[] Output
    {
        get
        {
            if (_output == null) return null;
            string[] output = new string[_output.Length];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = _output[i];
            }

            return output;
        }
    }

    public Task2(string text) : base(text)
    {
        _output = null;
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input) || string.IsNullOrWhiteSpace(Input)) return;
        string[] Strings = Input.Split(" ",StringSplitOptions.RemoveEmptyEntries);
        string tmp = "";
        string[] ans = new string[0];
        for (int i = 0; i < Strings.Length; i++)
        {
            if (string.IsNullOrEmpty(Strings[i]) || string.IsNullOrWhiteSpace(Strings[i])) continue;
            tmp += Strings[i];
            if (i + 1 < Strings.Length)
            {
                if (tmp.Length + Strings[i + 1].Length + 1 > 50)
                {
                    Array.Resize(ref ans, ans.Length + 1);
                    ans[ans.Length - 1] = tmp;
                    tmp = "";
                }
                else
                {
                    tmp += " ";
                }
            }
            else
            {
                Array.Resize(ref ans, ans.Length + 1);
                ans[ans.Length - 1] = tmp;
            }
        }

        tmp = "";
        for (int i = 0; i < ans.Length; i++)
        {
            int diff = 50 - ans[i].Length;
            if (diff == 0) continue;
            string[] strings = ans[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (strings.Length - 1 == 0) continue;
            if (diff % (strings.Length - 1) == 0)
            {
                for (int j = 0; j < strings.Length; j++)
                {
                    if (j != strings.Length - 1)
                    {
                        tmp += strings[j];
                        tmp += new string(' ', 1 + diff / (strings.Length - 1));
                    }
                    else
                    {
                        tmp += strings[j];
                    }
                }
            }
            else
            {
                for (int j = 0; j < strings.Length; j++)
                {
                    if (j != strings.Length - 1)
                    {
                        if (j < diff % (strings.Length - 1))
                        {
                            tmp += strings[j];
                            tmp += new string(' ', 2 + diff / (strings.Length - 1));
                        }
                        else
                        {
                            tmp += strings[j];
                            tmp += new string(' ', 1 + diff / (strings.Length - 1));
                        }
                    }
                    else
                    {
                        tmp += strings[j];
                    }
                }
            }

            ans[i] = tmp;
            tmp = "";
        }

        _output = ans;
    }

    public override string ToString()
    {
        string ans = "";
        for (int i = 0; i < _output.Length; i++)
        {
            if (i != _output.Length - 1)
            {
                ans += _output[i] + "\n";
            }
            else
            {
                ans += _output[i];
            }
        }

        return ans;
    }
}