using Lab9.Purple;


namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        //methods
        public override void Review ()
        {
            string Result = "";

            string[] ResultArr = new string[0];
            string[] SplitedText = Input.Split(" ");
            foreach (string word in SplitedText)
            {
                string ReversedWord = "";
                int StartIndex = -1;
                for (int i = word.Length - 1; i >= 0; i--)
                {
                    if (char.IsLetterOrDigit(word[i]) || word[i] == '-' || word[i] == '\'')
                    {
                        if (StartIndex == -1) { StartIndex = i; }
                        ReversedWord += word[i];
                    }
                }

                int Index = 0;
                string ResWord = "";
                for (int i = 0; i < word.Length; i++)
                {
                    if ((char.IsLetterOrDigit(word[i]) || word[i] == '-' || word[i] == '\'')
                        && Index < ReversedWord.Length)
                    {
                        ResWord += ReversedWord[Index++];
                    }
                    else
                    {
                        ResWord += word[i];
                    }
                }

                Array.Resize(ref ResultArr, ResultArr.Length + 1);
                if (word.Any(char.IsDigit)) { ResultArr[ResultArr.Length - 1] = word; }
                else { ResultArr[ResultArr.Length - 1] = ResWord; }
            }

            Result = String.Join(" ", ResultArr);
            Output = Result;
        }
        //constructor
        public Task1(string text) : base(text) {}
    }

}