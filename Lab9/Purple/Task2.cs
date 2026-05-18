namespace Lab9.Purple;

public class Task2 : Purple
{
    private string[] _output;
    
    public string[] Output => _output;
    
    public Task2(string str) : base(str)
    {
        _output = default(string[]);
    }

    public override void Review()
    {
        string[] words = Input.Split(' ');
        string[] res = [];

        int i = 0;
        while (i < words.Length) 
        {
            string temp = "";
            
            while (i < words.Length)
            {
                int nextLength = temp.Length + words[i].Length + (temp.Length > 0 ? 1 : 0);
            
                if (nextLength <= 50)
                {
                    if (temp.Length > 0) 
                        temp += " ";
                    
                    temp += words[i++];
                }
                else
                    break;
            }
            
            if (temp.Length < 50)
            {
                int spaceIndex = 0;
                while (temp.Contains(' ') && temp.Length < 50)
                {
                    spaceIndex = temp.IndexOf(' ', spaceIndex);

                    if (spaceIndex == -1)
                        spaceIndex = temp.IndexOf(' ', 0);

                    temp = temp.Insert(spaceIndex, " ");

                    while (spaceIndex < temp.Length && temp[spaceIndex] == ' ')
                        spaceIndex++;
                }
            }
        
            Array.Resize(ref res, res.Length + 1);
            res[res.Length - 1] = temp;
        }
        
        _output = res; 
    }

    public override string ToString()
    {
        return string.Join("\n", _output);
    }
}