using System;
using System.IO;
namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _name;
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

        public string Name => _name;
        public string FolderPath => _folderPath;
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;

        public string FullPath
        {
            get
            {
                if (_folderPath == null || _fileName == null || _fileExtension == null)
                    return string.Empty;
                return Path.Combine(_folderPath, _fileName + "." + _fileExtension);
            }
        }

        public MyFileManager(string name)
        {
            _name = name ?? string.Empty;
            _folderPath = string.Empty;
            _fileName = name ?? string.Empty;
            _fileExtension = "txt";
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name ?? string.Empty;
            _folderPath = folderPath ?? string.Empty;
            _fileName = fileName ?? string.Empty;
            _fileExtension = fileExtension ?? "txt";
        }

        public void SelectFolder(string folderPath)
        {
            _folderPath = folderPath ?? string.Empty;
        }

        public void ChangeFileName(string fileName)
        {
            _fileName = fileName ?? string.Empty;
        }

        public void ChangeFileFormat(string fileExtension)
        {
            _fileExtension = fileExtension?.TrimStart('.') ?? "txt";

            if (!string.IsNullOrEmpty(_folderPath) && !Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);

            if (!string.IsNullOrEmpty(FullPath) && !File.Exists(FullPath))
                using (File.Create(FullPath)) { }
        }

        public virtual void CreateFile()
        {
            if (string.IsNullOrEmpty(FullPath)) return;

            if (!string.IsNullOrEmpty(_folderPath) && !Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);

            if (!File.Exists(FullPath))
                using (File.Create(FullPath)) { }
        }

        public virtual void DeleteFile()
        {
            if (string.IsNullOrEmpty(FullPath)) return;

            if (File.Exists(FullPath))
                File.Delete(FullPath);
        }

        public virtual void EditFile(string text)
        {
            if (string.IsNullOrEmpty(FullPath)) return;

            if (!string.IsNullOrEmpty(_folderPath) && !Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);

            File.WriteAllText(FullPath, text ?? string.Empty);
        }

        public virtual void ChangeFileExtension(string fileExtension)
        {
            if (string.IsNullOrEmpty(FullPath)) return;

            string oldPath = FullPath;
            string content = File.Exists(oldPath) ? File.ReadAllText(oldPath) : string.Empty;

            ChangeFileFormat(fileExtension);

            if (!string.IsNullOrEmpty(_folderPath) && !Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);

            File.WriteAllText(FullPath, content);

            if (File.Exists(oldPath) && oldPath != FullPath)
                File.Delete(oldPath);
        }
    }
}
