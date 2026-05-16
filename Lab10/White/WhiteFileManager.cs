using System.IO;

namespace Lab10.White
{
    public abstract class WhiteFileManager : MyFileManager, IWhiteSerializer
    {
        public WhiteFileManager(string name) : base(name) { }

        public WhiteFileManager(string name, string folder, string fileName, string ext = "txt")
            : base(name, folder, fileName, ext) { }

        public abstract void Serialize(Lab9.White.White obj);
        public abstract Lab9.White.White Deserialize();

        public override void EditFile(string content)
        {
            if (!string.IsNullOrEmpty(FullPath)) File.WriteAllText(FullPath, content);
        }

        public override void ChangeFileExtension(string extension)
        {
            string oldPath = FullPath;
            ChangeFileFormat(extension);
            if (File.Exists(oldPath))
            {
                File.Move(oldPath, FullPath, true);
            }
        }
    }
}
