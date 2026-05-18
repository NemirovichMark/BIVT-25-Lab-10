using Lab9.Purple;

namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        //methods
        public new string[] Output { get; private set; } = new string[0];

        public override void Review()
        {
            int MaxLen = 50;

            string[] Words = Input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string[] ResultArr = new string[0];

            int i = 0;
            while (i < Words.Length)
            {
                string[] LineWords = new string[0];
                int WordsLenSum = 0;

                while (i < Words.Length)
                {
                    int NewWordsCount = LineWords.Length + 1;
                    int NewWordsLenSum = WordsLenSum + Words[i].Length;
                    int MinLineLen = NewWordsLenSum + (NewWordsCount - 1);

                    if (MinLineLen <= MaxLen)
                    {
                        Array.Resize(ref LineWords, LineWords.Length + 1);
                        LineWords[LineWords.Length - 1] = Words[i];
                        WordsLenSum = NewWordsLenSum;
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
                string Line = "";
                if (LineWords.Length == 1)
                {
                    Line = LineWords[0];
                }
                else
                {
                    int Gaps = LineWords.Length - 1;

                    int SpacesToAdd = MaxLen - WordsLenSum;
                    int[] GapSpaces = new int[Gaps];
                    for (int g = 0; g < Gaps; g++) GapSpaces[g] = 1;
                    SpacesToAdd -= Gaps;

                    int GapIndex = 0;
                    while (SpacesToAdd > 0)
                    {
                        GapSpaces[GapIndex]++;
                        SpacesToAdd--;
                        GapIndex++;
                        if (GapIndex >= Gaps) GapIndex = 0;
                    }
                    for (int w = 0; w < LineWords.Length; w++)
                    {
                        Line += LineWords[w];
                        if (w < Gaps)
                        {
                            Line += new string(' ', GapSpaces[w]);
                        }
                    }
                }
                Array.Resize(ref ResultArr, ResultArr.Length + 1);
                ResultArr[ResultArr.Length - 1] = Line;
            }

            Output = ResultArr;
        }

        public override string ToString()
        {
            if (Output == null) return "";
            return string.Join(Environment.NewLine, Output);
        }

        //constructor
        public Task2(string text) : base(text) { }
    }
}