using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        public Task3(string input) : base(input)
        {
        }
        [JsonConstructor]
        public Task3(string input, string output, (string, char)[] codes) : base(input)
        {
            Output = output;
            Codes = codes;
        }
        public string Output {  get; private set; }
        public (string, char)[] Codes { get; private set; }

        public override void Review()
        {
            Output = Input;
            string[] unique = new string[] {"placeholder", "placeholder", "placeholder", "placeholder", "placeholder", "placeholder" };
            for (int i = 0; i < Input.Length-1; i++)
            {
                bool isUnique = true;
                foreach (string s in unique)
                {
                    if (s.Equals(Input.Substring(i,2)))
                        isUnique = false;
                }
                if (isUnique) unique[5] = Input.Substring(i,2);
                unique = unique.OrderByDescending(x => Count(x)).ToArray();
            }
            int counter = 0, id = 32;
            Codes = new (string, char)[5];
            while (counter<5)
            {
                if (!Input.Contains((char)id))
                {
                    Codes[counter] = (unique[counter], (char)id);
                    Output = Output.Replace(unique[counter], ((char)id).ToString());
                    counter++;
                }
                id++;
            }
        }
        private int Count(string pair)
        {
            int count = 0;
            if (char.IsLetter(pair[0]) && char.IsLetter(pair[1]))
            {
                for (int i = 0; i < Input.Length - 1; i++)
                {
                    if (pair.Equals(Input.Substring(i, 2)))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public override string ToString()
        {
            return Output;
        }
    }
}
