namespace Lab9.Green;

public class Task4 : Green
    {
        public string[] Output { get; private set; }
        
        public Task4(string input) : base(input)
        {
            Output = new string[0];
        }
        
        public override void Review()
        {
            string[] surnames = SplitByComma(Input);
            for (int i = 0; i < surnames.Length; i++)
            {
                surnames[i] = Trim(surnames[i]);
            }
            
            for (int i = 0; i < surnames.Length - 1; i++)
            {
                for (int j = 0; j < surnames.Length - i - 1; j++)
                {
                    if (CompareStrings(surnames[j], surnames[j + 1]) > 0)
                    {
                        string temp = surnames[j];
                        surnames[j] = surnames[j + 1];
                        surnames[j + 1] = temp;
                    }
                }
            }
            Output = surnames;
        }
        private int CompareStrings(string a, string b)
        {
            int minLength = a.Length;
            if (b.Length < minLength)
            {
                minLength = b.Length;
            }
            
            for (int i = 0; i < minLength; i++)
            {
                if (a[i] != b[i])
                {
                    return a[i] - b[i];
                }
            }
            
            return a.Length - b.Length;
        }
        
        private string Trim(string str)
        {
            int start = 0;
            int end = str.Length - 1;
            
            while (start <= end && str[start] == ' ')
            {
                start++;
            }
            while (end >= start && str[end] == ' ')
            {
                end--;
            }
            if (start > end)
            {
                return "";
            }
            
            string result = "";
            for (int i = start; i <= end; i++)
            {
                result += str[i];
            }
            
            return result;
        }
        
       
        private string[] SplitByComma(string str)
        {
            int count = 1;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ',')
                {
                    count++;
                }
            }
            string[] result = new string[count];
            
            int currentIndex = 0;
            string currentItem = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ',')
                {
                    result[currentIndex] = currentItem;
                    currentIndex++;
                    currentItem = "";
                }
                else
                {
                    currentItem += str[i];
                }
            }
            result[currentIndex] = currentItem;
            
            return result;
        }
        public override string ToString()
        {
            if (Output.Length == 0)
            {
                return "";
            }
            string result = Output[0];
            
            for (int i = 1; i < Output.Length; i++)
            {
                result += Environment.NewLine + Output[i];
            }
            return result;
        }
    }