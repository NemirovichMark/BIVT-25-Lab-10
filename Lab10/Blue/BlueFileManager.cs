using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
    public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Blue.Blue
    {
        public BlueFileManager(string name) : base(name) { }
        public BlueFileManager(string name, string fileName, string folderPath, string fileExtension = "") : base(name, fileName, folderPath, fileExtension) { }

        public override void EditFile(string text)
        {
            if (File.Exists(FullPath)) base.EditFile(text);
        }
        public override void ChangeFileExtension(string text)
        {
            if(File.Exists(FullPath)) base.ChangeFileExtension(text);
        }

        public abstract void Serialize(T elem);
        public abstract T Deserialize();
    }
}
