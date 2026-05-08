namespace Lab9.Blue
{
  public abstract class Blue
  {
    private string _input;

    public char[] _specialChars = {'.', '!', '?', ',', ':', '"', ';', '–', '(', ')', '[', ']', '{', '}', '/'};

    public string Input => _input;

    protected Blue(string input)
    {
      _input = input;
    }

    public string GetSpecChars(string input)
    {
      string ans = "";
      foreach (char c in input)
      {
	if (_specialChars.Contains(c))
	{
	  ans += c;
	}
      }
      return ans;
    }

    public string GetOnlyDigits(string input)
    {
      string ans = "";
      foreach (char c in input)
      {
	if (c >= '0' && c <= '9')
	{
	  ans += c;
	}
      }

      return ans;
    }

    public string GetNotSpecChars(string input)
    {
      string ans = "";
      foreach (char c in input)
      {
	if (!_specialChars.Contains(c))
	{
	  ans += c;
	}
      }
      return ans;
    }

    public bool IsNumber(string input)
    {
      input = input.Trim().Trim(_specialChars);
      if (GetOnlyDigits(input).Length != 0 && GetSpecChars(input).Length <= 1)
      {
	return true;
      }
      return false;
    }

    public abstract void Review();

    public virtual void ChangeText(string text)
    {
      _input = text;
      Review();
    }
  }
}
