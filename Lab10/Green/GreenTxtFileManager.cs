using System.Reflection;

namespace Lab10.Green;

public class GreenTxtFileManager : GreenFileManager
{
    public override void EditFile(string newFileText)
    {
        Lab9.Green.Green h = Deserialize<Lab9.Green.Green>();
        h.ChangeText(newFileText);
        Serialize(h);
        //base.EditFile(newFileText);
    }

    public override void ChangeFileExtension(string newFileExtension)
    {
        ChangeFileFormat("txt");
        //base.ChangeFileExtension(newFileExtension);
    }

    public override void Serialize<T>(T obj)
    {
        ChangeFileFormat("txt");
        Type type = obj.GetType();
        
        List<string> lines = new List<string>();
        lines.Add("Type;;;" + type.AssemblyQualifiedName);

        Type currtype = type;
        while (currtype != null && currtype != typeof(object))
        {
            var fields = currtype.GetFields(BindingFlags.Instance | BindingFlags.Public |  BindingFlags.NonPublic);

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                
                var value = field.GetValue(obj);
                if (value != null && value.GetType() == typeof(string))
                {
                    string valstr = value.ToString().Replace("\n", "[-n-]");
                    lines.Add(field.Name + ";;;" + valstr);
                }
            }
            currtype = currtype.BaseType;
        }
        
        File.WriteAllLines(FullPath, lines);
    }

public override T Deserialize<T>()
    {
        string[] lines = File.ReadAllLines(FullPath);
        string typeStr = lines[0].Split(";;;")[1];
        Type type1 = Type.GetType(typeStr);
        
        var data = new Dictionary<string, string>();
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(";;;");
            data[parts[0]] = parts[1].Replace("[-n-]", "\n");
        }

        var constructor = type1.GetConstructors()[0];
        var parameters = constructor.GetParameters();
        object[] args = new object[parameters.Length];
        
        for (int i = 0; i < parameters.Length; i++)
        {
            string pName = parameters[i].Name; 
            
            if (data.TryGetValue("_" + pName, out string val))
            {
                args[i] = val;
            }
            
            else if (data.TryGetValue("_input", out string inputVal))
            {
                args[i] = inputVal;
            }
        }
        
        T res = (T)constructor.Invoke(args);

        if (data.TryGetValue("_input", out string finalInput))
        {
            res.ChangeText(finalInput);
        }

        return res;
    }
        
    

    public GreenTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") : base(name,
        folderPath, fileName, fileExtension){}

    public GreenTxtFileManager(string name) :  base(name) {}
}