namespace Lab9.Blue
{
    public class Task2 : Blue
    {
        private string _sequence;

        public string Sequence => _sequence;

        public Task2(string input, string sequence) : base(input)
        {
            _sequence = sequence;
        }
        public string Output { get; private set; }
        public override string ToString() => Output ?? "";
        public override void Review()
        {
            if (Input == null || _sequence == null)
            {
                Output = Input;
                return;
            }
            string[] parts = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int length = 0;
            foreach (string part in parts)
            {
                int start = 0;
                while (start < part.Length && !IsWordChar(part[start]))
                    start++;
                int end = part.Length - 1;
                while (end >= 0 && !IsWordChar(part[end]))
                    end--;
                if (start > end)
                {
                    length++;
                    continue;
                }

                string prefix = part.Substring(0, start);
                string word = part.Substring(start, end - start + 1);
                string suffix = part.Substring(end + 1);

                if (word.Contains(_sequence, StringComparison.OrdinalIgnoreCase))
                {
                    string newPart = prefix + suffix;
                    if (newPart.Length > 0) length++;
                }
                else length++;
            }

            string[] result = new string[length];
            int ind = 0;
            foreach (string part in parts)
            {
                int start = 0;
                while (start < part.Length && !IsWordChar(part[start]))
                    start++;
                int end = part.Length - 1;
                while (end >= 0 && !IsWordChar(part[end]))
                    end--;
                if (start > end)
                {
                    result[ind++] = part;
                    continue;
                }
                
                string prefix = part.Substring(0, start);
                string word = part.Substring(start, end - start + 1);
                string suffix = part.Substring(end + 1);

                if (word.Contains(_sequence, StringComparison.OrdinalIgnoreCase))
                {
                    string newPart = prefix + suffix;
                    if (newPart.Length > 0) result[ind++] = newPart;
                }
                else result[ind++] = part;
            }

            Output = String.Join(" ", result);
            Output = Output.Replace(" .", ".");
            Output = Output.Replace(" ,", ",");
            Output = Output.Replace(" !", "!");
            Output = Output.Replace(" ?", "?");
            Output = Output.Replace(" :", ":");
            Output = Output.Replace(" ;", ";");
        }
        private bool IsWordChar(char c)
        {
            return char.IsLetter(c) || c == '-'  || c == '\'';
        }
        
    }
}