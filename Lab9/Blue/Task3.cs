using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task3 : Blue
    {
        private (char, double)[] _output;
        public (char, double)[] Output => _output;
        public Task3(string input) : base(input)
        {
            _output = new (char, double)[0];
        }

        public override void Review()
        {
            double[] ans = new double[26];
            var t = String
                .Split(_input) // Разделили все по словам
                .Where(x=>String.OnlyLetters(x).Length > 0)
                .Select(x => String.OnlyLetters(x)[0]) // Выбираем только первые элементы
                .GroupBy(x => x) // Получаем двумерный массив где все аккуратно сгруппировано по элементам
                .GroupBy(x => x.Count()) // Считаем каждые элементы и группируем по их количеству
                .ToDictionary(x=>x.Key, x=>x.Select(x=>x.Key).ToList());
            double len = String
                .Split(_input)
                .Where(x => String.OnlyLetters(x).Length > 0)
                .Select(x => x) // Создаем массив только из слов 
                .ToArray()
                .Length; // Считаем его размер
            foreach(var elem in t)
            {
                foreach(var key in elem.Value)
                {
                    String.Add(ref _output, (key, (double)(elem.Key) / len * 100.0));
                }
            }
            _output = _output
                      .OrderBy(x => x.Item1)
                      .OrderByDescending(x => x.Item2)
                      .ToArray();
            return;
        }
        public override string ToString()
        {
            var ans = "";
            for (int i = 0; i < _output.Length; i++)
            {
                ans += $"{_output[i].Item1}:{_output[i].Item2:F4}";
                if (i != _output.Length - 1) ans += Environment.NewLine;
            }
            return ans;
        }
    }
}
