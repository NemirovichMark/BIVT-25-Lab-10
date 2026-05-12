using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        [JsonProperty]
        private string _outPut;
        private (string, char)[] _codes;

        public string Output => _outPut;
        public (string, char)[] Codes => _codes.ToArray();

        public Task3(string input) : base(input)
        {
            _codes = new (string, char)[5];
            Review();
        }
        internal char[] ChooseReplaces(string input)
        {
            char[] replacements = new char[5];
            int index = 0;
            for (int i = 32; i < 127; i++)
            {
                char replace = (char)i;
                bool hasText = false;
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[j] == replace)
                    {
                        hasText = true;
                        break;
                    }
                }
                if (!hasText)
                    replacements[index++] = replace;

                if (index == 5)
                    break;
            }
            return replacements;
        }
        public override void Review()
        {
            const int numOfCodes = 5;
            char[] replacements = ChooseReplaces(Input);
            (string, int)[] codeAndCountThisCode = new (string, int)[numOfCodes];
            for (int i = 0; i < Input.Length - 1; i++)
            {
                string code = Input.Substring(i, 2);
                if (char.IsLetter(code[0]) && char.IsLetter(code[1]))
                {
                    int count = Regex.Matches(Input, code).Count;
                    //Console.WriteLine(code + " " + count);
                    if (count > codeAndCountThisCode[^1].Item2)
                    {
                        bool hasArray = false;
                        for (int j = 0; j < codeAndCountThisCode.Length; j++)
                        {
                            if (codeAndCountThisCode[j].Item1 == code)
                                hasArray = true;
                        }

                        if (!hasArray)
                            codeAndCountThisCode[^1] = (code, count);
                    }

                    codeAndCountThisCode = codeAndCountThisCode.OrderByDescending(x => x.Item2).ToArray();
                }
            }

            string res = Input;

            for (int i = 0; i < codeAndCountThisCode.Length; i++)
            {
                _codes[i] = (codeAndCountThisCode[i].Item1, replacements[i]);

                string replaceInString = "";
                replaceInString += replacements[i];
                res = res.Replace(codeAndCountThisCode[i].Item1, replaceInString);
            }
            _outPut = res;
        }

        public override string ToString()
        {
            Review();
            return _outPut;
        }
    }
}