using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface IFileManager
    {
        string FolderPath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string FullPath { get; }

        void SelectFolder(string folder);
        void ChangeFileName(string file);
        void ChangeFileFormat(string extension);
    }
}