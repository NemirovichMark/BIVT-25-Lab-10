using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        private string[] _output;

        public string[] Output => _output;

        public Task2(string text) : base(text)
        {
            _output = new string[0];
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _output ?? new string[0]);
        }

        private void Print(string[] arr)
        {
            if (arr == null)
            {
                return;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }

        private void DoWordsFromInput()
        {
            if (_input == null)
            {
                _output = new string[0];
                return;
            }

            string[] words = _input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words == null || words.Length == 0)
            {
                _output = new string[0];
                return;
            }

            Array.Resize(ref _output, _output.Length + 1);
            _output[^1] = words[0];
            string substring = _output[^1];

            for (int i = 1; i < words.Length; i++)
            {
                substring += " " + words[i];
                if (substring.Length <= 50)
                {
                    _output[^1] = substring;
                }
                else
                {
                    Array.Resize(ref _output, _output.Length + 1);
                    _output[^1] = words[i];
                    substring = _output[^1];
                }
            }
        }

        private string AddTrimInString(string sentence)
        {
            if (sentence == null)
            {
                return "";
            }

            int len = sentence.Length;
            if (len == 50 || len == 0)
            {
                return sentence;
            }

            string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int cntGap = words.Length - 1;

            if (cntGap == 0)
            {
                return sentence;
            }

            string ans = words[0];

            int cntNecesSpace = 50 - len;
            int SpaceOnEveryWord = cntNecesSpace / cntGap;

            for (int i = 1; i < words.Length; i++)
            {
                ans += AddSpaces(SpaceOnEveryWord + 1);
                if (i - 1 < cntNecesSpace % cntGap)
                {
                    ans += " ";
                }
                ans += words[i];
            }
            return ans;
        }

        private string AddSpaces(int cnt)
        {
            string ans = "";
            for (int i = 0; i < cnt; i++)
            {
                ans += " ";
            }
            return ans;
        }

        public override void Review()
        {
            DoWordsFromInput();

            if (_output == null)
            {
                _output = new string[0];
                return;
            }

            _output = _output.Select(AddTrimInString).ToArray();
        }
    }
}
