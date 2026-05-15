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
            string json = JsonSerializer.Serialize(obj);
            File.WriteAllText(FullPath, json);
        }

        public override Lab9.White.White Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string json = File.ReadAllText(FullPath);
            // Используем конкретный тип Lab10.White.White для десериализации
            return JsonSerializer.Deserialize<Lab10.White.White>(json);
        }
    }
}
