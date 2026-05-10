using System.IO;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        public string Name { get; private set; }
        public string FolderPath { get; private set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }

        public string FullPath => string.IsNullOrEmpty(FolderPath) || string.IsNullOrEmpty(FileName)
            ? string.Empty
            : Path.Combine(FolderPath, FileName) + (string.IsNullOrEmpty(FileExtension) ? "" : "." + FileExtension);

        public MyFileManager(string name)
        {
            Name = name;
            FolderPath = string.Empty;
            FileName = string.Empty;
            FileExtension = string.Empty;
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            Name = name;
            FolderPath = folderPath ?? string.Empty;
            FileName = fileName ?? string.Empty;
            FileExtension = fileExtension ?? string.Empty;
        }

        public void SelectFolder(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath)) return;
            if (!Directory.Exists(folderPath)) return;
            FolderPath = folderPath;
        }

        public void SelectFile(string fileName)
        {
            ChangeFileName(fileName);
        }

        public void ChangeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;
            var invalidChars = Path.GetInvalidFileNameChars();
            if (invalidChars.Any(ch => fileName.Contains(ch))) return;
            FileName = fileName;
        }

        public void ChangeFormat(string fileExtension)
        {
            FileExtension = fileExtension ?? string.Empty;
        }

        public void ChangeFileFormat(string fileExtension)
        {
            FileExtension = fileExtension ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(FullPath) && !File.Exists(FullPath))
                CreateFile();
        }

        public void CreateFile()
        {
            if (string.IsNullOrWhiteSpace(FullPath)) return;
            var dir = Path.GetDirectoryName(FullPath);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);
            File.Create(FullPath).Close();
        }

        public void DeleteFile()
        {
            if (string.IsNullOrWhiteSpace(FullPath)) return;
            if (File.Exists(FullPath))
                File.Delete(FullPath);
        }

        public virtual void EditFile(string newText)
        {
            if (string.IsNullOrWhiteSpace(FullPath)) return;
            File.WriteAllText(FullPath, newText);
        }

        public virtual void ChangeFileExtension(string newExtension)
        {
            if (string.IsNullOrWhiteSpace(FullPath)) return;
            if (!File.Exists(FullPath)) return;

            var oldPath = FullPath;
            var newExt = (newExtension ?? string.Empty).Trim();

            FileExtension = newExt;

            var newPath = FullPath;

            if (string.Equals(oldPath, newPath, StringComparison.OrdinalIgnoreCase)) return;

            var dir = Path.GetDirectoryName(newPath);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            File.Copy(oldPath, newPath, overwrite: true);
            File.Delete(oldPath);
        }
    }
}
