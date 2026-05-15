using System.IO;
using Lab10;
using Lab10.White;

namespace Lab10.White 
{
    public abstract class WhiteFileManager : MyFileManager, IWhiteSerializer
    {
        public WhiteFileManager(string name) : base(name) { }
        public WhiteFileManager(string name, string folder, string fileName, string ext) 
            : base(name, folder, fileName, ext) { }

        public abstract void Serialize(Lab9.White.White obj);
        public abstract Lab9.White.White Deserialize();
    
        public override void EditFile(string content) => File.WriteAllText(FullPath, content);
        public override void ChangeFileExtension(string newExt) 
        {
            string oldPath = FullPath;
            base.ChangeFileFormat(newExt);
            if (File.Exists(oldPath)) File.Move(oldPath, FullPath, true);
        }
    }
}
