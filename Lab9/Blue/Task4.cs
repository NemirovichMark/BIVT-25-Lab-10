namespace Lab9.Blue;

public class Task4 : Blue
{
  private int _output;

  public int Output => _output;

  public Task4(string input) : base(input)
  {
    _output = 0;
  }

  public override void Review()
  {
    string[] words = Input.Split(_specialChars.Append(' ').ToArray());
    words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();

    int sum = 0;

    foreach (string word in words)
    {
      if (IsNumber(GetOnlyDigits(word)))
      {
	sum += Convert.ToInt32(GetOnlyDigits(word));
      }

    }
    _output = (int) sum;
  }

  public override string ToString()
  {
    return ((int)_output).ToString();
  }

}
