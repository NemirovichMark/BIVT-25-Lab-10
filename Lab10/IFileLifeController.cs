using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface IFileLifeController
    {
        public void CreateFile();
        public void DeleteFile();
        public void EditFile(string fileContent);
        public void ChangeFileExtension(string fileExtention);
    }
}
