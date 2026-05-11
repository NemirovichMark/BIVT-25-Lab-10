using System;
using System.IO;
using System.Linq;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _name;
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

        public string Name => _name;

        public string FolderPath { get => _folderPath; private set => _folderPath = value; }

        public string FileName { get => _fileName; private set => _fileName = value; }

        public string FileExtension { get => _fileExtension; private set => _fileExtension = value; }

        public string FullPath => Path.Combine(_folderPath, _fileName + "." + _fileExtension);

        public MyFileManager(string name)
        {
            _name = name;
            _folderPath = "";
            _fileName = "";
            _fileExtension = "";
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name;
            _folderPath = folderPath;
            _fileName = fileName;
            _fileExtension = fileExtension;
        }

        public virtual void SelectFolder(string folderPath)
        {
            if (folderPath != null)
            {
                _folderPath = folderPath;
            }
        }

        public virtual void ChangeFileName(string fileName)
        {
            if (fileName != null)
            {
                _fileName = fileName;
            }
        }

        public virtual void ChangeFileFormat(string extenxion)
        {
            if (extenxion == null)
                return;

            _fileExtension = extenxion;

            CreateFile();
        }

        public virtual void CreateFile()
        {
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }

        public virtual void DeleteFile()
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }

        public virtual void EditFile(string text)
        {
            File.WriteAllText(FullPath, text);
        }

        public virtual void ChangeFileExtension(string extension)
        {
            if (extension == null)
                return;

            if (!File.Exists(FullPath))
            {
                _fileExtension = extension;
                return;
            }

            string oldPath = FullPath;
            string text = File.ReadAllText(oldPath);
            _fileExtension = extension;
            string newPath = FullPath;
            File.WriteAllText(newPath, text);
            File.Delete(oldPath);
        }
    }
}