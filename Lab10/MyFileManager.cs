namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private readonly string _name;
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

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
            _fileExtension = NormalizeExtension(fileExtension);
        }

        public string Name => _name ?? string.Empty;
        public string FolderPath => _folderPath ?? string.Empty;
        public string FileName => _fileName ?? string.Empty;
        public string FileExtension => _fileExtension ?? string.Empty;

        public string FullPath
        {
            get
            {
                string folder = FolderPath;
                string fileName = FileName;
                string extension = FileExtension;
                string finalName = string.IsNullOrEmpty(extension)
                    ? fileName
                    : $"{fileName}.{extension}";

                return string.IsNullOrEmpty(folder)
                    ? finalName
                    : Path.Combine(folder, finalName);
            }
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
            _fileExtension = NormalizeExtension(fileExtension);
        }

        public virtual void CreateFile()
        {
            string path = FullPath;
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            EnsureDirectory(path);
            using FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        }

        public virtual void DeleteFile()
        {
            string path = FullPath;
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public virtual void EditFile(string content)
        {
            string path = FullPath;
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            EnsureDirectory(path);
            File.WriteAllText(path, content ?? string.Empty);
        }

        public virtual void ChangeFileExtension(string fileExtension)
        {
            string oldPath = FullPath;
            bool oldFileExists = !string.IsNullOrWhiteSpace(oldPath) && File.Exists(oldPath);
            string oldContent = oldFileExists ? File.ReadAllText(oldPath) : string.Empty;

            ChangeFileFormat(fileExtension);

            string newPath = FullPath;
            if (string.IsNullOrWhiteSpace(newPath) || string.Equals(oldPath, newPath, StringComparison.Ordinal))
            {
                return;
            }

            if (!oldFileExists)
            {
                return;
            }

            EnsureDirectory(newPath);
            File.WriteAllText(newPath, oldContent);
            File.Delete(oldPath);
        }

        private static string NormalizeExtension(string? extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                return string.Empty;
            }

            return extension.Trim().TrimStart('.');
        }

        private static void EnsureDirectory(string filePath)
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
