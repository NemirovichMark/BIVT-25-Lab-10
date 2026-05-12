using Newtonsoft.Json.Linq;
namespace serialization_28._04;

public class MySerializer
{
    public string _desktopPath;
    public string _path;
    public List<Student> _students;

    public MySerializer()
    {
        _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

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
    }

    public void Serialize()
    {
        _path = Path.Combine(_desktopPath, "example1.json");

        var jsonString = System.Text.Json.JsonSerializer.Serialize(_students);
        Console.WriteLine(jsonString);
        File.WriteAllText(_path, jsonString);

        _path = Path.Combine(_desktopPath, "example2.json");

        var jsonObject = JObject.FromObject(_students[0]);
        jsonObject.Add("Type", _students[0].GetType().Name);

        jsonString = jsonObject.ToString();
        Console.WriteLine(jsonString);
        File.WriteAllText(_path, jsonString);

        _students = null;

        var deJsonObject = JObject.Parse(jsonString);
        Student obj = null;

        switch (deJsonObject["Type"].ToString())
        {
            case "Student":
                obj = deJsonObject.ToObject<Student>();
                obj.Print();
                break;
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
            Console.WriteLine($"{student.Id} {student.Name} has marks: " +
                $"{string.Join(" ", student.Subjects.Select(x => x.FinalMark))}");
        }
    }
}