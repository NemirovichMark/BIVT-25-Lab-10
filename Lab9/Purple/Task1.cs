using System.Text;

namespace Lab9.Purple;

public class Task1: Purple
{
    private string? _output;
    
    public string Output => _output ?? default; //при null default 
    
    public Task1(string input) : base(input)
    {
    }

    public override void Review()
    {
        StringBuilder outputStringBuilder = new StringBuilder();
        StringBuilder wordStringBuilder = new StringBuilder();
        StringBuilder numberStringBuilder = new StringBuilder();

        for (int i = 0; i < Input.Length; i++)
        {
            char symbol = Input[i];
            bool wasNumber = i != 0 && char.IsDigit(Input[i - 1]); //предыдущая цифра
            
            if ((char.IsLetter(symbol) || symbol == '-' || symbol == '\'') && !wasNumber)
            {
                wordStringBuilder.Append(symbol);
            }
            else if (char.IsDigit(symbol))
            {
                numberStringBuilder.Append(symbol);
            }
            else
            {
                if (wordStringBuilder.Length > 0) //меняем если до этого было слово
                {
                    char[] wordChars = wordStringBuilder.ToString().ToCharArray();
                    Array.Reverse(wordChars);
                    outputStringBuilder.Append(wordChars);
                    wordStringBuilder.Clear();
                }

                if (numberStringBuilder.Length > 0)
                {
                    outputStringBuilder.Append(numberStringBuilder);
                    numberStringBuilder.Clear(); //чтобы потом повторов не было
                }

                outputStringBuilder.Append(symbol); //сам знак
            }
        }
        _output = outputStringBuilder.ToString();
    }

    public override string ToString() => Output;
}