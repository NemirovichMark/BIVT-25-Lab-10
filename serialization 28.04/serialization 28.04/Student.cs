using System.Text.Json.Serialization;
namespace serialization_28._04;

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
            new Subject("Math"),
            new Subject("CS"),
            new Subject("History")
        };
    }

    [JsonConstructor]
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
        Console.WriteLine($"{Id} {Name} has marks: " +
            $"{string.Join(" ", Subjects.Select(x => x.FinalMark))}");
    }
}



public class Course : Subject
{
    public int Duration { get; private set; }

    public Course(string name, int duration) : base(name)
    {
        Duration = duration;
    }

    public Course(string name, int[] marks, int duration) : base(name, marks)
    {
        Duration = duration;
    }
}