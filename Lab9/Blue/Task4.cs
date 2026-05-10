using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab9.Blue
{
  public class Task4 : Blue
  {
    private int _output;
    public int Output => _output;

    public Task4(string input) : base(input)
    {
      _output = 0;
    }

    [JsonConstructor]
    public Task4(string input, int output)
        : base(input)
        => _output = output;

    public override void Review()
    {
      _output = 0;
      string[] arrayWords = base.SplitToDigits();
      string[] strDigits = new string[0];
      for (int i = 0; i < arrayWords.Length; i++)
      {
        string s = "";
        for (int j = 0; j < arrayWords[i].Length; j++)
        {
          if (char.IsDigit(arrayWords[i][j]))
          {
            s += arrayWords[i][j];
          }
          else
          {
            Array.Resize(ref strDigits, strDigits.Length + 1);
            strDigits[strDigits.Length - 1] = s;
            s = "";
          }
        }
        Array.Resize(ref strDigits, strDigits.Length + 1);
        strDigits[strDigits.Length - 1] = s;
      }
      for (int i = 0; i < strDigits.Length; i++)
      {
        int a = 0;
        for (int j = 0; j < strDigits[i].Length; j++)
        {
          a = a * 10 + (strDigits[i][j] - '0');
        }
        _output += a;
      }
    }
    public override string ToString()
    {
      return $"{_output}";
    }
  }
}
