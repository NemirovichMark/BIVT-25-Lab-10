using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MySerialization
{
    public class MySerializater
    {
        protected string _desktopPath;
        protected string _path;
        protected List<Student> _students;
        public MySerializater()
        {
            // Получаем путь к рабочему столу текущего пользователя
            _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // Создаем путь к новому файлу на рабочем столе

            _students = new List<Student>(3)
            {
                new Student("Petya", "Ivanov"),
                new Student("Fedor", "Lazarev"),
                new Student("Tatyana", "Smirnova")
            };
            _students[0].AddMarks("Math", new int[] { 1, 1, 2, 3, 5, 5, 3, 4, 5 });
            _students[1].AddMarks("Math", new int[] { 2, 3, 5, 5, 3, 4, 5 });
            _students[2].AddMarks("Math", new int[] { 3, 5, 5, 3, 4, 5 });
            _students[0].AddMarks(new Subject("CS"), new int[] { 5, 3, 4, 5 });
            _students[1].AddMarks(new Subject("CS"), new int[] { 5 });
            _students[2].AddMarks("CS", new int[] { 4, 5 });

            foreach (var student in _students)
            {
                student.Print();
            }
        }
        public void Serialize()
        {
            _path = Path.Combine(_desktopPath, "example1.json");
            var jsonString = System.Text.Json.JsonSerializer.Serialize(_students);

            Console.WriteLine(jsonString);
            File.WriteAllText(_path, jsonString);

            _path = Path.Combine(_desktopPath, "example2.json");
            var jsonObject = JObject.FromObject(_students[0]); // добавляем в json одного студента 
            jsonObject.Add("Type", _students[0].GetType().Name); // добавляем в json ещё одно поле, в котором будет указано имя типа данных, который мы сериализуем

            jsonString = jsonObject.ToString();
            Console.WriteLine(jsonString);
            File.WriteAllText(_path, jsonString);

            _students = null;

            var deJsonObject = JObject.Parse(jsonString);
            Student obj = null; // объект базового класса

            switch (deJsonObject["Type"].ToString())
            {
                case "Student":
                    {
                        // тоже требует атрибутов как и Text.Json, они также конфликтуют, если у вас обе библиотеки подключены
                        obj = deJsonObject.ToObject<Student>();

                        obj.Print();
                        break;
                    }
            }

            _students = null;
        }
        public void Deserialize()
        {
            _path = Path.Combine(_desktopPath, "example1.json");
            Console.WriteLine();
            var jsonString = File.ReadAllText(_path);
            Console.WriteLine(jsonString);

            Student[] students = System.Text.Json.JsonSerializer.Deserialize<Student[]>(jsonString);

            foreach (var student in students)
            {
                student.Print();
            }
        }
    }
}
