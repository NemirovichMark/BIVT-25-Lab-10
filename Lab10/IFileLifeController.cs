using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface IFileLifeController
    {
        public abstract void CreateFile();
        public abstract void DeleteFile();
        public abstract void EditFile(string file);
        public abstract void ChangeFileExtension(string extention);
    }
}
