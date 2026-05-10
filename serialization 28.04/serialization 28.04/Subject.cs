using System.Text.Json.Serialization;
namespace serialization_28._04;

public class Subject
{
    private List<int> _marks;
    public string Name { get; private set; }
    public int[] Marks => _marks.ToArray();

    public int FinalMark
    {
        get
        {
            if (_marks != null && _marks.Count != 0)
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
    public Subject(string name, int[] marks)
    {
        Name = name;
        _marks = new List<int>(marks);
    }

    public void AddMark(int mark) => _marks.Add(mark);
    public void AddMark(int[] marks) => _marks.AddRange(marks);
}