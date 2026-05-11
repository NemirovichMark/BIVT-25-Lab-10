using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab10
{
    public interface IFileManager
    {
        string FolderPath { get; } string FileName { get; }
        string FileExtension { get; } string FullPath { get; }
        void SelectFolder(string a); void ChangeFileName(string a);
        void ChangeFileFormat(string a);

    }
    public interface IFileLifeController
    {
        void CreateFile(); void DeleteFile();
        void EditFile(string a); void ChangeFileExtension(string a);
    }
}
