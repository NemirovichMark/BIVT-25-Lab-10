namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        private string _output;
        private (string, char)[] _codes;

        public string Output => _output;
        public (string, char)[] Codes => _codes;

        public Task4(string input, (string, char)[] codes) : base(input)
        {
            _output = default;
            _codes = codes;
        }


        public override string ToString()
        {
            return _output;
        }

        public override void Review()
        {
            if (_codes == null || _codes.Length == 0)
            {
                return;
            }

            int lenTable = _codes.Length;
            char code;
            string pair;
            bool fl;

            for (int i = 0; i < _input.Length; i++)
            {
                fl = true;
                for (int j = 0; j < lenTable; j++)
                {
                    (pair, code) = _codes[j];
                    if (_input[i] == code)
                    {
                        _output += pair;
                        fl = false;
                        break;
                    }
                }
                if (fl)
                {
                    _output += _input[i];
                }
            }
        }
    }
}
