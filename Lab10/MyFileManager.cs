using System.IO;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        protected string _name;
        protected string _folderPath;
        protected string _fileName;
        protected string _fileExtension;

        public string Name => _name;
        public string FolderPath => _folderPath;
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;
        public string FullPath => Path.Combine(_folderPath ?? "", $"{_fileName}.{_fileExtension}");

        public MyFileManager(string name)
        {
            _name = name;
        }

        public MyFileManager(string name, string folder, string fileName, string ext = "txt")
        {
            _name = name;
            _folderPath = folder;
            _fileName = fileName;
            _fileExtension = ext;
        }

        public void SelectFolder(string path) => _folderPath = path;
        public void ChangeFileName(string name) => _fileName = name;
        public void ChangeFileFormat(string extension) => _fileExtension = extension;

        public virtual void CreateFile()
        {
            if (!string.IsNullOrEmpty(_folderPath) && !Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);
            File.Create(FullPath).Close();
        }

        public virtual void DeleteFile()
        {
            if (File.Exists(FullPath)) File.Delete(FullPath);
        }

        public abstract void EditFile(string content);
        public abstract void ChangeFileExtension(string extension);
    }
}
