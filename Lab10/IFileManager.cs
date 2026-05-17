using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public interface IFileManager
    {
        string FolderPath { get; }
        string FileExtension { get; }
        string FullPath { get; }
        string FileName { get; }
        void ChangeFileName(string name);
        void SelectFolder(string folder);
        void ChangeFileFormat(string format);
    }
}
