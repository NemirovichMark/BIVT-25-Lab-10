using System.IO;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        //parameters
        private string _Name;
        private string _FolderName = "";
        private string _FileName = "";
        private string _FileExtension = "txt";
        //constructors
        //constructor
        public MyFileManager(string name)
        {
            _Name = name;
        }
        public MyFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
        {
            _Name = name;
            _FolderName = folderName;
            _FileName = fileName;
            _FileExtension = fileExtension;
        }
        //methods
        public string Name { get { return _Name; } }
        //properties
        public string FolderPath { get { return _FolderName; } }
        public string FileName { get { return _FileName; } }
        public string FileExtension { get { return _FileExtension; } }
        public string FullPath { get { return $"{_FolderName}\\{_FileName}.{_FileExtension}"; } }
        //methods
        public void SelectFolder(string Folder)
        {
            _FolderName = Folder;
        }
        public void ChangeFileName(string File)
        {
            _FileName = File;
        }
        public void ChangeFileFormat(string format)
        {
            _FileExtension = format;
            if (string.IsNullOrEmpty(_FolderName))
                _FolderName = Directory.GetCurrentDirectory();

            Directory.CreateDirectory(_FolderName);
            using (var fs = File.Create(FullPath)) { }
        }

        public void CreateFile()
        {
            if (string.IsNullOrEmpty(_FolderName))
                _FolderName = Directory.GetCurrentDirectory();

            Directory.CreateDirectory(_FolderName);
            using (var fs = File.Create(FullPath)) { }
        }

        public void DeleteFile()
        {
            if (File.Exists(FullPath))
                File.Delete(FullPath);
        }

        public virtual void EditFile(string content)
        {
            if (!string.IsNullOrEmpty(_FolderName))
                Directory.CreateDirectory(_FolderName);

            File.WriteAllText(FullPath, content);
        }

        public virtual void ChangeFileExtension(string newExtension)
        {
            var oldPath = FullPath;
            var newPath = Path.ChangeExtension(oldPath, newExtension);

            if (File.Exists(oldPath))
            {
                var bytes = File.ReadAllBytes(oldPath);
                Directory.CreateDirectory(Path.GetDirectoryName(newPath) ?? string.Empty);
                File.WriteAllBytes(newPath, bytes);
                File.Delete(oldPath);
            }

            _FileExtension = newExtension;
        }

    }
}