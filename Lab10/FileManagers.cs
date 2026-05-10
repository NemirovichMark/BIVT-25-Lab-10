using System;
using System.IO;

namespace Lab10
{
    public interface ISerializer<T> where T : Lab9.Purple.Purple
    {
        void Serialize(T obj);
        T Deserialize();
    }

    public interface IFileManager
    {
        string FolderPath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string FullPath { get; }

        void SelectFolder(string text);
        void ChangeFileName(string text);
        void ChangeFileFormat(string text);
    }

    public interface IFileLifeController
    {
        void CreateFile();
        void DeleteFile();
        void EditFile(string text);
        void ChangeFileExtension(string text);
    }

    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;
        private string _name;

        public string FolderPath => _folderPath;
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;
        public string FullPath => Path.Combine(_folderPath, _fileName + "." + _fileExtension);
        public string Name => _name;

        public void SelectFolder(string text)
        {
            if (text == null || text == "")
            {
                _folderPath = Directory.GetCurrentDirectory();
            }
            else
            {
                _folderPath = text;
            }
        }

        public void ChangeFileName(string text)
        {
            _fileName = text ?? "";
        }

        public void ChangeFileFormat(string text)
        {
            if (text == null)
            {
                _fileExtension = "";
            }
            else
            {
                if (text.StartsWith("."))
                {
                    _fileExtension = text.Substring(1);
                }
                else
                {
                    _fileExtension = text;
                }
            }

            Directory.CreateDirectory(_folderPath);

            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }

        private void NormlizeExtension()
        {
            if (string.IsNullOrEmpty(_fileExtension))
            {
                _fileExtension = "txt";
            }

            if (_fileExtension.StartsWith("."))
            {
                _fileExtension = _fileExtension.Substring(1);
            }

            if (_fileExtension == "")
            {
                _fileExtension = "txt";
            }
        }

        public void CreateFile()
        {
            Directory.CreateDirectory(_folderPath);

            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }

        public void DeleteFile()
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }

        public virtual void EditFile(string text)
        {
            if (text == null)
            {
                Directory.CreateDirectory(_folderPath);
                File.WriteAllText(FullPath, "");
            }
            else
            {
                Directory.CreateDirectory(_folderPath);
                File.WriteAllText(FullPath, text);
            }
        }

        public virtual void ChangeFileExtension(string text)
        {
            var oldPath = FullPath;
            var content = File.Exists(oldPath) ? File.ReadAllText(oldPath) : "";

            ChangeFileFormat(text);

            Directory.CreateDirectory(_folderPath);
            File.WriteAllText(FullPath, content);

            if (File.Exists(oldPath) && oldPath != FullPath)
            {
                File.Delete(oldPath);
            }
        }

        public MyFileManager(string name)
        {
            _name = name ?? "";
            _folderPath = Directory.GetCurrentDirectory();
            _fileName = _name;
            _fileExtension = "txt";
        }

        public MyFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
        {
            _name = name ?? "";
            _folderPath = folderName ?? Directory.GetCurrentDirectory();
            _fileName = fileName ?? _name;
            _fileExtension = fileExtension ?? "txt";

            NormlizeExtension();
        }
    }
}