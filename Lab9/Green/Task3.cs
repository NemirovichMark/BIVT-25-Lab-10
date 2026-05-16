using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task3 : Green
    {
        private string[] _output;
        private string _pattern;

        public string Pattern => _pattern;

        public string[] Output
        {
            get
            {
                if (_output == null)
                    return null;

                string[] copy = new string[_output.Length];
                for (int i = 0; i < _output.Length; i++)
                {
                    copy[i] = _output[i];
                }
                return copy;
            }
        }

        public Task3(string input, string pattern) : base(input)
        {
            _output = new string[0];
            _pattern = pattern;
        }

        public override void Review()
        {
            _output = new string[0];
            if (string.IsNullOrEmpty(Input) || string.IsNullOrEmpty(_pattern))
            {
                _output = null;
                return;
            }

            string inputLower = Input.ToLower();
            string patternLower = _pattern.ToLower();
            char[] punc = { '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' };
            string[] originalWords = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(word => word.Trim(punc))
                                       .Where(word => word.Length > 0)
                                       .ToArray();
            string[] lowerWords = inputLower.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(word => word.Trim(punc))
                                       .Where(word => word.Length > 0)
                                       .ToArray();
            for (int i = 0; i < lowerWords.Length; i++)
            {
                if (lowerWords[i].Contains(patternLower))
                {
                    bool alreadyExists = false;
                    if (_output != null)
                    {
                        for (int j = 0; j < _output.Length; j++)
                        {
                            if (_output[j].ToLower() == lowerWords[i])
                            {
                                alreadyExists = true;
                                break;
                            }
                        }

                    }
                    if (!alreadyExists)
                    {
                        Add(ref _output, originalWords[i]);
                    }
                }
            }
        }

        private void Add(ref string[] array, string newItem)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = newItem;
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return string.Empty;

            return string.Join(Environment.NewLine, _output);
        }
    }
}