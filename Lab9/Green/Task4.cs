namespace Lab9.Green
{
    public class Task4 : Green
    {
        private string[] _output;

        public string[] Output => _output;

        public Task4(string text) : base(text)
        {
            _output = new string[0];
            Review();
        }

        public override void Review()
        {
            if (Input == null)
            {
                _output = new string[0];
                return;
            }

            var names = Input.Split(',')
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToArray();

            for (int i = 0; i < names.Length - 1; i++)
            {
                for (int j = 0; j < names.Length - 1 - i; j++)
                {
                    if (string.Compare(names[j], names[j + 1], StringComparison.CurrentCulture) > 0)
                    {
                        string tmp = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = tmp;
                    }
                }
            }

            _output = names;
        }

        public override string ToString()
        {
            if (_output == null) return string.Empty;
            return string.Join(Environment.NewLine, _output);
        }
    }
}
