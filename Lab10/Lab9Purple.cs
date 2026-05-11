using System;

namespace Lab9.Purple
{
    public abstract class Purple
    {
        private string _input;

        public string Input => _input;

        protected Purple(string text)
        {
            _input = text ?? "";
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text ?? "";
            Review();
        }
    }

    public class Task1 : Purple
    {
        private string _output;
        public string Output => _output;

        public Task1(string text) : base(text)
        {
            _output = "";
        }

        private bool IsWordChar(char c)
        {
            return char.IsLetter(c) || c == '-' || c == '\'';
        }

        private void AddToArray(ref string[] array, string item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = "";
                return;
            }

            string[] words = new string[0];
            string[] signs = new string[0];

            bool isWordFirst = IsWordChar(Input[0]);
            bool isWordNow = isWordFirst;
            string buffer = "";

            for (int i = 0; i < Input.Length; i++)
            {
                if (IsWordChar(Input[i]) == isWordNow)
                {
                    buffer += Input[i];
                }
                else
                {
                    if (isWordNow) AddToArray(ref words, buffer);
                    else AddToArray(ref signs, buffer);

                    isWordNow = !isWordNow;
                    buffer = Input[i].ToString();
                }
            }

            if (buffer.Length > 0)
            {
                if (isWordNow) AddToArray(ref words, buffer);
                else AddToArray(ref signs, buffer);
            }

            for (int i = 0; i < words.Length; i++)
            {
                char[] chars = words[i].ToCharArray();
                Array.Reverse(chars);
                words[i] = new string(chars);
            }

            string result = "";
            int wordIndex = 0;
            int signIndex = 0;

            while (wordIndex < words.Length || signIndex < signs.Length)
            {
                if (isWordFirst)
                {
                    if (wordIndex < words.Length) result += words[wordIndex++];
                    if (signIndex < signs.Length) result += signs[signIndex++];
                }
                else
                {
                    if (signIndex < signs.Length) result += signs[signIndex++];
                    if (wordIndex < words.Length) result += words[wordIndex++];
                }
            }

            _output = result;
        }

