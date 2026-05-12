namespace Lab9.Blue;

public class Task4 : Blue
{
    public Task4(string input) : base(input) {}
    
    public int Output { get; private set; }

    public override string ToString()
    {
        return Output.ToString();
    }

    public override void Review()
    {
        if (Input == null)
        {
            Output = 0;
            return;
        }

        int sum = 0;
        for (int i = 0; i < Input.Length; i++)
        {
            if (char.IsDigit((Input[i])))
            {
                int number = 0;
                while (i < Input.Length && char.IsDigit(Input[i]))
                {
                    number = number * 10 + (Input[i++] - '0');
                }
                sum += number;
            }
        }

        Output = sum;
    }
}