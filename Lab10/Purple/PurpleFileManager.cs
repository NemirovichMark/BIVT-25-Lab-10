using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Purple.Purple
    {
        protected PurpleFileManager(string name) : base(name)
        {
        }

        protected PurpleFileManager(string name, string folderName, string fileName, string fileExtension = "txt") : base(name, folderName, fileName, fileExtension)
        {
        }

        public abstract T Deserialize();

        public abstract void Serialize(T obj);

        public override void EditFile(string input)
        {
            base.EditFile(input);
        }
        public override void ChangeFileExtension(string extension)
        {
            base.ChangeFileExtension(extension);
        }
    }
}
