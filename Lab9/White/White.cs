namespace Lab9.White
{
    public abstract class White
    {
        private string _input;
        public string Input =>  _input;

        protected White(string text)
        {
            _input = text;
        }
        public abstract void Review();
        public virtual void ChangeText(string newText)
        {
            _input = newText;
        }
    }

    public class Task1 : White
    {
        private double _output;
        public double Output => _output;
        public Task1(string text) : base(text) {}

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input)) return;
            string[] sentences = Input.Split(new char[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);
            if (sentences.Length == 0) return;
            
            double total = 0;
            foreach (string s in sentences)
            {
                string[] words = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int punctCount = 0;
                foreach (char c in s)
                {
                    if (char.IsPunctuation(c)) punctCount++;
                }

                total += words.Length + punctCount;
            }

            _output = total + sentences.Length;
        }

        public override string ToString() => _output.ToString();
    }

    public class Task2 : White
    {
        private int[,] _output;
        public int[,] Output => _output;
        
        public Task2(string text) : base(text) {}

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input)) return;
            string[] words = Input.Split(new char[]  { ' ', '.', ',', '!', '?', ':', ';'}, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, int> sCounts = new Dictionary<int, int>();
            string glasnye = "аяэеоёуюыиaeoiy";

            foreach (string word in words)
            {
                int count = 0;
                foreach (char c in word.ToLower())
                {
                    if (glasnye.Contains(c))  count++;
                }
                if (count == 0) count = 1;

                if (sCounts.ContainsKey(count))
                {
                    sCounts[count]++;
                }
                else
                {
                    sCounts[count] = 1;
                }
            }
            
            var sorted = sCounts.OrderBy(x => x.Key).ToList();
            //продолжить дома!!!!!
            _output = new int[sorted.Count, 2];
            for (int i = 0; i < sorted.Count; i++)
            {
                _output[i, 0] = sorted[i].Key;
                _output[i, 1] = sorted[i].Value;
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < _output.GetLength(0); i++)
            {
                result += $"{_output[i, 0]} {_output[i, 1]}\n";
            }
            return result.TrimEnd();
        }
    }

    public class Task3 : White
    {
        private string[,] _codes;
        private string _output;
        public string Output => _output;

        public Task3(string text, string[,] new_codes) : base(text)
        {
            _codes = new_codes;
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input) || _codes == null) return;
            string result = Input;
            for (int i = 0; i < _codes.GetLength(0); i++)
            {
                string replaceWord = _codes[i, 0];
                string code =  _codes[i, 1];
                //заменяем только целые слова
                result = result.Replace(replaceWord, code);
            }
            _output = result;
        }

        public override string ToString() => _output;
    }

    public class Task4 : White
    {
        private int _output;
        public int Output => _output;
        public Task4(string text) : base(text) {}

        public override void Review()
        {
            _output = 0;
            if (string.IsNullOrEmpty(Input)) return;
            foreach (char c in Input)
            {
                //'0' имеет код 48, '9' - 57
                if (c >= '0' && c <= '9')
                {
                    _output += (c - '0');
                }
            }
        }
        public override string ToString() => _output.ToString();
    }
}
