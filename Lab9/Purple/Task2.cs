using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Lab9.Purple {
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
            if (_output == null)
            {
                return "";
            }
            string ans = _output[0];
            for (int i = 1; i < _output.Length; i++)
            {
                ans += "\n" + _output[i];
            }
            return ans;
        }

        private void Print(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                System.Console.WriteLine(arr[i]);
            }
        }


        private void DoWordsFromInput()
        {
            string[] words = _input.Split(' ');

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
            int len = sentence.Length;
            if (len == 50 || len == 0)
            {
                return sentence;
            }

            string[] words = sentence.Split(' ');
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
                ans += AddSpace(SpaceOnEveryWord + 1); // + 1 изначальный пробел
                if (i - 1 < cntNecesSpace % cntGap)
                {
                    ans += " ";
                }
                ans += words[i];
            }
            return ans;
        }

        private string AddSpace(int cnt)
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
            for (int i = 0; i < _output.Length; i++)
            {
                _output[i] = AddTrimInString(_output[i]);
            }
        }

        
    }
}
