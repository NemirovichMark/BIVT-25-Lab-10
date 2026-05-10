using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        private string[] _output;
        public string[] Output { get; private set; }
        [JsonConstructor]
        public Task2(string input, string[] output) : base(input)
        {
            Output = output;
        }
        public Task2(string input) : base(input)
        {
            _output = new string[0];
        }

        public override void Review()
        {
            _output = new string[0];
            string[] words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] result = new string[0];
            int charcount = 0;
            foreach (string word in words)
            {
                if (result.Length == 0 || charcount + result.Length + word.Length <= 50)
                {
                    Array.Resize(ref result, result.Length + 1);
                    result[^1] = word;
                    charcount += word.Length;
                }
                else
                {
                    Normalise(result, charcount);
                    charcount = word.Length;
                    result = new string[]{word};
                }
            }
            Normalise(result, charcount);
            Output = _output;
        }
        private void Normalise(string[] array, int length)
        {
            if (array.Length > 1)
            {
                for (int i = 0; i < 50 - length; i++)
                {
                    array[i % (array.Length - 1)] += " ";
                }
            }
            Array.Resize(ref _output, _output.Length + 1);
            _output[^1] = String.Concat(array);
        }
        public override string ToString()
        {
            return String.Join(Environment.NewLine, Output);
        }
    }
}
