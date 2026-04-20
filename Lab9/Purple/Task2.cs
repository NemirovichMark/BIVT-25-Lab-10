using System.Text;

namespace Lab9.Purple;

public class Task2 : Purple
{
    public Task2(string input) : base(input) {}
    public string[] Output {get;private set;}
    public override string ToString()
    {
        if (Output == null) return "";
        return string.Join(Environment.NewLine,Output);

    }
    public override void Review()
    {
        
        int len = 50;
        int index = 0;
        string[] answer = new string[0];
        string[] words = Input.Split(' ',StringSplitOptions.RemoveEmptyEntries);
        
        while(index < words.Length)
        {
            int start = index;
            int count = 0;
            int currentLength = 0;
            while(index < words.Length)
            {
                if (count == 0)
                {
                    currentLength = words[index].Length;
                    count = 1;
                    index++;
                }
                else if (currentLength + 1 + words[index].Length <= len)
                {
                    currentLength += words[index].Length+1;
                    count++;
                    index++;
                }
                else break;
            }

            StringBuilder str = new StringBuilder();

            if (count == 1) str.Append(words[start]);
            else
            {
                int gaps = count - 1;
                int countSymbols =0;
                for (int i=start; i<start+count; i++)
                    countSymbols += words[i].Length;
                int totalSpace = len - countSymbols;
                int requiredSpace = totalSpace / gaps;
                int extraSpace = totalSpace % gaps;

                for (int i=start; i<start + count; i++)
                {
                    str.Append(words[i]);
                    int spaces = requiredSpace;
                    if (i != start + count - 1) 
                    {
                        if (extraSpace > 0 && i!=start+count-1)
                        {
                            spaces++;
                            extraSpace--;
                            
                        }
                        str.Append(' ',spaces);
                    }
                }
            }
            Array.Resize(ref answer, answer.Length+1);
            answer[^1] = str.ToString();
        }
        Output = answer;
    }
}
