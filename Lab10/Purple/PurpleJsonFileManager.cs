using System.Text.Json.Nodes;
using Lab9.Purple;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Lab10.Purple;

public class PurpleJsonFileManager<T>: PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleJsonFileManager(string name) : base(name)
    {
    }

    public PurpleJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "") : base(name, folderPath,
        fileName, fileExtension)
    {
    }

    public override void EditFile(string content)
    {
        string fullPatch = FullPath;
        T item = Deserialize();
        item.ChangeText(content);
        Serialize(item);
    }

    public override void ChangeFileExtension(string fileExtension)
    {
        ChangeFileFormat("json");
    }

    public override T Deserialize()
    {
        string fullPath = FullPath;
        
        string content = File.ReadAllText(fullPath);
        
        DTOPurple dto = JsonConvert.DeserializeObject<DTOPurple>(content);
        
        //Type type = Type.GetType(dto.Type);
        
        Type type = Type.GetType($"{dto.Type}, Lab9");//TODO: спросить про сборку
        
        if (type == typeof(Task4))
        {
            (string, char)[] codes = dto.Codes.Select(kvp => (kvp.Key, kvp.Value)).ToArray();
            
            T result = (T)Activator.CreateInstance(type, new object[] { dto.Input, codes }); //создаем и сразу к прив. к Т
            result.Review();//заново считаем результат после создания объекта
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
        string content = System.Text.Json.JsonSerializer.Serialize(dto);
        CreateFile();
        File.WriteAllText(fullPath, content);
    }
}