using System.Text;

namespace Lab9.Purple;

public class Task1 : Purple
{
    private string _output;

    public string Output => _output;

    public Task1(string input) : base(input) { }

    private string Reverse(string text)
    {
        StringBuilder res = new StringBuilder();

        for (int i = text.Length - 1; i >= 0; i--)
        {
            res.Append(text[i]);
        }

        return res.ToString();
    }

    private void AppendWord(StringBuilder result, StringBuilder word, bool hasDigit)
    {
        if (word.Length == 0)
            return;

        if (hasDigit)
            result.Append(word);
        else
            result.Append(Reverse(word.ToString()));

        word.Clear();
    }

    public override void Review()
    {
        StringBuilder word = new StringBuilder();
        StringBuilder result = new StringBuilder();

        bool hasDigit = false;

        foreach (char c in Input)
        {
            bool isWordSymbol = char.IsLetterOrDigit(c) || c == '-' || c == '\'';

            if (isWordSymbol)
            {
                word.Append(c);

                if (char.IsDigit(c))
                    hasDigit = true;
            }
            else
            {
                AppendWord(result, word, hasDigit);
                result.Append(c);
                hasDigit = false;
            }
        }

        AppendWord(result, word, hasDigit);

        _output = result.ToString();
    }

    public override string ToString()
    {
        return _output;
    }
}