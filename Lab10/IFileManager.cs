using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void SelectFolder(string input);
        public void ChangeFileName(string input);
        public void ChangeFileFormat(string input);
    }
}
