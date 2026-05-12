namespace Lab10.Purple;

public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleTxtFileManager(string name) : base(name) { }
    public PurpleTxtFileManager(string name, string folder, string fileName, string extension = "")
        : base(name, folder, fileName, extension) { }

    public override void EditFile(string content)
    {
        if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return;
        T obj = Deserialize();
        if (obj == null) return;
        obj.ChangeText(content);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string extension)
    {
        ChangeFileFormat("txt");
    }

    public override void Serialize(T obj)
    {
        if (obj == null || FullPath == null) return;
        var lines = new List<string>
        {
            "Type:" + obj.GetType().Name,
            "Input:" + obj.Input
        };
        
        (string, char)[] codes = null;
        if (obj is Lab9.Purple.Task4 t4) codes = t4.Codes;
        if (codes != null)
        {
            lines.Add("CodesCount:" + codes.Length);
            for (int i = 0; i < codes.Length; i++)
                lines.Add($"Code{i}:{codes[i].Item1}|{codes[i].Item2}");
        }

        File.WriteAllLines(FullPath, lines);
    }

    public override T Deserialize()
    {
            if (FullPath == null || !File.Exists(FullPath)) return null;
 
            string[] lines = File.ReadAllLines(FullPath);
            if (lines == null || lines.Length == 0) return null;
 
            var dict = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                int sep = line.IndexOf(':');
                if (sep < 0) continue;
                dict[line.Substring(0, sep)] = line.Substring(sep + 1);
            }
 
            if (!dict.TryGetValue("Type", out string typeName)) return null;
            string input = dict.TryGetValue("Input", out string inp) ? inp : string.Empty;
 
            T obj = null;
 
            if (typeName == nameof(Lab9.Purple.Task1))
                obj = (T)(object)new Lab9.Purple.Task1(input);
            else if (typeName == nameof(Lab9.Purple.Task2))
                obj = (T)(object)new Lab9.Purple.Task2(input);
            else if (typeName == nameof(Lab9.Purple.Task3))
                obj = (T)(object)new Lab9.Purple.Task3(input);
            else if (typeName == nameof(Lab9.Purple.Task4))
            {
                int codesCount = dict.TryGetValue("CodesCount", out string cc) ? int.Parse(cc) : 0;
                var codes = new (string, char)[codesCount];
                for (int i = 0; i < codesCount; i++)
                {
                    if (!dict.TryGetValue($"Code{i}", out string codeVal)) continue;
                    int pipe = codeVal.LastIndexOf('|');
                    if (pipe < 0) continue;
                    codes[i] = (codeVal.Substring(0, pipe), codeVal[pipe + 1]);
                }
                obj = (T)(object)new Lab9.Purple.Task4(input, codes);
            }
 
            obj?.Review();
            return obj;
    }
}