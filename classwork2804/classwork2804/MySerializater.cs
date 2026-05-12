using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace classwork2804
{
    public class MySerializater
    {
        protected string _dekstopPath;
        protected string _path;
        protected List<Student> _students;
        public MySerializater()
        {
            _dekstopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            

            _students = new List<Student>(3)
            {
                new Student("Petya", "Ivanov"),
                new Student("Fedor", "Lazarev"),
                new Student("Tatyana", "Smirnova"),
            };
            
            _students[0].Addmarks("Math", new int[] {1,1,2,3,5,3,4,5});
            _students[1].Addmarks("Math", new int[] {1,1,2,3,4,5});
            _students[2].Addmarks("Math", new int[] {1,1,2,3,5});
            _students[0].Addmarks("CS", new int[] {5,3,4,5});
            _students[1].Addmarks("CS", new int[] {4,5});
            _students[2].Addmarks("CS", new int[] {5});
        }
        public void Serialize()
        {
             var jsonString = JsonSerializer.Serialize(_students);
             Console.WriteLine(jsonString);
             File.WriteAllText(_path, jsonString);

             _students = null;
        }

        public void Deserialize()
        {
            Console.WriteLine();
            var jsonString = File.ReadAllText(_path);
            Console.WriteLine(jsonString);

            Student[] students = JsonSerializer.Deserialize<Student[]>(jsonString);
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id} {student.Name} has marks: " + $"{string.Join(" ", student.Subjects.Select(x => x.FinalMark))}");
            }
        }
    }
}