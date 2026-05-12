namespace Lab10.Green;
using System;
using System.Text.Json;

public class GreenJsonFileManager : GreenFileManager
{
    public GreenJsonFileManager(string name, string folderPath, string fileName, string fileExtension) : base(name,
        folderPath, fileName, fileExtension)
    {
    }

    public GreenJsonFileManager(string name) : base(name)
    {
    }

    public override void EditFile(string newFileText)
    {
        Lab9.Green.Green h = Deserialize<Lab9.Green.Green>();
        h.ChangeText(newFileText);
        Serialize(h);
    }

    public override void ChangeFileExtension(string newFileExtension)
    {
        ChangeFileFormat("json");
    }

    public override void Serialize<T>(T obj)
    {
        ChangeFileFormat("json");

        string json = JsonSerializer.Serialize(obj, obj.GetType());

        json = json.Insert(1, $"\n  \"Type\": \"{obj.GetType().FullName}\",");

        File.WriteAllText(FullPath, json);
    }

    public override T Deserialize<T>()
    {
        if (!File.Exists(FullPath)) return null;

        string json = File.ReadAllText(FullPath);

        using (JsonDocument doc = JsonDocument.Parse(json))
        {
            var root = doc.RootElement;

            string type = null;
            
            if (root.TryGetProperty("Type", out var _type))
            {
                type = _type.GetString();
            }
            
            Type actualType = typeof(T);

            Type foundType = null;

            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < allAssemblies.Length; i++)
            {
                var assembly = allAssemblies[i];

                var t = assembly.GetType(type);

                if (t != null)
                {
                    foundType = t;
                    break;
                }
            }
            actualType = foundType;
            
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
            foreach (var n in root.EnumerateObject())
            {
                dict[n.Name] = n.Value.ToString();
            }

            var constructor = actualType.GetConstructors()[0];
            var param = constructor.GetParameters();
            var args = new object[param.Length];

            for (int i = 0; i < param.Length; i++)
            {
                string paran = param[i].Name.ToLower();

                if (dict.TryGetValue(paran, out string val))
                {
                    args[i] = val;
                }
            }

            T obj = (T)constructor.Invoke(args);

            var reviewMethod = obj.GetType().GetMethod("Review");
            reviewMethod.Invoke(obj, null);

            return obj;
        }
    }
}
