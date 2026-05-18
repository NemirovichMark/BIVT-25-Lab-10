using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;
        public string Name { get; private set; }
        public string FolderPath => _folderPath;
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;
        public string FullPath => Path.Combine(_folderPath, $"{_fileName}.{_fileExtension}");
        
        public MyFileManager(string name)
        {
            Name = name;
        }
        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            Name = name;
            _folderPath = folderPath;
            _fileName = fileName;
            _fileExtension = fileExtension;
        }

        public void SelectFolder(string path) => _folderPath = path;
        public void ChangeFileName(string name) => _fileName = name;
        public void ChangeFileFormat(string format)
        {
            _fileExtension = format;
            CreateFile();
        }

        public virtual void CreateFile()
        {
            if (!Directory.Exists(_folderPath)) Directory.CreateDirectory(_folderPath);
            if (!File.Exists(FullPath)) File.Create(FullPath).Close();
        }
        public virtual void DeleteFile()
        {
            if (File.Exists(FullPath)) File.Delete(FullPath);
        }
        public virtual void EditFile(string text)
        {
            if (string.IsNullOrEmpty(FullPath) || string.IsNullOrEmpty(text)) return;

            CreateFile();
            File.WriteAllText(FullPath, text);
        }
        public virtual void ChangeFileExtension(string extension)
        {
            string text = File.ReadAllText(FullPath);
            DeleteFile();

            _fileExtension = extension;
            CreateFile();
            EditFile(text);
        }
    }
}
