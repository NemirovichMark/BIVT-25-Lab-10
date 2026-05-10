using System.IO;
using System.Text.Json;
namespace Lab10.Blue
{
    public class BlueJsonFileManager<T> : BlueFileManager<T> where T : Blue
    {
        public BlueJsonFileManager(string name) : base(name) { }
        public BlueJsonFileManager(string name, string folder, string file, string ext = "json")
            : base(name, folder, file, ext) { }

        public override void Serialize(T obj)
        {
            if (obj == null) return;
            string json = JsonSerializer.Serialize(obj, obj.GetType());
            File.WriteAllText(FullPath, json);
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string json = File.ReadAllText(FullPath);
            return (T)JsonSerializer.Deserialize(json, typeof(T));
        }
    }
}
