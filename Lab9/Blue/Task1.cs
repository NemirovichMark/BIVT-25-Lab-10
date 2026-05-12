namespace Lab9.Blue
{
    public class Task1 : Blue
    {
        public Task1(string input) : base(input) {}
        public string[] Output { get; protected set; }
        public override void Review()
        {
            if (Input == null)
            {
                Output = null;
                return;
            }
            string[] words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string current = "";
            int count = 0;
            foreach (var i in words)
            {
                if (current.Length == 0)
                {
                    current += i;
                }
                else if (current.Length != 0 && current.Length + 1 + i.Length <= 50)
                {
                    current += " " + i;
                }
                else
                {
                    count++;
                    current = i;
                }
            }
            if (current.Length > 0) count++;
            
            Output = new string[count];
            int ind = 0;
            current = "";
            foreach (var i in words)
            {
                if (current.Length == 0)
                {
                    current += i;
                }
                else if (current.Length + 1 + i.Length <= 50)
                {
                    current += " " + i;
                }
                else
                {
                    Output[ind++] = current;
                    current = i;
                }
            }
            if (current.Length > 0) Output[ind] = current;
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Output);
        }
        
    }
}