        public override string ToString()
        {
            return Output;
        }
    }

    public class Task2 : Purple
    {
        private string[] _output;
        public string[] Output => _output;

        public Task2(string text) : base(text)
        {
            _output = new string[0];
        }

        private void AddToArray(ref string[] array, string item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = new string[0];
                return;
            }

            string[] words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] result = new string[0];
            string[] currentLine = new string[0];
            int charCount = 0;

            for (int i = 0; i < words.Length; i++)
            {
                int addLength = words[i].Length;
                if (currentLine.Length > 0)
                {
                    addLength++;
                }

                if (charCount + addLength <= 50)
                {
                    AddToArray(ref currentLine, words[i]);
                    charCount += addLength;
                }
                else
                {
                    AddToArray(ref result, SpaceLine(currentLine, 50));
                    currentLine = new string[0];
                    AddToArray(ref currentLine, words[i]);
                    charCount = words[i].Length;
                }
            }

            if (currentLine.Length > 0)
            {
                AddToArray(ref result, SpaceLine(currentLine, 50));
            }

            _output = result;
        }

        private string SpaceLine(string[] words, int targetLength)
        {
            if (words.Length == 0)
            {
                return new string(' ', targetLength);
            }

            int wordsLength = 0;
            for (int i = 0; i < words.Length; i++)
            {
                wordsLength += words[i].Length;
            }

            int totalSpaces = targetLength - wordsLength;
            if (words.Length == 1)
            {
                return words[0] + new string(' ', totalSpaces);
            }

            int gaps = words.Length - 1;
            int baseSpaces = totalSpaces / gaps;
            int extraSpaces = totalSpaces % gaps;
            string result = "";

            for (int i = 0; i < words.Length; i++)
            {
                result += words[i];
                if (i < gaps)
                {
                    int spaces = baseSpaces;
                    if (i < extraSpaces)
                    {
                        spaces++;
                    }

                    result += new string(' ', spaces);
                }
            }

            return result;
        }

        public override string ToString()
        {
            return string.Join("\n", Output);
        }
    }

    public class Task3 : Purple
    {
        private string _output;
        private (string, char)[] _codes;

        public string Output => _output;
        public (string, char)[] Codes => _codes;

        public Task3(string text) : base(text)
        {
            _output = "";
            _codes = new (string, char)[0];
        }

        private void AddToArray(ref string[] array, string item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        private void AddToArray(ref int[] array, int item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        private void AddToArray(ref char[] array, char item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        private void AddToArray(ref (string, char)[] array, (string, char) item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = "";
                _codes = new (string, char)[0];
                return;
            }

            string[] pairs = new string[0];
            int[] counts = new int[0];
            int[] firstSeen = new int[0];
            _codes = new (string, char)[0];

            for (int i = 0; i < Input.Length - 1; i++)
            {
                if (char.IsLetter(Input[i]) && char.IsLetter(Input[i + 1]))
                {
                    string pair = Input[i].ToString() + Input[i + 1].ToString();
                    bool found = false;

                    for (int j = 0; j < pairs.Length; j++)
                    {
                        if (pairs[j] == pair)
                        {
                            counts[j]++;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        AddToArray(ref pairs, pair);
                        AddToArray(ref counts, 1);
                        AddToArray(ref firstSeen, i);
                    }
                }
            }

            for (int i = 0; i < pairs.Length - 1; i++)
            {
                for (int j = 0; j < pairs.Length - i - 1; j++)
                {
                    if (counts[j] < counts[j + 1] || counts[j] == counts[j + 1] && firstSeen[j] > firstSeen[j + 1])
                    {
                        (pairs[j], pairs[j + 1]) = (pairs[j + 1], pairs[j]);
                        (counts[j], counts[j + 1]) = (counts[j + 1], counts[j]);
                        (firstSeen[j], firstSeen[j + 1]) = (firstSeen[j + 1], firstSeen[j]);
                    }
                }
            }

            int limit = pairs.Length < 5 ? pairs.Length : 5;
            char[] freeChars = new char[0];

            for (int code = 32; code <= 126 && freeChars.Length < limit; code++)
            {
                char c = (char)code;
                bool isUsed = false;

                for (int i = 0; i < Input.Length; i++)
                {
                    if (Input[i] == c)
                    {
                        isUsed = true;
                        break;
                    }
                }

                if (!isUsed)
                {
                    AddToArray(ref freeChars, c);
                }
            }

            for (int i = 0; i < limit; i++)
            {
                AddToArray(ref _codes, (pairs[i], freeChars[i]));
            }

            _output = Encode(Input, _codes);
        }

        private string Encode(string text, (string, char)[] codes)
        {
            string result = "";
            int index = 0;

            while (index < text.Length)
            {
                bool replaced = false;

                if (index < text.Length - 1)
                {
                    string pair = text[index].ToString() + text[index + 1].ToString();

                    for (int i = 0; i < codes.Length; i++)
                    {
                        if (pair == codes[i].Item1)
                        {
                            result += codes[i].Item2;
                            index += 2;
                            replaced = true;
                            break;
                        }
                    }
                }

                if (!replaced)
                {
                    result += text[index];
                    index++;
                }
            }

            return result;
        }

        public override string ToString()
        {
            return Output;
        }
    }

    public class Task4 : Purple
    {
        private string _output;
        private (string, char)[] _codes;

        public string Output => _output;
        public (string, char)[] Codes => _codes;

        public Task4(string text, (string, char)[] codes) : base(text)
        {
            _output = "";
            _codes = codes ?? new (string, char)[0];
        }

        public override void Review()
        {
            string result = "";

            for (int i = 0; i < Input.Length; i++)
            {
                bool decoded = false;

                for (int j = 0; j < _codes.Length; j++)
                {
                    if (Input[i] == _codes[j].Item2)
                    {
                        result += _codes[j].Item1;
                        decoded = true;
                        break;
                    }
                }

                if (!decoded)
                {
                    result += Input[i];
                }
            }

            _output = result;
        }

        public override string ToString()
        {
            return Output;
        }
    }
}
