
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace classwork
{
    internal class classwork_06_04
    {
        static void Main(string[] args)
        {
            string s = "Hello, World!";
            Console.WriteLine(s);
            Console.WriteLine(s[7]);

            var students = new Student[]
            {
                new Student("a", "A", new int[,] { { 1, 2, 3 }, { 4, 5, 6 } }),
                new Student("B", "b", new int[,] { { 2, 4, 3 }, { 4, 1, 1 } }),
                new Student("C", "C", new int[,] { { 5, 2, 1 }, { 2, 4, 4 } }),
            };

            foreach (var student in students)
            {
                Console.WriteLine(student[0]);
            }

            string str = "I am a good student";
            string str2 = "No! I am!";
            str = "No! I am!";
            str2 = str;

            Console.WriteLine(str2);
            //str2 = str2.Substring(2, 5);
            //str2 = str2.Replace("am", "sAS", StringComparison.InvariantCultureIgnoreCase);
            //int index = str2.IndexOf("I");

            var strings = str.Split(new char[] { '.', '?', '!'}, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(str2);

            foreach (var c in "Решения должны находиться в файле с названием, соответствующем его номеру внутри папки\r\nс названием вашей лиги. Решение номеров необходимо писать внутри класса с соответствующим\r\nномером (Task1 – Task4). Автотестами будет сравниваться состояние Ваших объектов с выходными\r\nданными.")
            {
                bool isLetter = Char.IsLetter(c);
                bool isDigit = Char.IsDigit(c);
                bool isSpaceTabNewLine = Char.IsSeparator(c);
                bool isPunctuation = Char.IsPunctuation(c);
            }

            string output = $"New \n text \r on \r\n each{Environment.NewLine} line!";

            StringBuilder sb = new StringBuilder();
            sb.Append("sdadsdad");
            sb.Remove(1, 5);
            //преобразование динамического массива символово в строку
            sb.ToString();


            //Regex regex = new Regex("[/d+]");
            //var result = regex.Match("Решения должны находиться в файле с названием, соответствующем его номеру внутри папки\r\nс названием вашей лиги. Решение номеров необходимо писать внутри класса с соответствующим\r\nномером (Task1 – Task4). Автотестами будет сравниваться состояние Ваших объектов с выходными\r\nданными.");
            //foreach (var match in result.Value)
            //{
            //    Console.WriteLine(match);
            //}
        }
    }

    public class Student
    {
        string _name;
        string _surname;

        int[,] _marks;

        public int[,] Marks => _marks;
        public double[] AverageMarks
        {
            get
            {
                if ( _marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0)
                {
                    return null;
                }
                var average = new double[_marks.GetLength(0)];
                for (int i = 0; i < average.Length; i++)
                {
                    for (int j = 0; j<_marks.GetLength(1); j++)
                    {
                        average[i] += (double)_marks[i, j] / _marks.GetLength(1);
                    }
                }
                return average;
            }
        }

        public char this[int idx]
        {
            get { return _name[idx]; }
        }

        //public double this[int idx]
        //{
        //    get { return AverageMarks[idx]; }
        //}

        public int this[int i, int j]
        {
            get { return _marks[i, j]; }
        }

        public Student(string name, string surname, int[,] marks = null)
        {
            _name = name;
            _surname = surname;
            if (_marks != null)
            {
                _marks = (int[,])marks.Clone();
            }
        }

        public override string ToString()
        {
            string output = _name + " " + _surname;
            for (int i = 0; i < _marks.GetLength(0); i++)
            {
                for (int j = 0; j<_marks.GetLength(1); j++)
                {
                    output += _marks[i, j] + " ";
                }
                output = output.TrimEnd();
                output += Environment.NewLine;
            }
            return output;

            StringBuilder sb = new StringBuilder(_name);
            sb.Append(" ");
            sb.AppendLine(_surname);
            for (int i = 0; i < _marks.GetLength(0); i++)
            {
                for (int j = 0; j < _marks.GetLength(1); j++)
                {
                    sb.Append(_marks[i, j]).Append(" ");
                }
                sb = sb.Remove(sb.Length-1, 1);
                sb.AppendLine();
            }


        }
    }
}