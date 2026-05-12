using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base(name)
        {
        }

        public PurpleFileManager(string name, string folder, string fileName, string ext = "txt") : base(name, folder, fileName, ext)
        {

        }

        public override void EditFile(string text)
        {
                base.EditFile(text);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            base.ChangeFileExtension(fileExtension);
        }
        public abstract void Serialize(T obj);

        public abstract T Deserialize();


    }
}
