using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10Test.Green
{
    internal interface IFileLifeController
    {
        public void CreateFile();
        public void DeleteFile();

        public void EditFile(string text);
        public void ChangeFileExtension(string extension);

    }
}
