using Newtonsoft.Json.Linq;
namespace Lab10.Blue;

public class BlueJsonFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
{
    public BlueJsonFileManager(string name) : base(name)
    {
    }
    public BlueJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "json") : base(name, folderPath, fileName, fileExtension)
    {
    }

    public override void EditFile(string text)
    {
        T obj = Deserialize();
        if (obj == null) return;
        obj.ChangeText(text);
        Serialize(obj);
    }
    
    public override void ChangeFileExtension(string newExtension)
    {
        ChangeFileFormat("json");
    }
    
    
    public override void Serialize(T obj)
    {
        if (obj == null) return;
        CreateFile();
        JObject jsonObject = JObject.FromObject(obj);
        jsonObject["Type"] = obj.GetType().Name;
        File.WriteAllText(FullPath, jsonObject.ToString());
    }
    
    public override T Deserialize()
    {
        if (!File.Exists(FullPath)) return null;

        string json = File.ReadAllText(FullPath);
        JObject jsonObject = JObject.Parse(json);

        string type = jsonObject["Type"].ToString();
        string input = jsonObject["Input"].ToString();
        string sequence = jsonObject["Sequence"]?.ToString();

        Lab9.Blue.Blue obj = null;

        if (type == "Task1")
        {
            obj = new Lab9.Blue.Task1(input);
        }
        if (type == "Task2")
        {
            obj = new Lab9.Blue.Task2(input, sequence);
        }
        if (type == "Task3")
        {
            obj = new Lab9.Blue.Task3(input);
        }
        if (type == "Task4")
        {
            obj = new Lab9.Blue.Task4(input);
        }
        obj.Review();
        
        return obj as T;
    }
}