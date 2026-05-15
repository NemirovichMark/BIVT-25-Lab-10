using System.IO;

namespace Lab10.White
{
    public class WhiteTxtFileManager : WhiteFileManager
    {
        public WhiteTxtFileManager(string name) : base(name) { }

        public WhiteTxtFileManager(string name, string folder, string fileName, string ext = "txt")
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
            ChangeFileFormat("txt");
        }

        public override void Serialize(Lab9.White.White obj)
        {
            if (obj == null) return;
            File.WriteAllText(FullPath, obj.ToString());
        }

        public override Lab9.White.White Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            string content = File.ReadAllText(FullPath);
            return new Lab9.White.White(content);
        }
    }
}
