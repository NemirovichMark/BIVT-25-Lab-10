using Lab9.Purple;

namespace Lab10.Purple;

public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleTxtFileManager(string name) : base(name)
    {
    }

    public PurpleTxtFileManager(string name, string folderName, string fileName, string fileExtension = "txt") : base(name, folderName, fileName, fileExtension)
    {
    }

    public override void EditFile(string changeFile)
    {
        T obj = Deserialize();
        if (obj == null) return;

        obj.ChangeText(changeFile);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string newExtension)
    {
        base.ChangeFileFormat("txt");
    }



    public override void Serialize(T obj)
    {
        if (obj == null || string.IsNullOrEmpty(FullPath)) return;
        List<string> lines = new List<string>();
        lines.Add($"Type:{obj.GetType().AssemblyQualifiedName}");
        lines.Add($"Input:{obj.Input}");
        if (obj is Lab9.Purple.Task4 task4 && task4.Codes != null)
        {
            foreach (var code in task4.Codes)
            {
                lines.Add($"Code:{code.Item1},{code.Item2}");
            }
        }
        if (!string.IsNullOrEmpty(FolderPath))
            Directory.CreateDirectory(FolderPath);
        File.WriteAllLines(FullPath, lines);
    }
    public override T Deserialize()
    {
        if (string.IsNullOrEmpty(this.FullPath) || !File.Exists(this.FullPath))
            return null!;
        string[] lines = File.ReadAllLines(this.FullPath);
        string type_name = "";
        string input = "";
        List<(string, char)> codes_list = new List<(string, char)>();
        foreach (string line in lines)
        {
            if (line.StartsWith("Type:"))
                type_name = line.Substring(5).Trim();
            else if (line.StartsWith("Input:"))
                input = line.Substring(6).Trim();
            else if (line.StartsWith("Code:"))
            {
                string[] parts = line.Substring(5).Trim().Split(',');
                if (parts.Length == 2 && parts[1].Length > 0)
                {
                    codes_list.Add((parts[0], parts[1][0]));
                }
            }
        }

        Type type = Type.GetType(type_name);
        if (type == null) return null!;

        object obj = null;
        if (type == typeof(Lab9.Purple.Task4))
        {
            obj = Activator.CreateInstance(type, input, codes_list.ToArray());
        }
        else
        {
            obj = Activator.CreateInstance(type, input);
        }

        if (obj == null) return null!;

        ((Lab9.Purple.Purple)obj).Review();

        return (T)obj;
    }
}