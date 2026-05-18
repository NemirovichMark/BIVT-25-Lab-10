using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace MySerialization
{
    public class MyXMLSerializater : MySerializater
    {
        public new void Serialize()
        {
            var ser = new XmlSerializer(typeof(DTOStudent[])); // создаем структуру XML-документа

            _path = Path.Combine(_desktopPath, "example.xml");

            using (var fs = new StreamWriter(_path))
            {
                var dtoObjects = new List<DTOStudent>(_students.Count);
                foreach (var student in _students) { // заменяем обычных студентов на дтошных с публичными сеттерами
                    dtoObjects.Add(new DTOStudent(student));
                }
                ser.Serialize(fs, dtoObjects.ToArray()); // заполняем содержимое XML-документа и сохраняем в файл
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

                foreach (var obj in objects) // обратно из дтошных в нормальных студентов
                {
                    _students.Add(obj.GetStudent());
                }
            }
            Console.WriteLine(  );
            foreach (var student in _students)
            {
                student.Print();
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
            public Student GetStudent()
            {
                var subjects = new Subject[Subjects.Length];
                for (int i = 0; i < Subjects.Length; i++)
                    subjects[i] = Subjects[i].GetSubject();

                return new Student(Id, Name, Surname, subjects);
            }
        }
        [XmlInclude(typeof(DTOCourse))]
        public class DTOSubject
        {
            [XmlElement(ElementName = "Subject")] // аттрибуты позволяют менять вид/свойства тегов в структуре XML-документа
            public string Name { get; set; }
            public string TypeName { get; set; }
            public int[] Marks { get; set; }
            public int Duration { get; set; }
            public DTOSubject()
            {

            }
            public DTOSubject(Subject subject)
            {
                Name = subject.Name;
                Marks = subject.Marks;
                if (subject is Cource cource)
                    Duration = cource.Duration;
            }
            public virtual Subject GetSubject()
            {
                if (Duration > 0)
                    return new Cource(Name, Marks, Duration);
                return new Subject(Name, Marks);
            }
        }
        public class DTOCourse : DTOSubject
        {
            [XmlElement(ElementName = "Subject")]
            public string Name { get; set; }
            public int[] Marks { get; set; }
            public int Duration { get; set; }
            public DTOCourse()
            {

            }
            public DTOCourse(Cource subject)
            {
                Name = subject.Name;
                Marks = subject.Marks;
                Duration = subject.Duration;
            }
            public override Cource GetSubject()
            {
                return new Cource(Name, Marks, Duration);
            }
        }
    }
}
