namespace Lab9.Purple;

public class Task1 : Purple
{
    private string _output;

    public string Output => _output;
    
    public Task1(string input) : base(input)
    {
        _output = default(string);
    }

    public override void Review()
    {
        string result = "";
        string currentWord = "";
        bool IsNumber = false;
        if (string.IsNullOrEmpty(Input)) return;

        for (int i = 0;i < Input.Length;i++)
        {
            if (char.IsDigit(Input[i])) IsNumber = true;
            if (Input[i] == ' ') IsNumber = false;
            
            if ((char.IsLetter(Input[i]) || Input[i] == '-' || Input[i] == '\'') && IsNumber == false)
            {
                currentWord = Input[i] + currentWord;
            }
            else
            {
                result += currentWord + Input[i];
                currentWord = "";
            }
        }
        result += currentWord;
        _output = result;
    }

    public override string ToString()
    {
        return _output;
    }
}
