using System.IO;
using System.Text.Json;
using Lab10;
using Lab10.White;

namespace Lab10.White 
{
    public class WhiteJsonFileManager : WhiteFileManager
    {
        public WhiteJsonFileManager(string name) : base(name) { }

        public WhiteJsonFileManager(string name, string folder, string fileName, string ext = "json")
            : base(name, folder, fileName, ext) { }

        public override void Serialize(Lab9.White.White obj)
        {
            string json = JsonSerializer.Serialize(obj);
            File.WriteAllText(FullPath, json);
        }

        public override Lab9.White.White Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string json = File.ReadAllText(FullPath);
            return JsonSerializer.Deserialize<Lab10.White.White>(json);
        }
    }
}
