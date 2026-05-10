namespace Lab9.Green
{
    public class Task3 : Green
    {
        private string _sequence;
        private string[] _output;

        public string[] Output => _output;
        public string Sequence => _sequence;

        public Task3(string text, string sequence) : base(text)
        {
            _sequence = sequence;
            _output = new string[0];
            Review();
        }

        public override void Review()
        {
            if (Input == null || _sequence == null)
            {
                _output = new string[0];
                return;
            }

            var words = Input.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => new string(w.Where(c => char.IsLetter(c) || c == '-' || c == '\'' || c == '`').ToArray()))
                .Where(w => w.Length > 0)
                .ToArray();

            string seq = _sequence.ToLower();

            var seen = new List<string>();
            foreach (var word in words)
            {
                if (word.ToLower().Contains(seq))
                {
                    string lower = word.ToLower();
                    if (!seen.Any(s => s.ToLower() == lower))
                        seen.Add(word);
                }
            }

            _output = seen.ToArray();
        }

        public override string ToString()
        {
            if (_output == null) return string.Empty;
            return string.Join(Environment.NewLine, _output);
        }
    }
}
