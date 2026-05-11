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
            _codes = new (string, char)[5];
        }

        public override void Review()
        {
            string[] pairs = new string[0];
            int[] firstIndex = new int[0];

            _output = Input;

            for (int i = 1; i < Input.Length; i++)
            {
                if (Char.IsLetter(Input[i - 1]) && Char.IsLetter(Input[i]))
                {
                    string pair = Input[i - 1].ToString() + Input[i].ToString();

                    if (!pairs.Contains(pair))
                    {
                        Array.Resize(ref pairs, pairs.Length + 1);
                        pairs[^1] = pair;

                        Array.Resize(ref firstIndex, firstIndex.Length + 1);
                        firstIndex[^1] = i - 1;
                    }
                }
            }

            int[] pairsCount = new int[pairs.Length];

            for (int i = 0; i < pairs.Length; i++)
            {
                int count = 0;

                for (int j = 0; j < Input.Length - 1; j++)
                {
                    string sub = Input.Substring(j, 2);

                    if (sub == pairs[i])
                    {
                        count++;
                    }
                }

                pairsCount[i] = count;
            }

            for (int i = 0; i < pairs.Length - 1; i++)
            {
                for (int j = i + 1; j < pairs.Length; j++)
                {
                    if (pairsCount[i] < pairsCount[j] || (pairsCount[i] == pairsCount[j] && firstIndex[i] > firstIndex[j]))
                    {
                        (pairsCount[i], pairsCount[j]) = (pairsCount[j], pairsCount[i]);
                        (pairs[i], pairs[j]) = (pairs[j], pairs[i]);
                        (firstIndex[i], firstIndex[j]) = (firstIndex[j], firstIndex[i]);
                    }
                }
            }

            int limit = pairs.Length;
            if (limit > 5)
            {
                limit = 5;
            }

            string[] topPairs = new string[limit];

            for (int i = 0; i < limit; i++)
            {
                topPairs[i] = pairs[i];
            }

            string[] tempCodes = new string[limit];

            for (int i = 0; i < limit; i++)
            {
                tempCodes[i] = "#" + i + "#";
                _output = _output.Replace(topPairs[i], tempCodes[i]);
            }

            int symbolIndex = 32;

            for (int i = 0; i < limit; i++)
            {
                while (Input.Contains(((char)symbolIndex).ToString()))
                {
                    symbolIndex++;
                }

                _output = _output.Replace(tempCodes[i], ((char)symbolIndex).ToString());
                _codes[i] = (pairs[i], (char)symbolIndex);
                symbolIndex++;
            }
        }

        public override string ToString()
        {
            return _output;
        }
    }
}
