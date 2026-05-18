using System.Text.Json.Serialization;

namespace MySerialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ser = new MySerializater();
            //ser.Serialize();
            //ser.Deserialize();


            var serX = new MyXMLSerializater();
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
                new Cource("Math", 4),
                new Subject("CS"),
                new Cource("History", 2)
            };
        }
        [JsonConstructor]
        [Newtonsoft.Json.JsonConstructor]
        public Student(int id, string name, string surname, Subject[] subjects)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Subjects = subjects;
        }
        public void AddMarks(string subjectName, int[] marks)
        {
            for (int i = 0; i < Subjects.Length; i++)
                if (Subjects[i].Name == subjectName)
                    Subjects[i].AddMark(marks);
        }
        public void AddMarks(Subject subject, int[] marks)
        {
            for (int i = 0; i < Subjects.Length; i++)
                if (Subjects[i].Name == subject.Name)
                    Subjects[i].AddMark(marks);
        }
        public virtual void Print()
        {
            Console.WriteLine($"{Id} {Name} has marks: ");
            foreach (var s in Subjects)
            {
                Console.Write(string.Join(" ", s.FinalMark));
                if (s is Cource c)
                    Console.Write(", " + c.Duration);
                Console.WriteLine();
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
                    return (int)Math.Round(_marks.Average());
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
        [Newtonsoft.Json.JsonConstructor]
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
        public void AddMark(int[] mark)
        {
            _marks.AddRange(mark);
        }
    }
    public class Cource : Subject
    {
        public int Duration { get; private set; }
        public Cource(string name, int duration) : base(name)
        {
            Duration = duration;
        }
        [Newtonsoft.Json.JsonConstructor]
        public Cource(string name, int[] marks, int duration) : base(name, marks)
        {
            Duration = duration;
        }
    }
}
