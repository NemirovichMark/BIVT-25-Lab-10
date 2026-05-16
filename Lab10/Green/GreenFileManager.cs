using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Green
{
    public abstract class GreenFileManager : MyFileManager, ISerializer
    {
        public GreenFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") : base(name, folderPath, fileName, fileExtension)
        {
        }
        public GreenFileManager(string name) : base(name)
        {
        }
        public override void EditFile(string newFileText)
        {
            File.Exists(FullPath);
            base.EditFile(newFileText);
        }

        public override void ChangeFileExtension(string newFileExtension)
        {
            File.Exists(FullPath);
            base.ChangeFileExtension(newFileExtension);
        }

        abstract public void Serialize<T>(T obj) where T : Lab9.Green.Green;
        abstract public T Deserialize<T>() where T : Lab9.Green.Green;
    }
}
