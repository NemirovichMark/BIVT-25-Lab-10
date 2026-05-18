using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab9
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
        public string FullPath => Path.Combine(_folderPath, _fileName + "." + _fileExtension);

        protected MyFileManager(string name)
        {
            _name = name;
            _folderPath = Directory.GetCurrentDirectory();
            _fileName = "default";
            _fileExtension = "txt";
        }

        protected MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name;
            _folderPath = folderPath ?? Directory.GetCurrentDirectory();
            _fileName = fileName ?? "default";
            _fileExtension = fileExtension ?? "txt";
        }

        public void SelectFolder(string folderPath)
        {
            _folderPath = folderPath ?? Directory.GetCurrentDirectory();
        }

        public void ChangeFileName(string fileName)
        {
            _fileName = fileName ?? "default";
        }

        public void ChangeFileFormat(string extension)
        {
            _fileExtension = extension ?? "txt";
        }

        public void CreateFile()
        {
            string directory = Path.GetDirectoryName(FullPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(FullPath))
            {
                File.WriteAllText(FullPath, string.Empty);
            }
        }

        public void DeleteFile()
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }

        public virtual void EditFile(string content)
        {
            if (string.IsNullOrEmpty(FullPath)) return;
            string directory = Path.GetDirectoryName(FullPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(FullPath, content ?? string.Empty);
        }

        public virtual void ChangeFileExtension(string newExtension)
        {
            if (string.IsNullOrEmpty(newExtension)) return;

            string oldFullPath = FullPath;
            string oldExtension = _fileExtension;

            ChangeFileFormat(newExtension);

            string newFullPath = FullPath;

            if (File.Exists(oldFullPath) && oldFullPath != newFullPath)
            {
                File.Move(oldFullPath, newFullPath);
            }
        }
    }
}