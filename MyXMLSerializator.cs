using System.Xml;
using System.Xml.Serialization;

namespace Serialization;

internal class MyXMLSerializator : MySerializater
{
    public new void Serialize(XmlWriter xmlWriter)
    {
        var ser = new XmlSerializer(typeof(Student[]));
        
        _path = Path.Combine(_desktopPath, "example.xml");
        using (var fs = new StreamWriter(_path))
        {
            var dtoObjects = new List<DTOStudent>(_students.Count);
            foreach (var student in _students)
            {
                dtoObjects.Add(new DTOStudent(student));
            }
            ser.Serialize(fs, _students);
        }

        _students = null;
    }

    public new void Deserialize(XmlReader xmlReader)
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
    }

    public class DTOStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Subject[]  Subjects { get; set; }

        public DTOStudent()
        {
        }

        public DTOStudent(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            Surname = student.Surname;
            Subjects = student.Subjects;
        }
        
        public Student GetStudent()
        {
            return new Student(Id, Name, Surname, Subjects);
        }
    }

    public class DTOSubject
    {
        public string Name { get; set; }
        public int[] Marks { get; set; }

        public DTOSubject()
        {
        }

        public DTOSubject(Subject subject)
        {
            Name = subject.Name;
            Marks = subject.Marks;
        }

        public Subject GetSubject()
        {
            return new Subject(Name, Marks);
        }
    }
}