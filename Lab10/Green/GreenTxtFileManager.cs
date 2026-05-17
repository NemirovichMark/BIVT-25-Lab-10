using Lab9.Green;
using System;
using System.IO;

namespace Lab10.Green
{
    public class GreenTxtFileManager : GreenFileManager
    {
        public GreenTxtFileManager(string name) : base(name) { }

        public GreenTxtFileManager(string name, string folder, string file, string extension = "txt")
            : base(name, folder, file, extension) { }

        public override void EditFile(string text)
        {
            var obj = Deserialize<Lab9.Green.Green>();
            obj?.ChangeText(text);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string ext)
        {
            ext = "txt";
            base.ChangeFileFormat(ext);
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null) return;
            Directory.CreateDirectory(FolderPath);
            File.WriteAllLines(FullPath, new string[]
            {
                $"Type:{obj.GetType().FullName}",
                $"Input:{obj.Input}"
            });
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath)) return null;
            string[] lines = File.ReadAllLines(FullPath);
            string typeName = lines[0].Split(':')[1];
            string input = lines[1].Split(':')[1];
            Type type = Type.GetType(typeName);
            return (T)Activator.CreateInstance(type, input);
        }
    }
}
