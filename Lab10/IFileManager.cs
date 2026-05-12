using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface IFileManager
    {
        public string FolderPath { get; }
        public string FileName { get; }
        public string FileExtension { get; }
        public string FullPath { get; }

        public void SelectFolder(string filed);
        public void ChangeFileName(string filed);
        public void ChangeFileFormat(string filed);

    }
}
