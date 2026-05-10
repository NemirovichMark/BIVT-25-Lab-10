using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Lab9.Blue
{
  public class Task1 : Blue
  {
    private string[] _output;
    public string[] Output => _output;

    public Task1(string input) : base(input)
    {
      _output = new string[0];
    }

    [JsonConstructor]
    public Task1(string input, string[] output)
        : base(input)
        => _output = output;

    public override void Review()
    {
      // обработать текст в Input, решая задание
      // (Текст в Input не должен изменяться)

      // по результатам выполнения метода
      // Output должен возврашать правильный ответ для текста в Input

      string[] arrayWords = base.SplitToRawWords();
      string ans = arrayWords[0];
      for (int i = 1; i < arrayWords.Length; i++)
      {
        if ((ans.Length + arrayWords[i].Length + 1) <= 50)
        {
          ans = string.Concat(ans, " ", arrayWords[i]);
        }
        else
        {
          Array.Resize(ref _output, _output.Length + 1);
          _output[_output.Length - 1] = ans;
          ans = arrayWords[i];
        }
      }
      Array.Resize(ref _output, _output.Length + 1);
      _output[_output.Length - 1] = ans;
    }
    public override string ToString()
    {
      return string.Join(Environment.NewLine, _output);
    }
  }
}
