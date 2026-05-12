using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        [JsonProperty]
        private string[] _outPut;

        public string[] Output => _outPut.ToArray();

        public Task2(string input) : base(input)
        {
            _outPut = new string[0];
            Review();
        }

        public override void Review()
        {
            var makeRows = new StringBuilder();
            var array = Input.Split();

            int lenOfString = 0;
            foreach (var word in array)
            {
                if (lenOfString == 0)
                {
                    lenOfString += word.Length;
                    makeRows.Append(word);
                }
                else if ((lenOfString + word.Length + 1) <= 50)
                {
                    lenOfString += word.Length + 1;
                    makeRows.Append(" " + word);
                }
                else
                {
                    makeRows.Append(Environment.NewLine);
                    makeRows.Append(word);
                    lenOfString = word.Length;
                }
            }

            string sWithNewLines = makeRows.ToString().Trim();

            var splitByRows = sWithNewLines.Split(Environment.NewLine);

            var numOfTabsInEveryRow = new int[0];
            for (int i = 0; i < splitByRows.Length; i++)
            {
                splitByRows[i] = splitByRows[i].Trim();
            }

            for (int row = 0; row < splitByRows.Length; row++)
            {
                while (splitByRows[row].Length != 50)
                {
                    int numOfWhiteSpaces = 0;
                    for (int i = 0; i < splitByRows[row].Length - 1; i++)
                    {
                        if (splitByRows[row].Length == 50)
                            break;
                        if (splitByRows[row][i] == ' ' && splitByRows[row][i + 1] != ' ')
                        {
                            numOfWhiteSpaces++;
                            splitByRows[row] = splitByRows[row].Substring(0, i) + "  " + splitByRows[row].Substring(i + 1);
                            i++;
                        }
                    }
                    if (numOfWhiteSpaces == 0)
                    {
                        //for (int i = splitByRows[row].Length; i < 51; i++)
                        //    splitByRows[row] += ' ';
                        break;
                    }
                }
            }

            Array.Resize(ref _outPut, splitByRows.Length);
            for (int i = 0; i < splitByRows.Length; i++)
                _outPut[i] = splitByRows[i];
        }


        public override string ToString()
        {
            return string.Join(Environment.NewLine, Output);
        }
    }
}