using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface IFileLifeController
    {
        void CreateFile();
        void DeleteFile();
        void EditFile(string fileName);
        void ChangeFileExtension(string extension);
    }
}
