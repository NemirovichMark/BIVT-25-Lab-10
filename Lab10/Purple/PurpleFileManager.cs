using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T: Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base (name) { }
        public PurpleFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base (name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string text)
        {
            if (!File.Exists(FullPath)) return;
            base.EditFile(text);
        }
        public override void ChangeFileExtension(string extension)
        {
            if (!File.Exists(FullPath)) return;
            base.ChangeFileExtension(extension);
        }
        public abstract void Serialize(T obj);
        public abstract T Deserialize();
    }
}
