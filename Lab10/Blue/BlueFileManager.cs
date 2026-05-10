using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
    public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Blue.Blue
    {
        protected BlueFileManager(string name) : base(name) { }
        protected BlueFileManager(string name, string folderPath, string fileName, string extension = "txt")
            : base(name, folderPath, fileName, extension) { }

        public override void EditFile(string content) => base.EditFile(content);
        public override void ChangeFileExtension(string newExtension) => base.ChangeFileExtension(newExtension);

        public abstract void Serialize(T obj);
        public abstract T Deserialize();
    }
}
