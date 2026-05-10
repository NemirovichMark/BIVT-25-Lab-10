using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        public string Output {  get; private set; }


        [JsonConstructor]
        public Task1(string input, string output) : base(input)
        {
            Output = output;
        }
        public Task1(string input) : base(input)
        {
        }

        public override void Review()
        {
            Output = Input;
            int leftIndex = 0, rightIndex = 0;
            for (int i = 1; i<Input.Length; i++)
            {
                if (IsInWord(Input[i]) && !IsInWord(Input[i - 1]))
                    leftIndex = i;
                else if (IsInWord(Input[i - 1]) && !IsInWord(Input[i]))
                {
                    rightIndex = i;
                    string word = Output.Substring(leftIndex, rightIndex - leftIndex);
                    if (!char.IsDigit(word[0]))
                        Output = String.Concat(Output.Substring(0, leftIndex), ReverseString(word), Output.Substring(rightIndex));
                }
                    
            }
            
        }
        public override string ToString()
        {
            return Output;
        }
        private string ReverseString(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        private bool IsInWord(char ch)
        {
            if (char.IsLetter(ch) || ch== '-' || ch== '\'' || char.IsDigit(ch))
                return true;
            else
                return false;
        }
    }
}
