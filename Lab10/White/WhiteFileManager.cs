using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public abstract class WhiteFileManager : MyFileManager, IWhiteSerializer
    {
        protected WhiteFileManager(string name) : base(name) { }

        protected WhiteFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) { }

        public abstract White Deserialize();
        public abstract void Serialize(White obj);

        public override void EditFile(string content)
        {
            base.EditFile(content);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            base.ChangeFileExtension(newExtension);
        }
    }
}
