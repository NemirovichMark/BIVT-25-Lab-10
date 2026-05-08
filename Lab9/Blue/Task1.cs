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
    string[] words = Input.Split(' ');
    words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();

    string[] lines = new string[0];
    foreach (string word in words)
    {
      if (lines.Length == 0)
      {
	lines = lines.Append("").ToArray();
      }
      if (lines[lines.Length-1].Length + word.Length + 1 <= 51)
      {
	lines[lines.Length-1] += word + ' ';
      }
      else
      {
	lines[lines.Length-1] = lines[lines.Length-1].TrimEnd();
	lines = lines.Append("").ToArray();
	lines[lines.Length-1] += word + ' ';
      }
    }
    lines[lines.Length-1] = lines[lines.Length-1].TrimEnd();
    _output = lines;
  }

  public override string ToString()
  {
    string ans = "";
    for (int i = 0; i < _output.Length-1; i++)
    {
      ans += _output[i] + System.Environment.NewLine;
    }
    if (_output.Length != 0) ans+=_output[_output.Length-1];

    return ans.TrimEnd();
  }

}
