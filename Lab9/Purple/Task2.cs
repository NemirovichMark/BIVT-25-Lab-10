using Lab9.Purple;
using System;
using System.Text;


namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        public string[] Output { get; private set; } = new string[0];
        public Task2(string text) : base(text)
        {
        }
        public override void Review()
        {
            int x = 0;
            string[] result = new string[0];
            string[] words = Input.Split(' ');

            while (x < words.Length)
            {
                int startI = x;
                int c = 0;
                int currentLength = 0;
                while (x < words.Length)
                {
                    if (c == 0)
                    {
                        currentLength = words[x].Length;
                        c = 1;
                        x++;
                    }
                    else if (currentLength + 1 + words[x].Length <= 50)
                    {
                        currentLength += words[x].Length + 1;
                        c++;
                        x++;
                    }
                    else
                        break;
                }

                string temp = "";

                if (c == 1) 
                    temp += words[startI];
                else
                {
                    int gaps = c - 1;
                    int countSymbols = 0;
                    for (int i = startI; i < startI + c; i++)
                        countSymbols += words[i].Length;
                    int totalSpace = 50 - countSymbols;
                    int requiredSpace = totalSpace / gaps;
                    int extraSpace = totalSpace % gaps;

                    for (int i = startI; i < startI + c; i++)
                    {
                        temp += words[i];
                        int spaces = requiredSpace;
                        if (i != startI + c - 1)
                        {
                            if (extraSpace > 0 && i != startI + c - 1)
                            {
                                spaces++;
                                extraSpace--;
                            }
                            for (int j = 0; j < spaces; j++)
                                temp += ' ';
                        }
                    }
                }
                Array.Resize(ref result, result.Length + 1);
                result[^1] = temp;
            }
            Output = result;
        }
        public override string ToString()
        {
            if (Output == null) 
                return "";
            return string.Join(Environment.NewLine, Output);
        }
    }
}
