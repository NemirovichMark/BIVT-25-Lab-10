using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab10.Blue
{
  public class BlueJsonFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
  {
    public BlueJsonFileManager(string name) : base(name) { }
    public BlueJsonFileManager(string name, string folder, string file, string extension = "txt") : base(name, folder, file, extension) { }

    public override void EditFile(string countent)
    {
      T obj = Deserialize();
      obj.ChangeText(countent);
      Serialize(obj);
    }
    public override void ChangeFileExtension(string format)
    {
      ChangeFileFormat("json");
    }
    public override void Serialize(T obj)
    {
      if (obj == null) return;

      JObject jsonObject = JObject.FromObject(obj);
      jsonObject["Type"] = obj.GetType().AssemblyQualifiedName;
      string jsonObjectString = jsonObject.ToString();

      Directory.CreateDirectory(FolderPath);
      File.WriteAllText(FullPath, jsonObjectString);
    }
    public override T Deserialize()
    {
      if (!File.Exists(FullPath)) return null!;

      string jsonObjectString = File.ReadAllText(FullPath);
      JObject jsonObject = JObject.Parse(jsonObjectString);

      string? json_object_type = jsonObject["Type"]?.ToString();
      if (string.IsNullOrWhiteSpace(json_object_type)) return null!;

      Type? object_type = Type.GetType(json_object_type);
      if (object_type == null) return null!;

      if (!typeof(T).IsAssignableFrom(object_type)) return null!;

      jsonObject.Remove("Type");

      object? obj = jsonObject.ToObject(object_type);
      if (obj == null) return null!;

      return (T)obj;
    }
  }
}
