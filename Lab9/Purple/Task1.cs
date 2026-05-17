using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        private string _output;
        public string Output => _output;
        public Task1(string text) : base(text)
        {
            _output = default(string);
            Review();
        }

        public override void Review()
        {
            string pattern = @"\b[\p{L}\-']+\b";
            _output = Regex.Replace(_input, pattern, m => new string(m.Value.Reverse().ToArray()));
        
        }
        public override string ToString() => _output;
    }
}
