using System.IO;
using System.Text.Json;

namespace Lab10.White
{
    public class WhiteJsonFileManager : WhiteFileManager
    {
        public WhiteJsonFileManager(string name) : base(name) { }

        public WhiteJsonFileManager(string name, string folder, string fileName, string ext = "json")
            : base(name, folder, fileName, ext) { }

        public override void EditFile(string content)
        {
            var obj = Deserialize();
            if (obj != null)
            {
                obj.ChangeText(content);
                Serialize(obj);
            }
        }

        public override void ChangeFileExtension(string extension)
        {
            ChangeFileFormat("json");
        }

        public override void Serialize(Lab9.White.White obj)
        {
            if (obj == null) return;
            var data = new { Type = obj.GetType().Name, Data = obj.ToString() };
            File.WriteAllText(FullPath, JsonSerializer.Serialize(data));
        }

        public override Lab9.White.White Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string json = File.ReadAllText(FullPath);
            var doc = JsonDocument.Parse(json);
            string text = doc.RootElement.GetProperty("Data").GetString();
            return new Lab9.White.White(text);
        }
    }
}
