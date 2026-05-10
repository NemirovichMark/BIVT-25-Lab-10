﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task1: Blue
    {
        private string[] _output = Array.Empty<string>();
        public string[] Output => (string[])_output.Clone();
        public Task1(string input) : base(input)
        {
            
        }
        public override void Review()
        {
            string summ =null ;
            string[] words = Input.Split(' ');
            
            for (int i = 0; i < words.Length; i++)
            {
                if (summ == null)
                {
                    summ = words[i]; 
                }
                else if (summ.Length + words[i].Length + 1 > 50)
                {
                    Array.Resize(ref _output, _output.Length + 1);
                    _output[^1] = summ;
                    summ = null;
                    summ = words[i];
                }
                else if (summ.Length + words[i].Length + 1 == 50)
                {
                    Array.Resize(ref _output, _output.Length + 1);
                    _output[^1] = summ + ' ' + words[i];
                    summ = null;
                }
                else
                {
                    summ = summ + ' ' + words[i];
                }
                if (i == words.Length - 1)
                {
                    Array.Resize(ref _output, _output.Length + 1);
                    _output[^1] = summ;
                    summ = null;
                }
            }

        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, _output);
        }
    }
}
