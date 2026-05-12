using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace classwork2804
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var ser = new MySerializater();
            // ser.Serialize();
            // ser.Deserialize();

            var serX = new MyXMLSerializer();
            serX.Serialize();
            serX.Deserialize();
        }
    }

    public class Student
    {
        private static int _counter = 0;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public Subject[] Subjects { get; private set; }

        public Student(string name, string surname)
        {
            Id = _counter++;
            Name = name;
            Surname = surname;
            Subjects = new Subject[3]
            {
                new Courses("Math", 4),
                new Subject("CS"),
                new Courses("History", 2),
            };
        }
        
        [JsonConstructor] //при десериализации вызывается именно этот конструктор
        public Student(int id, string name, string surname, Subject[] subjects)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Subjects = subjects;
        }

        public void Addmarks(string subjectname, int[] marks)
        {
            for (int i = 0; i < Subjects.Length; i++)
            {
                if (Subjects[i].Name == subjectname)
                    Subjects[i].AddMark(marks);
            }
        }
        
        public void Addmarks(Subject subject, int[] marks)
        {
            for (int i = 0; i < Subjects.Length; i++)
            {
                if (Subjects[i].Name == subject.Name)
                    Subjects[i].AddMark(marks);
            }
        }
    }

    public class Subject
    {
        private List<int> _marks;
        public string Name { get; private set; }
        public int[] Marks => _marks.ToArray();

        public int FinalMark
        {
            get
            {
                if (_marks != null && _marks.Count > 0)
                {
                    return (int)Math.Round(Marks.Average());
                }
                return 0;
            }
        }
        
        public Subject(string name)
        {
            Name = name;
            _marks = new List<int>();
        }

        [JsonConstructor]
        public Subject(string name, int[] marks)
        {
            Name = name;
            _marks = new List<int>();
            _marks.AddRange(marks);
        }
        public void AddMark(int mark)
        {
            _marks.Add(mark);
        }

        public void AddMark(int[] marks)
        {
            _marks.AddRange(marks);
        }
    }

    public class Courses : Subject
    {
        public int Duration { get; private set; }
        public Courses(string name, int duration) : base(name)
        {
            Duration = duration;
        }

        public Courses(string name, int[] marks, int duration) : base(name, marks)
        {
            Duration = duration;
        }
        
        
    }
}