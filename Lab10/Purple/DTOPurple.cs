using Lab9.Purple;

namespace Lab10.Purple;

public class DTOPurple
{
    public string Type { get; set; }
    public string Input { get; set; }
    public string Output { get; set; }
    public List<CodeItem> Codes { get; set; } = new(); //пустой массив чтобы не падать на других тасках
    public DTOPurple()
    {
        
    }
    
    public DTOPurple(Lab9.Purple.Purple purple)
    {
        Type = purple.GetType().FullName;
        Input = purple.Input;
        Output = purple.ToString();
        
        if (purple is Task4 task4)
            Codes = task4.Codes.Select(x => new CodeItem(x.Item1, x.Item2)).ToList(); //запоковали в codes
    }
}
public class CodeItem
{
    public string Key { get; set; }
    public char Value { get; set; }

    public CodeItem() { }

    public CodeItem(string key, char value)
    {
        Key = key;
        Value = value;
    }
}