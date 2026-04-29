using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Lab10.Purple;

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

    public override void Serialize(T obj)
    {
        Directory.CreateDirectory(FolderPath);
        JObject json_obj = JObject.FromObject(obj);
        json_obj.Add("Type",obj.GetType().FullName);

        string json_obj_string = json_obj.ToString();
        File.WriteAllText(FullPath, json_obj_string);
    }
    public override T Deserialize()
    {
        if (File.Exists(FullPath))
        {
            string json_string = File.ReadAllText(FullPath);
            JObject json_object = JObject.Parse(json_string);
            Type json_object_type = Type.GetType(json_object["Type"]!.ToString())!;
            // json_object_type может быть null и это надо проверять
            if (typeof(T).IsAssignableFrom(json_object_type))
            {
                json_object.Remove("Type");
                object obj = json_object.ToObject(json_object_type)!;
                return (T)obj!;
            }
            else return null!;
        }
        else return null!;
    }
}