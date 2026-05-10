using System;
using System.IO;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;
        private string _name;
        public MyFileManager(string name)
        {
            _name = name;
        }

        public MyFileManager(string name, string folder, string file, string extension = "")
        {
            _name = name;
            _folderPath = folder;
            _fileName = file;
            _fileExtension = extension;
        }
        
        public string FolderPath => _folderPath;
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;
        
        public string FullPath => Path.Combine(_folderPath ?? "", (_fileName ?? "") + (_fileExtension ?? ""));
        
        public void SelectFolder(string path) => _folderPath = path;
        public void ChangeFileName(string name) => _fileName = name;
        public void ChangeFileFormat(string extension) => _fileExtension = extension;
        
        public void CreateFile()
        {
            if (string.IsNullOrEmpty(FullPath)) return;
            if (!string.IsNullOrEmpty(_folderPath)) Directory.CreateDirectory(_folderPath);
            using (File.Create(FullPath)) {}
        }

        public void DeleteFile()
        {
            if (File.Exists(FullPath)) File.Delete(FullPath);
        }
        public abstract void EditFile(string content);
        public abstract void ChangeFileExtension(string newExtension);
    }
}
