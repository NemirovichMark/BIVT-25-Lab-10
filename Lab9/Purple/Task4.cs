using Newtonsoft.Json;

namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        private string _output = null;

        private (string, char)[] _codes;

        public string Output => _output;
        public (string, char)[] Codes => _codes;

        public Task4(string input, (string, char)[] codes) : base(input)
        {
            _codes = codes;
        }

        

        public override void Review()
        {
            string result = Input;

            foreach (var (pair, code) in _codes) 
            {
                result = result.Replace(code.ToString(), pair);
            }

            _output = result;
        }

        public override string ToString() => _output;
    }
}
