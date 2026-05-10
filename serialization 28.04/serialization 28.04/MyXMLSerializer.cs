using System.Xml.Serialization;
namespace serialization_28._04;

public class MyXMLSerializer : MySerializer
{
    public new void Serialize()
    {
        var ser = new XmlSerializer(typeof(DTOStudent[]));

        _path = Path.Combine(_desktopPath, "example.xml");

        using (var fs = new StreamWriter(_path))
        {
            var dtoObjects = new List<DTOStudent>(_students.Count);
            foreach (var student in _students)
            {
                dtoObjects.Add(new DTOStudent(student));
            }
            ser.Serialize(fs, dtoObjects.ToArray());
        }

        _students = null;
    }

    public new void Deserialize()
    {
        var ser = new XmlSerializer(typeof(DTOStudent[]));

        using (var fs = new StreamReader(_path))
        {
            var objects = ser.Deserialize(fs) as DTOStudent[];

            _students = new List<Student>();

            foreach (var obj in objects)
            {
                _students.Add(obj.GetStudent());
            }
        }

        Console.WriteLine();
        foreach (var student in _students)
        {
            Console.WriteLine($"{student.Id} {student.Name} has marks: " +
                $"{string.Join(" ", student.Subjects.Select(x => x.FinalMark))}");
        }
    }

    public class DTOStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DTOSubject[] Subjects { get; set; }

        public DTOStudent() { }

        public DTOStudent(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            Surname = student.Surname;

            var dtoObjects = new List<DTOSubject>(student.Subjects.Length);
            foreach (var subject in student.Subjects)
            {
                dtoObjects.Add(new DTOSubject(subject));
            }
            Subjects = dtoObjects.ToArray();
        }

        public Student GetStudent()
        {
            var subjects = new Subject[Subjects.Length];
            for (int i = 0; i < subjects.Length; i++)
            {
                subjects[i] = Subjects[i].GetSubject();
            }
            return new Student(Id, Name, Surname, subjects);
        }
    }

    public class DTOSubject
    {
        public string Name { get; set; }
        public int[] Marks { get; set; }
        public int Duration { get; set; }

        public DTOSubject() { }

        public DTOSubject(Subject subject)
        {
            Name = subject.Name;
            Marks = subject.Marks;

            if (subject is Course c)
            {
                Duration = c.Duration;
            }
        }

        public virtual Subject GetSubject()
        {
            return new Subject(Name, Marks);
        }
    }
}