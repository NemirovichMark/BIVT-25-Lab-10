using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
    public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Blue.Blue
    {
        public BlueFileManager(string name): base(name)
        {

        }
        public BlueFileManager(string name, string folderPath, string fileName, string fileExtension = "txt"): base(name,folderPath,fileName,fileExtension)
        {

        }
        public override void EditFile(string newContent)
        {
            if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return;
            base.EditFile(newContent);
        }
        public override void ChangeFileExtension(string newExtention)
        {
            if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return;
            base.ChangeFileExtension(newExtention);
        }
        public abstract void Serialize(T obj);
        public abstract T Deserialize();
    }
}
