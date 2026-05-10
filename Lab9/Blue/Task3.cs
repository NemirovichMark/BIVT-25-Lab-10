using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab9.Blue
{
  public class Task3 : Blue
  {
    private string _sequence;
    private (char, double)[] _output;
    public (char, double)[] Output => _output;

    public Task3(string input) : base(input)
    {
      _output = new (char, double)[0];
    }

    [JsonConstructor]
    public Task3(string input, (char, double)[] output, string sequence)
        : base(input) { _output = output; _sequence = sequence; }

    public override void Review()
    {
      string[] arrayWords = base.SplitToWords();
      for (int i = 0; i < arrayWords.Length; i++)
      {
        char ch = arrayWords[i].ToLower()[0];
        if (!Array.Exists(_output, word => word.Item1 == ch))
        {
          Array.Resize(ref _output, _output.Length + 1);
          _output[_output.Length - 1].Item1 = ch;
          _output[_output.Length - 1].Item2 = 1;
        }
        else
        {
          _output[Array.FindIndex(_output, word => word.Item1 == ch)].Item2 += 1;
        }
      }
      for (int i = 0; i < _output.Length; i++)
        _output[i].Item2 = _output[i].Item2 * 100f / arrayWords.Length;
      
      _output = _output.OrderBy(x => x.Item1).ToArray();
      _output = _output.OrderByDescending(x => x.Item2).ToArray();
    }
    public override string ToString()
    {
      string ans = $"{_output[0].Item1}:{string.Format("{0:F4}", _output[0].Item2)}";
      for (int i = 1; i < _output.Length; i++)
        ans += $"{Environment.NewLine}{_output[i].Item1}:{string.Format("{0:F4}", _output[i].Item2)}";
      return ans;
    }
  }
}
