using Lab9.Green;
using System;
using System.IO;
using System.Text.Json;

namespace Lab10.Green
{
    public class GreenJsonFileManager : GreenFileManager
    {
        public GreenJsonFileManager(string name) : base(name) { }

        public GreenJsonFileManager(string name, string folder, string file, string extension = "json")
            : base(name, folder, file, extension) { }

        public override void EditFile(string text)
        {
            var obj = Deserialize<Lab9.Green.Green>();
            obj?.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string ext)
        {
            ext = "json";
            base.ChangeFileFormat(ext);
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null) return;
            Directory.CreateDirectory(FolderPath);
            string json = JsonSerializer.Serialize(obj, obj.GetType(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FullPath, json);
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath)) return null;
            string json = File.ReadAllText(FullPath);
            return (T)JsonSerializer.Deserialize(json, typeof(T));
        }
    }
}
