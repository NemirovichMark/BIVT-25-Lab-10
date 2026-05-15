using System.IO;
using Lab10;
using Lab10.White;

namespace Lab10.White 
{
    public class WhiteTxtFileManager : WhiteFileManager 
    {
        public WhiteTxtFileManager(string name) : base(name) {}

        public WhiteTxtFileManager(string name, string folder, string file) : base(name, folder, file, ".txt") { }

        public override void Serialize(Lab10.White.White obj) {
            if (obj == null || string.IsNullOrEmpty(FullPath)) return;
            File.WriteAllText(FullPath, obj.ToString()); 
        }

        public override Lab10.White.White Deserialize() 
        {
            if (!File.Exists(Path.Combine(_folderPath, _fileName + ".txt")) return null;
            string content = File.ReadAllText(Path.Combine(_folderPath, _fileName + ".txt"));
            return new Lab10.White.White(content);
        }
    }
}
