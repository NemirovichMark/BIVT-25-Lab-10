namespace Lab10.Purple;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;


public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleJsonFileManager(string name) : base(name) { }

    public PurpleJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        : base(name, folderPath, fileName, fileExtension) { }

    public override void EditFile(string changeFile)
    {
        T obj = Deserialize();
        if (obj == null) return;

        obj.ChangeText(changeFile);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string newExtension)
    {
        base.ChangeFileFormat("json");
    }

    public override void Serialize(T obj)
    {
        if (obj == null || string.IsNullOrEmpty(FullPath)) return;

        JObject json = JObject.FromObject(obj);

        json["Type"] = obj.GetType().AssemblyQualifiedName;

        if (!string.IsNullOrEmpty(FolderPath))
            Directory.CreateDirectory(FolderPath);

        File.WriteAllText(FullPath, json.ToString());
    }



    public override T Deserialize()
    {
        if (string.IsNullOrEmpty(this.FullPath) || !File.Exists(this.FullPath))
            return null!;
        string json_text = File.ReadAllText(this.FullPath);
        JObject json = JObject.Parse(json_text);
        string type_name = json["Type"].ToString();
        Type type = Type.GetType(type_name);
        string input = json["Input"].ToString();
        Console.WriteLine(type);
        Console.WriteLine(json);
        object obj = null;
        if (type == typeof(Lab9.Purple.Task4))
        {
            List<(string, char)> codes_list = new List<(string, char)>();
            JArray codes_array = (JArray)(json["Codes"]);
            if (codes_array != null)
            {
                foreach (JObject item in codes_array)
                {
                    string pair = (item["Item1"]).ToString() ;
                    string code_str = (item["Item2"]).ToString() ;
                    char code = code_str[0];
                    codes_list.Add((pair, code));
                }
            }
            obj = Activator.CreateInstance(type, input, codes_list.ToArray());

        }
        else
        {
            obj = Activator.CreateInstance(type, input);

        }
        JsonConvert.PopulateObject(json.ToString(), obj);
        return (T)obj;
    }

}
