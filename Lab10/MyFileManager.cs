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
            _fileName = string.Empty;
            _fileExtension = "txt";
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name ?? string.Empty;
            _folderPath = folderPath ?? string.Empty;
            _fileName = fileName ?? string.Empty;
            _fileExtension = string.IsNullOrEmpty(fileExtension) ? "txt" : fileExtension;
        }

        public void SelectFolder(string folderPath)
        {
            if (folderPath == null)
                return;
            _folderPath = folderPath;
        }

        public void ChangeFileName(string fileName)
        {
            if (fileName == null)
                return;
            _fileName = fileName;
        }

        public void ChangeFileFormat(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return;
            _fileExtension = fileExtension;
            CreateFile();
        }

        public void CreateFile()
        {
            if (string.IsNullOrEmpty(_folderPath) || string.IsNullOrEmpty(_fileName) || string.IsNullOrEmpty(_fileExtension)){
                return;
            }
            if (!Directory.Exists(_folderPath)){
                Directory.CreateDirectory(_folderPath);
            }
            var path = FullPath;
            if (!File.Exists(path)){
                File.Create(path).Dispose();
            }
        }

        public void DeleteFile()
        {
            var path = FullPath;
            if (string.IsNullOrEmpty(path))
                return;

            if (File.Exists(path))
                File.Delete(path);
        }

        public virtual void EditFile(string content)
        {
            if (content == null)
                return;

            var path = FullPath;
            if (string.IsNullOrEmpty(path))
                return;

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            File.WriteAllText(path, content);
        }

        public virtual void ChangeFileExtension(string newExtension)
        {
            if (string.IsNullOrEmpty(newExtension))
                return;

            var oldPath = FullPath;
            string content = string.Empty;

            if (!string.IsNullOrEmpty(oldPath) && File.Exists(oldPath))
            {
                content = File.ReadAllText(oldPath);
                File.Delete(oldPath);
            }

            _fileExtension = newExtension;

            var newPath = FullPath;
            if (string.IsNullOrEmpty(newPath))
                return;

            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);

            File.WriteAllText(newPath, content);
        }
    }
}
