using System.IO;
using Lab9.White;

namespace Lab10.White 
{
    public class WhiteTxtFileManager : WhiteFileManager 
    {
        public WhiteTxtFileManager(string name) : base(name) {}

        public WhiteTxtFileManager(string name, string folder, string file) : base(name, folder, file, ".txt") { }

        public override void Serialize(Lab9.White.White obj) {
            if (obj == null || string.IsNullOrEmpty(FullPath)) return;
            File.WriteAllText(FullPath, obj.ToString()); 
        }

        public override Lab9.White.White Deserialize() 
        {
            if (!File.Exists(FullPath)) return null;
            string content = File.ReadAllText(FullPath);
            return new Lab9.White.Task1(content);
        }
    }
}
