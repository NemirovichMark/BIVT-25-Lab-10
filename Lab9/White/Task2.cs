namespace Lab9.White;

public class Task2 : White
{

    public Task2(string text) : base(text){}
    
    public override void Review(){}

    public int[,] Output => syllablesMatrix(Input);
    
    public static int[,] syllablesMatrix(string text)
{
    char[] separators = { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', ' ' };

    string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

    char[] vowels =
    {
        'а','е','ё','и','о','у','ы','э','ю','я',
        'А','Е','Ё','И','О','У','Ы','Э','Ю','Я',
        'a','e','i','o','u','y',
        'A','E','I','O','U','Y'
    };

    int[] syllablesPerWord = new int[words.Length];
    int maxSyllables = 0;

    for (int i = 0; i < words.Length; i++)
    {
        int syllables = 0;

        foreach (char c in words[i])
        {
            if (Array.IndexOf(vowels, c) != -1)
                syllables++;
        }

        syllablesPerWord[i] = syllables;

        if (syllables > maxSyllables)
            maxSyllables = syllables;
    }

    int[,] matrix = new int[maxSyllables, 2];

    for (int i = 0; i < words.Length; i++)
    {
        int s = syllablesPerWord[i];

        if (s > 0)
        {
            matrix[s - 1, 0] = s;
            matrix[s - 1, 1]++;
        }
    }

    int filledCount = 0;
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        if (matrix[i, 1] > 0)
            filledCount++;
    }

    int[,] result = new int[filledCount, 2];
    int resultIndex = 0;
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        if (matrix[i, 1] > 0)
        {
            result[resultIndex, 0] = matrix[i, 0];
            result[resultIndex, 1] = matrix[i, 1];
            resultIndex++;
        }
    }

    return result;
}

        public override string ToString()
        {
            var matrix = syllablesMatrix(Input);
            var result = new System.Text.StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result.Append(matrix[i, 0]);
                result.Append(':');
                result.Append(matrix[i, 1]);
                result.AppendLine();
            }

            return result.ToString().TrimEnd();
        }
}