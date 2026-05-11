using System.IO;
using System.Text.Json;
using Lab9.White;

namespace Lab10.White 
{
    public class WhiteJsonFileManager : WhiteFileManager 
    {
        public WhiteJsonFileManager(string name) : base(name) {}
        public WhiteJsonFileManager(string name, string folder, string file) : base(name, folder, file, ".json") { }

        public override void Serialize(Lab9.White.White obj) 
        {
            if (obj == null || string.IsNullOrEmpty(FullPath)) return;
            string json = JsonSerializer.Serialize(obj);
            File.WriteAllText(FullPath, json);
        }

        public override Lab9.White.White Deserialize() 
        {
            if (!File.Exists(FullPath)) return null;
            string json = File.ReadAllText(FullPath);
            // return JsonSerializer.Deserialize<Lab9.White.White>(json);
            return System.Text.Json.JsonSerializer.Deserialize<Lab9.White.Task1>(json);
        }
    }
}
