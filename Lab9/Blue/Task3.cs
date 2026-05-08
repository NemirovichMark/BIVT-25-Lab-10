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
    Dictionary <char, int> dict = new Dictionary <char, int>();

    string text = Input.ToLower();
    text = GetNotSpecChars(text);

    int total = 0;

    foreach (string word in text.Split())
    {
      if (String.IsNullOrEmpty(word) || String.IsNullOrWhiteSpace(word) || (word[0] >= '0' && word[0] <= '9')) continue;
      if (!dict.ContainsKey(word[0])) dict.Add(word[0], 1);
      else
      {
	dict[word[0]]+=1;
      }
      total++;
    }

    Dictionary <char, double> ans = new Dictionary<char, double>();

    foreach (KeyValuePair<char, int> count in dict)
    {
      ans.Add(count.Key, (double)count.Value/total*100);
    }

    ans = ans.OrderBy(x => x.Key).ToDictionary();
    ans = ans.OrderByDescending(x => x.Value).ToDictionary();

    (char, double)[] output = new (char, double)[0];
    foreach (var element in ans)
    {
      output = output.Append((element.Key, Double.Round(element.Value, 4))).ToArray();
    }

    _output = output;

  }

  public override string ToString()
  {
    string ans = "";
    for (int i = 0; i < _output.Length-1; i++)
    {
      ans += _output[i].Item1 + ":" + String.Format("{0, 0:F4}", _output[i].Item2) + System.Environment.NewLine;
    }
    if (_output.Length != 0) ans+=_output[_output.Length-1].Item1 + ":" + _output[_output.Length-1].Item2;

    return ans.TrimEnd();
  }


}
