using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        private string _output;
        public string Output=>_output;
        public Task1(string text) : base(text)
        { 
            _output = string.Empty;
        }
        public override void Review()
        {
            if (_input == null)
            { _output = null; return; }
            StringBuilder s = new StringBuilder();
            StringBuilder word = new StringBuilder();
            for (int i = 0; i < Input.Length; i++)
            {
                char c = Input[i];
                if (c == '-' || c == '\'' || char.IsLetterOrDigit(c))
                    word.Append(c);
                else
                {
                    if (word.Length > 0)
                    {
                        bool digit = false;
                        for (int j = 0; j < word.Length; j++)
                            if (char.IsDigit(word[j])) { digit = true; break; }
                        if (digit) s.Append(word);
                        else
                            for (int j = word.Length - 1; j >= 0; j--)
                                s.Append(word[j]);
                        word.Clear();
                    }
                    s.Append(c);
                }
            }
                if (word.Length > 0)
                {
                    bool digit = false;
                    for (int j = 0; j < word.Length; j++)
                        if (char.IsDigit(word[j])) { digit = true; break; }
                    if (digit) s.Append(word);
                    else
                        for (int j = word.Length - 1; j >= 0; j--)
                            s.Append(word[j]);
                }
                _output = s.ToString();
        }
        public override string ToString()
        {
            return Output ?? string.Empty; // если Output=null, вернем "".
        }
    }
}
