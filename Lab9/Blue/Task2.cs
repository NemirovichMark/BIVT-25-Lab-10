using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Lab9.Blue
{
  public class Task2 : Blue
  {
    private string _sequence;
    private string _output;
    public string Output => _output;
    public string Sequence => _sequence;

    public Task2(string input, string sequence) : base(input)
    {
      _sequence = sequence;
    }

    [JsonConstructor]
    public Task2(string input, string output, string sequence)
        : base(input) { _output = output; _sequence = sequence; }

    public override void Review()
    {
      _output = Input;
      string[] arrayWords = base.SplitToWords();
      int count = 0;
      for (int i = 0; i < arrayWords.Length; i++)
        if (arrayWords[i].ToLower().Contains(_sequence))
          count++;
      string[] array = new string[count];
      count = 0;
      for (int i = 0; i < arrayWords.Length; i++)
        if (arrayWords[i].ToLower().Contains(_sequence))
          array[count++] = arrayWords[i];
      arrayWords = array;

      for (int i = 0; i < arrayWords.Length; i++)
      {
        string word = arrayWords[i];
        int index = _output.IndexOf(word);
        if (index != -1)
          _output = _output.Remove(index, word.Length);
        _output = _output.Replace("  ", " ").Replace(" .", ".").Replace(" !", "!").Replace(" ?", "?")
          .Replace(" ,", ",").Replace(" :", ":").Replace(" ;", ";").Replace(" )", ")").Replace(" [", "[")
          .Replace(" ]", "]").Replace(" {", "{").Replace(" }", "}").Replace(" /", "/");
        if (_output[0] == ' ')
          _output = _output.Remove(0, 1);
      }
    }
    public override string ToString()
    {
      return _output;
    }
  }
}
