using System.Text;

namespace Lab9.Blue
{
    public class Task1 : Blue
    {
        private string[] _output;

        public string[] Output => _output;

        public Task1(string input) : base(input)
        {
            _output = null;
        }

        public override void Review()
        {
            if (Input == null || Input.Length == 0)
            {
                _output = [];
                return;
            }

            string[] parts = SplitByWhitespace(Input);
            if (parts.Length == 0)
            {
                _output = [];
                return;
            }

            List<string> lines = new List<string>();
            string currentLine = string.Empty;

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];

                if (currentLine.Length == 0)
                {
                    currentLine = part;
                }
                else if (currentLine.Length + 1 + part.Length <= 50)
                {
                    currentLine += " " + part;
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = part;
                }
            }

            if (currentLine.Length > 0)
            {
                lines.Add(currentLine);
            }

            _output = lines.ToArray();
        }

        public override string ToString()
        {
            return _output == null ? string.Empty : string.Join(Environment.NewLine, _output);
        }

        private static string[] SplitByWhitespace(string text)
        {
            List<string> parts = new List<string>();
            int index = 0;

            while (index < text.Length)
            {
                while (index < text.Length && char.IsWhiteSpace(text[index]))
                {
                    index++;
                }

                int start = index;

                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                {
                    index++;
                }

                if (index > start)
                {
                    parts.Add(text.Substring(start, index - start));
                }
            }

            return parts.ToArray();
        }
    }
}
