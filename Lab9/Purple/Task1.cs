using System.Text;

namespace Lab9.Purple;

public class Task1 : Purple
{
    public Task1(string input) : base(input) {}
    public string Output {get; private set;}
    public override string ToString() => Output;  
    public override void Review()
    {
        int index=0;
        string[] words = Input.Split();
        StringBuilder crypto = new StringBuilder();
        while (index < words.Length)
        {
            char[] word = words[index].ToCharArray();
            bool hasLetter = word.Any(char.IsLetter);
            bool hasDigit = word.Any(char.IsDigit);
            if (hasLetter && !hasDigit)
            {
                int indexFirstLetter = Array.FindIndex(word,char.IsLetter);
                int indexLastLetter = Array.FindLastIndex(word,char.IsLetter);
                if (indexFirstLetter <= indexLastLetter)
                {
                    Array.Reverse(word,indexFirstLetter,indexLastLetter-indexFirstLetter+1);
                }
            }
            crypto.Append(word);
            if (index < words.Length-1) 
                crypto.Append(" ");
            index++;
        }
        Output = crypto.ToString();
    }
}

