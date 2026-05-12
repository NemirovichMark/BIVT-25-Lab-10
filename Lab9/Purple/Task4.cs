using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        public (string, char)[] Codes {  get; private set; }
        public string Output { get; private set; }
        [JsonConstructor]
        public Task4(string input, string output, (string, char)[] codes) : base(input)
        {
            Output = output;
            Codes = codes;
        }
        public Task4(string input, (string, char)[] codes) : base(input)
        {
            Codes = codes;
        }

        public override void Review()
        {
            Output = Input;
            foreach ((string,char) pair in Codes)
            {
                Output = Output.Replace((pair.Item2).ToString(), pair.Item1);
            }
        }
        public override string ToString()
        {
            return Output;
        }
    }
}
