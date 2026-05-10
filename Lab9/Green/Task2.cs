namespace Lab9.Green
{
    public class Task2 : Green
    {
        private char[] _output;

        public char[] Output => _output;

        public Task2(string text) : base(text)
        {
            _output = new char[0];
            Review();
        }

        public override void Review()
        {
            if (Input == null)
            {
                _output = new char[0];
                return;
            }

            var tokens = Input.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length == 0)
            {
                _output = new char[0];
                return;
            }

            var firstLetters = tokens
                .Where(t => (t.Length > 0 && char.IsLetter(t[0])) || t.Count(char.IsLetter) >= 2)
                .Select(t => char.ToLower(t.First(char.IsLetter)))
                .ToArray();

            _output = firstLetters
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .Select(g => g.Key)
                .ToArray();
        }

        public override string ToString()
        {
            if (_output == null) return string.Empty;
            return string.Join(", ", _output);
        }
    }
}
