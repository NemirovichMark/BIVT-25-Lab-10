namespace Lab10.Purple;
using Newtonsoft.Json.Linq;

public class PurpleJsonFileManager<T> : PurpleFileManager<T>
    where T : Lab9.Purple.Purple
{
    public PurpleJsonFileManager(string name) : base (name)
    {
    }
    public PurpleJsonFileManager(
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
        base.ChangeFileFormat("json");

    public override void Serialize(T obj)
    {
        if (obj == null) return;

        JObject json_object = JObject.FromObject(obj);
        json_object.Add("Type", obj.GetType().AssemblyQualifiedName);
        string json_object_string = json_object.ToString();

        Directory.CreateDirectory(FolderPath);
        File.WriteAllText(FullPath,json_object_string);
    }
    public override T Deserialize()
    {
        if (!File.Exists(FullPath)) return null!;

        string json_object_string = File.ReadAllText(FullPath);
        JObject json_object = JObject.Parse(json_object_string);

        string? json_object_type = json_object["Type"]?.ToString();
        if (string.IsNullOrWhiteSpace(json_object_type)) return null!;

        Type? object_type = Type.GetType(json_object_type);
        if (object_type == null) return null!;

        if (!typeof(T).IsAssignableFrom(object_type)) return null!;

        json_object.Remove("Type");

        object? obj = json_object.ToObject(object_type);
        if (obj == null) return null!;

        return (T)obj;
    }
}
