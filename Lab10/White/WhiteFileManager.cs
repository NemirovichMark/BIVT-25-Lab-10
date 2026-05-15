using System.IO;
using Lab10;
using Lab10.White;

namespace Lab10.White 
{
    public abstract class WhiteFileManager : MyFileManager, IWhiteSerializer 
    {
        public WhiteFileManager(string name) : base(name) {}
        public WhiteFileManager(string name, string folder, string file, string ext = "") : base(name, folder, file, ext) { }
        
        public abstract Lab10.White.White Deserialize();
        public abstract void Serialize(Lab10.White.White obj);

        public override void EditFile(string content) 
        {
            var obj = Deserialize();
            if (obj != null) 
            {
                obj.ChangeText(content); 
                Serialize(obj);
            }
        }

        public override void ChangeFileExtension(string newExt) 
        {
            if (File.Exists(FullPath)) 
            {
                string newPath = Path.ChangeExtension(FullPath, newExt);
                File.Move(FullPath, newPath);
                ChangeFileFormat(newExt);
            }
        }
    }
}
