namespace Lab9.Purple;

public class Task1:Purple
{
    private string _output;
    public string Output => _output;

    public Task1(string text) : base(text)
    {
        _output = default(string);
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input) || string.IsNullOrWhiteSpace(Input)) return;
        string[] strings = Input.Split(" ",StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < strings.Length; i++)
        {
            char[] Chars = strings[i].ToCharArray();
            string tmp = strings[i].Trim(Punc);
            if (string.IsNullOrEmpty(tmp) || string.IsNullOrWhiteSpace(tmp)) continue;
            char[] chars = tmp.ToCharArray();
            int index = chars.Length - 1;
            for (int j = 0; j <= (chars.Length - 1) / 2; j++)
            {
                if (char.IsDigit(chars[j])) break;
                char tmp1 = chars[j];
                chars[j] = chars[index];
                chars[index] = tmp1;
                index--;
            }

            index = 0;
            for (int j = 0; j < Chars.Length; j++)
            {
                if (char.IsDigit(Chars[j])) break;
                if (char.IsPunctuation(Chars[j]))
                {
                    if (Chars[j] == '-' || Chars[j] == '\'')
                    {
                        Chars[j] = chars[index];
                        index++;
                    }
                }
                else
                {
                    Chars[j] = chars[index];
                    index++;
                }
            }

            strings[i] = new string(Chars);
        }

        _output = string.Join(" ", strings);
    }

    public override string ToString()
    {
        return _output;
    }
}