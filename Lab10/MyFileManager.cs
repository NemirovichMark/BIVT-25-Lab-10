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
                {
                    return string.Empty;
                }

                return Path.Combine(_folderPath, _fileName + "." + _fileExtension);
            }
        }

        public MyFileManager(string name)
        {
            _name = name;
            _folderPath = string.Empty;
            _fileName = name;
            _fileExtension = "txt";
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name;
            _folderPath = folderPath;
            _fileName = fileName;
            _fileExtension = fileExtension;
        }

        public void SelectFolder(string folderPath)
        {
            _folderPath = folderPath;
        }

        public void ChangeFileName(string fileName)
        {
            _fileName = fileName;
        }

        public void ChangeFileFormat(string fileExtension)
        {
            if (fileExtension == null)
            {
                return;
            }

            if (fileExtension.StartsWith("."))
            {
                _fileExtension = fileExtension.Substring(1);
            }
            else
            {
                _fileExtension = fileExtension;
            }
        }

        public virtual void CreateFile()
        {
            if (FullPath == null || FullPath == string.Empty)
            {
                return;
            }

            if (_folderPath != null && _folderPath != string.Empty && !Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            using (FileStream stream = File.Create(FullPath))
            {
            }
        }

        public virtual void DeleteFile()
        {
            if (FullPath == null || FullPath == string.Empty)
            {
                return;
            }

            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }

        public virtual void EditFile(string text)
        {
            if (FullPath == null || FullPath == string.Empty)
            {
                return;
            }

            if (_folderPath != null && _folderPath != string.Empty && !Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            File.WriteAllText(FullPath, text);
        }

        public virtual void ChangeFileExtension(string fileExtension)
        {
            if (FullPath == null || FullPath == string.Empty)
            {
                return;
            }

            string oldPath = FullPath;
            string content = string.Empty;

            if (File.Exists(oldPath))
            {
                content = File.ReadAllText(oldPath);
            }

            ChangeFileFormat(fileExtension);

            if (_folderPath != null && _folderPath != string.Empty && !Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            File.WriteAllText(FullPath, content);

            if (File.Exists(oldPath) && oldPath != FullPath)
            {
                File.Delete(oldPath);
            }
        }
    }
}
