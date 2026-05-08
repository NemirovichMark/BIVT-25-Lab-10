namespace Lab9.Blue;

public class Task2 : Blue
{
    private string sequence;
    private string output;

    public string Output
    {
        get
        {
            return output;
        }
        private set
        {
            output = value;
        }
    }

    public Task2(string text, string sequenceToRemove) : base(text)
    {
        sequence = sequenceToRemove;
        Output = "";
        Review();
    }

    private bool IsWordChar(char c)
    {
        return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') ||
               (c >= 'А' && c <= 'Я') || (c >= 'а' && c <= 'я') ||
               c == 'ё' || c == 'Ё' || c == '-' || c == '\'';
    }

    private bool IsSpace(char c)
    {
        return c == ' ';
    }

    private bool IsPunctuation(char c)
    {
        return c == '.' || c == ',' || c == '!' || c == '?' || c == ':' || c == ';';
    }

    private bool WordHasSequence(string word, string pattern)
    {
        if (pattern.Length == 0)
        {
            return true;
        }

        if (word.Length < pattern.Length)
        {
            return false;
        }

        for (int i = 0; i <= word.Length - pattern.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < pattern.Length; j++)
            {
                if (word[i + j] != pattern[j])
                {
                    match = false;
                    break;
                }
            }

            if (match)
            {
                return true;
            }
        }
        
        return false;
    }

    public override void Review()
    {
        string inputText = Input;
        
        if (inputText == null || inputText.Length == 0 || sequence.Length == 0)
        {
            Output = inputText;
            return;
        }

        char[] buffer = new char[inputText.Length * 2];
        int bufferIndex = 0;
        int pos = 0;

        while (pos < inputText.Length)
        {
            while (pos < inputText.Length && !IsWordChar(inputText[pos]))
            {
                buffer[bufferIndex++] = inputText[pos];
                pos++;
            }

            if (pos >= inputText.Length)
            {
                break;
            }

            int wordStart = pos;
            while (pos < inputText.Length && IsWordChar(inputText[pos]))
            {
                pos++;
            }
            int wordLength = pos - wordStart;

            char[] wordArray = new char[wordLength];
            for (int k = 0; k < wordLength; k++)
            {
                wordArray[k] = inputText[wordStart + k];
            }
            string currentWord = new string(wordArray);

            if (!WordHasSequence(currentWord, sequence))
            {
                for (int k = 0; k < wordLength; k++)
                {
                    buffer[bufferIndex++] = wordArray[k];
                }
            }
        }
        
        char[] clean = new char[bufferIndex];
        int cleanIndex = 0;
        bool prevIsSpace = true;

        for (int i = 0; i < bufferIndex; i++)
        {
            char ch = buffer[i];
            if (IsSpace(ch))
            {
                if (!prevIsSpace)
                {
                    clean[cleanIndex++] = ' ';
                    prevIsSpace = true;
                }
            }
            else
            {
                clean[cleanIndex++] = ch;
                prevIsSpace = false;
            }
        }

        if (cleanIndex > 0 && clean[cleanIndex - 1] == ' ')
        {
            cleanIndex--;
        }

        char[] final = new char[cleanIndex];
        int finalIndex = 0;
        for (int i = 0; i < cleanIndex; i++)
        {
            if (!(clean[i] == ' ' && i + 1 < cleanIndex && IsPunctuation(clean[i + 1])))
            {
                final[finalIndex++] = clean[i];
            }
        }

        Output = new string(final, 0, finalIndex);
    }

    public override string ToString()
    {
        return Output;
    }
}
