using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T>
         where T : Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base(name) { }

        public PurpleFileManager(string name, string folderName, string fileName, string fileExtension = "txt") : base(name, folderName, fileName, fileExtension) { }

        public override void EditFile(string content) 
        {
            if (File.Exists(FullPath) && !string.IsNullOrEmpty(FullPath))
            {
                base.EditFile(content);
            }
        }
        public override void ChangeFileExtension(string extension)
        {
            if (File.Exists(FullPath) && Path.Exists(FolderPath))
            {
                string content = File.ReadAllText(FullPath);
                string oldPath = FullPath;
                ChangeFileFormat(extension);
                File.WriteAllText(FullPath, content);
                File.Delete(oldPath);
            }
        }

        public abstract T Deserialize();
        public abstract void Serialize(T obj);
    }
}
