namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string _output;
        private (string, char)[] _codes = [];

        public string Output => _output;
        public (string, char)[] Codes => _codes;

        public Task3(string str) : base(str)
        {
            _output = str;
        }

        public override void Review()
        {
            if (Input.Length < 2) 
                return;

            string[] pairs = new string[0];
            int[] counts = new int[0];

            for (int i = 0; i < Input.Length - 1; i++)
            {
                if (char.IsLetter(Input[i]) && char.IsLetter(Input[i + 1]))
                {
                    string currentPair = Input.Substring(i, 2);
                    
                    int foundIndex = -1;
                    for (int j = 0; j < pairs.Length; j++)
                    {
                        if (pairs[j] == currentPair)
                        {
                            foundIndex = j;
                            break;
                        }
                    }

                    if (foundIndex != -1)
                        counts[foundIndex]++;
                    else
                    {
                        Array.Resize(ref pairs, pairs.Length + 1);
                        Array.Resize(ref counts, counts.Length + 1);
                        pairs[pairs.Length - 1] = currentPair;
                        counts[counts.Length - 1] = 1;
                    }
                }
            }


            for (int i = 0; i < pairs.Length - 1; i++)
            {
                for (int j = 0; j < pairs.Length - i - 1; j++)
                {
                    bool needSwap = false;
                    
                    if (counts[j] < counts[j + 1]) 
                        needSwap = true;
                    else if (counts[j] == counts[j + 1] && Input.IndexOf(pairs[j]) > Input.IndexOf(pairs[j + 1])) 
                        needSwap = true;

                    if (needSwap)
                    {
                        (counts[j], counts[j + 1]) = (counts[j + 1], counts[j]);
                        (pairs[j], pairs[j + 1]) = (pairs[j + 1], pairs[j]);
                    }
                }
            }

            int actualTopCount = 0;
            for (int i = 0; i < Math.Min(5, pairs.Length); i++)
                if (counts[i] > 1) 
                    actualTopCount++;

            _codes = new (string, char)[actualTopCount];
            int codesFound = 0;
            for (int i = 32; i <= 126 && codesFound < actualTopCount; i++)
            {
                char c = (char)i;
        
                if (!Input.Contains(c)) 
                {
                    _codes[codesFound] = (pairs[codesFound], c);
                    codesFound++;
                }
            }

            string result = Input;
            for (int i = 0; i < codesFound; i++)
            {
                result = result.Replace(_codes[i].Item1, _codes[i].Item2.ToString());
            }

            _output = result;
        }

        public override string ToString()
        {
            return _output;
        }
    }
}