namespace Lab9.Purple;

public class Task1 : Purple
{
    private string _output;
    
    public string Output => _output;
    
    public Task1(string str) : base(str)
    {
        _output = default(string);
    }

    public override void Review()
    {
        char[] chars = Input.ToCharArray();
        int n = chars.Length;

        for (int i = 0; i < n; i++)
        {
            if (char.IsLetterOrDigit(chars[i]) || chars[i] == '-' || chars[i] == '\'')
            {
                int start = i;
                bool hasDigit = false;
                
                while (i < n && (char.IsLetterOrDigit(chars[i]) || chars[i] == '-' || chars[i] == '\''))
                {
                    if (char.IsDigit(chars[i])) 
                    {
                        hasDigit = true;
                    }
                    i++;
                }

                int end = i - 1;
                
                if (!hasDigit)
                {
                    int left = start;
                    int right = end;
                    while (left < right)
                        (chars[left], chars[right]) = (chars[right--], chars[left++]);
                }
            }
        }

        _output = new string(chars);
    }
    
    public override string ToString()
    {
        return _output;
    }
}