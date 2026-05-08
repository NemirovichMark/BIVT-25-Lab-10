namespace Lab9.Blue;

public class Task2 : Blue
{
  private string _output;
  private string _substr;

  public string Output => _output;

  public Task2(string input, string substr) : base(input)
  {
    _substr = substr;
    _output = "";
  }

  public override void Review()
  {
    string[] words = Input.Split(' ');
    words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();

    string answer = "";
    foreach (string word in words)
    {
      if (word.Contains(_substr))
      {
	if (GetSpecChars(word).Length <= 1)
	  answer += GetSpecChars(word);
	else
	  answer += ' ' + GetSpecChars(word);
      }
      else
	answer += ' ' + word;
    }

    _output = answer.Trim();
  }

  public override string ToString()
  {
    return _output;
  }

}
