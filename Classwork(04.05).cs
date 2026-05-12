using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using static MySerialization.MyXMLSerializator;

namespace MySerialization
{
    public class MyXMLSerializator : MySerializater
    {
        public MyXMLSerializator()
        {
        }

        public new void Serialize()
        {
            var ser = new XmlSerializer(typeof(DTOStudent[]));

            _path = Path.Combine(_desktopPath, "example.xml");
            
            using(var fs = new StreamReader(_path))
            {
                var dto = new List<DTOStudent>(_students.Count);

                foreach (var student in _students)
                    dtoObjects.Add(new DTOStudent(student);
                
                ser.Serialize(fs, dtoObjects);
            }
        }
        public new void Deserialize()
        {
            var ser = new XmlSerializer(typeof(DTOSubject));

            using (var fs = new StreamReader(_path))
            {
                var objects = ser.Deserialize(fs) as DTOStudent[];
                _students = new List<Student>;

                foreach (var obj in objects)

            }
        }

        public class DTOStudent
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }

            public Subject[] Students { get; set; }
            public DTOStudent()
            {
                _students.Add(obj.GetStudent());
            }

            public DTOStudent(Student)
            {
                Id = Student.id;
                Name = Student.name;
                Surname = Student.surname;
                var dtoObjects = new List<DTOSubject>(_students.Subject.Length);
                foreach (var subject in Students.Subjects)
                {
                    dtoObjects.Add(new DTOSubject(subject));
                }
                Subjects = Student.subjects;
            }


            public Student GetStudent()
            {
                var 
                return new Student(Id, Name, Surname, Subjects);
            }
    }

        public class DTOSubject
        {
            [XmlElement(ElementName = "Subject")]

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
