using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace classwork2804
{
    public class MyXMLSerializer : MySerializater
    {
        public new void Serialize()
        {
            var ser = new XmlSerializer(typeof(DTOStudent[]));
            _path = Path.Combine(_dekstopPath, "example.xml");
            using (var fs = new StreamWriter(_path))
            {
                var dtoObjects = new List<DTOStudent>(_students.Count);
                foreach (var student in _students)
                {
                    dtoObjects.Add(new DTOStudent(student));
                }
                ser.Serialize(fs, dtoObjects.ToArray());
            }
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
                    _students.Add(obj.FormStudent());
                }
            }
        }

        public class DTOStudent
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DTOSubject[] Subjects { get; set; }

            public DTOStudent()
            {
                
            }

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

            public Student FormStudent()
            {
                var subjects = new Subject[Subjects.Length];
                for (int i = 0; i < Subjects.Length; i++)
                    subjects[i] = Subjects[i].GetSubject();
                
                return new Student(Id, Name, Surname, subjects);
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
}