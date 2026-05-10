using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Lab10.Purple;

public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleJsonFileManager(string name) : base(name) { }
    public PurpleJsonFileManager(string name, string folder, string fileName, string extension = "")
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
        ChangeFileFormat("json");
    }

    public override void Serialize(T obj)
    {
        if (obj == null) return;
        var jsonObject = JObject.FromObject(obj); 
        jsonObject.Add("Type", obj.GetType().Name); 
        File.WriteAllText(FullPath, jsonObject.ToString());
    }

    public override T Deserialize() 
    {
        if (FullPath == null || !File.Exists(FullPath)) return null;

        string content = File.ReadAllText(FullPath);
        var data = JObject.Parse(content);
        string typeName = data["Type"]?.ToString() ?? string.Empty;
        if (typeName == null) return null;

        string input = data["Input"]?.ToString() ?? string.Empty;;

        T? obj = null;

        if (typeName == nameof(Lab9.Purple.Task1))
            obj = (T)(object)new Lab9.Purple.Task1(input);
        else if (typeName == nameof(Lab9.Purple.Task2))
            obj = (T)(object)new Lab9.Purple.Task2(input);
        else if (typeName == nameof(Lab9.Purple.Task3))
            obj = (T)(object)new Lab9.Purple.Task3(input);
        else if (typeName == nameof(Lab9.Purple.Task4))
        {
            var codesToken = data["Codes"];
            var codes = codesToken != null
                ? codesToken.ToObject<(string, char)[]>()
                : Array.Empty<(string, char)>();
            obj = (T)(object)new Lab9.Purple.Task4(input, codes);
        }

        obj?.Review();
        return obj;
    }
}