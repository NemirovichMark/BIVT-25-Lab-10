using System.IO;
using Lab10;
using Lab10.White;

namespace Lab10.White 
{
    public class WhiteTxtFileManager : WhiteFileManager
    {
        public WhiteTxtFileManager(string name) : base(name) { }

        public WhiteTxtFileManager(string name, string folder, string fileName, string ext = "txt")
            : base(name, folder, fileName, ext) { }

        public override void Serialize(Lab9.White.White obj)
        {
            File.WriteAllText(FullPath, obj?.ToString());
        }

        public override Lab9.White.White Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string content = File.ReadAllText(FullPath);
            return new Lab10.White.White(content);
        }
    }
}
