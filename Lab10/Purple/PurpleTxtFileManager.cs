namespace Lab10.Purple;

public class PurpleTxtFileManager<T> : PurpleFileManager<T>
    where T : Lab9.Purple.Purple
{
    public PurpleTxtFileManager(string name) : base(name)
    {
    }
    public PurpleTxtFileManager(
        string name,
        string folderPath,
        string fileName,
        string fileExtension = "")
        : base(name, folderPath, fileName, fileExtension)
    {
    }

    public override void EditFile(string change_file)
    {
        T obj = Deserialize();
        obj.ChangeText(change_file);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string new_extension) =>
        base.ChangeFileFormat("txt");

    public override void Serialize(T obj)
    {
        if (obj == null) return;
        Directory.CreateDirectory(FolderPath);

        using (StreamWriter sw = new  StreamWriter(FullPath))
        {
            sw.WriteLine($"Type: {obj.GetType().AssemblyQualifiedName}");
            sw.WriteLine($"Input: {obj.Input}");
            if (obj is Lab9.Purple.Task4 task4)
            {
                sw.WriteLine("Codes: ");
                foreach (var code in task4.Codes)
                {
                    sw.WriteLine($"{code.Item1}|{code.Item2}");
                }
            }
        }
    }

    public override T Deserialize()
    {
        if (!File.Exists(FullPath)) return null!;

        string txt_object_type = null!;
        string txt_object_input = null!;
        bool need_codes = false;
        var codes = new List<(string, char)>();

        using (var sr = new StreamReader(FullPath))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine()!;
                
                if (line.StartsWith("Type: "))
                {
                    txt_object_type = line.Substring("Type: ".Length);
                    need_codes = false;
                }
                else if (line.StartsWith("Input: "))
                {
                    txt_object_input = line.Substring("Input: ".Length);
                    need_codes = false;
                }
                else if (line.StartsWith("Codes: ")) 
                {
                    need_codes = true;
                    continue;
                }
                else if (need_codes)
                {
                    string[] pair = line.Split("|");
                    codes.Add((pair[0],pair[1][0]));
                }
            }
        }

        if (txt_object_type == null) return null!;
        if (txt_object_input == null) return null!;

        Type? object_type = Type.GetType(txt_object_type);
        if (object_type == null) return null!;

        if (!typeof(T).IsAssignableFrom(object_type)) return null!;

        Lab9.Purple.Purple obj;
        if (object_type == typeof(Lab9.Purple.Task1))
        {
            obj = new Lab9.Purple.Task1(txt_object_input);
        }
        else if (object_type == typeof(Lab9.Purple.Task2))
        {
            obj = new Lab9.Purple.Task2(txt_object_input);
        }
        else if (object_type == typeof(Lab9.Purple.Task3))
        {
            obj = new Lab9.Purple.Task3(txt_object_input);
        }
        else obj = new Lab9.Purple.Task4(txt_object_input, codes.ToArray());

        obj.Review();
        return (T)obj;
    }
}
