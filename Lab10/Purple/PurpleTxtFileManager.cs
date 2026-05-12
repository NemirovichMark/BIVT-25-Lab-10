using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using Lab9.Purple;

namespace Lab10.Purple;

public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleTxtFileManager(string name) : base(name)
    {
    }
    
    public PurpleTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "") : base(name, folderPath,
        fileName, fileExtension)
    {
    }

    public override void EditFile(string content)
    {
        T obj = Deserialize();
        obj.ChangeText(content);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string fileExtension)
    {
        ChangeFileFormat("txt");
    }

    public override T Deserialize()
    {
        string fullPath = FullPath;
        string[] lines = File.ReadAllLines(fullPath);

        string typeString = string.Empty;
        string input = string.Empty;
        string output = string.Empty;
        Dictionary<string, char> codes = new Dictionary<string, char>();
        
        foreach (string line in lines)
        {
            string[] parts = line.Split(":^^");
            
            if (parts[0] == nameof(DTOPurple.Type))
                typeString = parts[1];
            else if (parts[0] == nameof(DTOPurple.Input))
                input = parts[1];
            else if (parts[0] == nameof(DTOPurple.Output))
                output = parts[1];
            
            else if (parts[0] == nameof(DTOPurple.Codes) && !string.IsNullOrEmpty(parts[1]))
            {
                string[] pairs = parts[1].Split("~~");
                foreach (string pair in pairs)
                {
                    string[] dictionaryPairs = pair.Split("&&");
                    codes.Add(dictionaryPairs[0], dictionaryPairs[1][0]);
                }
            }
        }
        
        DTOPurple dto = new DTOPurple()
        {
            Type = typeString,
            Input = input,
            Output = output,
            Codes = codes.Select(x => new CodeItem(x.Key, x.Value)).ToList() //dict в лист
        };
        
        Type type = Type.GetType($"{dto.Type}, Lab9");
        
        if (type == typeof(Task4))
        {
            (string, char)[] codesChar = dto.Codes.Select(kvp => (kvp.Key, kvp.Value)).ToArray();
            
            T result =  (T)Activator.CreateInstance(type, new object[] { dto.Input, codesChar });
            result.Review();
            return result;
        }
        T result2 = (T)Activator.CreateInstance(type, new object[] {dto.Input}); 
        result2.Review();
        return result2;

    }

    public override void Serialize(T item)
    {
        string fullPath = FullPath;

        DTOPurple dto = new DTOPurple(item);

        List<string> lines = new List<string>()
        {
            $"{nameof(dto.Type)}:^^{dto.Type}",
            $"{nameof(dto.Input)}:^^{dto.Input}",
            $"{nameof(dto.Output)}:^^{dto.Output}",
            $"{nameof(dto.Codes)}:^^{string.Join("~~", dto.Codes.Select(kvp => $"{kvp.Key}&&{kvp.Value}"))}"
        };
        
        CreateFile();
        File.WriteAllLines(fullPath, lines);
    }
}