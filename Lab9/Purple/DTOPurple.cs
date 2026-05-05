namespace Lab9;

public class DTOPurple
{
    public string TypeObject {get;set;}
    public string Input {get; set;}
    public DTOCode[] Codes {get; set;} = Array.Empty<DTOCode>();

    public DTOPurple()
    {
    }

    public DTOPurple (Lab9.Purple.Purple purple)
    {
        TypeObject = purple.GetType().AssemblyQualifiedName!;
        Input = purple.Input;

        if (purple is Lab9.Purple.Task4 task4)
        {
            Codes = task4.Codes
                    .Select(pair => new DTOCode(pair.Item1, pair.Item2))
                    .ToArray();
        }
    }

    public Lab9.Purple.Purple GetPurpleTask()
    {
        Type? object_type = Type.GetType(TypeObject);
        if (object_type == null) return null!;

        Lab9.Purple.Purple obj = null!;
        if (object_type == typeof(Lab9.Purple.Task1))
        {
            obj = new Lab9.Purple.Task1(Input);
        }
        else if (object_type == typeof(Lab9.Purple.Task2))
        {
            obj = new Lab9.Purple.Task2(Input);
        }
        else if (object_type == typeof(Lab9.Purple.Task3))
        {
            obj = new Lab9.Purple.Task3(Input);
        }
        else if (object_type == typeof(Lab9.Purple.Task4))
        {
            (string,char)[] codes = Codes
                                    .Select(pair => (pair.Symbols,pair.Code))
                                    .ToArray();

            obj = new Lab9.Purple.Task4(Input, codes.ToArray());
        }
        else return null!;
        
        if (obj == null) return null!;

        obj.Review();
        return obj;
    }
}

public class DTOCode
{
    public string Symbols {get; set;}
    public char Code {get; set;}
    public (string,char) GetTask4Code => (Symbols,Code);
    public DTOCode()
    {
    }
    public DTOCode (string symbols, char code)
    {
        Symbols = symbols;
        Code = code;
    }
    
}
