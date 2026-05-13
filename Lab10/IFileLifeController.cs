using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface IFileLifeController
    {
        // CreateFile, DeleteFile
        void CreateFile();
        void DeleteFile();
        //  EditFile и ChangeFileExtension
        void EditFile(string file);
        void ChangeFileExtension(string file);
    }
}
