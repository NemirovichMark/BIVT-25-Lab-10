using Newtonsoft.Json;

namespace Lab10.Blue;

public class BlueJsonFileManager<T> : BlueFileManager<T> where T:Lab9.Blue.Blue
{
  public BlueJsonFileManager(string name) : base(name){}
  public BlueJsonFileManager(string name, string folderpath, string filename, string ext = "") : base(name, folderpath, filename, ext){}

  public override void EditFile(string data)
  {
    if (data == null) return;
    T obj = Deserialize();
    obj.ChangeText(data);
    Serialize(obj);
  }

  public override void ChangeFileExtension(string extension = "json")
  {
    base.ChangeFileExtension("json"); // MyFileManager.ChangeFileExtension already uses ChangeFileFormat;
  }

  public override void Serialize(T obj)
  {
    if (obj == null) return;

    string json;
    using (var stringWriter = new StringWriter())
    {
      using (var jsonWriter = new JsonTextWriter(stringWriter))
      {
	JsonSerializer serializer = new JsonSerializer();
	serializer.Formatting = Formatting.Indented; //for readable form
	serializer.TypeNameHandling = TypeNameHandling.All; //for type field
	serializer.Serialize(jsonWriter, obj);
	json = stringWriter.ToString();
      }
    }

    EditFile(json);
  }

  public override T Deserialize()
  {
    if (!File.Exists(FullPath)) return null;

    string jsonContent = File.ReadAllText(FullPath);

    if (String.IsNullOrEmpty(jsonContent)) return null;

    var settings = new JsonSerializerSettings
    {
      TypeNameHandling = TypeNameHandling.All //now deserealizer will look onto the type field before deserealization
    };

    T result = JsonConvert.DeserializeObject<T>(jsonContent, settings);

    return result;}

}
