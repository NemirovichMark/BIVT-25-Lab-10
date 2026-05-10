namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char, double)[] _output;

        public (char, double)[] Output => _output;

        public Task1(string text) : base(text)
        {
            _output = new (char, double)[0];
            Review();
        }

        public override void Review()
        {
            if (Input == null)
            {
                _output = new (char, double)[0];
                return;
            }

            string russianLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            int total = Input.Count(char.IsLetter);
            var letters = Input.ToLower().Where(c => russianLetters.Contains(c)).ToArray();

            if (total == 0)
            {
                _output = new (char, double)[0];
                return;
            }

            _output = russianLetters
                .Where(c => letters.Contains(c))
                .Select(c => (c, (double)letters.Count(x => x == c) / total))
                .ToArray();
        }

        public override string ToString()
        {
            if (_output == null) return string.Empty;
            return string.Join("\n", _output.Select(t => $"{t.Item1}:{t.Item2:F4}"));
        }
    }
}
