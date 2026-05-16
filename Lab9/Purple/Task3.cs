using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string _output;
        private (string, char)[] _codes;
        public string Output => _output;
        public (string, char)[] Codes => _codes;
        public Task3(string input) : base(input)
        {
            _output = "";
            _codes = new (string, char)[0];
        }

        public override void Review()
        {
            string[] pairs = new string[Input.Length - 1];
            int[] counts = new int[Input.Length - 1];
            int uniquePairsCount = 0;

            for (int i = 0; i < Input.Length - 1; i++)
            {
                if (char.IsLetter(Input[i]) && char.IsLetter(Input[i + 1]))
                {
                    string currentPair = "" + Input[i] + Input[i + 1];
                    int index = -1;
                    for (int j = 0; j < uniquePairsCount; j++)
                    {
                        if (pairs[j] == currentPair)
                        {
                            index = j;
                            break; 
                        }
                    }
                    if (index != -1) counts[index]++;
                    else
                    {
                        pairs[uniquePairsCount] = currentPair;
                        counts[uniquePairsCount] = 1;
                        uniquePairsCount++;
                    }
                }
            }


            int topCount = Math.Min(5, uniquePairsCount);
            _codes = new (string, char)[topCount];

            for (int i = 0; i < uniquePairsCount - 1; i++)
            {
                for (int j = 0; j < uniquePairsCount - i - 1; j++)
                {
                    if (counts[j + 1] > counts[j])
                    {
                        (counts[j], counts[j + 1]) = (counts[j + 1], counts[j]);
                        (pairs[j], pairs[j + 1]) = (pairs[j + 1], pairs[j]);
                    }
                }
            }

            int codesCount = 0;
            while (codesCount < 5 && codesCount < uniquePairsCount)
            {
                codesCount++;
            }
            _codes = new (string, char)[codesCount];
            for (int i = 0; i < codesCount; i++)
            {
                _codes[i].Item1 = pairs[i];
            }

            int currentPairIndex = 0;
            for (int i = 32; i <= 126 && currentPairIndex < codesCount; i++)
            {
                char code = (char)i;

                bool existsInText = false;
                for (int k = 0; k < Input.Length; k++)
                {
                    if (Input[k] == code)
                    {
                        existsInText = true;
                        break;
                    }
                }
                if (!existsInText)
                {
                    _codes[currentPairIndex].Item2 = code;
                    currentPairIndex++;
                }
            }

            string compressedText = Input;
            for (int i = 0; i < currentPairIndex; i++)
            {
                compressedText = compressedText.Replace(_codes[i].Item1, _codes[i].Item2.ToString());
            }

            _output = compressedText;
        }

        public override string ToString()
        {
            return _output;
        }
    }
}
