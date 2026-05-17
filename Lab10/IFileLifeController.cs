using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public interface IFileLifeController
    {
        void CreateFile();
        void EditFile(string filename);
        void DeleteFile();
        void ChangeFileExtension(string ext);
    }
}
