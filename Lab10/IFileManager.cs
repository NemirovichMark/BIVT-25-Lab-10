using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10Test.Green
{
    internal interface IFileManager
    {
        string FolderPath { get; set; }
        string FileName { get; set; }
        string FileExtension { get; set; }
        string FullPath { get; set; }

        public void SelectFolder(string path);
        public void ChangeFileName(string name);

        public void ChangeFileFormat(string format);

    }
}
