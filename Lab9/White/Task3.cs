using System.Text.RegularExpressions;

namespace Lab9.White
{
    public class Task3 : White
    {
        private string[,] _codeTable;
        private string _output;
        private string _originalText;

        public string Output
        {
            get
            {
                if (_output == null)
                    return Input;
                return _output;
            }
        }

        public Task3(string input, string[,] codeTable) : base(input)
        {
            _codeTable = codeTable;
            _originalText = input;
            _output = null;
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(_originalText))
            {
                _output = string.Empty;
                return;
            }

            string result = _originalText;

            
            for (int i = 0; i < _codeTable.GetLength(0); i++)
            {
                string word = _codeTable[i, 0];
                string code = _codeTable[i, 1];

                if (!string.IsNullOrEmpty(word))
                {
                    string pattern = $@"\b{Regex.Escape(word)}\b";
                    result = Regex.Replace(result, pattern, code);
                }
            }

            _output = result;
        }

        public override void ChangeText(string text)
        {
            _originalText = text ?? string.Empty;
            _output = null;
            base.ChangeText(text);
        }

        public override string ToString()
        {
            return Output;
        }
    }
}