using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Green
{
    public abstract class GreenFileManager : MyFileManager, ISerializer
    {
        protected GreenFileManager(string name) : base(name)
        {
        }

        protected GreenFileManager(string name, string folderpath, string filename, string fileextension = "txt") : base(name, folderpath, filename, fileextension)
        {
        }

        public override void EditFile(string text)
        {
            if (text == null || text == "" || !File.Exists(FullPath)) return;
            base.EditFile(text);
        }

        public override void ChangeFileExtension(string extension)
        {
            if (extension == null || extension == "" || !File.Exists(FullPath)) return;
            base.ChangeFileExtension(extension);
        }

        public abstract T Deserialize<T>() where T : Lab9.Green.Green;
        public abstract void Serialize<T>(T obj) where T : Lab9.Green.Green;
    }
}
