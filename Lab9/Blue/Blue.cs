namespace Lab9.Blue
{
  public abstract class Blue : Object
  {
    private string _input;
    protected char[] _ends = new char[] { '.', '?', '!' };
    protected char[] _punctuation = new char[] { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' };
    public string Input => _input;

    protected Blue(string input)
    {
      _input = input;
    }

    public abstract void Review();
    public virtual void ChangeText(string text)
    {
      _input = text;
      //Review();
    }
    protected string[] SplitToSentences()
    {
      return _input.Split(_ends, StringSplitOptions.RemoveEmptyEntries);
    }
    protected string[] SplitToRawWords()
    {
      var rawWords = _input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      return rawWords;
    }
    protected string[] SplitToDigits()
    {
      var rawWords = _input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      var words = new string[rawWords.Length];
      for (int i = 0; i < rawWords.Length; i++)
        words[i] = rawWords[i].Trim(_punctuation);
      int count = 0;
      for (int i = 0; i < words.Length; i++)
        if (!string.IsNullOrEmpty(words[i]) && char.IsDigit(words[i][0]))
          count++;
      string[] res = new string[count];
      for (int i = 0, j = 0; i < words.Length; i++)
        if (!string.IsNullOrEmpty(words[i]) && char.IsDigit(words[i][0]))
          res[j++] = words[i];
      return res;
    }
    protected string[] SplitToWords()
    {
      var rawWords = _input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      var words = new string[rawWords.Length];
      for (int i = 0; i < rawWords.Length; i++)
      {
        words[i] = rawWords[i].Trim(_punctuation);
      }
      var count = 0;
      for (int i = 0; i < rawWords.Length; i++)
      {
        if (string.IsNullOrEmpty(words[i]) || char.IsDigit(words[i][0]))
          count++;
      }
      count = 0;
      var result = new string[words.Length - count];
      for (int i = 0, j = 0; i < rawWords.Length; i++)
      {
        if (string.IsNullOrEmpty(words[i]) || char.IsDigit(words[i][0]))
        {
          count++;
        }
        else
        {
          result[j] = words[i];
          j++;
        }
      }
      var res = words.Where(word => !string.IsNullOrEmpty(word) && char.IsLetter(word[0])).ToArray();
      return res;
    }
  }
}