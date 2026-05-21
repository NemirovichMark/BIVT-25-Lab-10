using Lab10.Interfaces;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        public string Name { get; }
        public string FolderPath { get; private set; } = string.Empty;
        public string FileName { get; private set; } = string.Empty;
        public string FileExtension { get; private set; } = string.Empty;

        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(FolderPath) || string.IsNullOrEmpty(FileName))
                {
                    return string.Empty;
                }
                if (string.IsNullOrEmpty(FileExtension))
                {
                    return Path.Combine(FolderPath, FileName);
                }
                return Path.Combine(FolderPath, $"{FileName}.{FileExtension}");
            }
        }

        public MyFileManager(string name)
        {
            Name = name;
        }

        public MyFileManager(string name, string folder_path, string file_name, string file_extension = "")
        {
            Name = name;
            FolderPath = folder_path;
            FileName = file_name;
            FileExtension = file_extension;
        }

        public void SelectFolder(string folder_path)
        {
            FolderPath = folder_path;
        }

        public void ChangeFileName(string file_name)
        {
            FileName = file_name;
        }

        public void ChangeFileFormat(string file_extension)
        {
            FileExtension = file_extension;
            if (!string.IsNullOrEmpty(this.FullPath))
            {
                this.CreateFile();
            }
        }

        public virtual void CreateFile()
        {
            if (string.IsNullOrEmpty(this.FullPath))
            {
                return;
            }
            if (!string.IsNullOrEmpty(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            if (!File.Exists(this.FullPath))
            {
                File.Create(this.FullPath).Close();
            }
        }

        public virtual void DeleteFile()
        {
            if (!string.IsNullOrEmpty(this.FullPath) && File.Exists(this.FullPath))
            {
                File.Delete(this.FullPath);
            }
        }

        public virtual void EditFile(string text)
        {
            if (!string.IsNullOrEmpty(this.FullPath) && File.Exists(this.FullPath))
            {
                File.WriteAllText(this.FullPath, text ?? string.Empty);
            }
        }

        public virtual void ChangeFileExtension(string new_extension)
        {
            if (string.IsNullOrEmpty(this.FullPath))
            {
                return;
            }
            string old_path = this.FullPath;
            this.ChangeFileFormat(new_extension);
            string new_path = this.FullPath;
            if (File.Exists(old_path) && old_path != new_path)
            {
                File.Move(old_path, new_path);
            }
        }
    }
}